using StorageCRUD.Models;
using System.Reflection.Metadata;

namespace StorageCRUD.StorageInterface
{
    public interface BlobInterface
    {
        Task<BlobModel> AddFileAsync(Stream stream, string blobfileName);
        Task<IEnumerable<BlobModel>> GetFileAsync();
        Task<BlobModel> GetFileAsync(string blobfileName);
        Task DeleteFileAsync(string blobfileName);
    }
}
