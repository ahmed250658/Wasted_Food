using System.IdentityModel.Tokens.Jwt;
using Wasted_Food.Data.Entities.Identity;
using Wasted_Food.Data.Helper;

namespace Wasted_Food.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(Users user);
        public JwtSecurityToken ReadJwtToken(string accessToken);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
        public Task<JwtAuthResult> GetRefreshToken(Users user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
        public Task<string> ValidateToken(string accessToken);
        public Task<string> ConfirmEmail(int? userId, string? code);
        public Task<string> SendResetPasswordCode(string Email);
        public Task<string> ConfirmResetPassword(string Code, string Email);
        public Task<string> ResetPassword(string Email, string password);
    }
}
