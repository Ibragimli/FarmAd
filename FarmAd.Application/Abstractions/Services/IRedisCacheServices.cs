using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services
{
    public interface IRedisCacheServices
    {
        Task ClearAsync(string key);
        Task<string> GetValueAsync(string key);
        Task<bool> SetValueAsync(string key, string value);
        Task ClearAllAsync();
        //Task DeleteAsync(string key);
    }
}
