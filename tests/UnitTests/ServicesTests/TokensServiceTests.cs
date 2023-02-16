using Microsoft.Extensions.Configuration;
using TodoList.Models.User;
using TodoList.Services;
using UnitTests.TestDataCollections.ServicesTests.TokensServiceTests;

namespace UnitTests.ServicesTests;

// TODO: сделать
public class TokensServiceTests
{
    [Theory]
    [MemberData(
        nameof(TokensServiceTestsData.TokensValidConfigurationSuitCollection),
        MemberType = typeof(TokensServiceTestsData))]
    public void CreateJWT_ConfigurationWithExistsData_ReturnStringJWT(
        User user,
        IConfiguration configuration)
    {
        // Arrange
        var tokensService = new TokensService(configuration);

        // Act
        var createdToken = tokensService.CreateJWT(user);

        // Assert
        Assert.NotNull(createdToken);
    }
}
