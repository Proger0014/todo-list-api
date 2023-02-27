using TodoList.DTO.User;

namespace IntegrationTests.TestDataCollections.AuthControllerTests;

internal class AuthControllerTestsDataInit
{
    public static IEnumerable<object[]> LoginDtoAndIdDataCollectionInit()
    {
        const int MAX_COUNT = 2;

        var loginDtoAndIdDataCollection = new List<object[]>();

        for (int i = 0; i < MAX_COUNT; i++)
        {
            loginDtoAndIdDataCollection.Add(new object[]
            {
                new UserLoginRequest()
                {
                    Login = $"user-login-{i}",
                    Password = $"user-password-{i}"
                },
                i
            });
        }

        return loginDtoAndIdDataCollection;
    }
}
