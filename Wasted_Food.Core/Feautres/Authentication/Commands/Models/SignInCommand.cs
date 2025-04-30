using MediatR;
using Wasted_Food.Core.Bases;
using Wasted_Food.Data.Helper;

namespace Wasted_Food.Core.Feautres.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
