using Microsoft.AspNetCore.Mvc;

namespace SampleApp.Controllers
{
    public class HealthCheckController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
