using Microsoft.EntityFrameworkCore;
using Wasted_Food.Data.Entities.Identity;
using Wasted_Food.Infrastructure.Base;
using Wasted_Food.Infrastructure.Data;
using Wasted_Food.Infrastructure.Repository.Abstracts;

namespace Wasted_Food.Infrastructure.Repository.Implements
{
    public class RefreshTokenReposatory : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenReposatory
    {
        #region Fileds


        private DbSet<UserRefreshToken> _userTokens;
        #endregion
        #region Constructor

        public RefreshTokenReposatory(AppDbContext dbContext) : base(dbContext)
        {
            _userTokens = dbContext.Set<UserRefreshToken>();
        }
        #endregion
    }
}
