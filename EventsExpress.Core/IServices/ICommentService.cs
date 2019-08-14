using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface ICommentService
    {
        IEnumerable<CommentDTO> GetCommentByEventId(Guid id, int page, int pageSize, out int count);
        Task<OperationResult> Delete(Guid id);
        Task<OperationResult> Create(CommentDTO comment);

    }
}
