using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface ICacheHelper
    {
        CacheDto GetValue(string key);

        bool Add(CacheDto value);

        void Update(CacheDto value);

        void Delete(string key);
    }
}
