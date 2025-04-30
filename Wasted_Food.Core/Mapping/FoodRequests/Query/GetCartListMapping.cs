using Wasted_Food.Core.Feautres.FoodRequsts.Query.Dtos;
using Wasted_Food.Data.Entities;

namespace Wasted_Food.Core.Mapping.FoodRequests
{
    public partial class FoodRequestProfile
    {
        public void GetCartListMapping()
        {
            CreateMap<FoodRequest, GetCartListResponse>()
                .ForMember(dest => dest.MealName, opt => opt.MapFrom(src => src.Donation.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Donation.Quantity))
                .ForMember(dest => dest.ResturantName, opt => opt.MapFrom(src => src.Donation.RestaurantName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
