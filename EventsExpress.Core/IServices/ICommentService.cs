using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface ICommentService
    {
        IEnumerable<CommentDTO> GetCommentByEventId(Guid id, int page, int pageSize, out int count);

        Task Delete(Guid id);

        Task Create(CommentDTO comment);
    }
}
