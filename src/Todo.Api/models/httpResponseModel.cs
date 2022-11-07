namespace Todo.Api.models;

public class httpResponse
{
    public class Root<T>
    {
        public int code { get; set; }
        public string message { get; set; }
        public bool error { get; set; }
        public T? data { get; set; }
    }
    public class DataColumnRow
    {
        public List<string> columns { get; set; }
        public List<List<object>> rows { get; set; }
        public DataColumnRow()
        {
            columns = new List<string>();
            rows = new List<List<object>>();
        }
    }

}