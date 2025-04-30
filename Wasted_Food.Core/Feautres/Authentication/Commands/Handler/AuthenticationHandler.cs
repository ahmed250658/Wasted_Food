using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Bases;
using Wasted_Food.Core.Feautres.Authentication.Commands.Models;
using Wasted_Food.Core.Shared;
using Wasted_Food.Data.Entities.Identity;
using Wasted_Food.Data.Helper;
using Wasted_Food.Service.Abstracts;

namespace Wasted_Food.Core.Feautres.Authentication.Commands.Handler
{
    public class AuthenticationHandler : ResponseHandler,
                                        IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
                                        IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>,
                                        IRequestHandler<SendResetPasswordCommand, Response<string>>,
                                        IRequestHandler<ResetPasswordCommand, Response<string>>

    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        #endregion
        #region Constructor
        public AuthenticationHandler(IStringLocalizer<SharedResource> stringLocalizer, IMapper mapper, UserManager<Users> userManager, SignInManager<Users> signInManager, IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }
        #endregion
        #region Handle Function
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            ///check if user is Exist or Not
            var user = await _userManager.FindByEmailAsync(request.Email);
            //Return User Not Found
            if (user == null) return NotFound<JwtAuthResult>(_stringLocalizer[SharedREsourceKeys.EmailIsNotFound]);
            //Sign in
            var signin = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            //Failed Signin
            if (!signin.Succeeded)
                return BadRequest<JwtAuthResult>(_stringLocalizer[SharedREsourceKeys.PasswordNotCorrect]);
            //Confrim Email
            if (!user.EmailConfirmed)
                return BadRequest<JwtAuthResult>(_stringLocalizer[SharedREsourceKeys.ConfrimEmail]);
            //Generate Token
            var result = await _authenticationService.GetJWTToken(user);
            //Return token
            return Success(result);
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = _authenticationService.ReadJwtToken(request.AccessToken);
            var userIdAndExpireDate = await _authenticationService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
            switch (userIdAndExpireDate)
            {
                case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedREsourceKeys.AlgorithmIsWrong]);
                case ("TokenIsNotExpired", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedREsourceKeys.TokenIsNotExpired]);
                case ("RefreshTokenIsNotFound", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedREsourceKeys.RefreshTokenIsNotFound]);
                case ("RefreshTokenIsExpired", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedREsourceKeys.RefreshTokenIsExpired]);
            }
            var (userId, ExpireDate) = userIdAndExpireDate;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound<JwtAuthResult>();
            }
            var result = await _authenticationService.GetRefreshToken(user, jwtToken, ExpireDate, request.RefreshToken);
            return Success(result);
        }

        public async Task<Response<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.SendResetPasswordCode(request.Email);
            switch (result)
            {
                case "EmailNotFound": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.EmailIsNotFound]);
                case "ErrorInUpdateUser": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.UpdatedFailed]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.TryAgain]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ResetPassword(request.Email, request.Password);
            switch (result)
            {
                case "EmailNotFound": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.EmailIsNotFound]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.TryAgain]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(result);
            }
        }
        #endregion
    }
}
