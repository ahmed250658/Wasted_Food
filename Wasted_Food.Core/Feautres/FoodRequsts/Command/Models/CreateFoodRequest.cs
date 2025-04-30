using MediatR;
using Wasted_Food.Core.Bases;

namespace Wasted_Food.Core.Feautres.FoodRequsts.Command.Models
{
    public class CreateFoodRequest : IRequest<Response<string>>
    {
        public int userId { get; set; }
        public int id { get; set; }

    }
}
