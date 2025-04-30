using Wasted_Food.Data.Entities.Identity;

namespace Wasted_Food.Service.AuthService.Abstracts
{
    public interface ICurrentUserService
    {
        public Task<Users> GetUserAsync();
        public int GetUserId();
        public Task<List<string>> GetCurrentUserRolesAsync();
    }
}
