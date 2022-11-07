namespace Todo.Api.Tests;

public class TestTable
{
    public string TestName { get; set; }
    public object? Args { get; set; }
    public bool WantError { get; set; }//parameter for testing exception
    public object? ExpectedResult { get; set; }
    public Action Mock { get; set; }
    public TestTable()
    {
        TestName = "Fill test name";
    }
}