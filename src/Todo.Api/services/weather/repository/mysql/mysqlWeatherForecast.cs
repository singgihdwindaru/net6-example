using System.Data;
using MySql.Data.MySqlClient;
using Todo.Api.models;
using static Todo.Api.models.weatherForecastModel;
namespace Todo.Api.services.weather.repository.mysql;

public partial class mysqlWeatherForecast : IWeatherForecastMysqlRepo
{

    protected readonly string _mysqlConnString;
    protected readonly IDbConnection _db;
    public mysqlWeatherForecast(string connString)
    {
        _mysqlConnString = connString;
    }

    public mysqlWeatherForecast(IDbConnection db)
    {
        _db = db;
    }

    public  (Exception? error, IEnumerable<dto>? result) GetAll()
    {
        (Exception? error, IEnumerable<dto>? data)  result = (null,null);
        List<weatherForecastModel.dto> listDto = new List<weatherForecastModel.dto>();
        try
        {
            using (var dbConn = _db)
            {
                ConnectionState originalState = _db.State;
                if (originalState != ConnectionState.Open)
                    _db.Open();

                IDbCommand command = _db.CreateCommand();
                command.CommandText = selectAll;
                fetch2(listDto, command);
            }
        }
        catch (System.Exception e)
        {
            result.error = e;
            return result;
        }
        result.data = listDto;
        return result;
    }

    public (Exception? error, dto? result) GetById(long id)
    {
        (Exception? error, dto? data) result = (null, null);
        List<weatherForecastModel.dto> dto = new List<weatherForecastModel.dto>();
        try
        {
            using (MySqlConnection conn = new MySqlConnection(_mysqlConnString))
            {
                using (var dbConn = _db)
                {
                    ConnectionState originalState = _db.State;
                    if (originalState != ConnectionState.Open)
                        _db.Open();

                    IDbCommand command = _db.CreateCommand();
                    command.CommandText = getById;
                    command.AddParameter("@id", id);
                    fetch2(dto, command);
                }
            }
        }
        catch (System.Exception e)
        {
            result.error = e;
            return result;
        }
        dto? data = dto.Count > 0 ? dto.Last() : null;
        result.data = data;
        return result;
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

    private void fetch2(List<dto> result, IDbCommand cmd)
    {
        using (IDataReader rdr = cmd.ExecuteReader())
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