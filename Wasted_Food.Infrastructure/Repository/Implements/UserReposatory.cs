using Microsoft.EntityFrameworkCore;
using Wasted_Food.Data.Entities.Identity;
using Wasted_Food.Infrastructure.Base;
using Wasted_Food.Infrastructure.Data;
using Wasted_Food.Infrastructure.Repository.Abstracts;

namespace Wasted_Food.Infrastructure.Repository.Implements
{
    public class UserReposatory : GenericRepositoryAsync<Users>, IUserReposatory
    {
        #region Fileds


        private DbSet<Users> _users;
        #endregion
        #region Constructor

        public UserReposatory(AppDbContext dbContext) : base(dbContext)
        {
            _users = dbContext.Set<Users>();
        }
        #endregion
    }
}
