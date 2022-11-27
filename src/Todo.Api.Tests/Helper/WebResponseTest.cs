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
    public static TestTable[] tcHttpResponse
    {
        get
        {
            return new TestTable[] {
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
            }
        };
        }
    }
    public static TheoryData<TestTableBuilder> tdHttpResponse => TestTable.BuildTestTable(tcHttpResponse);
    [Theory]
    [MemberData(nameof(tdHttpResponse))]
    public void TestHttpResponse(TestTableBuilder Case)
    {
        try
        {
            TestTable testData = tcHttpResponse[Case.Index];

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