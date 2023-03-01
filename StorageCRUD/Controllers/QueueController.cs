using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageCRUD.Models;
using StorageCRUD.StorageInterface;

namespace StorageCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly QueueInterface _queueRepository;

        public QueueController(QueueInterface queueRepository)
        {
            this._queueRepository = queueRepository;
        }

        [HttpPost("CreateMessage")]
        public async Task AddMessage(QueueModel message)
        {
            await _queueRepository.AddMessageAsync(message);
        }


        [HttpGet("DequeueMessage")]
        public async Task<QueueModel> DequeueMessage()
        {
            return await _queueRepository.DequeueMessageAsync();

        }

        [HttpPut("UpdateMessage")]
        public async Task UpdateMessage(QueueModel message)
        {
            await _queueRepository.UpdateMessageAsync(message);
        }

        [HttpDelete("ClearQueue")]
        public async Task ClearQueue()
        {
            await _queueRepository.ClearQueueAsync();
        }
    }
}
