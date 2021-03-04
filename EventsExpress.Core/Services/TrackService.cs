using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EventsExpress.Core.Services
{
    public class TrackService : BaseService<ChangeInfo>, ITrackService
    {
        public TrackService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
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
                        User = _context.Users.Where(c => c.Id == x.UserId).FirstOrDefault(),
                    }));

            tracks = model.EntityName.Any()
                ? tracks.Where(x => model.EntityName.Contains(x.Name)).ToList()
                : tracks;
            tracks = model.ChangesType.Any()
                ? tracks.Where(x => model.ChangesType.Contains(x.ChangesType)).ToList()
                : tracks;
            count = tracks.Count();
            return tracks;
        }
    }
}
