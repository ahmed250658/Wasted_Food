using Wasted_Food.Core.Feautres.User.Commands.Models;
using Wasted_Food.Data.Entities.Identity;

namespace Wasted_Food.Core.Mapping.User
{
    public partial class UserProfile
    {
        public void EditUserMapping()
        {
            CreateMap<EditUserCommand, Users>().
                 ForMember(d => d.UserName, s => s.MapFrom(src => src.FullName)).
                 ForMember(d => d.Email, s => s.MapFrom(src => src.Email));


        }
    }
}
