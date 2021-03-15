using System.ComponentModel.DataAnnotations.Schema;

namespace EventsExpress.Db.Entities
{
    [NotMapped]
    public class Photo : BaseEntity
    {
        public byte[] Thumb { get; set; }

        public byte[] Img { get; set; }
    }
}
