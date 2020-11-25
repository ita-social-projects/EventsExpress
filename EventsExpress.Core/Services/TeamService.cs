using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.Core.Services
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public TeamService(
            IUnitOfWork uow,
            IPhotoService photoService,
            IMapper mapper)
        {
            _db = uow;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task<OperationResult> AddDevAsync(DeveloperDTO developer)
        {
            var devEntity = _mapper.Map<Developer>(developer);

            try
            {
                devEntity.Photo = await _photoService.AddPhoto(developer.Photo);

                _db.DeveloperRepository.Insert(devEntity);
                await _db.SaveAsync();
            }
            catch (ArgumentException ex)
            {
                return new OperationResult(false, ex.Message, nameof(developer.Photo));
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, string.Empty);
            }

            return new OperationResult(true);
        }

        public async Task<OperationResult> AddTeamAsync(TeamDTO team)
        {
            var teamEntity = _mapper.Map<Team>(team);
            teamEntity.TeamPhotos = new List<TeamPhoto>();

            try
            {
                foreach (var pic in team.Photos ?? new List<IFormFile>())
                {
                    var photo = await _photoService.AddPhoto(pic);

                    teamEntity.TeamPhotos.Add(new TeamPhoto
                    {
                        Team = teamEntity,
                        Photo = photo,
                    });
                }

                _db.TeamRepository.Insert(teamEntity);
                await _db.SaveAsync();
            }
            catch (Exception e)
            {
                return new OperationResult(false);
            }

            return new OperationResult(true);
        }

        public IEnumerable<Team> All() => _db.TeamRepository.Get("Developers.Photo,TeamPhotos.Photo").AsEnumerable();
    }
}
