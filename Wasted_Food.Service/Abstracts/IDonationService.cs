using Microsoft.AspNetCore.Http;
using Wasted_Food.Data.Entities;

namespace Wasted_Food.Service.Abstracts
{
    public interface IDonationService
    {
        public Task<List<Donations>> GetDonationListAsync();
        public Task<Donations> GetByIDAsync(int id);
        public Task<bool> NameIsExist(string dname);
        public Task<string> AddDonationAsync(int id, Donations donation, IFormFile file);
        public Task<string> DeleteDonationWithRequests(int dontId);
        public Task<string> DeleteExpireAsync();

    }
}
