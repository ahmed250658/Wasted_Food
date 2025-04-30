using MediatR;
using Wasted_Food.Core.Bases;

namespace Wasted_Food.Core.Feautres.User.Commands.Models
{
    public class CreateUserCommand : IRequest<Response<string>>
    {
        public string OrganizationName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfrimPassword { get; set; }


    }
}
