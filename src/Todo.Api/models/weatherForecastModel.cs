namespace Todo.Api.models;

public class weatherForecastModel
{
    public interface IWeatherForecastNoSqlRepo
    {
        IEnumerable<weatherForecastModel.dto>? GetData();
    }
    public interface IWeatherForecastMysqlRepo
    {
        (Exception? error, IEnumerable<dto>? result) GetAll();
        (Exception? error, dto? result) GetById(long id);
        Exception Insert(IEnumerable<dto>? data);
        Exception Update(IEnumerable<dto>? data);
        Exception Delete(long id);
    }
    public interface IWeatherForecastUsecase
    {
        (Exception? error, IEnumerable<response>? result) GetAll();
        (Exception? error, response? result) GetById(long id);
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