using System.Net;
using TodoList.DTO.Token;
using TodoList.DTO.User;
using IntegrationTests.Extensions;
using IntegrationTests.Constants;
using System.Text.Json;
using IntegrationTests.Common;
using IntegrationTests.TestDataCollections.AuthControllerTests;

namespace IntegrationTests.Tests;

/**
 * IClassFixture нужен, чтобы создать экземпляр класса в дженерике для всего класса теста
 * Затем этот инстанс передается в конструктор
 */
public class AuthControllerTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public AuthControllerTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [MemberData(
        nameof(AuthControllerTestsData.LoginDtoAndIdDataCollection),
        MemberType = typeof(AuthControllerTestsData))]
    public async Task LoginAndGetUserInfo_SendExistingUserAndValidJwt_ReturnUserAnd200(
        UserLoginRequest userLoginDto, int id)
    {
        // Arrange
        _factory.Services.CreateAndReinitializeTestDb();

        var client = _factory.CreateClient();

        var stringContent = userLoginDto.StringContentJsonFromEntity();

        // Act
        // get refreshToken and access jwt
        var authInfo = await client.PostAsync(UrlConstants.LoginUrl, stringContent);
        var jwtToken = await JsonSerializer
            .DeserializeAsync<TokenResponse>(authInfo.Content.ReadAsStream());

        Assert.Equal(HttpStatusCode.OK, authInfo.StatusCode);
        Assert.NotNull(jwtToken);

        // create new get request for get user with id = 1
        var requestMessage = new HttpRequestMessage(
            HttpMethod.Get, new Uri(string.Format(UrlConstants.UserGetUrl, id)));

        requestMessage.Headers.Add("Authorization", $"Bearer {jwtToken.AccessToken}");

        var response = await client.SendAsync(requestMessage);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Login_SendNotExistingUser_Return404()
    {
        var client = _factory.CreateClient();

        var userLogin = new UserLoginRequest()
        {
            Login = "string",
            Password = "string"
        };

        var stringContent = userLogin.StringContentJsonFromEntity();

        var authInfo = await client.PostAsync(UrlConstants.LoginUrl, stringContent);

        Assert.Equal(HttpStatusCode.NotFound, authInfo.StatusCode);
    }
}