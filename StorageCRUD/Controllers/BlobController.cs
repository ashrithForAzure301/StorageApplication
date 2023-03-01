using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageCRUD.Models;
using StorageCRUD.StorageInterface;
using System.Reflection.Metadata;

namespace StorageCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly BlobInterface _repository;

        public BlobController(BlobInterface repository)
        {
            _repository = repository;
        }

        [HttpGet("Retrive")]
        public async Task<IActionResult> GetAsync()
        {
            var blobs = await _repository.GetFileAsync();
            return Ok(blobs);
        }

        [HttpGet("Retrive/{blobfileName}")]
        public async Task<ActionResult<BlobModel>> GetAsync(string blobfileName)
        {
            var blob = await _repository.GetFileAsync(blobfileName);
            if (blob == null)
            {
                return NotFound();
            }
            return blob;
        }

        [HttpPost("Upload")]
        public async Task<ActionResult<BlobModel>> AddAsync(IFormFile file, string blobfileName)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest();
            }
            var blob = await _repository.AddFileAsync(file.OpenReadStream(), blobfileName);
            return blob;
        }

        [HttpDelete("Delete/{blobfileName}")]
        public async Task<IActionResult> DeleteAsync(string blobfileName)
        {
            await _repository.DeleteFileAsync(blobfileName);
            return NoContent();
        }
    }
}
