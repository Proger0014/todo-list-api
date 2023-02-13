namespace UnitTests.TestDataCollections.ServicesTests.RefreshTokenServiceTests;

public class RefreshTokenServiceTestData
{
    public static IEnumerable<object[]> RefreshTokensSuit { get; private set; } =
        RefreshTokenServiceTestDataInit.RefreshTokensSuitInit();

    public static IEnumerable<object[]> CollectionDataForGenerateRefreshToken { get; private set; } =
        RefreshTokenServiceTestDataInit.CollectionDataForGenerateRefreshTokenInit();
}
