using AutoMapper;

namespace Wasted_Food.Core.Mapping.donation
{
    public partial class donationProfile : Profile
    {
        public donationProfile()
        {

            CreateCommandMapping();
            GetListDonationMapping();
        }
    }
}
