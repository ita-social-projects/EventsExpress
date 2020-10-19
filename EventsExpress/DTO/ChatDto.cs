using System.Collections.Generic;

namespace EventsExpress.DTO
{
    public class ChatDto : UserChatDto
    {
        public IEnumerable<MessageDto> Messages { get; set; }
    }
}
