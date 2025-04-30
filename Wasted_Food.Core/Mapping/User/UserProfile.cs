using AutoMapper;

namespace Wasted_Food.Core.Mapping.User
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            AddUserMapping();
            GetUserListMapping();
            GetUserByIdMapping();
            EditUserMapping();
        }
    }
}
