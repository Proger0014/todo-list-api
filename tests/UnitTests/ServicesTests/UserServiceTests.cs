using Moq;
using System.Security.Claims;
using TodoList.DTO.User;
using TodoList.Models.User;
using TodoList.Services;

namespace UnitTests.ServicesTests;

public class UserServiceTests
{
    private const string USER_1_ID = "1";
    private readonly User USER_1 = new User() 
    {
        Id = 1,
        NickName = "user1",
        Login = "login-user1",
        Password = "password-user1"
    };

    private const string USER_2_ID = "2";
    private readonly User USER_2 = new User()
    {
        Id = 2,
        NickName = "user2",
        Login = "login-user2",
        Password = "password-user2"
    };

    private Mock<IUserRepository> CreateMock()
    {
        var mock = new Mock<IUserRepository>();

        mock.Setup(ur => ur.GetById(USER_1.Id))
            .Returns(USER_1);

        mock.Setup(ur => ur.GetById(USER_2.Id))
            .Returns(USER_2);

        return mock;
    }

    [Fact]
    public void GetUserWithAccessDeniedCheck_ExistingIdInClaim_ReturnUser()
    {
        var userService = new UserService(CreateMock().Object);

        var accessDeniedCheck = new UserAccessDeniedCheck()
        {
            UserClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, USER_1_ID)
            },
            UserId = USER_1.Id
        };

        var expectedUser = (User)USER_1.Clone();
        var actualUser = userService.GetUserWithAccessDeniedCheck(accessDeniedCheck);

        Assert.Equal(expectedUser, actualUser);
    }
}
