using System.Security.Claims;
using TodoList.DTO.User;
using TodoList.Models.User;

namespace UnitTests.TestDataCollections.ServicesTests.UserServiceTests;

internal class UserServiceTestsDataInit
{
    internal static IEnumerable<object[]> UserAccessDeniedCheckCollectionInit()
    {
        const int MAX_COUNT = 2;

        var userAccessDeniedCheckCollection = new List<object[]>();

        for (int i = 0; i < MAX_COUNT; i++)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, i.ToString())
            };

            userAccessDeniedCheckCollection.Add(new object[]
            {
                new UserAccessDeniedCheck()
                {
                    UserId = i,
                    UserClaims = claims
                }
            });
        }

        return userAccessDeniedCheckCollection;
    }

    internal static IEnumerable<object[]> UserAccessDeniedCheckSuitCollectionInit()
    {
        const int MAX_COUNT = 2;

        var userAccessDeniecCheckSuitCollection = new List<object[]>();

        for (int i = 0; i < MAX_COUNT; i++)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, i.ToString())
            };

            userAccessDeniecCheckSuitCollection.Add(new object[]
            {
                new UserAccessDeniedCheck()
                {
                    UserId = i,
                    UserClaims = claims
                },
                new User()
                {
                    Id = i,
                    NickName = $"nickname-user-with-id-{i}",
                    Login = $"login-user-with-id-{i}",
                    Password = $"password-user-with-id-{i}"
                }
            });
        }

        return userAccessDeniecCheckSuitCollection;
    }

    internal static IEnumerable<object[]> UserAccessDeniedCheckWithAnotherUserIdCollectionInit()
    {
        const int MAX_COUNT = 2;

        var userAccessDeniedCheckWithAnotherUserIdCollection = new List<object[]>();

        for (int i = 0; i < MAX_COUNT; i++)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, i.ToString())
            };

            userAccessDeniedCheckWithAnotherUserIdCollection.Add(new object[]
            {
                new UserAccessDeniedCheck()
                {
                    UserId = i + MAX_COUNT,
                    UserClaims = claims
                }
            });
        }

        return userAccessDeniedCheckWithAnotherUserIdCollection;
    }

    internal static IEnumerable<object[]> UserLoginRequestDTOSuitCollectionInit()
    {
        const int MAX_COUNT = 2;

        var userLoginRequestDTOSuitCollection = new List<object[]>();

        for (int i = 0; i < MAX_COUNT; i++)
        {
            string login = $"some-user-{i}-login";
            string password = $"some-user-{i}-password";

            userLoginRequestDTOSuitCollection.Add(new object[]
            {
                new UserLoginRequest()
                {
                    Login = login,
                    Password = password
                },
                new User()
                {
                    Id = i,
                    NickName = $"some-user-{i}-nickname",
                    Login = login,
                    Password = password
                }
            });
        }

        return userLoginRequestDTOSuitCollection;
    }

    internal static IEnumerable<object[]> UserLoginRequestDTOCollectionInit()
    {
        const int MAX_COUNT = 2;

        var userLoginRequestDTOCollection = new List<object[]>();

        for (int i = 0; i < MAX_COUNT; i++)
        {
            userLoginRequestDTOCollection.Add(new object[]
            {
                new UserLoginRequest()
                {
                    Login = $"some-user-{i}-login",
                    Password = $"some-user-{i}-password"
                }
            });
        }

        return userLoginRequestDTOCollection;
    }

    internal static IEnumerable<object[]> UserRegisterRequestDTOSuitCollectionInit()
    {
        const int MAX_COUNT = 2;

        var userRegisterRequestDTOCollection = new List<object[]>();

        for (int i = 0; i < MAX_COUNT; i++)
        {
            string nickName = $"some-user-{i}-nickname";
            string login = $"some-user-{i}-login";
            string password = $"some-user-{i}-password";

            userRegisterRequestDTOCollection.Add(new object[]
            {
                new UserRegisterRequest()
                {
                    Nickname = nickName,
                    Login = login,
                    Password = password
                },
                new User()
                {
                    Id = 0,
                    NickName = nickName,
                    Login = login,
                    Password = password
                }
            });
        }

        return userRegisterRequestDTOCollection;
    }
}
