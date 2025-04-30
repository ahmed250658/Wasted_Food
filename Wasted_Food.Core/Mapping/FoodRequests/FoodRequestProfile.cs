using AutoMapper;

namespace Wasted_Food.Core.Mapping.FoodRequests
{
    public partial class FoodRequestProfile : Profile
    {
        public FoodRequestProfile()
        {
            GetListFoodRequestMapping();
            GetCartListMapping();

        }
    }
}
