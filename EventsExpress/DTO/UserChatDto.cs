using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.DTO
{
    public class UserChatDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<UserPreviewDto> Users { get; set; }
                                                        
    }
}
