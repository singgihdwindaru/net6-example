using Moq;
using Todo.Api.controllers;
using Todo.Api.models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Text.Json;
using static Todo.Api.models.weatherForecastModel;
using Newtonsoft.Json;

namespace Todo.Api.Tests.Controller;

public class WeatherForecastControllerTest
{
    private static Mock<IWeatherForecastUsecase> _mockWeatherForecastUsecase;
    static WeatherForecastControllerTest()
    {
        _mockWeatherForecastUsecase = new Mock<IWeatherForecastUsecase>();
    }
    #region TestGetById
    public static TheoryData<TestTableBuilder> TestGetByIdCases()
    {
        List<TestTable> tp = new List<TestTable>();
        var now = DateTime.Now;

        tp.Add(
            new TestTable
            {
                TestName = "#1 success",
                Args = 1,
                WantError = false,
                Mock = () =>
                {
                    response data = new response
                    {
                        Date = now,
                        TemperatureC = 2,
                        TemperatureF = 5,
                        Summary = "summary"
                    };
                    (Exception? error, response? result) result = (null, data);
                    _mockWeatherForecastUsecase.Setup(x => x.GetById(1)).Returns(result);
                },
                ExpectedResult = JsonConvert.SerializeObject(
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
                    }),
            });

        tp.Add(
           new TestTable
           {
               TestName = "#2 Error",
               Args = 2,
               WantError = true,
               Mock = () =>
               {
                   (Exception? error, response? result) result = (new Exception("some error"), null);
                   _mockWeatherForecastUsecase.Setup(x => x.GetById(2)).Returns(result);
               },
               ExpectedResult = JsonConvert.SerializeObject(
                   new
                   {
                       code = 500,
                       message = "Internal Server Error",
                       error = true,
                       data = (string?)null
                   }),
           });
        var data = new TheoryData<TestTableBuilder>();
        foreach (var item in tp)
        {
            data.Add(new TestTableBuilder(item));
        }
        return data;
    }

    [Theory]
    [MemberData(nameof(TestGetByIdCases))]
    public void TestGetById(TestTableBuilder Case)
    {
        TestTable testData = Case.Build();
        testData.Mock();

        var controller = new WeatherForecastController(_mockWeatherForecastUsecase.Object);
        // Act
        int? id = testData.Args as int?;
        IActionResult actionResult = controller.GetById(id.HasValue ? id.Value : -1);
        string actualResult = !testData.WantError ?
         JsonConvert.SerializeObject((actionResult as OkObjectResult)?.Value)
         : JsonConvert.SerializeObject((actionResult as ObjectResult)?.Value);

        testData.ExpectedResult?.Should().BeEquivalentTo(actualResult);
    }

    #endregion End of TestGetById

    #region  TestGet
    public static TheoryData<TestTableBuilder> TestGetCases()
    {
        List<TestTable> tp = new List<TestTable>();
        var now = DateTime.Now;

        tp.Add(
            new TestTable
            {
                TestName = "#1 success",
                Args = "",
                WantError = false,
                Mock = () =>
                {
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
                    (Exception? error, IEnumerable<response>? result) result = (null, data);
                    _mockWeatherForecastUsecase.Setup(x => x.GetAll()).Returns(result);
                },
                ExpectedResult = JsonConvert.SerializeObject(
                new
                {
                    code = 200,
                    message = "success",
                    error = false,
                    data = new
                    {
                        columns = new object[] { "Date", "TemperatureC", "TemperatureF", "Summary" },
                        rows = new List<object>(){
                            new object[]{now,2,5,"summary"},
                            new object[]{now,4,7,"summary2"},
                        },
                    }
                }),
            });

        tp.Add(
           new TestTable
           {
               TestName = "#2 Error",
               Args = "",
               WantError = true,
               Mock = () =>
               {
                   (Exception? error, IEnumerable<response>? result) result = (new Exception("some error"), null);
                   _mockWeatherForecastUsecase.Setup(x => x.GetAll()).Returns(result);
               },
               ExpectedResult = JsonConvert.SerializeObject(
                new
                {
                    code = 500,
                    message = "Internal Server Error",
                    error = true,
                    data = new
                    {
                        columns = new object[] { "Date", "TemperatureC", "TemperatureF", "Summary" },
                        rows = new object[] { },
                    }
                }),
           });
        var data = new TheoryData<TestTableBuilder>();
        foreach (var item in tp)
        {
            data.Add(new TestTableBuilder(item));
        }
        return data;
    }

    [Theory]
    [MemberData(nameof(TestGetCases))]
    public void TestGet(TestTableBuilder Case)
    {
        TestTable testData = Case.Build();
        testData.Mock();

        var controller = new WeatherForecastController(_mockWeatherForecastUsecase.Object);
        IActionResult actionResult = controller.Get();
        string actualResult = !testData.WantError ?
                 JsonConvert.SerializeObject((actionResult as OkObjectResult)?.Value)
                 : JsonConvert.SerializeObject((actionResult as ObjectResult)?.Value);

        testData.ExpectedResult?.Should().BeEquivalentTo(actualResult);

    }
    #endregion End of TestGet

}