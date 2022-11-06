using Moq;
using Todo.Api.controllers;
using Todo.Api.models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Text.Json;
using static Todo.Api.models.weatherForecastModel;

namespace Todo.Api.Tests.Controller;

public class WeatherForecastControllerTest
{
    private readonly Mock<IWeatherForecastUsecase> _mockWeatherForecastUsecase;
    public WeatherForecastControllerTest()
    {
        _mockWeatherForecastUsecase = new Mock<IWeatherForecastUsecase>();
    }
    [Fact]
    public void TestGetById()
    {
        // TODO : Dirapihin lagi codenya !!

        var now = DateTime.Now;
        string expectedResult = JsonSerializer.Serialize(
            new
            {
                code = 200,
                message = "success",
                error = false,
                data = new
                {
                    Date = now,
                    TemperatureC = 2,
                    TemperatureF = 5,
                    Summary = "summary"
                }
            }
        );
        response data = new response
        {
            Date = now,
            TemperatureC = 2,
            TemperatureF = 5,
            Summary = "summary"
        };
        _mockWeatherForecastUsecase.Setup(x => x.GetById(42)).Returns(data);
        var controller = new WeatherForecastController(_mockWeatherForecastUsecase.Object);

        // Act
        IActionResult actionResult = controller.GetById(42);
        string actualResult = JsonSerializer.Serialize((actionResult as OkObjectResult).Value);

        expectedResult.Should().BeEquivalentTo(actualResult);
    }
    [Fact]
    public void TestGetData()
    {
        // TODO : Dirapihin lagi codenya !!
        var now = DateTime.Now;
        var columns = new object[] { "Date", "TemperatureC", "TemperatureF", "Summary" };
        var rows = new List<object>(){
            new object[]{now,2,5,"summary"},
            new object[]{now,4,7,"summary2"},
        };
        string expectedResult = JsonSerializer.Serialize(
            new
            {
                code = 200,
                message = "success",
                error = false,
                data = new
                {
                    columns = columns,
                    rows = rows,
                }
            }
        );
        IEnumerable<response> data = new List<response>(){
            new response
            {
                Date = now,
                TemperatureC = 2,
                TemperatureF = 5,
                Summary = "summary"
            },
            new response
            {
                Date = now,
                TemperatureC = 4,
                TemperatureF = 7,
                Summary = "summary2"
            }
        };
        _mockWeatherForecastUsecase.Setup(x => x.GetData()).Returns(data.ToList());

        // Act
        var controller = new WeatherForecastController(_mockWeatherForecastUsecase.Object);
        IActionResult actionResult = controller.Get();
        string actualResult = JsonSerializer.Serialize((actionResult as OkObjectResult).Value);

        expectedResult.Should().BeEquivalentTo(actualResult);

    }
}