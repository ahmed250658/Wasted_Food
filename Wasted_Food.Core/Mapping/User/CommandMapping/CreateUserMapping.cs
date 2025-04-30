using Wasted_Food.Core.Feautres.User.Commands.Models;
using Wasted_Food.Data.Entities.Identity;

namespace Wasted_Food.Core.Mapping.User
{
    public partial class UserProfile
    {
        public void AddUserMapping()
        {
            CreateMap<CreateUserCommand, Users>().
                 ForMember(d => d.UserName, s => s.MapFrom(src => src.OrganizationName)).
                 ForMember(d => d.Email, s => s.MapFrom(src => src.Email)).
                 ForMember(d => d.PasswordHash, s => s.MapFrom(src => src.Password));
        }
    }
}
