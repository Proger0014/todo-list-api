namespace UnitTests.TestDataCollections.ServicesTests.UserServiceTests;

public class UserServiceTestsData
{
    public static IEnumerable<object[]> UserAccessDeniedCheckSuitCollection { get; private set; } =
        UserServiceTestsDataInit.UserAccessDeniedCheckSuitCollectionInit();

    public static IEnumerable<object[]> UserAccessDeniedCheckCollection { get; private set; } =
        UserServiceTestsDataInit.UserAccessDeniedCheckCollectionInit();

    public static IEnumerable<object[]> UserAccessDeniedCheckWithAnotherUserIdCollection { get; private set; } =
        UserServiceTestsDataInit.UserAccessDeniedCheckWithAnotherUserIdCollectionInit();

    public static IEnumerable<object[]> UserLoginRequestDTOSuitCollection { get; private set; } =
        UserServiceTestsDataInit.UserLoginRequestDTOSuitCollectionInit();

    public static IEnumerable<object[]> UserLoginRequestDTOCollection { get; private set; } =
        UserServiceTestsDataInit.UserLoginRequestDTOCollectionInit();

    public static IEnumerable<object[]> UserRegisterRequestDTOSuitCollection { get; private set; } =
        UserServiceTestsDataInit.UserRegisterRequestDTOSuitCollectionInit();
}
