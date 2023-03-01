using StorageCRUD.Models;

namespace StorageCRUD.StorageInterface
{
    public interface TableInterface
    {
        Task Insert(TableModel entity);
        Task<IEnumerable<TableModel>> GetAll();
        Task<TableModel> Get(string partitionKey, string rowKey);
        Task Update(TableModel entity);
        Task Delete(string partitionKey, string rowKey);
    }
}
