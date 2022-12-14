using System.Data;
using Todo.Api.models;
using static Todo.Api.models.weatherForecastModel;
using MySql.Data.MySqlClient;

namespace Todo.Api.services.weather.repository.mysql;

public partial class mysqlWeatherForecast : IWeatherForecastMysqlRepo
{
    protected readonly IDbConnection _db;
    public mysqlWeatherForecast(IDbConnection db)
    {
        _db = db;
    }
    protected void fetch(List<dto> result, IDbCommand cmd)
    {
        using (IDataReader rdr = cmd.ExecuteReader())
        {
            // Fetch data
            while (rdr.Read())
            {
                weatherForecastModel.dto dto = new weatherForecastModel.dto();
                dto.id = Convert.ToInt64(rdr["id"]);
                dto.Summary = rdr["summary"].ToString();
                dto.TemperatureC = Convert.ToInt32(rdr["temperatureC"]);
                dto.TemperatureF = Convert.ToInt32(rdr["temperatureF"]);
                dto.Date = Convert.ToDateTime(rdr["date"]);
                result.Add(dto);
            }
        }
    }
    public (Exception? error, IEnumerable<dto>? result) GetAll()
    {
        (Exception? error, IEnumerable<dto>? data) result = (null, null);
        List<weatherForecastModel.dto> listDto = new List<weatherForecastModel.dto>();
        try
        {
            using (var dbConn = _db)
            {
                dbConn.Open();

                IDbCommand command = dbConn.CreateCommand();
                command.CommandText = selectAll;
                fetch(listDto, command);
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
            using (var dbConn = _db)
            {
                dbConn.Open();

                IDbCommand command = dbConn.CreateCommand();
                command.CommandText = getById;
                command.Parameters.Clear();
                command.AddParameter("@id", id);

                fetch(dto, command);
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

    public Exception Insert(IEnumerable<dto>? data)
    {
        throw new NotImplementedException();
    }

    public Exception Update(IEnumerable<dto>? data)
    {
        throw new NotImplementedException();
    }

    public Exception Delete(long id)
    {
        throw new NotImplementedException();
    }

}