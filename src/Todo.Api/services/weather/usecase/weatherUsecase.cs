using Todo.Api.models;
using static Todo.Api.models.weatherForecastModel;

namespace Todo.Api.services.weather.usecase;

public class weatherUsecase : IWeatherForecastUsecase
{
    private readonly IWeatherForecastMysqlRepo _weather;
    public weatherUsecase(IWeatherForecastMysqlRepo weather)
    {
        _weather = weather;
    }
    public weatherForecastModel.response? GetById(long id)
    {
        weatherForecastModel.response result;
        try
        {
            var data = _weather.GetById(id);
            if (data.error != null)
            {
                throw data.error;
            }
            if (data.result == null)
            {
                return null;
            }
            
            result = new weatherForecastModel.response();
            result.Summary = data.result.Summary;
            result.Date = data.result.Date;
            result.TemperatureC = data.result.TemperatureC;
            result.TemperatureF = data.result.TemperatureF;
            return result;
        }
        catch (System.Exception e)
        {
            _ = e.Message;
            return null;
        }
    }

    public IEnumerable<weatherForecastModel.response>? GetData()
    {

        List<weatherForecastModel.response> result = new List<weatherForecastModel.response>();
        try
        {
            var data = _weather.GetAll();
            if (data.error != null)
            {
                throw data.error;
            }
            if (data.result == null)
            {
                return null;
            }

            foreach (var item in data.result)
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