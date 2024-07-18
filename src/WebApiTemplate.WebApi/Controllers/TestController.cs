using Microsoft.AspNetCore.Mvc;

namespace WebApiTemplate.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Ahoy! This is a log message from the TestController.");
        return Ok("Hello World!");
    }
    
    [HttpPost]
    public IActionResult Post()
    {
        return Ok("Hello World!");
    }
}