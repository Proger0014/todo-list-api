using TodoList.DTO.Token;
using TodoList.Models.RefreshToken;

namespace UnitTests.TestDataCollections.ServicesTests.RefreshTokenServiceTests;

internal class RefreshTokenServiceTestDataInit
{
    internal static IEnumerable<object[]> RefreshTokensInit()
    {
        const int MAX_COUNT = 2;

        var refreshTokens = new List<object[]>();

        for (int i = 0; i < MAX_COUNT; i++)
        {
            refreshTokens.Add(new object[]
            {
                new RefreshToken()
                {
                    Id = Guid.NewGuid(),
                    FingerPrint = "finger_print" + i,
                    UserId = i,
                    AddedTime = DateTime.Now.AddMinutes(i),
                    ExpirationTime = DateTime.Now.AddMinutes(i * 2)
                }
            });
        }

        return refreshTokens;
    }

    internal static IEnumerable<object[]> RefreshTokenCreateDTOsInit()
    {
        const int MAX_COUNT = 2;

        var refreshTokenCreateDTOs = new List<object[]>();

        for (int i = 0; i < MAX_COUNT; i++)
        {
            refreshTokenCreateDTOs.Add(new object[]
            {
                new RefreshTokenCreate()
                {
                    UserId = i,
                    FingerPrint = $"finger-print_{i}"
                }
            });
        }

        return refreshTokenCreateDTOs;
    }
}
