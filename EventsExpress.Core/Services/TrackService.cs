namespace EventsExpress.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using EventsExpress.Core.IServices;
    using EventsExpress.Db.BaseService;
    using EventsExpress.Db.EF;
    using EventsExpress.Db.Entities;
    using Microsoft.EntityFrameworkCore;

    public class TrackService : BaseService<ChangeInfo>, ITrackService
    {
        public TrackService(AppDbContext context, IMapper mapper)
           : base(context, mapper)
        {
        }

        public async Task<ChangeInfo> GetChangeInfoByScheduleIdAsync(Guid id)
        {
            return await Context.ChangeInfos
              .FromSqlRaw(@"SELECT * FROM ChangeInfos WHERE EntityName = 'EventSchedule' AND JSON_VALUE(EntityKeys, '$.Id') = '" + id + "' AND ChangesType = 2").FirstOrDefaultAsync();
        }
    }
}
