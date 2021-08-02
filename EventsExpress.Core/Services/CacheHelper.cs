using System;
using System.Runtime.Caching;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;

namespace EventsExpress.Core.Services
{
    public class CacheHelper : ICacheHelper
    {
        private readonly MemoryCache _memoryCache;

        public CacheHelper()
        {
            _memoryCache = MemoryCache.Default;
        }

        public CacheDto GetValue(string key)
        {
            return _memoryCache.Get(key) as CacheDto;
        }

        public bool Add(CacheDto value)
        {
            return _memoryCache.Add(value.Key, value, DateTime.Now.AddDays(10));
        }

        public void Update(CacheDto value)
        {
            _memoryCache.Set(value.Key, value, DateTime.Now.AddDays(10));
        }

        public void Delete(string key)
        {
            if (_memoryCache.Contains(key))
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
