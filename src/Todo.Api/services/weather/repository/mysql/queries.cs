namespace Todo.Api.services.weather.repository.mysql;

public partial class mysqlWeatherForecast {
    static string selectAll = @"
    SELECT id, summary, temperatureC, temperatureF, date 
    FROM forecast ";

    public static string getById = selectAll + @"
    WHERE id=@id";
}