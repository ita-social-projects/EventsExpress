using System;
using System.Runtime.Caching;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.Infrastructure
{
    public class CacheHelper : ICacheHelper
    {
        public CacheDto GetValue(Guid userId)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(userId.ToString()) as CacheDto;
        }

        public bool Add(CacheDto value)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(value.AuthLocalId.ToString(), value, DateTime.Now.AddDays(10));
        }

        public void Update(CacheDto value)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set(value.AuthLocalId.ToString(), value, DateTime.Now.AddDays(10));
        }

        public void Delete(Guid userId)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(userId.ToString()))
            {
                memoryCache.Remove(userId.ToString());
            }
        }
    }
}
