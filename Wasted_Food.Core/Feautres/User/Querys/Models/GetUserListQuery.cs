using MediatR;
using Wasted_Food.Core.Feautres.User.Querys.Dtos;
using Wasted_Food.Core.Pagination;

namespace Wasted_Food.Core.Feautres.User.Querys.Models
{
    public class GetUserListQuery : IRequest<PaginatedResult<GetUserListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
