using Microsoft.Extensions.Configuration;
using TodoList.DTO.Token;
using TodoList.Models.RefreshToken;
using TodoList.Services;
using TodoList.Services.DateTimeProvider;
using UnitTests.Utils;
using UnitTests.Utils.ServicesTestsUtils;

namespace UnitTests.TestDataCollections.ServicesTests.RefreshTokenServiceTests;

internal class RefreshTokenServiceTestsDataInit
{
    internal static IEnumerable<object[]> RefreshTokensSuitInit()
    {
        const int MAX_COUNT = 2;

        var settings = new Dictionary<string, string>()
        {
            ["Jwt:RefreshToken:MaxAge"] = "2"
        };

        var refreshTokens = new List<object[]>();
        IDateTimeProvider dateTimeProviderFake = RefreshTokenServiceTestsFakes
            .CreateDateTimeProviderFake(DateTime.Now).Object;
        IConfiguration configurationFake = CommonUtils.CreateConfigurationFake(settings);
        var authCookieOptions = new AuthCookieOptions(configurationFake, dateTimeProviderFake);

        for (int i = 0; i < MAX_COUNT; i++)
        {
            refreshTokens.Add(new object[]
            {
                new RefreshToken()
                {
                    Id = Guid.NewGuid(),
                    FingerPrint = "finger_print" + i,
                    UserId = i,
                    AddedTime = dateTimeProviderFake.DateTimeNow.AddMinutes(i),
                    ExpirationTime = dateTimeProviderFake.DateTimeNow.AddMinutes(i * 2)
                },
                dateTimeProviderFake,
                authCookieOptions
            });
        }

        return refreshTokens;
    }

    internal static IEnumerable<object[]> CollectionDataForGenerateRefreshTokenInit()
    {
        const int MAX_COUNT = 2;

        var settings = new Dictionary<string, string>()
        {
            ["Jwt:RefreshToken:MaxAge"] = "2"
        };
        var configurationFake = CommonUtils.CreateConfigurationFake(settings);

        var collectionDataForGenerateRefreshToken = new List<object[]>();

        for (int i = 0; i < MAX_COUNT; i++)
        {
            var dateTimeProviderStub = RefreshTokenServiceTestsFakes.CreateDateTimeProviderFake(DateTime.Now);

            collectionDataForGenerateRefreshToken.Add(new object[]
            {
                new RefreshTokenCreate()
                {
                    Id = Guid.NewGuid(),
                    UserId = i,
                    FingerPrint = $"finger-print_{i}"
                },
                dateTimeProviderStub.Object,
                new AuthCookieOptions(configurationFake, dateTimeProviderStub.Object)
            });
        }

        return collectionDataForGenerateRefreshToken;
    }
}
