using FarmAd.Application.Abstractions.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Infrastructure.Service
{
    public class FileManager:IFileManager
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileManager(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public (string,string) Save(string folder, IFormFile file)
        {
            string rootPath = _webHostEnvironment.WebRootPath;
            string filename = file.FileName;
            filename = filename.Length <= 64 ? (filename) : (filename.Substring(filename.Length - 64, 64));

            filename = Guid.NewGuid().ToString() + filename;

            string pathExist = Path.Combine(rootPath, folder);
            if (!Directory.Exists(pathExist))
                Directory.CreateDirectory(pathExist);

            string path = Path.Combine(rootPath, folder, filename);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return (filename,path);

        }

        public bool Delete(string folder, string filename)
        {
            string rootPath = _webHostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, folder, filename);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
