using System;
using System.Threading;
using StackExchange.Redis;

namespace RedisPresentation.Cache
{
    public sealed class RedisCache
    {
        // todo get from configuration
        private string _connectionString = "localhost:6379";

        private readonly Lazy<ConnectionMultiplexer> _lazy;

        public RedisCache()
        {
            _lazy = new Lazy<ConnectionMultiplexer>(
                () => ConnectionMultiplexer.Connect(_connectionString), LazyThreadSafetyMode.PublicationOnly);
        }

        public IConnectionMultiplexer Connection { get { return _lazy.Value; } }
    }
}
