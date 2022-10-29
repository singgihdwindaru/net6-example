using MySql.Data.MySqlClient;

namespace Todo.Api.configuration;

public class databases
{
    public string mysqlConnstring { get; }
    public long dateTimeNow { get; }
    public IConfiguration _config;

    public databases(IConfiguration config)
    {
        _config = config;
        mysqlConnstring = config["Databases:mysql"];
        dateTimeNow = DateTime.Now.Ticks;
    }

    public MySqlConnection initMysql()
    {
        MySqlConnection conn = new MySqlConnection(mysqlConnstring);
        conn.Open();
        return conn;
    }
}