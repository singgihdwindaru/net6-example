namespace Todo.Api.configuration;

public class databases
{
    public string MysqlConnstring { get; }
    public IConfiguration _config;

    public databases(IConfiguration config)
    {
        _config = config;
        MysqlConnstring = config["Databases:mysql"];
    }
}