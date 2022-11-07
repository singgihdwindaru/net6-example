using Moq;
using Todo.Api.controllers;
using Todo.Api.models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Text.Json;
using static Todo.Api.models.weatherForecastModel;
using Newtonsoft.Json;
using Todo.Api.services.weather.usecase;

namespace Todo.Api.Tests.Services.Weather;

public class WeatherUsecaseTest
{
    private static Mock<IWeatherForecastMysqlRepo> _mockWeatherForecastMysqlRepo;

    static WeatherUsecaseTest()
    {
        _mockWeatherForecastMysqlRepo = new Mock<IWeatherForecastMysqlRepo>();
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
                    dto data = new dto
                    {
                        id = 1,
                        Date = now,
                        TemperatureC = 2,
                        TemperatureF = 5,
                        Summary = "summary"
                    };
                    _mockWeatherForecastMysqlRepo.Setup(x => x.GetById(1)).Returns(data);
                },
                ExpectedResult = new weatherForecastModel.response
                {
                    Date = now,
                    TemperatureC = 2,
                    TemperatureF = 5,
                    Summary = "summary"
                },
            });

        tp.Add(
           new TestTable
           {
               TestName = "#2 Error",
               Args = 2,
               WantError = true,
               Mock = () =>
               {
                   dto? data = null;
                   _mockWeatherForecastMysqlRepo.Setup(x => x.GetById(2)).Returns(data);
               },
               ExpectedResult = null,
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
        TestTable test = Case.Build();
        test.Mock();

        var usecase = new weatherUsecase(_mockWeatherForecastMysqlRepo.Object);
        int? id = test.Args as int?;
        weatherForecastModel.response? actualResult = usecase.GetById(id.HasValue ? id.Value : -1);
        test.ExpectedResult?.Should().BeEquivalentTo(actualResult);
    }
    #endregion End Of TestGetById

    #region TestGetData
    public static TheoryData<TestTableBuilder> TestGetDataCases()
    {
        List<TestTable> tp = new List<TestTable>();
        var now = DateTime.Now;

        tp.Add(
            new TestTable
            {
                TestName = "#1 success",
                WantError = false,
                Mock = () =>
                {
                    List<dto> data = new List<dto>() {
                        new dto {
                            id = 1,
                            Date = now,
                            TemperatureC = 2,
                            TemperatureF = 5,
                            Summary = "summary"
                        }
                    };
                    _mockWeatherForecastMysqlRepo.Setup(x => x.GetAll()).Returns(data);
                },
                ExpectedResult = new List<response>() {
                    new response
                    {
                        Date = now,
                        TemperatureC = 2,
                        TemperatureF = 5,
                        Summary = "summary"
                    }
                },
            });

        tp.Add(
           new TestTable
           {
               TestName = "#2 Error",
               WantError = true,
               Mock = () =>
               {
                   List<dto>? data = null;
                   _mockWeatherForecastMysqlRepo.Setup(x => x.GetAll()).Returns(data);
               },
               ExpectedResult = null,
           });
        var data = new TheoryData<TestTableBuilder>();
        foreach (var item in tp)
        {
            data.Add(new TestTableBuilder(item));
        }
        return data;
    }

    [Theory]
    [MemberData(nameof(TestGetDataCases))]
    public void TestGetData(TestTableBuilder Case)
    {
        TestTable test = Case.Build();
        test.Mock();

        var usecase = new weatherUsecase(_mockWeatherForecastMysqlRepo.Object);
        IEnumerable<response>? actualResult = usecase.GetData();
        test.ExpectedResult?.Should().BeEquivalentTo((!test.WantError ? actualResult?.ToList() : actualResult));
    }

    #endregion End Of TestGetData

}
