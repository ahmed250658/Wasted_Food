using Microsoft.EntityFrameworkCore;
using Wasted_Food.Data.Entities;
using Wasted_Food.Infrastructure.Base;
using Wasted_Food.Infrastructure.Data;
using Wasted_Food.Infrastructure.Repository.Abstracts;

namespace Wasted_Food.Infrastructure.Repository.Implements
{
    public class FoodRequestRepository : GenericRepositoryAsync<FoodRequest>, IFoodRequestRepository
    {
        #region Fileds
        private DbSet<FoodRequest> _request;
        #endregion
        #region Constructor

        public FoodRequestRepository(AppDbContext dbContext) : base(dbContext)
        {
            _request = dbContext.Set<FoodRequest>();
        }



        #endregion

    }
}
