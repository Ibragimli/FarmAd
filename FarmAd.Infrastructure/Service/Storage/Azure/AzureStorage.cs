using FarmAd.Application.Abstractions.Storage;
using FarmAd.Application.Abstractions.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FarmAd.Infrastructure.Service.Storage.Azure
{
    public class AzureStorage : Storage, IAzureStorage
    {
        //readonly BlobServiceClient _blobServiceClient;
        //BlobContainerClient _blobContainerClient;
        //public AzureStorage(IConfiguration configuration)
        //{
        //    _blobServiceClient = new(configuration["Storage:Azure"]);
        //}
        //public async Task DeleteAsync(string containerName, string fileName)
        //{
        //    _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        //    BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
        //    await blobClient.DeleteAsync();
        //}

        //public List<string> GetFiles(string containerName)
        //{
        //    _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        //    return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
        //}

        //public bool HasFile(string containerName, string fileName)
        //{
        //    _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        //    return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
        //}

        //public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
        //{
        //    _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        //    await _blobContainerClient.CreateIfNotExistsAsync();
        //    await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        //    List<(string fileName, string pathOrContainerName)> datas = new();
        //    foreach (IFormFile file in files)
        //    {

        //        string filename = file.FileName;
        //        filename = filename.Length <= 64 ? (filename) : (filename.Substring(filename.Length - 64, 64));
        //        filename = Guid.NewGuid().ToString() + filename;


        //        BlobClient blobClient = _blobContainerClient.GetBlobClient(filename);
        //        await blobClient.UploadAsync(file.OpenReadStream());
        //        datas.Add((filename, $"{containerName}/{filename}"));
        //    }
        //    return datas;
        public Task DeleteAsync(string pathOrContainerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFiles(string pathOrContainerName)
        {
            throw new NotImplementedException();
        }

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            throw new NotImplementedException();
        }

        public (string fileName, string path) UploadAsync(string pathOrContainerName, IFormFile file)
        {
            throw new NotImplementedException();
        }

        List<(string fileName, string path)> IStorage.UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            throw new NotImplementedException();
        }
    }

}
