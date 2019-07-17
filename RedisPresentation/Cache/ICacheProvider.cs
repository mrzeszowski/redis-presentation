using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedisPresentation.Cache
{
    public interface ICacheProvider
    {
        Task<T> GetAsync<T>(string key);

        Task SetAsync<T>(string key, T value);

        Task<bool> ExistsAsync(string key);
    }
}
