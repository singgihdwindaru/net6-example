using Todo.Api.models;

namespace Todo.Api.Repositories.NoSql;

public class weatherForcastNoSqlRepo : IWeatherForecastNoSqlRepo
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