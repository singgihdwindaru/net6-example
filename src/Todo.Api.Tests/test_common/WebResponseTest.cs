using Todo.Api.models;
namespace Todo.Api.Tests.test_controllers;
using FluentAssertions;

public class WebResponseTest
{
    [Fact]
    public void TestHttpResponse()
    {
        // var args = new { code = 200, message = "success", isError = true, data = "success" };
        // Given
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

        // When

        // Then
        foreach (var item in testTable)
        {
            var actualResult = common.WebResponse.HttpResponse(item.arg.code, item.arg.message, item.arg.isError, item.arg.data);
            // item.expectedResult.Should().BeEquivalentTo(actualResult);
            Assert.Equal(item.expectedResult.code, actualResult.code);
        }
    }
}