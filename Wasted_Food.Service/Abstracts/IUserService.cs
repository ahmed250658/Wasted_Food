using Wasted_Food.Data.Entities.Identity;

namespace Wasted_Food.Service.Abstracts
{
    public interface IUserService
    {
        public Task<string> Addpublic(int id);
        public Task<string> AddChart(int id);
        public Task<string> AddUserAsync(Users user, string password);


    }

}

