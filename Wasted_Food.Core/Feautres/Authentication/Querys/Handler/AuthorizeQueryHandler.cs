using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Bases;
using Wasted_Food.Core.Feautres.Authentication.Querys.Models;
using Wasted_Food.Core.Shared;
using Wasted_Food.Data.Entities.Identity;
using Wasted_Food.Service.Abstracts;

namespace Wasted_Food.Core.Feautres.Authentication.Querys.Handler
{
    public class AuthorizeQueryHandler : ResponseHandler,
                                        IRequestHandler<AuthorizeUserQuery, Response<string>>,
                                        IRequestHandler<ConfirmEmailQuery, Response<string>>,
                                        IRequestHandler<ConfirmResetPasswordQuery, Response<string>>


    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IAuthenticationService _authenticationService;

        #endregion
        #region Constructor
        public AuthorizeQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IMapper mapper, UserManager<Users> userManager, SignInManager<Users> signInManager, IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }
        #endregion
        #region Handle Function
        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToke);
            if (result == "NotExpired") return Success(result);
            return Unauthorized<string>(_stringLocalizer[SharedREsourceKeys.TokenIsExpired]);
        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var confirmEamil = await _authenticationService.ConfirmEmail(request.userId, request.code);
            if (confirmEamil == "ErrorWhenConfirmEmail")
                return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.ErrorWhenConfirmEmail]);
            return Success<string>(_stringLocalizer[SharedREsourceKeys.ConfirmEmailDone]);
        }

        public async Task<Response<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ConfirmResetPassword(request.Code, request.Email);
            switch (result)
            {
                case "EmailNotFound": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.EmailIsNotFound]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.InvaildCode]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(result);
            }
        }
        #endregion
    }
}
