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
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<UserEventInventoryDto>> GetAllMarkItemsByEventId(Guid eventId)
        {
            return Mapper.Map<IEnumerable<UserEventInventoryDto>>(
                 await Context.UserEventInventories
                    .Include(i => i.UserEvent.User)
                    .Where(i => i.EventId == eventId).ToListAsync());
        }

        public async Task MarkItemAsTakenByUser(UserEventInventoryDto userEventInventoryDTO)
        {
            if (!Context.Events.Any(e => e.Id == userEventInventoryDTO.EventId))
            {
                throw new EventsExpressException("Event not found!");
            }

            if (!Context.Users.Any(e => e.Id == userEventInventoryDTO.UserId))
            {
                throw new EventsExpressException("User not found!");
            }

            if (!Context.UserEvent.Any(ue => ue.UserId == userEventInventoryDTO.UserId))
            {
                throw new EventsExpressException("Don't have permision");
            }

            if (!Context.Inventories.Any(e => e.Id == userEventInventoryDTO.InventoryId))
            {
                throw new EventsExpressException("Inventory not found!");
            }

            Context.UserEventInventories.Add(Mapper.Map<UserEventInventoryDto, UserEventInventory>(userEventInventoryDTO));
            await Context.SaveChangesAsync();
        }

        public async Task Delete(UserEventInventoryDto userEventInventoryDTO)
        {
            if (!Context.UserEventInventories.Any(e => e.EventId == userEventInventoryDTO.EventId
                                                    && e.UserId == userEventInventoryDTO.UserId
                                                    && e.InventoryId == userEventInventoryDTO.InventoryId))
            {
                throw new EventsExpressException("Object not found!");
            }

            Context.Remove(Mapper.Map<UserEventInventoryDto, UserEventInventory>(userEventInventoryDTO));
            await Context.SaveChangesAsync();
        }

        public async Task Edit(UserEventInventoryDto userEventInventoryDTO)
        {
            var entity = Context.UserEventInventories.FirstOrDefault(e => e.EventId == userEventInventoryDTO.EventId
                                                                        && e.UserId == userEventInventoryDTO.UserId
                                                                        && e.InventoryId == userEventInventoryDTO.InventoryId);

            if (entity == null)
            {
                throw new EventsExpressException("Object not found");
            }

            entity.Quantity = userEventInventoryDTO.Quantity;
            await Context.SaveChangesAsync();
        }
    }
}
