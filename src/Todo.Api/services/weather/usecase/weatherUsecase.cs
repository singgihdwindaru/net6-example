using Todo.Api.models;

namespace Todo.Api.services.weather.usecase;

public class weatherUsecase : IWeatherForecastUsecase
{
    private readonly IWeatherForecastMysqlRepo _weather;
    public weatherUsecase(IWeatherForecastMysqlRepo weather)
    {
        _weather = weather;
    }
    public weatherForecastModel.response GetById(long id)
    {
        weatherForecastModel.response result;
        try
        {
            var data = _weather.GetById(id);
            if (data == null)
            {
                return null;
            }
            result = new weatherForecastModel.response();
            result.Summary = data.Summary;
            result.Date = data.Date;
            result.TemperatureC = data.TemperatureC;
            result.TemperatureF = data.TemperatureF;
            return result;
        }
        catch (System.Exception e)
        {
            _ = e.Message;
            return null;
        }
    }

    public IEnumerable<weatherForecastModel.response> GetData()
    {
        List<weatherForecastModel.response> result = new List<weatherForecastModel.response>();
        try
        {
            var data = _weather.GetAll();
            if (data == null)
            {
                return null;
            }
            foreach (var item in data)
            {
                weatherForecastModel.response rsp = new weatherForecastModel.response();
                rsp.Summary = item.Summary;
                rsp.Date = item.Date;
                rsp.TemperatureC = item.TemperatureC;
                rsp.TemperatureF = item.TemperatureF;
                result.Add(rsp);
            }
            return result;
        }
        catch (System.Exception e)
        {
            _ = e.Message;
            return null;
        }
    }
}