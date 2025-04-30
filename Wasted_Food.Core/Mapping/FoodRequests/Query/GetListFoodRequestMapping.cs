using Wasted_Food.Core.Feautres.FoodRequsts.Query.Dtos;
using Wasted_Food.Data.Entities;

namespace Wasted_Food.Core.Mapping.FoodRequests
{
    public partial class FoodRequestProfile
    {
        public void GetListFoodRequestMapping()
        {
            CreateMap<FoodRequest, GetFoodRequestListResponse>()
                .ForMember(dest => dest.MealName, opt => opt.MapFrom(src => src.Donation.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Donation.Quantity))
                .ForMember(dest => dest.RequestedBy, opt => opt.MapFrom(src => src.RequestedBy))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));


        }
    }
}
