using System;
using System.Collections.Generic;

namespace EventsExpress.ViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid UserId { get; set; }

        public Guid EventId { get; set; }

        public DateTime Date { get; set; }

        public string UserName { get; set; }

        public Guid? CommentsId { get; set; }

        public IEnumerable<CommentViewModel> Children { get; set; }
    }
}
