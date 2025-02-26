using FarmAd.Application.Abstractions.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Infrastructure.Service
{
    public class RedisCacheServices : IRedisCacheServices
    {
        private readonly IDatabase _database;

        public RedisCacheServices(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase(); // Burada IDatabase başlatılıyor!
        }

        public async Task<bool> SetValueAsync(string key, string value)
        {
            return await _database.StringSetAsync(key, value, TimeSpan.FromMinutes(6));
        }

        public async Task<string> GetValueAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task ClearAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task ClearAllAsync()
        {
            var endpoints = _database.Multiplexer.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = _database.Multiplexer.GetServer(endpoint);
                await server.FlushAllDatabasesAsync();
            }
        }
    }

}
