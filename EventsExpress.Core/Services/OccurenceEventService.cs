using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using MediatR;

namespace EventsExpress.Core.Services
{
    public class OccurenceEventService : IOccurenceEventService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IMediator _mediator;

        public OccurenceEventService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator,
            IPhotoService photoSrv)
        {
            _db = unitOfWork;
            _mapper = mapper;
            _photoService = photoSrv;
            _mediator = mediator;
        }

        public async Task<OperationResult> Create(OccurenceEventDTO eventDTO)
        {
            var ev = _mapper.Map<OccurenceEventDTO, OccurenceEvent>(eventDTO);

            try
            {
                var result = _db.OccurenceEventRepository.Insert(ev);
                await _db.SaveAsync();

                return new OperationResult(true, "Create new OccurenceEvent", result.Id.ToString());
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, string.Empty);
            }
        }

        public Task<OperationResult> Delete(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Edit(OccurenceEventDTO e)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OccurenceEventDTO> Events(EventFilterViewModel model, out int count)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public OccurenceEventDTO OccurenceEventById(Guid eventId)
        {
            throw new NotImplementedException();
        }
    }
}
