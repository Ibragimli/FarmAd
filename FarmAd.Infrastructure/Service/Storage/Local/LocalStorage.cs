using FarmAd.Application.Abstractions.Helpers;
using FarmAd.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FarmAd.Infrastructure.Service.Storage.Local
{
    public class LocalStorage : Storage, ILocalStorage
    {
        private readonly IFileManager _fileManager;

        public LocalStorage(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        public bool HasFile(string path, string fileName)
                => File.Exists($"{path}\\{fileName}");

        public async Task DeleteAsync(string path, string fileName)
           => _fileManager.Delete(path, fileName);


        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public List<(string fileName, string path)> Upload(string pathOrContainer, IFormFileCollection files)
        {
            List<(string fileName, string path)> datas = new();

            foreach (var file in files)
            {
                var (filename, path) = _fileManager.Save(pathOrContainer, file);
                datas.Add((filename, path));
            }
            return datas;
        }

        public (string fileName, string path) Upload(string pathOrContainer, IFormFile file)
        {
            return _fileManager.Save(pathOrContainer, file);
        }

        //public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainer, IFormFileCollection files)
        //{
        //    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");

        //    if (!Directory.Exists(uploadPath))
        //        Directory.CreateDirectory(uploadPath);

        //    Random r = new();

        //    foreach (IFormFile file in files)
        //    {
        //        string fullPath = Path.Combine(uploadPath, $"{r.Next()}{Path.GetExtension(file.FileName)}");
        //        using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
        //        await file.CopyToAsync(fileStream);
        //        await fileStream.FlushAsync();
        //    }
        //    return null;
        //}

    }
}
