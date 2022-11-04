using Microsoft.AspNetCore.Mvc;
using Todo.Api.models;
using System.Net;
using static Todo.Api.models.weatherForecastModel;
using helper = Todo.Api.Helper.WebResponse;

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
            IEnumerable<response> data = _weatherForecast.GetData();
            if (data == null)
            {
                int code = StatusCodes.Status500InternalServerError;
                var msg = "Internal Server Error";
                rsp = helper.HttpResponseColumnRows<object>(code, msg, true, new List<response>());
                return StatusCode(code, rsp);
            }
            var d = 
            rsp = helper.HttpResponseColumnRows<object>(200, "success", false, data.ToList());
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
            response data = _weatherForecast.GetById(id);
            if (data == null)
            {
                int code = StatusCodes.Status500InternalServerError;
                var msg = "Internal Server Error";
                rsp = helper.HttpResponse(code, msg, true, null);
                return StatusCode(code, rsp);
            }
            rsp = helper.HttpResponse(200, "success", false, data);
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