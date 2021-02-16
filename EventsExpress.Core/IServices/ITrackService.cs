namespace EventsExpress.Core.IServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using EventsExpress.Db.Entities;

    public interface ITrackService
    {
        public Task<ChangeInfo> GetChangeInfoByScheduleIdAsync(Guid id);
    }
}
