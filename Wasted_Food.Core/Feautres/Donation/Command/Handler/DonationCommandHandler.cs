using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Bases;
using Wasted_Food.Core.Feautres.Donation.Command.Models;
using Wasted_Food.Core.Shared;
using Wasted_Food.Data.Entities;
using Wasted_Food.Infrastructure.Repository.Abstracts;
using Wasted_Food.Service.Abstracts;

namespace Wasted_Food.Core.Feautres.Donation.Command.Handler
{
    public class DonationCommandHandler : ResponseHandler
                                  , IRequestHandler<CreateDonationCommand, Response<string>>
                                  , IRequestHandler<DeleteDonationCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IDonationService _donationService;
        private readonly IDonationRepository _donationRepository;
        #endregion
        #region Constructor
        public DonationCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IDonationService donationService, IMapper mapper, IDonationRepository donationRepository) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _donationService = donationService;
            _mapper = mapper;
            _donationRepository = donationRepository;
        }

        #endregion
        #region Handle Function
        public async Task<Response<string>> Handle(CreateDonationCommand request, CancellationToken cancellationToken)
        {
            var donation = await _donationService.NameIsExist(request.Name);
            var mapping = _mapper.Map<Donations>(request);
            var result = await _donationService.AddDonationAsync(request.UserId, mapping, request.Image);
            switch (result)
            {
                case "NoImage": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.NoImage]);
                case "FailedToUploadImage": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.FailedToUploadImage]);
                case "FailedInAdd": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.AddFailed]);
            }
            return Success("");
        }

        public async Task<Response<string>> Handle(DeleteDonationCommand request, CancellationToken cancellationToken)
        {
            var result = await _donationService.DeleteDonationWithRequests(request.Id);
            switch (result)
            {
                case "Donation not found": return NotFound<string>();
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.DeleteFailed]);
                case "Success": return Deleted<string>();
                default: return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.DeleteFailed]);
            }

        }
        #endregion
    }
}
