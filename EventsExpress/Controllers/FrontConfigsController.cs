namespace EventsExpress.Controllers
{
    using EventsExpress.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    public class FrontConfigsController : Controller
    {
        private readonly IOptions<FrontConfigsViewModel> _frontConfigs;

        public FrontConfigsController(IOptions<FrontConfigsViewModel> frontConfigs)
        {
            _frontConfigs = frontConfigs;
        }

        public IActionResult GetConfigs()
        {
            _ = new FrontConfigsViewModel();
            FrontConfigsViewModel configResult = _frontConfigs.Value;
            return Ok(configResult);
        }
    }
}
