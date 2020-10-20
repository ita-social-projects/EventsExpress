using System;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;

namespace EventsExpress.Core.IServices
{
    public interface IEventStatusHistoryService
    {
        Task<OperationResult> CancelEvent(Guid eventId, string reason);
    }
}
