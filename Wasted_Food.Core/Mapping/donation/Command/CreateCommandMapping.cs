using Wasted_Food.Core.Feautres.Donation.Command.Models;
using Wasted_Food.Data.Entities;

namespace Wasted_Food.Core.Mapping.donation
{
    public partial class donationProfile
    {
        public void CreateCommandMapping()
        {
            CreateMap<CreateDonationCommand, Donations>().
                 ForMember(d => d.Name, s => s.MapFrom(src => src.Name)).
                 //  ForMember(d => d.RestaurantName, s => s.MapFrom(src => src.ResturantName)).
                 ForMember(d => d.Quantity, s => s.MapFrom(src => src.Quantity)).
                 ForMember(d => d.ExpiryData, s => s.MapFrom(src => src.ExpiryData)).
                 ForMember(d => d.Image, s => s.Ignore()).
                 ForMember(d => d.Location, s => s.MapFrom(src => src.Location));
        }
    }
}
