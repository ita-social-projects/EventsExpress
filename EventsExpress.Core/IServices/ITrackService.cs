<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Text;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EventsExpress.Core.IServices
{
    public interface ITrackService
    {
        IEnumerable<TrackDTO> GetAllTracks(TrackFilterViewModel model, out int count);
    }
}
=======
﻿namespace EventsExpress.Core.IServices
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
>>>>>>> development
