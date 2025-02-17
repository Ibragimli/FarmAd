using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Helpers
{
    public interface IFileManager
    {
        public (string, string) Save(string folder, IFormFile file);
        public bool Delete(string folder, string filename);
      
    }
}
