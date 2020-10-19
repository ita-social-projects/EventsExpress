using System;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.Infrastructure
{
    public interface ICacheHelper
    {
        CacheDTO GetValue(Guid userId);

        bool Add(CacheDTO value);

        void Update(CacheDTO value);

        void Delete(Guid userId);
    }
}
