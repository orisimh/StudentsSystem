using Microsoft.VisualStudio.RpcContracts;
using Microsoft.VisualStudio.RpcContracts.Caching;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication2.Services
{
    public class RedisCacheService : ICacheService
    {
        private IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

      


        public async Task<T> GetRecordAsync<T>(RedisKey key , string k) //(this IDistributedCache cache, string recordId)
        {

            var _db = _connectionMultiplexer.GetDatabase();
            var jsonData =  await _db.StringGetAsync(k);
           // var jsonData = await cache.GetStringAsync(recordId);

            if (jsonData.IsNull )
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }

        Task<bool> ICacheService.CheckExistsAsync(CacheItemKey key, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task ICacheService.DownloadContainerAsync(CacheContainerKey containerKey, IProgress<ProgressData> progress, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        ValueTask<string> ICacheService.GetRelativePathBaseAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task ICacheService.SetItemAsync(CacheItemKey key, PipeReader reader, bool shareable, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICacheService.TryGetItemAsync(CacheItemKey key, PipeWriter writer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
