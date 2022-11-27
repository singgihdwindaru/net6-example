using System.Xml.Linq;
using Xunit.Abstractions;

namespace Todo.Api.Tests;

public class TestTable : IXunitSerializable
{
    public TestTable()
    {
        TestName = "Fill test name";
    }
    public string TestName { get; set; }
    public object? Args { get; set; }
    public bool WantError { get; set; }//parameter for testing exception
    public object? ExpectedResult { get; set; }
    public Action Mock { get; set; }

    public void Deserialize(IXunitSerializationInfo info)
    {
        TestName = info.GetValue<string>("TestName");
        Args = info.GetValue<object>("Args");
        WantError = info.GetValue<bool>("WantError");
        ExpectedResult = info.GetValue<object>("ExpectedResult");
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue("TestName", TestName);
        info.AddValue("Args", Args);
        info.AddValue("WantError", WantError, typeof(bool));
        info.AddValue("ExpectedResult", ExpectedResult);
    }

     public static TheoryData<TestTableBuilder> BuildTestTable(TestTable[] tc)
    {
        TheoryData<TestTableBuilder> t11 = new TheoryData<TestTableBuilder>();
        for (int i = 0; i < tc.Length; i++)
        {
            t11.Add(new TestTableBuilder(i, tc[i]));
        }
        return t11;
    }
}


