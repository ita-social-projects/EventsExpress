using System;
using System.Collections.Generic;

namespace EventsExpress.DTO
{
    public class UserChatDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string LastMessage { get; set; }

        public DateTime LastMessageTime { get; set; }

        public IEnumerable<UserPreviewDto> Users { get; set; }
    }
}
