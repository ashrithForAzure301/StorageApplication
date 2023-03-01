using StorageCRUD.StorageInterface;
using System.Reflection.Metadata;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;
using StorageCRUD.Models;

namespace StorageCRUD.Repositories
{
    public class BlobRepository : BlobInterface
    {
        private readonly CloudBlobContainer _container;

        public BlobRepository(IConfiguration configuration)
        {
            var connectionString = configuration["Storage:ConnectionString"];
            var containerName = configuration.GetValue<string>("Storage:ContainerName");
            var blobClient = CloudStorageAccount.Parse(connectionString).CreateCloudBlobClient();
            _container = blobClient.GetContainerReference(containerName);
        }

        public async Task<IEnumerable<BlobModel>> GetFileAsync()
        {
            var blobs = await _container.ListBlobsSegmentedAsync(null);
            return blobs.Results.Select(b => new BlobModel { BlobName = b.Uri.Segments.Last(), BlobUrl = b.Uri.AbsoluteUri });
        }

        public async Task<BlobModel> GetFileAsync(string blobfileName)
        {
            var blob = _container.GetBlockBlobReference(blobfileName);
            if (await blob.ExistsAsync())
            {
                return new BlobModel
                {
                    BlobName = blobfileName,
                    BlobUrl = blob.Uri.AbsoluteUri
                };
            }
            return null;
        }

        public async Task<BlobModel> AddFileAsync(Stream stream, string blobfileName)
        {
            var blob = _container.GetBlockBlobReference(blobfileName);
            await blob.UploadFromStreamAsync(stream);
            return new BlobModel
            {
                BlobName = blobfileName,
                BlobUrl = blob.Uri.AbsoluteUri
            };
        }

        public async Task DeleteFileAsync(string blobfileName)
        {
            var blob = _container.GetBlockBlobReference(blobfileName);
            await blob.DeleteAsync();
        }
    }
}
