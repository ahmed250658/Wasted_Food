using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Shared;

namespace Wasted_Food.Core.Bases
{
    public class ResponseHandler
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public ResponseHandler(IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        public Response<T> Deleted<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = Message == null ? _stringLocalizer[SharedREsourceKeys.Deleted] : Message

            };
        }
        public Response<T> Success<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = _stringLocalizer[SharedREsourceKeys.Success],
                Meta = Meta
            };
        }
        public Response<T> Unauthorized<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = Message == null ? _stringLocalizer[SharedREsourceKeys.UnAuthroized] : Message

            };
        }
        public Response<T> BadRequest<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? _stringLocalizer[SharedREsourceKeys.BadRequest] : Message
            };
        }
        public Response<T> UnprocessableEntity<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = Message == null ? _stringLocalizer[SharedREsourceKeys.UnprocessableEntity] : Message
            };
        }

        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? _stringLocalizer[SharedREsourceKeys.NotFound] : message
            };
        }

        public Response<T> Created<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = _stringLocalizer[SharedREsourceKeys.Created],
                Meta = Meta
            };
        }
    }
}
