using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
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

        public IEnumerable<UserEventInventoryDTO> GetAllMarksByItemId(Guid itemId)
        {
            return _mapper.Map<IEnumerable<UserEventInventoryDTO>>(
                _context.UserEventInventories
                    .Include(i => i.UserEvent.User)
                    .Where(i => i.InventoryId == itemId));
        }

        public async Task<OperationResult> MarkItemAsTakenByUser(UserEventInventoryDTO userEventInventoryDTO)
        {
            try
            {
                var res = _context.UserEventInventories.Add(_mapper.Map<UserEventInventoryDTO, UserEventInventory>(userEventInventoryDTO));
                await _context.SaveChangesAsync();
                return new OperationResult(true, "Good", string.Empty);
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, string.Empty);
            }
        }

        
    }
}
