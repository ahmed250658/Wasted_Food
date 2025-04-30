using MediatR;
using Wasted_Food.Core.Bases;
using Wasted_Food.Core.Feautres.User.Querys.Dtos;

namespace Wasted_Food.Core.Feautres.User.Querys.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        public int Id { get; set; }
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
