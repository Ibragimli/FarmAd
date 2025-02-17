using Microsoft.AspNetCore.Http;

namespace FarmAd.Application.Abstractions.Storage
{
    public interface IStorage
    {
        List<(string fileName, string path)> UploadAsync(string pathOrContainerName, IFormFileCollection files);
        (string fileName, string path) UploadAsync(string pathOrContainerName, IFormFile file);
        Task DeleteAsync(string pathOrContainerName, string fileName);
        List<string> GetFiles(string pathOrContainerName);
        bool HasFile(string pathOrContainerName, string fileName);
    }

}
