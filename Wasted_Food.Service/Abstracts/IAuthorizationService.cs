namespace Wasted_Food.Service.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleName(string roleName);
    }
}
