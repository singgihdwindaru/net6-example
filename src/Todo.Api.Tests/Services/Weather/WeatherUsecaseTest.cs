using Moq;
using Todo.Api.models;
using FluentAssertions;
using static Todo.Api.models.weatherForecastModel;
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
    private static TestTable[] tcGetById
    {
        get
        {
            var now = DateTime.Now;
            return new TestTable[]
            {
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
                        (Exception? error, dto? res) result = (null, data);
                        _mockWeatherForecastMysqlRepo.Setup(x => x.GetById(1)).Returns(result);
                    },
                    ExpectedResult = new weatherForecastModel.response
                    {
                        Date = now,
                        TemperatureC = 2,
                        TemperatureF = 5,
                        Summary = "summary"
                    },
                },
                new TestTable
                {
                    TestName = "#2 Error",
                    Args = 2,
                    WantError = true,
                    Mock = () =>
                    {
                        (Exception? error, dto? res) result = (new Exception("some error"), null);
                        _mockWeatherForecastMysqlRepo.Setup(x => x.GetById(2)).Returns(result);
                    },
                    ExpectedResult = null,
                }
            };
        }
    }
    public static TheoryData<TestTableBuilder> tdGetById()
    {
        return TestTable.BuildTestTable(tcGetById);
    }
    [Theory]
    [MemberData(nameof(tdGetById))]
    public void TestGetById(TestTableBuilder Case)
    {
        TestTable test = Case._testTable;
        tcGetById[Case.Index].Mock.Invoke();

        var usecase = new weatherUsecase(_mockWeatherForecastMysqlRepo.Object);
        int? id = test.Args as int?;
        var actualResult = usecase.GetById(id.HasValue ? id.Value : -1);

        bool isError = actualResult.error == null ? false : true;
        test.WantError.Should().Be(isError);
        test.ExpectedResult?.Should().BeEquivalentTo(actualResult.result);
    }
    #endregion End Of TestGetById

    #region TestGetData
    public static TestTable[] tcGetAll
    {
        get
        {
            var now = DateTime.Now;
            return new TestTable[]
            {
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
                        (Exception? ex, List<dto>? data) result = (null, data);
                        _mockWeatherForecastMysqlRepo.Setup(x => x.GetAll()).Returns(result);
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
                },

                new TestTable
                {
                    TestName = "#2 Error",
                    WantError = true,
                    Mock = () =>
                    {
                        (Exception? ex, List<dto>? data) result = (new Exception("some error"), null);
                        _mockWeatherForecastMysqlRepo.Setup(x => x.GetAll()).Returns(result);
                    },
                    ExpectedResult = null,
                }
            };
        }
    }
    public static TheoryData<TestTableBuilder> tdGetAll()
    {
        return TestTable.BuildTestTable(tcGetAll);
    }
    [Theory]
    [MemberData(nameof(tdGetAll))]
    public void TestGetAll(TestTableBuilder Case)
    {
        TestTable test = Case._testTable;
        tcGetAll[Case.Index].Mock.Invoke();

        var usecase = new weatherUsecase(_mockWeatherForecastMysqlRepo.Object);
        var actualResult = usecase.GetAll();

        bool isError = actualResult.error == null ? false : true;
        test.WantError.Should().Be(isError);
        test.ExpectedResult?.Should().BeEquivalentTo(actualResult.result);
    }

    #endregion End Of TestGetData

}
