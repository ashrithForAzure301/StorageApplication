namespace StorageCRUD.StorageInterface
{
    public interface FileShareInterface
    {
        Task<bool> UploadFile(IFormFile file);
        Task<byte[]> DownloadFile(string fileName);
        Task DeleteFileAsync(string fileName);
    }
}
