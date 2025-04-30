using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Bases;
using Wasted_Food.Core.Feautres.User.Commands.Models;
using Wasted_Food.Core.Shared;
using Wasted_Food.Data.Entities.Identity;
using Wasted_Food.Data.Enum;
using Wasted_Food.Service.Abstracts;

namespace Wasted_Food.Core.Feautres.User.Commands.Handler
{
    public class UserCommandHandler : ResponseHandler,
                                      IRequestHandler<CreateUserCommand, Response<string>>,
                                      IRequestHandler<EditUserCommand, Response<string>>,
                                      IRequestHandler<DeleteUserCommand, Response<string>>,
                                      IRequestHandler<AddpublicCommand, Response<string>>,
                                      IRequestHandler<AddCharityCommand, Response<string>>

    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly UserManager<Users> _userManager;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        #endregion
        #region Constructor
        public UserCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IMapper mapper, UserManager<Users> userManager, IUserService userService, IHttpContextAccessor httpContextAccessor, IEmailService emailService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
        }
        #endregion
        #region Handle Function
        public async Task<Response<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //Mapping
            var identityUser = _mapper.Map<Users>(request);
            //Create user
            var result = await _userService.AddUserAsync(identityUser, request.Password);
            switch (result)
            {
                case "EmailIsExist": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.EmailIsExist]);
                case "UserNameIsExist": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.IsExist]);
                case "ErrorInCreateUser": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.FaildAddUser]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.TryToRegisterAgain]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(result);
            }
            //Failed

            return Success("");

        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            //Check if user is exist
            var olduser = await _userManager.FindByIdAsync(request.Id.ToString());
            //if user is not found
            if (olduser == null) return NotFound<string>(_stringLocalizer[SharedREsourceKeys.NotFound]);
            //mapper
            var newuser = _mapper.Map(request, olduser);
            //Change the password
            var changePasswordResult = await _userManager.ChangePasswordAsync(olduser, request.CurrentPassword, request.ChangePassword);
            if (!changePasswordResult.Succeeded)
                return BadRequest<string>(changePasswordResult.Errors.FirstOrDefault().Description);

            //Edit the user
            var result = await _userManager.UpdateAsync(newuser);
            // if result not success
            if (!result.Succeeded)
                return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.UpdatedFailed]);
            //message
            return Success((string)_stringLocalizer[SharedREsourceKeys.Updated]);

        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            //check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //if Not Exist notfound
            if (user == null) return NotFound<string>(_stringLocalizer[SharedREsourceKeys.DeleteFailed]);
            //Delete the User
            var result = await _userManager.DeleteAsync(user);
            //in case of Failure
            if (!result.Succeeded) return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.DeleteFailed]);
            return Success((string)_stringLocalizer[SharedREsourceKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(AddpublicCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.Addpublic(request.Id);
            if (result == "UserNotFound") return NotFound<string>();
            else if (result == "TypeAlreadySelected") return NotFound<string>(_stringLocalizer[SharedREsourceKeys.AlreadySelectType]);
            else if (result == "UpdateFailed") return NotFound<string>(_stringLocalizer[SharedREsourceKeys.UpdatedFailed]);
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            var role = user.TypeOfOrgn switch
            {
                TypeOfOrganization.PublicInstitution => "PublicInstitution",
                // TypeOfOrganization.CharityOrganization => "CharityOrganization",
                _ => throw new InvalidOperationException("Invalid organization type")
            };

            await _userManager.AddToRoleAsync(user, role);
            return Success("");
        }

        public async Task<Response<string>> Handle(AddCharityCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.AddChart(request.Id);
            if (result == "UserNotFound") return NotFound<string>();
            else if (result == "TypeAlreadySelected") return NotFound<string>(_stringLocalizer[SharedREsourceKeys.AlreadySelectType]);
            else if (result == "UpdateFailed") return NotFound<string>(_stringLocalizer[SharedREsourceKeys.UpdatedFailed]);
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            var role = user.TypeOfOrgn switch
            {
                //TypeOfOrganization.PublicInstitution => "PublicInstitution",
                TypeOfOrganization.CharityOrganization => "CharityOrganization",
                _ => throw new InvalidOperationException("Invalid organization type")
            };

            await _userManager.AddToRoleAsync(user, role);
            return Success("");
        }

        //public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        //{
        //    //get user
        //    //check if user is exist
        //    var user = await _userManager.FindByIdAsync(request.Id.ToString());
        //    //if Not Exist notfound
        //    if (user == null) return NotFound<string>();

        //    //Change User Password
        //    var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        //    //var user1=await _userManager.HasPasswordAsync(user);
        //    //await _userManager.RemovePasswordAsync(user);
        //    //await _userManager.AddPasswordAsync(user, request.NewPassword);

        //    //result
        //    if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);
        //    return Success((string)_stringLocalizer[SharedREsourceKeys.Success]);
        //}
        #endregion

    }
}
