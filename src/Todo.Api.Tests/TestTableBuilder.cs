using Xunit.Abstractions;

namespace Todo.Api.Tests;

public class TestTableBuilder : IXunitSerializable
{
     public int Index { get; private set; }
    public TestTable _testTable { get; private set; }
    public TestTableBuilder()
    {
    }
    public TestTableBuilder(int indexTest, TestTable testTable)
    {
        Index = indexTest;
        _testTable = testTable;

        /* Note  : 
          Mock= mock; // cannot serialize/Deserialize Type Action delegate in current xunit v2.xx 
        */
    }

    public void Deserialize(IXunitSerializationInfo info)
    {
        try
        {
            Index = info.GetValue<int>("Index");
            _testTable = info.GetValue<TestTable>("TestTable");
        }
        catch (Exception ex)
        {
            _ = ex.Message;
            throw;
        }
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        try
        {
            info.AddValue("Index", Index);
            info.AddValue("TestTable", _testTable, typeof(TestTable));
        }
        catch (Exception ex)
        {
            _ = ex.Message;
            throw;
        }
    }

    public override string ToString()
    {
        return _testTable.TestName;
    }
}