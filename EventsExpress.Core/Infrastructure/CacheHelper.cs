using System;
using System.Runtime.Caching;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.Infrastructure
{
    public class CacheHelper : ICacheHelper
    {
        public CacheDTO GetValue(Guid userId)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(userId.ToString()) as CacheDTO;
        }

        public bool Add(CacheDTO value)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(value.UserId.ToString(), value, DateTime.Now.AddDays(10));
        }

        public void Update(CacheDTO value)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set(value.UserId.ToString(), value, DateTime.Now.AddDays(10));
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
