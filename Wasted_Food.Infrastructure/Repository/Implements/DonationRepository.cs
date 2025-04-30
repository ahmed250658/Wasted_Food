using Microsoft.EntityFrameworkCore;
using Wasted_Food.Data.Entities;
using Wasted_Food.Infrastructure.Base;
using Wasted_Food.Infrastructure.Data;
using Wasted_Food.Infrastructure.Repository.Abstracts;

namespace Wasted_Food.Infrastructure.Repository.Implements
{
    public class DonationRepository : GenericRepositoryAsync<Donations>, IDonationRepository
    {
        #region Fileds


        private DbSet<Donations> _donation;
        #endregion
        #region Constructor

        public DonationRepository(AppDbContext dbContext) : base(dbContext)
        {
            _donation = dbContext.Set<Donations>();
        }

        async Task<string> IDonationRepository.DeleteAsync(Donations donations)
        {
            var del = await _donation.Include(x => x.FoodRequests.Select(x => x.Status == RequestStatus.Accepted)).ExecuteDeleteAsync();
            return "success";
        }
        #endregion

    }
}
