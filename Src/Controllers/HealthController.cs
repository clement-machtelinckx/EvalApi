using Microsoft.AspNetCore.Mvc;

namespace EvalApi.Src.Controllers;

[ApiController]
[Route("api/health")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Content("OK", "text/plain");
    }
}
