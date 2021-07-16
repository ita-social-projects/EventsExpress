using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels
{
    public class RegisterBindViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public AuthExternalType Type { get; set; }
    }
}
