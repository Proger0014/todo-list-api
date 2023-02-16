namespace UnitTests.TestDataCollections.ServicesTests.TokensServiceTests;

public class TokensServiceTestsData
{
    public static IEnumerable<object[]> TokensValidConfigurationSuitCollection { get; private set; } =
        TokensServiceTestsDataInit.TokensValidConfigurationSuitCollectionInit();
}
