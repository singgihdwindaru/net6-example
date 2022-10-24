using Todo.Api.Models;

namespace Todo.Api.Repositories.NoSql;

public class weatherForcastRepo_NoSql : IWeatherForecastNoSqlRepo
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    public IEnumerable<weatherForecastModel> GetData()
    {
        return Enumerable.Range(1, 5).Select(index => new weatherForecastModel
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();
    }
}