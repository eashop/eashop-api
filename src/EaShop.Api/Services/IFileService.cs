using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace EaShop.Api.Services
{
    public interface IFileService
    {
        Task<string> AddFile(IFormFile file);

        Task<Stream> GetFile(string name);

        Task<bool> DeleteFile(string name);

        Task ConfirmUpload(string name);
    }
}