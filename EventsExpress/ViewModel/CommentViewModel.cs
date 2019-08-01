using EventsExpress.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.ViewModel
{
    public class CommentViewModel
    {
        public IEnumerable<CommentDto> Comments { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
