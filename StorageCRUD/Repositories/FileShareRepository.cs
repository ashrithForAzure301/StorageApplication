using StorageCRUD.StorageInterface;
using Microsoft.Azure.Storage.File;
using Azure.Storage.Files.Shares;
using Azure;

namespace StorageCRUD.Repositories
{
    public class FileShareRepository : FileShareInterface
    {
        private readonly ShareClient shareClient;

        public FileShareRepository(IConfiguration configuration)
        {
            string connectionString = configuration["Storage:ConnectionString"];
            string fileShare = configuration.GetValue<string>("Storage:FileShare");
            shareClient = new ShareClient(connectionString, fileShare);
        }

        public async Task<bool> UploadFile(IFormFile file)
        {
            var shareDirectoryClient = shareClient.GetDirectoryClient("");
            var shareFileClient = shareDirectoryClient.GetFileClient(file.FileName);

            using (var stream = file.OpenReadStream())
            {
                shareFileClient.Create(stream.Length);
                await shareFileClient.UploadRangeAsync(new HttpRange(0, file.Length), stream);
            }
            return true;
        }


        public async Task<byte[]> DownloadFile(string fileName)
        {
            var shareDirectoryClient = shareClient.GetDirectoryClient("");
            var shareFileClient = shareDirectoryClient.GetFileClient(fileName);

            var response = await shareFileClient.DownloadAsync();
            using var memoryStream = new MemoryStream();
            await response.Value.Content.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }


        public async Task DeleteFileAsync(string fileName)
        {
            var directoryClient = shareClient.GetDirectoryClient("");
            var fileClient = directoryClient.GetFileClient(fileName);

            await fileClient.DeleteAsync();
        }

    }
}
