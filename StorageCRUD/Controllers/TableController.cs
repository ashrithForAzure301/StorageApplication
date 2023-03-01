using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageCRUD.Models;
using StorageCRUD.StorageInterface;

namespace StorageCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly TableInterface _repository;

        public TableController(TableInterface repository)
        {
            _repository = repository;
        }

        [HttpPost("Insert")]
        public async Task Insert(TableModel entity)
        {
            await _repository.Insert(entity);
        }

        [HttpGet("All")]
        public async Task<IEnumerable<TableModel>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet("{partitionKey}/{rowKey}")]
        public async Task<TableModel> Get(string partitionKey, string rowKey)
        {
            return await _repository.Get(partitionKey, rowKey);
        }

        [HttpPut("Update")]
        public async Task Update(TableModel entity)
        {
            await _repository.Update(entity);
        }

        [HttpDelete("{partitionKey}/{rowKey}")]
        public async Task Delete(string partitionKey, string rowKey)
        {
            await _repository.Delete(partitionKey, rowKey);
        }
    }
}
