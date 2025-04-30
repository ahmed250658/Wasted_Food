using Wasted_Food.Core.Feautres.Donation.Query.Dtos;
using Wasted_Food.Data.Entities;

namespace Wasted_Food.Core.Mapping.donation
{
    public partial class donationProfile
    {
        public void GetListDonationMapping()
        {
            CreateMap<Donations, GetListDonationResponse>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
              .ForMember(dest => dest.ResturnatName, opt => opt.MapFrom(src => src.RestaurantName))
              .ForMember(dest => dest.ExpiryData, opt => opt.MapFrom(src => src.ExpiryData.ToString("dd MMMM yyyy")))
              .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
              .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));
        }
    }
}
