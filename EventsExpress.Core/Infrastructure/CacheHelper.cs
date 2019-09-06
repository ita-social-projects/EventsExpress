using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Caching;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.Infrastructure
{
    public class CacheHelper:ICacheHelper
    {
         public CacheDTO GetValue(Guid UserId)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(UserId.ToString()) as CacheDTO;
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

        public void Delete(Guid UserId)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(UserId.ToString()))
            {
                memoryCache.Remove(UserId.ToString());
            }
        }
    }
}
