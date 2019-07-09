using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.Models
{
    public class EventModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile EventPhoto { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Required]
        public DateTime DateFrom { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateTo { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public List<string> SelectedCategories { get; set; }
        public List<Category> Categories { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public IFormFile OwnerAvatar { get; set; }    
    }
}
