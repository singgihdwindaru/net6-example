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
    public (Exception? error, response? result) GetById(long id)
    {
        (Exception? error, response? data) result = (null, null);
        try
        {
            var data = _weather.GetById(id);
            if (data.error != null)
            {
                throw data.error;
            }
            if (data.result == null)
            {
                return result;
            }

            result.data = new weatherForecastModel.response();
            result.data.Summary = data.result.Summary;
            result.data.Date = data.result.Date;
            result.data.TemperatureC = data.result.TemperatureC;
            result.data.TemperatureF = data.result.TemperatureF;
            return result;
        }
        catch (System.Exception e)
        {
            result.error = e;
            return result;
        }
    }

    public (Exception? error, IEnumerable<response>? result) GetAll()
    {
        (Exception? error, IEnumerable<response>? data) result = (null, null);
        try
        {
            var data = _weather.GetAll();
            if (data.error != null)
            {
                throw data.error;
            }
            if (data.result == null)
            {
                return result;
            }
            
            List<response> weatherResponse = new List<response>();
            foreach (var item in data.result)
            {
                response rsp = new response();
                rsp.Summary = item.Summary;
                rsp.Date = item.Date;
                rsp.TemperatureC = item.TemperatureC;
                rsp.TemperatureF = item.TemperatureF;
                weatherResponse.Add(rsp);
            }
            result.data = weatherResponse;
            return result;
        }
        catch (System.Exception e)
        {
            result.error = e;
            return result;
        }
    }
}