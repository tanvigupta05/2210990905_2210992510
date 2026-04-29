using LoggingObservabilityDemoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoggingObservabilityDemoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly TestService _service = new TestService();

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation("Request started for ID: {Id}", id);

            var result = _service.GetData(id);

            _logger.LogInformation("Request completed for ID: {Id}", id);

            return Ok(new { data = result });
        }
    }
}