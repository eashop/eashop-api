using EaShop.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EaShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file.Length > 0)
            {
                return Ok(new { FileName = await _fileService.AddFile(file) });
            }
            return BadRequest(file);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmUpload(string name)
        {
            await _fileService.ConfirmUpload(name);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile([FromRoute]string name)
        {
            var file = await _fileService.GetFile(name);
            if (file == null)
            {
                return BadRequest();
            }
            return File(file, "application/octet-stream");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile([FromRoute]string name)
        {
            var successfull = await _fileService.DeleteFile(name);
            if (!successfull)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}