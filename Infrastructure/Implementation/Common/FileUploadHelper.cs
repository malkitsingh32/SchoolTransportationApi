using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace Infrastructure.Implementation.Common
{
    public static class FileUploadHelper
    {
        public static string SaveUploadedFile(IFormFile file, IHostingEnvironment _environment, string folder, Guid id)
        {
            string fileLocation = Path.Combine(_environment.WebRootPath, @$"UploadedData\{folder}");
            if (!Directory.Exists(fileLocation))
            {
                Directory.CreateDirectory(fileLocation);
            }
            fileLocation = Path.Combine(fileLocation, @$"{file.FileName}");

            using (FileStream fs = System.IO.File.Create(fileLocation))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            return (@$"\UploadedData\{folder}\{file.FileName}");
        }
        public static void RemoveFileFromPath(IHostingEnvironment _environment, string filePath)
        {
            filePath = _environment.WebRootPath + $"{filePath}";
            if (!string.IsNullOrEmpty(filePath))
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }
    }
}
