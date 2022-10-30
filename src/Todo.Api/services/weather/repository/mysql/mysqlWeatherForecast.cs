using MySql.Data.MySqlClient;
using Todo.Api.models;

namespace Todo.Api.services.weather.repository.mysql;

public partial class mysqlWeatherForecast : IWeatherForecastMysqlRepo
{
    protected readonly string _mysqlConnString;
    public mysqlWeatherForecast(string connString)
    {
        _mysqlConnString = connString;
    }

    public IEnumerable<weatherForecastModel.dto> GetAll()
    {
        List<weatherForecastModel.dto> result = new List<weatherForecastModel.dto>();
        try
        {
            using (MySqlConnection conn = new MySqlConnection(_mysqlConnString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(selectAll, conn);
                fetch(result, cmd);
            }
        }
        catch (System.Exception e)
        {
            _ = e.Message;
            result = null;
        }


        return result;
    }

    public weatherForecastModel.dto GetById(long id)
    {
        List<weatherForecastModel.dto> result = new List<weatherForecastModel.dto>();
        using (MySqlConnection conn = new MySqlConnection(_mysqlConnString))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(getById, conn);
            cmd.Parameters.AddWithValue("@id", id);
            fetch(result, cmd);
        }
        return result.Last();
    }
    private static void fetch(List<weatherForecastModel.dto> result, MySqlCommand cmd)
    {
        using (MySqlDataReader rdr = cmd.ExecuteReader())
        {
            if (rdr.HasRows)
            {
                // Fetch data
                while (rdr.Read())
                {
                    weatherForecastModel.dto dto = new weatherForecastModel.dto();
                    dto.id = rdr.GetInt64(0);
                    dto.Summary = rdr.GetString(1);
                    dto.TemperatureC = rdr.GetInt32(2);
                    dto.TemperatureF = rdr.GetInt32(3);

                    result.Add(dto);
                }
            }
        }
    }

}