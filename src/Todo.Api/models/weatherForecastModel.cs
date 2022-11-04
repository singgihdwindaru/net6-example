namespace Todo.Api.models;

public class weatherForecastModel
{
    public interface IWeatherForecastNoSqlRepo
    {
        IEnumerable<weatherForecastModel.dto> GetData();
    }
    public interface IWeatherForecastMysqlRepo
    {
        IEnumerable<weatherForecastModel.dto> GetAll();
        weatherForecastModel.dto GetById(long id);
    }
    public interface IWeatherForecastUsecase
    {
        IEnumerable<weatherForecastModel.response> GetData();
        weatherForecastModel.response GetById(long id);
    }
    
    public class dto
    {
        public long id { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        // public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public int TemperatureF { get; set; }

        public string? Summary { get; set; }
    }

    public class response
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        // public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public int TemperatureF { get; set; }

        public string? Summary { get; set; }
    }
    public class request { }
}