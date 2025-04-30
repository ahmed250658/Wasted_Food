using Wasted_Food.Data.Entities;

namespace Wasted_Food.Service.Abstracts
{
    public interface IFoodRequestService
    {
        public Task<string> CreateAsync(Donations donation, int userid);
        public Task<List<FoodRequest>> GetAllPendingAsync();
        public Task<List<FoodRequest>> GetAccpetListAsync();
        public Task<FoodRequest> GetByIdAsync(int id);
        public Task<string> AcceptedStatusAsync(int requestId);
        public Task<string> RejectedStatusAsync(int requestId);
        public Task<string> CanceldStatusAsync(int requestId);


    }
}
