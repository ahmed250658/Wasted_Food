using Wasted_Food.Data.Entities.Identity;
using Wasted_Food.Infrastructure.Base;

namespace Wasted_Food.Infrastructure.Repository.Abstracts
{
    public interface IRefreshTokenReposatory : IGenericRepositoryAsync<UserRefreshToken>
    {
    }
}
