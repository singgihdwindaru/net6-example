using Todo.Api.models;
using FluentAssertions;
using helper = Todo.Api.Helper.WebResponse;
using System.Collections.Immutable;
using System.Collections;

namespace Todo.Api.Tests.Helper;

public class WebResponseTest
{
    public static TheoryData<TestTableBuilder> TestHttpResponseData()
    {
        List<TestTable> tp = new List<TestTable>();
        tp.Add(
            new TestTable
            {
                TestName = "#1 success",
                Args = new { code = 200, message = "success", error = false, data = "success" },
                ExpectedResult = new httpResponse.Root<object>
                {
                    code = 200,
                    message = "success",
                    error = false,
                    data = "success"
                },
                WantError = false
            });
        tp.Add(
            new TestTable
            {
                TestName = "#2 Error",
                Args = new { code = 400, message = "fail", error = true, data = (string?)null },
                ExpectedResult = default,
                WantError = true
            });
        var data = new TheoryData<TestTableBuilder>();
        foreach (var item in tp)
        {
            data.Add(new TestTableBuilder(item));
        }
        return data;
    }

    [Theory]
    [MemberData(nameof(TestHttpResponseData))]
    public void TestHttpResponse(TestTableBuilder Case)
    {
        try
        {
            TestTable testData = Case.Build();
            dynamic arg = testData.Args;
            var actualResult = helper.HttpResponse(arg.code, arg.message, arg.error, arg.data);
            testData.ExpectedResult?.Should().BeEquivalentTo(actualResult);
        }
        catch (System.Exception ex)
        {
            Assert.True(false, ex.Message);
        }

    }
    [Theory]
    [InlineData("#2.1 success", 200, "success", false, "success")]
    [InlineData("#2.2 error", 200, "success", false, "success")]
    public void TestHttpResponse2(string testname, int code, string message, bool error, object data)
    {
        try
        {
            var ExpectedResult = new httpResponse.Root<object>
                {
                    code = 200,
                    message = "success",
                    error = false,
                    data = "success"
                };
            // TestParam testData = Case.Build();
            // dynamic arg = testData.Args;
            var actualResult = helper.HttpResponse(code, message, error, data);
            ExpectedResult?.Should().BeEquivalentTo(actualResult);
        }
        catch (System.Exception ex)
        {
            Assert.True(false, ex.Message);
        }

    }
}