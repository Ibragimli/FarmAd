using FarmAd.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;

namespace FarmAd.Infrastructure.Service.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }
        public string StorageName { get => _storage.GetType().Name; }

        public List<string> GetFiles(string pathOrContainerName)
             => _storage.GetFiles(pathOrContainerName);

        public bool HasFile(string pathOrContainerName, string fileName)
             => _storage.HasFile(pathOrContainerName, fileName);

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
             => await _storage.DeleteAsync(pathOrContainerName, fileName);
        public List<(string fileName, string path)> UploadAsync(string pathOrContainerName, IFormFileCollection files)
            => _storage.UploadAsync(pathOrContainerName, files);
        public (string fileName, string path) UploadAsync(string pathOrContainerName, IFormFile file)
            => _storage.UploadAsync(pathOrContainerName, file);


    }
}
