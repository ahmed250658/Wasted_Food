using MediatR;
using Wasted_Food.Core.Bases;

namespace Wasted_Food.Core.Feautres.FoodRequsts.Command.Models
{
    public class DeleteRequestCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteRequestCommand(int id)
        {
            Id = id;
        }
    }
}
