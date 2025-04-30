using MediatR;
using Wasted_Food.Core.Bases;
using Wasted_Food.Core.Feautres.FoodRequsts.Query.Dtos;

namespace Wasted_Food.Core.Feautres.FoodRequsts.Query.Models
{
    public class GetCartList : IRequest<Response<List<GetCartListResponse>>>
    {
    }
}
