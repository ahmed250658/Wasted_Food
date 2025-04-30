using MediatR;
using Wasted_Food.Core.Bases;

namespace Wasted_Food.Core.Feautres.Authentication.Querys.Models
{
    public class ConfirmResetPasswordQuery : IRequest<Response<string>>
    {
        public string Code { get; set; }
        public string Email { get; set; }
    }
}
