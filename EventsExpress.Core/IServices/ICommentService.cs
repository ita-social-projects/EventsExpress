using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;

namespace EventsExpress.Core.IServices
{
    public interface ICommentService
    {
        IEnumerable<CommentDTO> GetCommentByEventId(Guid id, int page, int pageSize, out int count);

        Task<OperationResult> Delete(Guid id);

        Task<OperationResult> Create(CommentDTO comment);
    }
}
