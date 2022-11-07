using Xunit.Abstractions;

namespace Todo.Api.Tests;

public class TestTableBuilder : IXunitSerializable
{
    private string _testName;
    private readonly TestTable _testTable;
    public TestTableBuilder() { }// required for deserializer


    public TestTableBuilder(TestTable testTable)
    {
        _testName = testTable.TestName;
        _testTable = testTable;
    }
    public TestTable Build()
    {
        return _testTable;
    }
    public void Deserialize(IXunitSerializationInfo info)
    {
        _testName = info.GetValue<string>("TestName");
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue("TestName", _testName);
    }
    public override string ToString()
    {
        return _testName;
    }
}