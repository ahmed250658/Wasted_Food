using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Bases;
using Wasted_Food.Core.Feautres.User.Querys.Dtos;
using Wasted_Food.Core.Feautres.User.Querys.Models;
using Wasted_Food.Core.Pagination;
using Wasted_Food.Core.Shared;
using Wasted_Food.Data.Entities.Identity;

namespace Wasted_Food.Core.Feautres.User.Querys.Handler
{
    public class UserQueryHandler : ResponseHandler,
                                   IRequestHandler<GetUserListQuery, PaginatedResult<GetUserListResponse>>,
                                   IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<Users> _userManager;
        #endregion
        #region Constructor
        public UserQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IMapper mapper, UserManager<Users> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion
        #region Handle Function

        public async Task<PaginatedResult<GetUserListResponse>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var user = _userManager.Users.AsQueryable();
            var paginatedlist = await _mapper.ProjectTo<GetUserListResponse>(user).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedlist;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            //var user= await _userManager.Users.FirstOrDefaultAsync(x=>x.Id == request.Id);
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<GetUserByIdResponse>(_stringLocalizer[SharedREsourceKeys.NotFound]);
            var result = _mapper.Map<GetUserByIdResponse>(user);
            return Success(result);

        }
        #endregion

    }
}
