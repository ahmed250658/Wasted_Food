using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Bases;
using Wasted_Food.Core.Feautres.Donation.Query.Dtos;
using Wasted_Food.Core.Feautres.Donation.Query.Modles;
using Wasted_Food.Core.Shared;
using Wasted_Food.Service.Abstracts;

namespace Wasted_Food.Core.Feautres.Donation.Query.Handler
{
    public class DonationQueryHandler : ResponseHandler,
                                      IRequestHandler<GetListDonation, Response<List<GetListDonationResponse>>>

    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IDonationService _donationService;
        #endregion
        #region Constructor
        public DonationQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IDonationService donationService, IMapper mapper) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _donationService = donationService;
            _mapper = mapper;
        }
        #endregion
        #region Handle Function
        public async Task<Response<List<GetListDonationResponse>>> Handle(GetListDonation request, CancellationToken cancellationToken)
        {
            var donations = await _donationService.GetDonationListAsync();

            if (donations == null) return NotFound<List<GetListDonationResponse>>();
            var donationsMapping = _mapper.Map<List<GetListDonationResponse>>(donations);

            var result = Success(donationsMapping);
            result.Meta = new { Count = donationsMapping.Count() };
            return result;
        }


        #endregion
    }
}
