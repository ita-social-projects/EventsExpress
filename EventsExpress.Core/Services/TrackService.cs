namespace EventsExpress.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
	using EventsExpress.Core.DTOs;
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
            var tracks = _mapper.Map<List<TrackDTO>>(
                _context.ChangeInfos
                    .Select(x => new TrackDTO
                    {
                        Id = x.Id,
                        Name = x.EntityName,
                        ChangesType = x.ChangesType,
                        EntityKeys = x.EntityKeys,
                        PropertyChangesText = x.PropertyChangesText,
                        Time = x.Time,
                        User = _context.Users.FirstOrDefault(c => c.Id == x.UserId),
                    }));

            tracks = model.EntityName.Any()
                ? tracks.Where(x => model.EntityName.Contains(x.Name)).ToList()
                : tracks;
            tracks = model.ChangesType.Any()
                ? tracks.Where(x => model.ChangesType.Contains(x.ChangesType)).ToList()
                : tracks;
            count = tracks.Count();

            var result = tracks.OrderBy(x => x.Time)
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToList();

            return _mapper.Map<IEnumerable<TrackDTO>>(result);
            return await Context.ChangeInfos
              .FromSqlRaw(@"SELECT * FROM ChangeInfos WHERE EntityName = 'EventSchedule' AND JSON_VALUE(EntityKeys, '$.Id') = '" + id + "' AND ChangesType = 2").FirstOrDefaultAsync();
        }
	
		public IEnumerable<TrackDTO> GetAllTracks(TrackFilterViewModel model, out int count)
        {
            var tracks = _mapper.Map<List<TrackDTO>>(
                _context.ChangeInfos
                    .Select(x => new TrackDTO
                    {
                        Id = x.Id,
                        Name = x.EntityName,
                        ChangesType = x.ChangesType,
                        EntityKeys = x.EntityKeys,
                        PropertyChangesText = x.PropertyChangesText,
                        Time = x.Time,
                        User = _context.Users.FirstOrDefault(c => c.Id == x.UserId),
                    }));

            tracks = model.EntityName.Any()
                ? tracks.Where(x => model.EntityName.Contains(x.Name)).ToList()
                : tracks;
            tracks = model.ChangesType.Any()
                ? tracks.Where(x => model.ChangesType.Contains(x.ChangesType)).ToList()
                : tracks;
            count = tracks.Count();

            var result = tracks.OrderBy(x => x.Time)
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToList();

            return _mapper.Map<IEnumerable<TrackDTO>>(result);
        }

        public IEnumerable<TrackDTO> GetEntityNames()
        {
            var entityNames = _mapper.Map<List<TrackDTO>>(
                _context.ChangeInfos
                    .Select(x => new TrackDTO
                    {
                        Name = x.EntityName,
                    }).Distinct());

            return _mapper.Map<IEnumerable<TrackDTO>>(entityNames);
        }
    }
}
