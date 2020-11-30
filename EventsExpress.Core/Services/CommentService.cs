using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork uow, IMapper mapper)
        {
            Db = uow;
            _mapper = mapper;
        }

        public IUnitOfWork Db { get; set; }

        public IEnumerable<CommentDTO> GetCommentByEventId(Guid id, int page, int pageSize, out int count)
        {
            count = Db.CommentsRepository.Get().Count(x => x.EventId == id && x.CommentsId == null);

            var comments = Db.CommentsRepository
                .Get("User.Photo,Children")
                .Where(x => x.EventId == id && x.CommentsId == null)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsEnumerable();

            var com = _mapper.Map<IEnumerable<CommentDTO>>(comments);
            foreach (var c in com)
            {
                c.Children = _mapper.Map<IEnumerable<CommentDTO>>(c.Children);
                foreach (var child in c.Children)
                {
                    child.User = Db.UserRepository.Get("Photo").FirstOrDefault(u => u.Id == child.UserId);
                }
            }

            return com;
        }

        public async Task Create(CommentDTO comment)
        {
            if (Db.UserRepository.Get(comment.UserId) == null)
            {
                throw new EventsExpressException("Current user does not exist!");
            }

            if (Db.EventRepository.Get(comment.EventId) == null)
            {
                throw new EventsExpressException("Wrong event id!");
            }

            Db.CommentsRepository.Insert(new Comments()
            {
                Text = comment.Text,
                Date = DateTime.Now,
                UserId = comment.UserId,
                EventId = comment.EventId,
                CommentsId = comment.CommentsId,
            });

            await Db.SaveAsync();
        }

        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new EventsExpressException("Id field is null");
            }

            var comment = Db.CommentsRepository.Get("Children").Where(x => x.Id == id).FirstOrDefault();
            if (comment == null)
            {
                throw new EventsExpressException("Not found");
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
        }
    }
}
