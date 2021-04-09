using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface ITrackService
    {
        IEnumerable<TrackDto> GetAllTracks(TrackFilterViewModel model, out int count);

        public Task<ChangeInfo> GetChangeInfoByScheduleIdAsync(Guid id);

        IEnumerable<EntityNamesDto> GetDistinctNames();
    }
}
