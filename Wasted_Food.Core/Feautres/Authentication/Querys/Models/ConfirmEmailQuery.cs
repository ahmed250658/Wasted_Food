using MediatR;
using Wasted_Food.Core.Bases;

namespace Wasted_Food.Core.Feautres.Authentication.Querys.Models
{
    public class ConfirmEmailQuery : IRequest<Response<string>>
    {
        public int userId { get; set; }
        public string code { get; set; }
    }
}
