using EventsExpress.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.DTO
{
    public class ChatDto : UserChatDto
    {                          
        public IEnumerable<MessageDto> Messages { get; set; }       
    }
}
