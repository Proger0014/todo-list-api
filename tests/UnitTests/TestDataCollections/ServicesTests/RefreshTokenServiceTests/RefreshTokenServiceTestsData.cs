namespace UnitTests.TestDataCollections.ServicesTests.RefreshTokenServiceTests;

public class RefreshTokenServiceTestsData
{
    public static IEnumerable<object[]> RefreshTokensSuit { get; private set; } =
        RefreshTokenServiceTestsDataInit.RefreshTokensSuitInit();

    public static IEnumerable<object[]> CollectionDataForGenerateRefreshToken { get; private set; } =
        RefreshTokenServiceTestsDataInit.CollectionDataForGenerateRefreshTokenInit();
}
