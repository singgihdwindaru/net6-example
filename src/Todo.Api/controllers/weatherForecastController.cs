using Microsoft.AspNetCore.Mvc;
using Todo.Api.models;

namespace Todo.Api.controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastUsecase _weatherForecast;
    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastUsecase weatherForecast)
    {
        _logger = logger;
        _weatherForecast = weatherForecast;
    }

    [HttpGet("~/WeatherForecast")]
    public IActionResult Get()
    {
        return Ok(_weatherForecast.GetData());
    }
    [HttpGet("~/WeatherForecast/{id}")]
    public IActionResult GetById(long id)
    {
        return Ok(_weatherForecast.GetById(id));
    }
}
