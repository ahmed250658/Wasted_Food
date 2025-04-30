using MediatR;
using Wasted_Food.Core.Bases;

namespace Wasted_Food.Core.Feautres.User.Commands.Models
{
    public class EditUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string ChangePassword { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
