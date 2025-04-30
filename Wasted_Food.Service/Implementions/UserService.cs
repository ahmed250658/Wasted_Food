using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wasted_Food.Data.Entities.Identity;
using Wasted_Food.Data.Enum;
using Wasted_Food.Infrastructure.Data;
using Wasted_Food.Service.Abstracts;

namespace Wasted_Food.Service.Implementions
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly UserManager<Users> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IUrlHelper _urlHelper;

        #endregion

        #region Constructor
        public UserService(AppDbContext dbContext, UserManager<Users> userManager,
            IHttpContextAccessor httpContextAccessor, IEmailService emailService,
            IUrlHelper urlHelper)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _urlHelper = urlHelper;
        }

        public async Task<string> AddChart(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return "UserNotFound";
            }


            if (user.TypeOfOrgn == TypeOfOrganization.PublicInstitution || user.TypeOfOrgn == TypeOfOrganization.CharityOrganization)
            {
                return "TypeAlreadySelected";
            }


            user.TypeOfOrgn = TypeOfOrganization.CharityOrganization;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded ? "Success" : "UpdateFailed";
        }

        #endregion

        #region Handler Function   
        public async Task<string> Addpublic(int id)
        {

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return "UserNotFound";
            }


            if (user.TypeOfOrgn == TypeOfOrganization.PublicInstitution || user.TypeOfOrgn == TypeOfOrganization.CharityOrganization)
            {
                return "TypeAlreadySelected";
            }


            user.TypeOfOrgn = TypeOfOrganization.PublicInstitution;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded ? "Success" : "UpdateFailed";
        }



        public async Task<string> AddUserAsync(Users user, string password)
        {
            var trans = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                //Check if Email is Exist
                var Existuser = await _userManager.FindByEmailAsync(user.Email);
                //Email is Exist
                if (Existuser != null) return "EmailIsExist";
                // Check if Email is Exist
                var ExistName = await _userManager.FindByNameAsync(user.UserName);
                //Email is Exist
                if (ExistName != null) return "UserNameIsExist";
                //Create user
                var result = await _userManager.CreateAsync(user, password);
                //Failed
                if (!result.Succeeded) return "ErrorInCreateUser";
                // Send Confrim Email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var accessor = _httpContextAccessor.HttpContext.Request;
                var requestUrl = $"{accessor.Scheme}://{accessor.Host}{_urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code })}";
                var message = $"To Confirm Email Click Link: <a href='{requestUrl}'>Link Of Confirmation</a>";
                //$"/Api/V1/Authentication/ConfirmEmail?userId={user.Id}&code={code}";
                // Message Body
                var sendEmail = await _emailService.SendEmail(user.Email, requestUrl, "Confirm Email");
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
