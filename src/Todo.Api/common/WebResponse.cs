using System.Collections;
using System.Reflection;
using Todo.Api.models;

namespace Todo.Api.common;

public class WebResponse
{
    public static httpResponse.Root<object> HttpResponse(int code, string message, bool isError, object data)
    {
        httpResponse.Root<object> rsp = new httpResponse.Root<object>();
        rsp.code = code;
        rsp.message = message;
        rsp.error = isError;
        rsp.data = data;
        return rsp;
    }
    public static httpResponse.Root<T> HttpResponseColumnRows<T>(int code, string message, bool isError, T data) where T : class
    {
        httpResponse.Root<T> rsp = new httpResponse.Root<T>();
        rsp.code = code;
        rsp.message = message;
        rsp.error = isError;

        httpResponse.DataColumnRow dr = new httpResponse.DataColumnRow();
        IEnumerable<T> enumerable = (IEnumerable<T>)data;
        ConvertToColumnsAndRows(enumerable, dr);
        T result = (T)(object)dr;
        rsp.data = result;
        return rsp;
    }
    public static void ConvertToColumnsAndRows<T>(IEnumerable<T> values, httpResponse.DataColumnRow data) where T : class
    {
        Type type = values.GetType().GenericTypeArguments[0];
        // Gets all properties from the generic type T
        PropertyInfo[] props = type.GetProperties();
        string[] columns = props.Select(x => x.Name).ToArray();
        data.columns = columns.ToList();
        foreach (var item in values)
        {
            List<object> lS = new List<object>();
            for (int i = 0; i < columns.Length; i++)
            {
                lS.Add(item.GetType().GetProperty(columns[i])?.GetValue(item, null));
            }
            data.rows.Add(lS);
        }
        // result2.Add(result);
        // return result;
    }


}