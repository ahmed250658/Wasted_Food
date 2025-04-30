using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Wasted_Food.Service.Abstracts;

namespace Wasted_Food.Service.Implementions
{
    public class FileService : IFileService
    {
        #region Fileds
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Constructor
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Handle function
        public async Task<string> UploadImage(string Location, IFormFile file)
        {
            var path = _webHostEnvironment.WebRootPath + "/" + Location + "/";
            var extention = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extention;
            if (file.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = File.Create(path + fileName))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                        return $"/{Location}/{fileName}";
                    }
                }
                catch (Exception)
                {
                    return "FailedToUploadImage";
                }
            }
            else
            {
                return "NoImage";
            }
        }
        public async Task<string> DeleteImage(string imageUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(imageUrl))
                {
                    return "EmptyUrl";
                }

                // استخراج اسم الملف من الرابط
                var fileName = Path.GetFileName(imageUrl);
                if (string.IsNullOrEmpty(fileName))
                {
                    return "InvalidUrl";
                }

                // المسار الكامل للملف
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
                var filePath = Path.Combine(uploadsFolder, fileName);

                if (!File.Exists(filePath))
                {
                    return "FileNotFound";
                }

                File.Delete(filePath);
                return "Success";
            }
            catch (Exception ex)
            {
                return "DeleteFailed";
            }
        }
    }
    #endregion

}

