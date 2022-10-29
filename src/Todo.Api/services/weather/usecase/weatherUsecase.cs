using Todo.Api.models;

namespace Todo.Api.services.weather.usecase;

public class weatherUsecase : IWeatherForecastUsecase
{
    public weatherUsecase()
    {

    }

    public IEnumerable<weatherForecastModel.response> GetData(weatherForecastModel.request request)
    {
        throw new NotImplementedException();
    }
}