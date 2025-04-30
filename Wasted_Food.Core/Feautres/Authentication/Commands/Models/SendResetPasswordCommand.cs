using MediatR;
using Wasted_Food.Core.Bases;

namespace Wasted_Food.Core.Feautres.Authentication.Commands.Models
{
    public class SendResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { set; get; }
    }
}
