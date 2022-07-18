using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication2.Services
{
     
    public static class DistributedCacheExtensions //: IDistributedCache// static
    {

        // IDistributedCache cache;


        //public DistributedCacheExtensions_() : base()
        //{
        //    // Do additional work here otherwise you can leave it empty
        //}
        //public  async Task SetRecordAsync<T>(// this IDistributedCache cache, //static
        //    string recordId,
        //    T data,
        //    TimeSpan? absoluteExpireTime = null,
        //    TimeSpan? unusedExpireTime = null)
        //{
        //    var options = new DistributedCacheEntryOptions();

        //    options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
        //    options.SlidingExpiration = unusedExpireTime;
        //    var jsonData = JsonSerializer.Serialize(data);
        //   // await cache.SetStringAsync(recordId, jsonData, options);

        //}

        //public  async Task<T> GetRecordAsync<T>(//this IDistributedCache cache , 
        //     string recordId)
        //{
        //    var jsonData = await Task.Run(() => { return 5; }
        //    ) ;
        //        //cache.GetStringAsync(recordId); //await DistributedCacheExtensions.GetStringAsync(this, recordId);
        //    //

        //    //if (jsonData is null)
        //    //{
        //    //    return default(T);
        //    //}

        //    //return JsonSerializer.Deserialize<T>(jsonData);
        //    return  default(T);
        //}

        //====================================================

        public static async Task SetRecordAsync<T>(this IDistributedCache cache, //static
          string recordId,
          T data,
          TimeSpan? absoluteExpireTime = null,
          TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = unusedExpireTime;
            var jsonData = JsonConvert.SerializeObject(data); // JsonSerializer.Serialize
            await cache.SetStringAsync(recordId, jsonData, options);
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache,
             string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId); //await DistributedCacheExtensions.GetStringAsync(this, recordId);
            

            if (jsonData is null)
            {
                return default(T);
            }
            var r = JsonConvert.DeserializeObject<T>(jsonData);

            //T r = JObject.Parse(jsonData); // JsonSerializer.Deserialize<T>(jsonData);
            return r;// default(T);

            //return  default(T);
        }







        //==============================================


        //public byte[] Get(string key)
        //{
        //   // throw new NotImplementedException();
        //   return  Encoding.ASCII.GetBytes("author");
        //}



        //public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        //{
        //    throw new NotImplementedException();
        //}


        //public void Refresh(string key)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task RefreshAsync(string key, CancellationToken token = default)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Remove(string key)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task RemoveAsync(string key, CancellationToken token = default)
        //{
        //    throw new NotImplementedException();
        //}

        //public  Task<byte[]> GetAsync(string key, CancellationToken token = default)
        //{
        //    // byte[] vs = Encoding.ASCII.GetBytes(
        //    // return 

        //    var b = DistributedCacheExtensions.GetStringAsync(this, key);

        //    var a =  Task.Run(() =>
        //    {
        //        return Encoding.ASCII.GetBytes(DistributedCacheExtensions.GetString(this, key));
        //    });

        //    return a;

        //      //vs;
        //}

        //public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
        //{
        //    return  DistributedCacheExtensions.SetAsync( this , key, value, token);
        //}


    }
}