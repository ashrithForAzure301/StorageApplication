using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageCRUD.Models;
using StorageCRUD.StorageInterface;

namespace StorageCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileShareController : ControllerBase
    {
        private readonly FileShareInterface _repository;

        public FileShareController(FileShareInterface repository)
        {
            _repository = repository;
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] FileShareModel file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repository.UploadFile(file.FileDetail);

            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpGet("Download/{fileName}")]

        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var file = await _repository.DownloadFile(fileName);

            if (file == null)
            {
                return NotFound();
            }

            return File(file, "application/octet-stream", fileName);
        }


        [HttpDelete("Delete/{fileName}")]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            await _repository.DeleteFileAsync(fileName);
            return NoContent();
        }
    }
}
