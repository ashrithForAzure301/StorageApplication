using StorageCRUD.Models;

namespace StorageCRUD.StorageInterface
{
    public interface QueueInterface
    {

        Task AddMessageAsync(QueueModel message);
        Task<QueueModel> DequeueMessageAsync();
        Task UpdateMessageAsync(QueueModel message);
        Task ClearQueueAsync();
    }
}
