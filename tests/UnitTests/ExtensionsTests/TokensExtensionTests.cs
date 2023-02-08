using TodoList.Extensions;
using TodoList.Models.User;

namespace UnitTests.ExtensionsTests;

public class TokensExtensionTests
{
    [Fact]
    public void GenerateJWT_ReturnString()
    {
        var actual = new User().GenerateJWT();

        Assert.NotNull(actual);
    }
}
