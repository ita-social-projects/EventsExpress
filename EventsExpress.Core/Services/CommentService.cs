using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{
    public class CommentService : ICommentService
    {
        public IUnitOfWork Db { get; set; }
        private readonly IMapper _mapper;


        public CommentService(IUnitOfWork uow, IMapper mapper, IUserService userService)
        {
            Db = uow;
            _mapper = mapper;
        }

        public IEnumerable<CommentDTO> GetCommentByEventId(Guid id, int page, int pageSize, out int count)
        {
            var comments = Db.CommentsRepository.Get("User.Photo")
                .Where(x => x.EventId == id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            count = Db.CommentsRepository.Get().Count(x => x.EventId == id);
            return _mapper.Map<IEnumerable<CommentDTO>>(comments);
        }


        public async Task<OperationResult> Create(CommentDTO comment)
        {
            if (Db.UserRepository.Get(comment.UserId) == null)
            {
                return new OperationResult(false, "Current user does not exist!", "");
            }

            if (Db.EventRepository.Get(comment.EventId) == null)
            {
                return new OperationResult(false, "Wrong event id!", "");
            }
            
            Db.CommentsRepository.Insert(new Comments
            {
                Text = comment.Text,
                Date = DateTime.Now,
                UserId = comment.UserId,
                EventId = comment.EventId,
            });

            await Db.SaveAsync();

            return new OperationResult(true);
        }


        public async Task<OperationResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new OperationResult(false, "Id field is null", "");
            }

            var comment = Db.CommentsRepository.Get(id);
            if (comment == null)
            {
                return new OperationResult(false, "Not found", "");
            }

            Db.CommentsRepository.Delete(comment);
            await Db.SaveAsync();
            return new OperationResult(true);
        }
    }
}
