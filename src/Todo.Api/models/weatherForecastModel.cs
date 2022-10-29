namespace Todo.Api.models;

public class weatherForecastModel
{
    public class dto : response
    {
        public long id { get; }
    }
    public class response
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
    public class request {}
}
public interface IWeatherForecastNoSqlRepo
{
    IEnumerable<weatherForecastModel.dto> GetData();
}