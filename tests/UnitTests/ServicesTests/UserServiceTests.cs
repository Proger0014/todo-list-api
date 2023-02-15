using Moq;
using TodoList.Constants;
using TodoList.DTO.User;
using TodoList.Exceptions;
using TodoList.Models.User;
using TodoList.Services;
using UnitTests.TestDataCollections.ServicesTests.UserServiceTests;
using UnitTests.Utils.ServicesTestsUtils;

namespace UnitTests.ServicesTests;

public class UserServiceTests
{
    [Theory]
    [MemberData(
        nameof(UserServiceTestsData.UserAccessDeniedCheckSuitCollection),
        MemberType = typeof(UserServiceTestsData))]
    public void GetUserWithAccessDeniedCheck_ExistingIdInClaim_ReturnUser(
        UserAccessDeniedCheck userAccessDeniedCheck,
        User expectedUser)
    {
        // Arrange
        var stubRepo = UserServiceTestsFakes.CreateGetByIdFake(
            userAccessDeniedCheck, expectedUser).Object;

        var userService = new UserService(stubRepo);

        // Act
        User actualUser = userService.GetUserWithAccessDeniedCheck(
            userAccessDeniedCheck);

        // Assert
        Assert.Equal(expectedUser, actualUser);
    }

    [Theory]
    [MemberData(
        nameof(UserServiceTestsData.UserAccessDeniedCheckCollection),
        MemberType = typeof(UserServiceTestsData))]
    public void GetUserWithAccessDeniedCheck_NotExistsUserIdInClaim_ThrowsNotFoundException(
        UserAccessDeniedCheck userAccessDeniedCheck)
    {
        // Arrange
        var stubRepo = new Mock<IUserRepository>().Object;
        var userService = new UserService(stubRepo);

        // Act
        Action act = () => { userService.GetUserWithAccessDeniedCheck(userAccessDeniedCheck); };

        // Assert
        NotFoundException actualException = Assert.Throws<NotFoundException>(act);
        Assert.Equal(string.Format(
            ExceptionMessage.USER_NOT_FOUND_WITH_ID, 
            userAccessDeniedCheck.UserId), 
            actualException.Message);
    }

    [Theory]
    [MemberData(
        nameof(UserServiceTestsData.UserAccessDeniedCheckWithAnotherUserIdCollection),
        MemberType = typeof(UserServiceTestsData))]
    public void GetUserWithAccessDeniedCheck_AnotherUserIdAccessDeniedCheck_ThrowsAccessDeniedCheck(
        UserAccessDeniedCheck userAccessDeniedCheck)
    {
        // Arrage
        var stubRepo = new Mock<IUserRepository>().Object;
        var userService = new UserService(stubRepo);

        // Act
        Action act = () => { userService.GetUserWithAccessDeniedCheck(userAccessDeniedCheck); };

        // Assert
        AccessDeniedException actualException = Assert.Throws<AccessDeniedException>(act);
        Assert.Equal(
            ExceptionMessage.ACCESS_DENIED_FOR_ANOTHER_USER_DATA, 
            actualException.Message);
    }
}
