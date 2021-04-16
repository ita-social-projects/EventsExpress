using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class CommentService : BaseService<Comments>, ICommentService
    {
        public CommentService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public IEnumerable<CommentDto> GetCommentByEventId(Guid id, int page, int pageSize, out int count)
        {
            count = Context.Comments.Count(x => x.EventId == id && x.CommentsId == null);

            var comments = Context.Comments
                .Include(c => c.Children)
                    .ThenInclude(c => c.User)
                .Include(c => c.User)
                .Where(x => x.EventId == id && x.CommentsId == null)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsEnumerable();

            var com = Mapper.Map<IEnumerable<CommentDto>>(comments);
            foreach (var c in com)
            {
                c.Children = Mapper.Map<IEnumerable<CommentDto>>(c.Children);
                foreach (var child in c.Children)
                {
                    child.User = c.User;
                }
            }

            return com;
        }

        public async Task Create(CommentDto comment)
        {
            if (Context.Users.Find(comment.UserId) == null)
            {
                throw new EventsExpressException("Current user does not exist!");
            }

            if (Context.Events.Find(comment.EventId) == null)
            {
                throw new EventsExpressException("Wrong event id!");
            }

            Insert(new Comments()
            {
                Text = comment.Text,
                Date = DateTime.Now,
                UserId = comment.UserId,
                EventId = comment.EventId,
                CommentsId = comment.CommentsId,
            });

            await Context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new EventsExpressException("Id field is null");
            }

            var comment = Context.Comments
                .Include(c => c.Children)
                .FirstOrDefault(x => x.Id == id);

            if (comment == null)
            {
                throw new EventsExpressException("Not found");
            }

            if (comment.Children != null)
            {
                foreach (var com in comment.Children)
                {
                    Delete(Context.Comments.Find(com.Id));
                }
            }

            Delete(comment);
            await Context.SaveChangesAsync();
        }
    }
}
