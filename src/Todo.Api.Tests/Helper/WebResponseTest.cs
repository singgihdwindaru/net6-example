using Todo.Api.models;
using FluentAssertions;
using helper = Todo.Api.Helper.WebResponse;
using System.Collections.Immutable;
using System.Collections;
using System.Text.Json;
using Newtonsoft.Json;

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
                Args = JsonConvert.SerializeObject(new { code = 200, message = "success", error = false, data = "success" }),
                ExpectedResult = new httpResponse.Root<object>
                {
                    code = 200,
                    message = "success",
                    error = false,
                    data = "success",
                    errors = (string?) null
                },
                WantError = false
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
            var definition = new { code = 0, message = "0", error = false, data = "", errors = (Exception?)null };
            var arg = JsonConvert.DeserializeAnonymousType(testData.Args?.ToString(), definition);
            // dynamic arg = testData.Args;
            var actualResult = helper.HttpResponse(arg.code, arg.message, arg.error, arg.data, arg.errors);
            testData.ExpectedResult?.Should().BeEquivalentTo(actualResult);
        }
        catch (System.Exception ex)
        {
            Assert.True(false, ex.Message);
        }

    }
}