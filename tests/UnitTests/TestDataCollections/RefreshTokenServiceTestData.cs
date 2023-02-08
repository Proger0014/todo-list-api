using TodoList.Models.RefreshToken;

namespace UnitTests.TestDataCollections;

public class RefreshTokenServiceTestData
{
    public static IEnumerable<object[]> RefreshTokens { get; private set; } = 
        RefreshTokenServiceTestDataInit.RefreshTokensInit();
}

internal class RefreshTokenServiceTestDataInit
{
    private static int _refreshTokenCounter = 0;

    private static int RefreshTokenCounter
    {
        get
        {
            return ++_refreshTokenCounter;
        }
    }

    internal static IEnumerable<object[]> RefreshTokensInit()
    {
        var refreshTokens = new List<object[]>();

        for (int i = 0; i < 2; i++)
        {
            refreshTokens.Add(new object[] 
            { 
                new RefreshToken()
                {
                    Id = Guid.NewGuid(),
                    FingerPrint = "finger_print" + RefreshTokenCounter,
                    UserId = RefreshTokenCounter,
                    AddedTime = DateTime.Now.AddMinutes(RefreshTokenCounter),
                    ExpirationTime = DateTime.Now.AddMinutes(RefreshTokenCounter * 2) 
                }
            });
        }

        return refreshTokens;
    }
}
