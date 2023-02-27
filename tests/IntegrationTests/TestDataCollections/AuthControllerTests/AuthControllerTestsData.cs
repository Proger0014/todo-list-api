namespace IntegrationTests.TestDataCollections.AuthControllerTests;

public static class AuthControllerTestsData
{
    public static IEnumerable<object[]> LoginDtoAndIdDataCollection { get; } =
        AuthControllerTestsDataInit.LoginDtoAndIdDataCollectionInit();
}
