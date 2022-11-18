using System.Data;

namespace Todo.Api;
public static class DbCommandExtensionMethods
{
    public static void AddParameter (this IDbCommand command, string name, object value)
    {
        IDbDataParameter parameter = command.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value;
        command.Parameters.Add(parameter);
    }
}