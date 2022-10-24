using System.Collections.Generic;

namespace Todo.Api.Models;

public class weatherForecastModel
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}
public interface IWeatherForecastNoSqlRepo
{   
    IEnumerable<weatherForecastModel> GetData();
}