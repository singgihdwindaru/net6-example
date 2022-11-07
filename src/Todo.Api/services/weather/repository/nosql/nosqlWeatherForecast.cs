using Todo.Api.models;
using static Todo.Api.models.weatherForecastModel;

namespace Todo.Api.services.weather.repository.nosql;

public class nosqlWeatherForecast : IWeatherForecastNoSqlRepo
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    public IEnumerable<weatherForecastModel.dto> GetData()
    {
        return Enumerable.Range(1, 5).Select(index => new weatherForecastModel.dto
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();
    }
}