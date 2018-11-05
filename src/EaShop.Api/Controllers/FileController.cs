using EaShop.Api.Services;
using EaShop.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EaShop.Api.Controllers
{
    [Produces("application/json")]
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(FileViewModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file.Length > 0)
            {
                return Ok(new FileViewModel { FileName = await _fileService.AddFile(file) });
            }
            return BadRequest(file);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ConfirmUpload(string name)
        {
            await _fileService.ConfirmUpload(name);
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(FileStreamResult))]
        [ProducesResponseType(206)]
        [ProducesResponseType(400)]
        [ProducesResponseType(416)]
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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