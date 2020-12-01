using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{

    public class EventOwnersService : IEventOwnersService
    {
        private readonly IUnitOfWork _db;

        public EventOwnersService(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        public async Task<OperationResult> PromoteToOwner(Guid userId, Guid eventId)
        {
            if (!_db.EventOwnersRepository.Get().Any(x => x.EventId == eventId && x.UserId == userId))
            {
                _db.EventOwnersRepository.Insert(new EventOwner { EventId = eventId, UserId = userId });

                _db.UserEventRepository.Delete(new UserEvent { UserId = userId, EventId = eventId });

                await _db.SaveAsync();
            }

            return new OperationResult(true);
        }

        public async Task<OperationResult> DeleteOwnerFromEvent(Guid userId, Guid eventId)
        {
            _db.EventOwnersRepository.Delete(new EventOwner { UserId = userId, EventId = eventId });
            await _db.SaveAsync();
            return new OperationResult(true);
        }
    }
}
