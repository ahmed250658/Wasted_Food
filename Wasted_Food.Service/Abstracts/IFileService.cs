using Microsoft.AspNetCore.Http;

namespace Wasted_Food.Service.Abstracts
{
    public interface IFileService
    {
        public Task<string> UploadImage(string Location, IFormFile file);
        public Task<string> DeleteImage(string imageUrl);
    }
}
