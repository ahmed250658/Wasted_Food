using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Bases;
using Wasted_Food.Core.Feautres.FoodRequsts.Command.Models;
using Wasted_Food.Core.Shared;
using Wasted_Food.Data.Entities.Identity;
using Wasted_Food.Infrastructure.Repository.Abstracts;
using Wasted_Food.Service.Abstracts;


namespace Wasted_Food.Core.Feautres.FoodRequsts.Command.Handler
{
    public class FoodRequstHandler : ResponseHandler,
                                     IRequestHandler<CreateFoodRequest, Response<string>>,
                                     IRequestHandler<AcceptedRequestCommand, Response<string>>,
                                     IRequestHandler<CancleRequestCommand, Response<string>>,
                                     IRequestHandler<RejectedRequestCommand, Response<string>>
    //IRequestHandler<DeleteRequestCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IFoodRequestService _foodRequestService;
        private readonly IFoodRequestRepository _foodRequestRepository;
        private readonly IDonationService _donationService;
        #endregion
        #region Constructor
        public FoodRequstHandler(IStringLocalizer<SharedResource> stringLocalizer, IMapper mapper, IFoodRequestService foodRequestService, IFoodRequestRepository foodRequestRepository, UserManager<Users> userManager, IDonationService donationService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _foodRequestService = foodRequestService;
            _foodRequestRepository = foodRequestRepository;
            _donationService = donationService;
        }

        #endregion
        #region Handler Functions
        public async Task<Response<string>> Handle(CreateFoodRequest request, CancellationToken cancellationToken)
        {
            var donation = await _donationService.GetByIDAsync(request.id);
            if (donation == null)
                return NotFound<string>(_stringLocalizer[SharedREsourceKeys.donationIsNotFound]);
            var result = await _foodRequestService.CreateAsync(donation, request.userId);
            switch (result)
            {
                case "UserNotFound": return NotFound<string>(_stringLocalizer[SharedREsourceKeys.UserIsNotFound]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(AcceptedRequestCommand request, CancellationToken cancellationToken)
        {
            var result = await _foodRequestService.AcceptedStatusAsync(request.Id);
            switch (result)
            {
                case "Request not found": return NotFound<string>();
                case "AllreadySelected": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.RequestAlreadyProcessed]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(result);
            }
        }

        //public async Task<Response<string>> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
        //{
        //    var foodRequest = await _foodRequestService.GetByIdAsync(request.Id);
        //    if (foodRequest == null)
        //        return NotFound<string>(_stringLocalizer[SharedREsourceKeys.ThereAreNoRequests]);

        //    //var result = await _foodRequestService.DeleteRequestAsync(foodRequest);
        //    //switch (result)
        //    //{
        //    //    case "Success": return Deleted<string>();
        //    //    case "CannotDelete": return Deleted<string>(_stringLocalizer[SharedREsourceKeys.DeleteFailed]);
        //    //    default: return BadRequest<string>(result);
        //    //}
        //    return Deleted<string>();
        //}

        public async Task<Response<string>> Handle(CancleRequestCommand request, CancellationToken cancellationToken)
        {
            var result = await _foodRequestService.CanceldStatusAsync(request.Id);
            switch (result)
            {
                case "Request not found": return NotFound<string>();
                case "AllreadySelected": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.RequestAlreadyProcessed]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(RejectedRequestCommand request, CancellationToken cancellationToken)
        {
            var result = await _foodRequestService.RejectedStatusAsync(request.Id);
            switch (result)
            {
                case "Request not found": return NotFound<string>();
                case "AllreadySelected": return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.RequestAlreadyProcessed]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(result);
            }
        }
        #endregion
    }
}
