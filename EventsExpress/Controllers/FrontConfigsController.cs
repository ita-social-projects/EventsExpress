namespace EventsExpress.Controllers
{
    using EventsExpress.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [Route("api/[controller]")]
    [ApiController]
    public class FrontConfigsController : Controller
    {
        private readonly IOptions<FrontConfigsViewModel> _frontConfigs;

        public FrontConfigsController(IOptions<FrontConfigsViewModel> frontConfigs)
        {
            _frontConfigs = frontConfigs;
        }

        public IActionResult GetConfigs()
        {
            FrontConfigsViewModel configResult = _frontConfigs.Value;
            return Ok(configResult);
        }
    }
}
