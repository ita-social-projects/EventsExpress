using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.ViewModel
{
    public class PageViewModel
    {
        public int PageNumber { get; }
        public int TotalPages { get; }

        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        if(TotalPages != 0) { 
            if (PageNumber > TotalPages)
            {
                throw new ArgumentOutOfRangeException();
            }
          }
        }

        public bool HasPreviousPage => (PageNumber > 1);
        public bool HasNextPage => (PageNumber < TotalPages);
        
    }
}
