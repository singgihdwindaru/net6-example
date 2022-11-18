
using System.Data;
using Moq;
using Todo.Api.services.weather.repository.mysql;
using FluentAssertions;

using static Todo.Api.models.weatherForecastModel;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace Todo.Api.Tests.Services.Weather;

public class mysqlWeatherTest
{
    readonly mysqlWeatherForecast _repository;
    readonly Mock<IDbConnection> _moqConnection;

    public mysqlWeatherTest()
    {
        this._moqConnection = new Mock<IDbConnection>(MockBehavior.Strict);
        _moqConnection.Setup(x => x.Open());
        _moqConnection.Setup(x => x.Dispose());
        _repository = new mysqlWeatherForecast(_moqConnection.Object);
    }
    [Fact]
    public void TestGetAll()
    {
        DateTime now = DateTime.Now;

        // Define the data reader, that return only one record.
        var moqDataReader = new Mock<IDataReader>();
        moqDataReader.SetupSequence(x => x.Read())
            .Returns(true) // First call return a record: true
            .Returns(false); // Second call finish

        moqDataReader.SetupGet<object>(x => x["id"]).Returns(1);
        moqDataReader.SetupGet<object>(x => x["summary"]).Returns("Summary");
        moqDataReader.SetupGet<object>(x => x["temperatureC"]).Returns(32);
        moqDataReader.SetupGet<object>(x => x["temperatureF"]).Returns(64);
        moqDataReader.SetupGet<object>(x => x["date"]).Returns(now);

        // Define the command to be mock and use the data reader
        var commandMock = new Mock<IDbCommand>();
        commandMock.Setup(m => m.ExecuteReader()).Returns(moqDataReader.Object);

        // Now the mock if IDbConnection configure the command to be used
        _moqConnection.Setup(m => m.CreateCommand()).Returns(commandMock.Object);

        // And we are ready to do the call.
        (Exception? error, IEnumerable<dto>? data) actual = _repository.GetAll();

        IEnumerable<dto> expectedResult = new List<dto>(){
            new dto {
                id=1,
                Summary="Summary",
                TemperatureC =32,
                TemperatureF=64,
                Date=now
            }
        };
        expectedResult.Should().BeEquivalentTo(actual.data?.ToList());
        // Assert.Single(result);
        // commandMock.Verify(x => x.Parameters.Add(It.IsAny<IDbDataParameter>()), Times.Exactly(1));
    }


    [Fact]
    public void TestGetById()
    {
        DateTime now = DateTime.Now;

        // Define the data reader, that return only one record.
        var moqDataReader = new Mock<IDataReader>(MockBehavior.Strict);
        moqDataReader.SetupSequence(x => x.Read())
            .Returns(true) // First call return a record: true
            .Returns(false); // Second call finish
        moqDataReader.Setup(c => c.Dispose());

        moqDataReader.SetupGet<object>(x => x["id"]).Returns(1);
        moqDataReader.SetupGet<object>(x => x["summary"]).Returns("Summary");
        moqDataReader.SetupGet<object>(x => x["temperatureC"]).Returns(32);
        moqDataReader.SetupGet<object>(x => x["temperatureF"]).Returns(64);
        moqDataReader.SetupGet<object>(x => x["date"]).Returns(now);

        // Define the command to be mock and use the data reader
        var commandMock = new Mock<IDbCommand>(MockBehavior.Strict);
        _moqConnection.Setup(m => m.CreateCommand()).Returns(commandMock.Object);
        // commandMock.Setup(c => c.Dispose());

        /* SetupSet lets you indicate an expectation that the property will be set */
        // commandMock.SetupSet(m => m.CommandText = mysqlWeatherForecast.getById);

        /* SetupProperty a way to stub a property on the mock */
        commandMock.SetupProperty<string>(c => c.CommandText);
        commandMock.Setup(m => m.Parameters.Clear());

        var dataParameter = new Mock<IDbDataParameter>(MockBehavior.Strict);
        commandMock.Setup(m => m.CreateParameter()).Returns(dataParameter.Object);
        dataParameter.SetupSet(p => p.ParameterName = "@id");
        dataParameter.SetupSet(p => p.Value = (long)1);
        commandMock.Setup(m => m.Parameters.Add(dataParameter.Object)).Returns(0);

        commandMock.Setup(m => m.ExecuteReader()).Returns(moqDataReader.Object);

        // And we are ready to do the call.
        (Exception? error, dto? result) actual = _repository.GetById(1);
        dto expectedResult = new()
        {

            id = 1,
            Summary = "Summary",
            TemperatureC = 32,
            TemperatureF = 64,
            Date = now

        };
        expectedResult.Should().BeEquivalentTo(actual.result);
        // commandMock.Verify();
        // commandMock.Verify(x => x.Parameters.Add(It.IsAny<IDbDataParameter>()), Times.Exactly(1));
    }
}