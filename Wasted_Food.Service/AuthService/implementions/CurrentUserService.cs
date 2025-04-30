using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Wasted_Food.Data.Entities.Identity;
using Wasted_Food.Data.Helper;
using Wasted_Food.Service.AuthService.Abstracts;

namespace Wasted_Food.Service.AuthService.implementions
{
    public class CurrentUserService : ICurrentUserService
    {
        #region fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Users> _userManager;
        #endregion

        #region Constructorr
        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<Users> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        #endregion

        #region Handle Function
        public Task<List<string>> GetCurrentUserRolesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Users> GetUserAsync()
        {
            var userId = GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            return user;
        }

        public int GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(claims => claims.Type == nameof(UserClaimModel.Id)).Value;
            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }
            return int.Parse(userId);
        }
        #endregion

    }
}
