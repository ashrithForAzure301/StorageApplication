using StorageCRUD.StorageInterface;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Cosmos.Table;

namespace StorageCRUD.Repositories
{
    public class TableRepository : TableInterface
    {
        private readonly CloudTableClient _tableClient;
        private readonly CloudTable _table;


        public TableRepository(IConfiguration configuration)
        {
            var connectionString = configuration["Storage:ConnectionString"];
            var tableName = configuration.GetValue<string>("Storage:TableName");

            var storageAccount = Microsoft.Azure.Cosmos.Table.CloudStorageAccount.Parse(connectionString);
            _tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            _table = _tableClient.GetTableReference(tableName);
            _table.CreateIfNotExistsAsync().Wait();
        }

        public async Task<IEnumerable<Models.TableModel>> GetAll()
        {
            var query = new TableQuery<Models.TableModel>();
            var segment = await _table.ExecuteQuerySegmentedAsync(query, null);
            return segment.Results;
        }

        public async Task<Models.TableModel> Get(string partitionKey, string rowKey)
        {
            var operation = TableOperation.Retrieve<Models.TableModel>(partitionKey, rowKey);
            var result = await _table.ExecuteAsync(operation);
            return (Models.TableModel)result.Result;
        }

        public async Task Insert(Models.TableModel entity)
        {
            var operation = TableOperation.Insert(entity);
            await _table.ExecuteAsync(operation);
        }

        public async Task Update(Models.TableModel entity)
        {
            var operation = TableOperation.Replace(entity);
            await _table.ExecuteAsync(operation);
        }

        public async Task Delete(string partitionKey, string rowKey)
        {
            var operation = TableOperation.Retrieve<Models.TableModel>(partitionKey, rowKey);
            var result = await _table.ExecuteAsync(operation);
            var deleteEntity = (Models.TableModel)result.Result;

            if (deleteEntity != null)
            {
                operation = TableOperation.Delete(deleteEntity);
                await _table.ExecuteAsync(operation);
            }
        }
    }
}
