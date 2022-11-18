using System.Collections;
using System.Reflection;
using Todo.Api.models;

namespace Todo.Api.Helper;

public static class WebResponse
{
    public static httpResponse.Root<object>? HttpResponse(int code, string message, bool isError, object? data, Exception? error)
    {
        // if (data == null)
        // {
        //     return default;
        // }
        httpResponse.Root<object> rsp = new httpResponse.Root<object>();
        rsp.code = code;
        rsp.message = message;
        rsp.error = isError;
        rsp.data = data;
        if (error == null)
        {
            rsp.errors = (string?)null;
            return rsp;
        }
        rsp.errors = error.Message;
        return rsp;
    }
    public static httpResponse.Root<T> HttpResponseColumnRows<T>(int code, string message, bool isError, T data) where T : class
    {
        httpResponse.Root<T> rsp = new httpResponse.Root<T>();
        rsp.code = code;
        rsp.message = message;
        rsp.error = isError;

        httpResponse.DataColumnRow dr = new httpResponse.DataColumnRow();
        // IEnumerable<T> enumerable = (IEnumerable<T>)data;
        ConvertToColumnsAndRows(data, dr);
        T result = (T)(object)dr;
        rsp.data = result;
        return rsp;
    }
    public static void ConvertToColumnsAndRows<T>(T values, httpResponse.DataColumnRow data) where T : class
    {
        Type type = values.GetType();

        // if (typeof(IEnumerable<>).IsAssignableFrom(type))
        if (type.GetInterface(nameof(ICollection)) != null)
        {
            //TODO : handle error kalo bukan IEnumerable<>
            PropertyInfo[] props = type.GenericTypeArguments[0].GetProperties();
            string[] columns = props.Select(x => x.Name).ToArray();
            data.columns = columns.ToList();
            foreach (var item in (IEnumerable<T>)values)
            {
                List<object>? lS = new List<object>();
                for (int i = 0; i < columns.Length; i++)
                {
                    lS.Add(item.GetType().GetProperty(columns[i])?.GetValue(item, null));
                }
                data.rows.Add(lS);
            }
        }
        // Gets all properties from the generic type T

        // result2.Add(result);
        // return result;
    }


}