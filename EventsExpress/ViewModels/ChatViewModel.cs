using System.Collections.Generic;

namespace EventsExpress.ViewModels
{
    public class ChatViewModel : UserChatViewModel
    {
        public IEnumerable<MessageViewModel> Messages { get; set; }
    }
}
