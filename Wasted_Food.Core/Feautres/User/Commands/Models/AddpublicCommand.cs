using MediatR;
using Wasted_Food.Core.Bases;

namespace Wasted_Food.Core.Feautres.User.Commands.Models
{
    public class AddpublicCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

    }
}
