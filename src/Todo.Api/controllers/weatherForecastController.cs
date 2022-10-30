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
        IEnumerable<weatherForecastModel.response> data = _weatherForecast.GetData();
        var l1 = new List<List<object>>();
        foreach (var item in data)
        {
            List<object> l2 = new List<object>();
            l2.Add(item.Date);
            l2.Add(item.TemperatureC);
            l2.Add(item.TemperatureF);
            l2.Add(item.Summary);
            l1.Add(l2);
        }

        httpResponse.Root rsp = new httpResponse.Root();
        rsp.code = 200;
        rsp.message = "success";
        rsp.error = false;
        rsp.data = new httpResponse.Data
        {
            // Todo get column name by reflection model or add attribute
            column = new List<string> {
                "date","temperatureC","temperatureF","summary"
            },
            values = l1
        };
        return Ok(rsp);
    }
    [HttpGet("~/WeatherForecast/{id}")]
    public IActionResult GetById(long id)
    {
        return Ok(_weatherForecast.GetById(id));
    }
}

/* example response
{
    "code": 400,
    "message": "success",
    "error": true,
    "data": {
      "column": [
        "date",
        "temperatureC",
        "temperatureF",
        "summary"
      ],
      "values": [
        [
          "0001-01-01T00:00:00",
          27,
          80,
          "Freezing"
        ],
        [
          "0001-01-01T00:00:00",
          27,
          80,
          "Freezing"
        ]
      ]
   }
}



*/