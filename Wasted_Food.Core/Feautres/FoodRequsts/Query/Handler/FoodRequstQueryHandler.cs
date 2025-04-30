using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Bases;
using Wasted_Food.Core.Feautres.FoodRequsts.Query.Dtos;
using Wasted_Food.Core.Feautres.FoodRequsts.Query.Models;
using Wasted_Food.Core.Shared;
using Wasted_Food.Service.Abstracts;

namespace Wasted_Food.Core.Feautres.FoodRequsts.Query.Handler
{
    public class FoodRequstQueryHandler : ResponseHandler,
                                      IRequestHandler<GetFoodRequestList, Response<List<GetFoodRequestListResponse>>>,
                                      IRequestHandler<GetCartList, Response<List<GetCartListResponse>>>,
                                      IRequestHandler<GetListAcceptedCommand, Response<List<GetCartListResponse>>>


    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IFoodRequestService _foodRequestService;
        #endregion
        #region Constructor
        public FoodRequstQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IMapper mapper, IFoodRequestService foodRequestService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _foodRequestService = foodRequestService;
        }
        #endregion
        #region Handler Functions
        public async Task<Response<List<GetFoodRequestListResponse>>> Handle(GetFoodRequestList request, CancellationToken cancellationToken)
        {
            var RequestList = await _foodRequestService.GetAllPendingAsync();
            if (RequestList == null) return NotFound<List<GetFoodRequestListResponse>>(_stringLocalizer[SharedREsourceKeys.ThereAreNoRequests]);
            var RequestListMapper = _mapper.Map<List<GetFoodRequestListResponse>>(RequestList);
            var result = Success(RequestListMapper);
            return result;
        }

        public async Task<Response<List<GetCartListResponse>>> Handle(GetCartList request, CancellationToken cancellationToken)
        {
            var RequestList = await _foodRequestService.GetAllPendingAsync();
            if (RequestList == null) return NotFound<List<GetCartListResponse>>(_stringLocalizer[SharedREsourceKeys.ThereAreNoRequests]);
            var RequestListMapper = _mapper.Map<List<GetCartListResponse>>(RequestList);
            var result = Success(RequestListMapper);
            return result;
        }

        public async Task<Response<List<GetCartListResponse>>> Handle(GetListAcceptedCommand request, CancellationToken cancellationToken)
        {
            var RequestList = await _foodRequestService.GetAccpetListAsync();
            if (RequestList == null) return NotFound<List<GetCartListResponse>>(_stringLocalizer[SharedREsourceKeys.ThereAreNoRequests]);
            var RequestListMapper = _mapper.Map<List<GetCartListResponse>>(RequestList);
            var result = Success(RequestListMapper);
            return result;
        }


        #endregion
    }
}
