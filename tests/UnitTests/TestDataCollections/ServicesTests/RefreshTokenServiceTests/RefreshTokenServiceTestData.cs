namespace UnitTests.TestDataCollections.ServicesTests.RefreshTokenServiceTests;

public class RefreshTokenServiceTestData
{
    public static IEnumerable<object[]> RefreshTokens { get; private set; } =
        RefreshTokenServiceTestDataInit.RefreshTokensInit();

    public static IEnumerable<object[]> RefreshTokenCreateDTOs { get; private set; } =
        RefreshTokenServiceTestDataInit.RefreshTokenCreateDTOsInit();
}
