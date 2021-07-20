using EventsExpress.Db.Enums;

namespace EventsExpress.Core.DTOs
{
    public class RegisterBindDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public AuthExternalType Type { get; set; }
    }
}
