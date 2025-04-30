using MediatR;
using Wasted_Food.Core.Bases;

namespace Wasted_Food.Core.Feautres.Authentication.Commands.Models
{
    public class ResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
