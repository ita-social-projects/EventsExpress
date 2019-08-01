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
        private readonly IUserService _userService;


        public CommentService(IUnitOfWork uow, IMapper mapper, IUserService userService)
        {
            Db = uow;
            _mapper = mapper;
            _userService = userService;
        }

        public IEnumerable<CommentDTO> GetCommentByEventId(Guid id, int page, int pageSize ,out int count)
        {
            IQueryable<Comments> comments = Db.CommentsRepository.Filter(filter: x => x.EventId == id, includeProperties: "User.Photo").Skip((page - 1) * pageSize).Take(pageSize);/*Get().AsQueryable().Where(x => x.EventId == id))*/
            count = Db.CommentsRepository.Filter(filter: x => x.EventId == id, includeProperties: "User.Photo").Count();
            return _mapper.Map<IEnumerable<CommentDTO>>(comments);
          
        }

        public async Task<OperationResult> Create(CommentDTO comment)
        {
            if (comment.Text == null)
            {
                return new OperationResult(false, "Incorrect text!", "");
            }

            if (comment.UserId == null)
            {
                return new OperationResult(false, "Current user does not exist!", "");
            }

            if (comment.EventId == null)
            {
                return new OperationResult(false, "Wrong event id!", "");
            }
            
            Db.CommentsRepository.Insert(new Comments() { Text = comment.Text,
                                                          Date = DateTime.Now,
                                                          UserId = comment.UserId,
                                                          EventId = comment.EventId,
            });

            await Db.SaveAsync();

            return new OperationResult(true, "", "");
        }

        public async Task<OperationResult> Edit(CommentDTO comment)
        {
            if (comment.Id == null)
            {
                return new OperationResult(false, "Id field is '0'", "");
            }

            Comments oldComment = Db.CommentsRepository.Get(comment.Id);
            if (oldComment == null)
            {
                return new OperationResult(false, "Not found", "");
            }

            oldComment.Text = comment.Text;
            oldComment.Date = DateTime.Now;

            await Db.SaveAsync();

            return new OperationResult(true, "", "");
        }

        public async Task<OperationResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new OperationResult(false, "Id field is null", "");
            }

            Comments comment = Db.CommentsRepository.Get(id);
            if (comment == null)
            {
                return new OperationResult(false, "Not found", "");
            }

            var result = Db.CommentsRepository.Delete(comment);
            if (result.Id != id)
                return new OperationResult(false, "", "");
            await Db.SaveAsync();
            return new OperationResult(true, "", "");
        }
    }
}
