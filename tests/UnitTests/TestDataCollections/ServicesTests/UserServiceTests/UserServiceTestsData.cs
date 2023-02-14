namespace UnitTests.TestDataCollections.ServicesTests.UserServiceTests;

public class UserServiceTestsData
{
    public static IEnumerable<object[]> UserAccessDeniedCheckSuitCollection { get; private set; } =
        UserServiceTestsDataInit.UserAccessDeniedCheckSuitCollectionInit();

    public static IEnumerable<object[]> UserAccessDeniedCheckCollection { get; private set; } =
        UserServiceTestsDataInit.UserAccessDeniedCheckCollectionInit();
}
