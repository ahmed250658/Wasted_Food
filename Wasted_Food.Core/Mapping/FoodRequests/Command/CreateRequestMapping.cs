using Wasted_Food.Core.Feautres.FoodRequsts.Command.Models;
using Wasted_Food.Data.Entities;

namespace Wasted_Food.Core.Mapping.FoodRequests
{
    public partial class FoodRequestProfile
    {
        public void CreateRequestMapping()
        {
            CreateMap<CreateFoodRequest, FoodRequest>();
            // ForMember(dest=>dest.Name,opt=>opt.MapFrom(src => src.DonationFoodRequests.Select(f => f.Donation.Name)))
            //. ForMember(dest=>dest.,opt=>opt.MapFrom(src => src.DonationFoodRequests.Select(f=>f.Donation.Quantity)))
            //. ForMember(dest=>dest.DonationFoodRequests.Select(f=>f.Donation.ExpiryData),opt=>opt.MapFrom(src => src.ExpiryData))
            //. ForMember(dest=>dest.DonationFoodRequests.Select(f=>f.Donation.Location),opt=>opt.MapFrom(src => src.Location))
            //. ForMember(dest=>dest.DonationFoodRequests.Select(f=>f.Donation.Image),opt=>opt.MapFrom(src => src.Image))
        }
    }
}
