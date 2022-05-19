namespace EventsExpress.ViewModels
{
    using Microsoft.AspNetCore.Http;

    public class PhotoViewModelBase
    {
        public IFormFile Photo { get; set; }
    }
}
