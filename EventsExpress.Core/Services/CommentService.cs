using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class CommentService : BaseService<Comments>, ICommentService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context, IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<CommentDTO> GetCommentByEventId(Guid id, int page, int pageSize, out int count)
        {
            count = Get().Count(x => x.EventId == id && x.CommentsId == null);

            var comments = Get("User.Photo,Children")
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
                    child.User = _context.Users.Include("Photo").FirstOrDefault(u => u.Id == child.UserId);
                }
            }

            return com;
        }

        public async Task<OperationResult> Create(CommentDTO comment)
        {
            if (_context.Users.Find(comment.UserId) == null)
            {
                return new OperationResult(false, "Current user does not exist!", string.Empty);
            }

            if (_context.Events.Find(comment.EventId) == null)
            {
                return new OperationResult(false, "Wrong event id!", string.Empty);
            }

            Insert(new Comments()
            {
                Text = comment.Text,
                Date = DateTime.Now,
                UserId = comment.UserId,
                EventId = comment.EventId,
                CommentsId = comment.CommentsId,
            });

            await _context.SaveChangesAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new OperationResult(false, "Id field is null", string.Empty);
            }

            var comment = Get("Children").Where(x => x.Id == id).FirstOrDefault();
            if (comment == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            if (comment.Children != null)
            {
                foreach (var com in comment.Children)
                {
                    var res = Delete(Get(com.Id));
                }
            }

            var result = Delete(comment);
            await _context.SaveChangesAsync();
            return new OperationResult(true);
        }
    }
}
