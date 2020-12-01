using System;
using System.Collections.Generic;

namespace EventsExpress.ViewModels
{
    public class UserChatViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string LastMessage { get; set; }

        public DateTime LastMessageTime { get; set; }

        public IEnumerable<UserPreviewViewModel> Users { get; set; }
    }
}
