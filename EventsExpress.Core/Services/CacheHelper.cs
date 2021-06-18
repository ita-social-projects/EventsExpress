using System;
using System.Runtime.Caching;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;

namespace EventsExpress.Core.Services
{
    public class CacheHelper : ICacheHelper
    {
        public CacheDto GetValue(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;

            return memoryCache.Get(key) as CacheDto;
        }

        public bool Add(CacheDto value)
        {
            MemoryCache memoryCache = MemoryCache.Default;

            return memoryCache.Add(value.Key, value, DateTime.Now.AddDays(10));
        }

        public void Update(CacheDto value)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set(value.Key, value, DateTime.Now.AddDays(10));
        }

        public void Delete(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;

            if (memoryCache.Contains(key))
            {
                memoryCache.Remove(key);
            }
        }
    }
}
