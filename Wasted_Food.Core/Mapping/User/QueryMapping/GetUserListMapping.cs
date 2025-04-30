using Wasted_Food.Core.Feautres.User.Querys.Dtos;
using Wasted_Food.Data.Entities.Identity;

namespace Wasted_Food.Core.Mapping.User
{
    public partial class UserProfile
    {
        public void GetUserListMapping()
        {
            CreateMap<Users, GetUserListResponse>().
                ForMember(d => d.FullName, s => s.MapFrom(src => src.UserName)).
                ForMember(d => d.Email, s => s.MapFrom(src => src.Email)).
             ForMember(d => d.Type, s => s.MapFrom(src => src.TypeOfOrgn.ToString()));
        }
    }
}
