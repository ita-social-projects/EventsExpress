using EventsExpress.Db.EF;

namespace EventsExpress.Db.Entities
{
    public class Photo : BaseEntity
    {
        public byte[] Thumb { get; set; }

        public byte[] Img { get; set; }
    }
}
