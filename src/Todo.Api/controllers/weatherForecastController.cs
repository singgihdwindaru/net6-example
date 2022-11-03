using Microsoft.AspNetCore.Mvc;
using Todo.Api.models;
using Todo.Api.common;
using System.Net;

namespace Todo.Api.controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    // private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastUsecase _weatherForecast;
    public WeatherForecastController(IWeatherForecastUsecase weatherForecast)
    {
        // _logger = logger;
        _weatherForecast = weatherForecast;
    }

    [HttpGet("~/WeatherForecast")]
    public IActionResult Get()
    {
        httpResponse.Root<object> rsp;
        try
        {
            IEnumerable<weatherForecastModel.response> data = _weatherForecast.GetData();
            if (data == null)
            {
                int code = StatusCodes.Status500InternalServerError;
                var msg = "Internal Server Error";
                rsp = common.WebResponse.HttpResponseColumnRows<object>(code, msg, true, new List<weatherForecastModel.response>());
                return StatusCode(code, rsp);
            }
            rsp = common.WebResponse.HttpResponseColumnRows<object>(200, "success", false, data.ToArray());
        }
        catch (System.Exception e)
        {
            // TODO : create a nice error handling
            return StatusCode((int)HttpStatusCode.InternalServerError, e);
        }

        return Ok(rsp);
    }
    [HttpGet("~/WeatherForecast/{id}")]
    public IActionResult GetById(long id)
    {
        httpResponse.Root<object> rsp;
        try
        {
            weatherForecastModel.response data = _weatherForecast.GetById(id);
            if (data == null)
            {
                int code = StatusCodes.Status500InternalServerError;
                var msg = "Internal Server Error";
                rsp = common.WebResponse.HttpResponse(code, msg, true, null);
                return StatusCode(code, rsp);
            }
            rsp = common.WebResponse.HttpResponse(200, "success", false, data);
        }
        catch (System.Exception e)
        {
            // TODO : create a nice error handling
            return StatusCode((int)HttpStatusCode.InternalServerError, e);
        }

        return Ok(rsp);
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