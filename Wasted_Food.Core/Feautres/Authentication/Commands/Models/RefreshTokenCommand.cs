using MediatR;
using Wasted_Food.Core.Bases;
using Wasted_Food.Data.Helper;

namespace Wasted_Food.Core.Feautres.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
