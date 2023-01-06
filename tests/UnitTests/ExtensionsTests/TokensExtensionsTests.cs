using TodoList.Extensions;
using TodoList.Models.User;

namespace UnitTests.ExtensionsTests;

public class TokensExtensionsTests
{
    [Fact]
    public void GenerateJWT_ReturnString()
    {
        var actual = new User().GenerateJWT();
        var expectedMinimalLength = 50;

        bool isJwt = false;

        if (actual.Length >= expectedMinimalLength)
        {
            isJwt = true;
        }

        Assert.True(isJwt);
    }
}
