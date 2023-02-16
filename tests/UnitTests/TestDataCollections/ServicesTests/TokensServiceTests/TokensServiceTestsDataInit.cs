using TodoList.Models.User;
using UnitTests.Utils;

namespace UnitTests.TestDataCollections.ServicesTests.TokensServiceTests;

internal class TokensServiceTestsDataInit
{
    internal static IEnumerable<object[]> TokensValidConfigurationSuitCollectionInit()
    {
        const int MAX_COUNT = 2;

        var settings = new Dictionary<string, string>()
        {
            ["Jwt:Key"] = "jwt-secret-key-some-text",
            ["Jwt:Expires"] = "1",
            ["Jwt:Issuer"] = "jwt-issuer",
            ["Jwt:Audience"] = "jwt-audience"
        };

        var configurationFake = CommonUtils
            .CreateConfigurationFake(settings);

        var tokensValidConfigurationSuitCollection = new List<object[]>();

        for (int i = 0; i < MAX_COUNT; i++)
        {
            tokensValidConfigurationSuitCollection.Add(new object[]
            {
                new User()
                {
                    Id = i,
                    NickName = $"user-{i}-nickname",
                    Login = $"user-{i}-login",
                    Password = $"user-{i}-password"
                },
                configurationFake
            });
        }

        return tokensValidConfigurationSuitCollection;
    }
}
