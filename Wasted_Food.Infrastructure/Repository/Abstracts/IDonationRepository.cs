using Wasted_Food.Data.Entities;
using Wasted_Food.Infrastructure.Base;

namespace Wasted_Food.Infrastructure.Repository.Abstracts
{
    public interface IDonationRepository : IGenericRepositoryAsync<Donations>
    {
        public Task<string> DeleteAsync(Donations donations);
    }
}
