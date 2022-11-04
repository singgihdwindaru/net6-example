using Todo.Api.models;
using FluentAssertions;
using helper = Todo.Api.Helper.WebResponse;
using System.Collections.Immutable;

namespace Todo.Api.Tests.helperTest;

public class WebResponseTest
{
    private int sumNumber(int a, int b)
    {
        return a + b;
    }
    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(2, 2, 4)]
    public void TestName(int a, int b, int expectedResult)
    {
        expectedResult.Should().BeInRange(sumNumber(a, b),sumNumber(a, b),"success");
    }
    [Fact]
    public void TestHttpResponse()
    {
        var testTable = new[] {
           new {
            name = "success",
            arg = new { code = 200, message = "success", isError = false, data = "success" },
            expectedResult =
                new httpResponse.Root<object>(){
                    code =200,
                    message="success",
                    error=false,
                    data="success"
                }
            },
        };

        foreach (var item in testTable)
        {
            var actualResult = helper.HttpResponse(item.arg.code, item.arg.message, item.arg.isError, item.arg.data);
            item.expectedResult.Should().BeEquivalentTo(actualResult, item.name);
            // Assert.Equal(item.expectedResult.code, actualResult.code);
        }
    }
}