using AutoMapper;
using EventsExpress.Core.DTOs;
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
    public class CommentService : ICommentService
    {
        public IUnitOfWork Db { get; set; }
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork uow, IMapper mapper)
        {
            Db = uow;
            _mapper = mapper;
        }

        public IEnumerable<CommentDTO> GetCommentByEventId(Guid id, int page, int pageSize, out int count)
        {
            var comments = Db.CommentsRepository
                .Get("User.Photo,Children")
                .Where(x => x.CommentsId == null)
                .Where(x => x.EventId == id)
                .Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            count = Db.CommentsRepository.Get()
                .Where(x => x.CommentsId == null)
                .Where(x => x.EventId == id).Count();
            var com = _mapper.Map<IEnumerable<CommentDTO>>(comments);
            foreach (var c in com)
            {
                c.Children = _mapper.Map<IEnumerable<CommentDTO>>(c.Children);
            }
            return com;
        }

        public async Task<OperationResult> Create(CommentDTO comment)
        {
            if (string.IsNullOrEmpty(comment.Text))
            {
                return new OperationResult(false, "Incorrect text!", "");
            }

            if (Db.UserRepository.Get(comment.UserId) == null)
            {
                return new OperationResult(false, "Current user does not exist!", "");
            }

            if (Db.EventRepository.Get(comment.EventId) == null)
            {
                return new OperationResult(false, "Wrong event id!", "");
            }
            


            Db.CommentsRepository.Insert(new Comments() { Text = comment.Text,
                                                          Date = DateTime.Now,
                                                          UserId = comment.UserId,
                                                          EventId = comment.EventId,
                                                          CommentsId = comment.CommentsId
            });

            await Db.SaveAsync();

            return new OperationResult(true, "", "");
        }

        public async Task<OperationResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new OperationResult(false, "Id field is null", "");
            }

            var comment = Db.CommentsRepository.Get("Children").Where(x => x.Id == id).FirstOrDefault();
            if (comment == null)
            {
                return new OperationResult(false, "Not found", "");
            }
            if (comment.Children != null)
            {
                foreach (var com in comment.Children)
                {
                    var res = Db.CommentsRepository.Delete(Db.CommentsRepository.Get(com.Id));
                }
            }
            var result = Db.CommentsRepository.Delete(comment);
            await Db.SaveAsync();
            return new OperationResult(true);
        }
    }
}
