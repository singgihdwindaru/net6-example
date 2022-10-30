namespace Todo.Api.models;

public class httpResponse
{
    public class Root
    {
        public int code { get; set; }
        public string message { get; set; }
        public bool error { get; set; }
        public Data data { get; set; }
    }
    public class Data
    {
        public List<string> column { get; set; }
        public List<List<object>> values { get; set; }
    }

}