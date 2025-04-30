using Wasted_Food.Data.Entities;
using Wasted_Food.Infrastructure.Base;

namespace Wasted_Food.Infrastructure.Repository.Abstracts
{
    public interface IFoodRequestRepository : IGenericRepositoryAsync<FoodRequest>
    {
        //public Task<List<string>> GetRequestStatusOptionsAsync(RequestStatus status);
    }
}
