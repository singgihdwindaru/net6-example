using Moq;
using Todo.Api.controllers;
using Todo.Api.models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Text.Json;

namespace Todo.Api.Tests.test_controllers;

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
        var now = DateTime.Now;
        string response = JsonSerializer.Serialize(
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
        weatherForecastModel.response expectedResult = new weatherForecastModel.response
        {
            Date = now,
            TemperatureC = 2,
            TemperatureF = 5,
            Summary = "summary"
        };
        _mockWeatherForecastUsecase.Setup(x => x.GetById(42))
        .Returns(expectedResult);
        var controller = new WeatherForecastController(_mockWeatherForecastUsecase.Object);

        // Act
        IActionResult actionResult = controller.GetById(42);
        string actualResult = JsonSerializer.Serialize((actionResult as OkObjectResult).Value);

        response.Should().BeEquivalentTo(actualResult);

    }
}