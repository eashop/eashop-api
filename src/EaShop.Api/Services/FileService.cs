using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EaShop.Api.Services
{
    public class FileService : IFileService
    {
        private readonly string _path = "Uploads";

        public async Task<string> AddFile(IFormFile file)
        {
            //file should be confirmed or deleted
            var extension = Path.GetExtension(file.FileName);
            var name = Guid.NewGuid().ToString("N") + extension;
            var path = Path.Combine(_path, name);
            using (var stream = new FileStream(path, FileMode.CreateNew))
            {
                await file.CopyToAsync(stream);
            }
            return name;
        }

        public async Task<Stream> GetFile(string name)
        {
            var path = Path.Combine(_path, name);
            if (!File.Exists(path))
            {
                return null;
            }
            return File.OpenRead(path);
        }

        public Task<bool> DeleteFile(string name) => Task.Run(() =>
                                                               {
                                                                   var path = Path.Combine(_path, name);
                                                                   if (!File.Exists(path))
                                                                   {
                                                                       return false;
                                                                   }
                                                                   File.Delete(path);
                                                                   return true;
                                                               });

        public async Task ConfirmUpload(string name)
        {
            //should be called
            return;
        }
    }
}