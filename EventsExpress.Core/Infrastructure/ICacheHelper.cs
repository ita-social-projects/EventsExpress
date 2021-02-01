using System;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.Infrastructure
{
    public interface ICacheHelper
    {
        CacheDto GetValue(Guid userId);

        bool Add(CacheDto value);

        void Update(CacheDto value);

        void Delete(Guid userId);
    }
}
