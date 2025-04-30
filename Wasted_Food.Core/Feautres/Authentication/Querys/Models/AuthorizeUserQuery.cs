using MediatR;
using Wasted_Food.Core.Bases;

namespace Wasted_Food.Core.Feautres.Authentication.Querys.Models
{
    public class AuthorizeUserQuery : IRequest<Response<string>>
    {
        public string AccessToke { get; set; }
    }
}
