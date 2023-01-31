using System.Net;
using TodoList.DTO.Token;
using TodoList.DTO.User;
using IntegrationTests.Extensions;
using IntegrationTests.Constants;

namespace IntegrationTests;

/**
 * IClassFixture нужен, чтобы создать экземпляр класса в дженерике для всего класса теста
 * Затем этот инстанс передается в конструктор
 */
public class AuthTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public AuthTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task LoginAndGetUserInfo_SendExistingUser_ReturnUserAnd200()
    {
        // Arrange
        var client = _factory.CreateClient();

        var userLogin = new UserLoginRequest()
        {
            Login = "asd",
            Password = "asd"
        };

        var stringContent = userLogin.StringContentJsonFromEntity();

        // get refreshToken and access jwt
        var authInfo = await client.PostAsync(UrlConstants.LoginUrl, stringContent);

        var jwtToken = await System.Text.Json.JsonSerializer
            .DeserializeAsync<TokenResponse>(authInfo.Content.ReadAsStream());

        if (jwtToken == null)
        {
            Assert.True(false);
        }

        Assert.Equal(HttpStatusCode.OK, authInfo.StatusCode);

        // create new get request for get user with id = 1
        var requestMessage = new HttpRequestMessage(
            HttpMethod.Get, new Uri(string.Format(UrlConstants.UserGetUrl, 1)));

        requestMessage.Headers.Add("Authorization", $"Bearer {jwtToken.AccessToken}");

        // Act
        var response = await client.SendAsync(requestMessage);
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