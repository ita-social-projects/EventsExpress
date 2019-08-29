using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.DTO
{
    public class MessageDto
    {
        public Guid Id { get; set; }

        public Guid ChatRoomId { get; set; }

        public Guid SenderId { get; set; }

        public DateTime DateCreated { get; set; }

        public string Text { get; set; }

        public bool Edited { get; set; } = false;

        public bool Seen { get; set; } = false;
    }
}
