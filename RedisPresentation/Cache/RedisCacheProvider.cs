using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace RedisPresentation.Cache
{
    public class RedisCacheProvider : ICacheProvider
    {
        private RedisCache _cache;

        public RedisCacheProvider(RedisCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await Task.Run(() =>
            {
                var db = _cache.Connection.GetDatabase();
                var json = db.StringGet(key);

                return JsonConvert.DeserializeObject<T>(json);
            });
        }

        public async Task SetAsync<T>(string key, T value)
        {
            await Task.Run(() =>
            {
                var db = _cache.Connection.GetDatabase();
                var json = db.StringSet(key, JsonConvert.SerializeObject(value));
            });
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await Task.Run(() => 
            {
                var db = _cache.Connection.GetDatabase();
                return db.KeyExists(key);
            });
        }
    }
}
