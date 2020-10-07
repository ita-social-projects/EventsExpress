using System;

namespace EventsExpress.Db.Entities
{
  public  class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public string ReplacedByToken { get; set; }
        public string CreatedByIp { get; set; }
        public string RevokedByIp { get; set; }
    }
}
