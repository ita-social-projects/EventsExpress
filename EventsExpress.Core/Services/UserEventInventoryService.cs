using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{
    public class UserEventInventoryService : BaseService<UserEventInventory>, IUserEventInventoryService
    {
        public UserEventInventoryService(
            AppDbContext context,
            IMapper mapper)
            : base(context, mapper)
        {
        }

        public IEnumerable<UserEventInventoryDTO> GetAllMarkItemsByEventId(Guid eventId)
        {
            return _mapper.Map<IEnumerable<UserEventInventoryDTO>>(
                _context.UserEventInventories
                    .Include(i => i.UserEvent.User)
                    .Where(i => i.EventId == eventId));
        }

        public async Task MarkItemAsTakenByUser(UserEventInventoryDTO userEventInventoryDTO)
        {
            if (!_context.Events.Any(e => e.Id == userEventInventoryDTO.EventId))
            {
                throw new EventsExpressException("Event not found!");
            }

            if (!_context.Users.Any(e => e.Id == userEventInventoryDTO.UserId))
            {
                throw new EventsExpressException("User not found!");
            }

            if (!_context.UserEvent.Any(ue => ue.UserId == userEventInventoryDTO.UserId))
            {
                throw new EventsExpressException("Don't have permision");
            }

            if (!_context.Inventories.Any(e => e.Id == userEventInventoryDTO.InventoryId))
            {
                throw new EventsExpressException("Inventory not found!");
            }

            _context.UserEventInventories.Add(_mapper.Map<UserEventInventoryDTO, UserEventInventory>(userEventInventoryDTO));
            await _context.SaveChangesAsync();
        }
    }
}
