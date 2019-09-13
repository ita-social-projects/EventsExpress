using EventsExpress.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.Infrastructure
{
    public interface ICacheHelper
    {
        CacheDTO GetValue(Guid UserId);
        bool Add(CacheDTO value);
        void Update(CacheDTO value);
        void Delete(Guid UserId);
    }
}
