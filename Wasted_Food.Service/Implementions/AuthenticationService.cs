using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Wasted_Food.Data.Entities.Identity;
using Wasted_Food.Data.Helper;
using Wasted_Food.Infrastructure.Data;
using Wasted_Food.Infrastructure.Repository.Abstracts;
using Wasted_Food.Service.Abstracts;

namespace Wasted_Food.Service.Implementions
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly jwtSettings _jwtSettings;
        private readonly IRefreshTokenReposatory _refreshTokenReposatory;
        private readonly UserManager<Users> _userManager;
        private readonly IEmailService _emailService;
        private readonly AppDbContext _dbContext;
        private readonly IEncryptionProvider _encryptionProvider;

        #endregion
        #region Constructor
        public AuthenticationService(jwtSettings jwtSettings, IRefreshTokenReposatory refreshTokenReposatory, UserManager<Users> userManager, IEmailService emailService, AppDbContext dbContext)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenReposatory = refreshTokenReposatory;
            _userManager = userManager;
            _emailService = emailService;
            _dbContext = dbContext;
            _encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");
        }
        #endregion
        #region Handler Function
        public async Task<JwtAuthResult> GetJWTToken(Users user)
        {
            var (jwtToken, accessToken) = await GenerateJwtToken(user);
            var refreshToke = GetRefreshToke(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = false,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToke.Tokenstring,
                Token = accessToken,
                UserId = user.Id,
            };
            await _refreshTokenReposatory.AddAsync(userRefreshToken);
            var response = new JwtAuthResult();
            response.accessToken = accessToken;
            response.refreshToken = refreshToke;
            return response;
        }
        private async Task<(JwtSecurityToken, string)> GenerateJwtToken(Users user)
        {
            var claims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }
        private RefreshToken GetRefreshToke(string username)
        {
            var refreshToken = new RefreshToken
            {
                UserName = username,
                Tokenstring = GenerateRefreshToke(),
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate)
            };

            return refreshToken;
        }
        private string GenerateRefreshToke()
        {
            var randomNumber = new byte[32];
            var random = RandomNumberGenerator.Create();
            random.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public async Task<List<Claim>> GetClaims(Users user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.FullName), user.UserName),
               new Claim(nameof(UserClaimModel.Email), user.Email),
               new Claim(nameof(UserClaimModel.Id), user.Id.ToString()),

            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;

        }

        public async Task<JwtAuthResult> GetRefreshToken(Users user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
        {
            var (jwtSecurityToken, newToken) = await GenerateJwtToken(user);
            var response = new JwtAuthResult();
            response.accessToken = newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.FullName)).Value;
            refreshTokenResult.Tokenstring = refreshToken;
            refreshTokenResult.ExpireAt = (DateTime)expiryDate;
            response.refreshToken = refreshTokenResult;
            return response;

        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                {
                    return "InvalidToken";
                }

                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("AlgorithmIsWrong", null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);
            }

            //Get User

            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;
            var userRefreshToken = await _refreshTokenReposatory.GetTableNoTracking()
                                             .FirstOrDefaultAsync(x => x.Token == accessToken &&
                                                                     x.RefreshToken == refreshToken &&
                                                                    x.UserId == int.Parse(userId));
            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }

            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenReposatory.UpdateAsync(userRefreshToken);
                return ("RefreshTokenIsExpired", null);
            }
            var expirydate = userRefreshToken.ExpiryDate;
            return (userId, expirydate);
        }

        public JwtSecurityToken ReadJwtToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        public async Task<string> ConfirmEmail(int? userId, string? code)
        {
            if (userId == null || code == null)
                return "ErrorWhenConfirmEmail";
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
            if (!confirmEmail.Succeeded)
                return "ErrorWhenConfirmEmail";
            return "Success";
        }

        public async Task<string> SendResetPasswordCode(string Email)
        {
            var trans = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                //user
                var user = await _userManager.FindByEmailAsync(Email);
                //user not Exist => not found
                if (user == null)
                    return "UserNotFound";
                //Generate Random Number

                //Random generator = new Random();
                //string randomNumber = generator.Next(0, 1000000).ToString("D6");
                var chars = "0123456789";
                var random = new Random();
                var randomNumber = new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());

                //update User In Database Code
                user.Code = randomNumber;
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                    return "ErrorInUpdateUser";
                var message = "Code To Reset Passsword : " + user.Code;
                //Send Code To  Email 
                await _emailService.SendEmail(user.Email, message, "Reset Password");
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }

        }

        public async Task<string> ConfirmResetPassword(string Code, string Email)
        {
            //Get User
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
                return "EmailNotFound";
            //DEcrypt Code  
            var userCode = user.Code;
            //Equal With Code
            if (userCode == Code) return "Success";
            return "Failed";
        }

        public async Task<string> ResetPassword(string Email, string password)
        {
            var trans = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await _userManager.FindByEmailAsync(Email);
                if (user == null)
                    return "EmailNotFound";
                //Reomve Password
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, password);
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }



        #endregion

    }
}
