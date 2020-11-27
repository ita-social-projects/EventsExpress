using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.IServices
{
    public interface IEventOwnersService
    {
        Task<OperationResult> DeleteOwnerFromEvent(Guid userId, Guid eventId);

        Task<OperationResult> PromoteToOwner(Guid userId, Guid eventId);
    }
}
