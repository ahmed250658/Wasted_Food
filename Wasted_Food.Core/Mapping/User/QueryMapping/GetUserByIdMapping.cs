using Wasted_Food.Core.Feautres.User.Querys.Dtos;
using Wasted_Food.Data.Entities.Identity;

namespace Wasted_Food.Core.Mapping.User
{
    public partial class UserProfile
    {
        public void GetUserByIdMapping()
        {
            CreateMap<Users, GetUserByIdResponse>().
                ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.UserName)).
                ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)).
                ForMember(d => d.Password, s => s.MapFrom(src => src.PasswordHash));
        }
    }
}
