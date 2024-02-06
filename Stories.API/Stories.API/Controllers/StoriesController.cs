using Microsoft.AspNetCore.Mvc;

namespace Stories.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        [HttpGet]
        public Task<IActionResult> GetAll()
        {
          return 
        }
    }
}
