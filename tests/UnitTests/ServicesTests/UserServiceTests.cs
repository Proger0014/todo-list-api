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
            ExceptionMessage.USER_NOT_FOUND_WITH_ID, userAccessDeniedCheck.UserId), 
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

    [Theory]
    [MemberData(
        nameof(UserServiceTestsData.UserLoginRequestDTOSuitCollection),
        MemberType = typeof(UserServiceTestsData))]
    public void GetUserByLogin_ExistsUserLoginRequestsDTO_ReturnUser(
        UserLoginRequest userLoginRequestDTO,
        User expectedUser)
    {
        // Arrange
        var stubRepo = UserServiceTestsFakes.CreateGetByUserLoginFake(
            userLoginRequestDTO, expectedUser).Object;

        var userService = new UserService(stubRepo);

        // Act
        User actualUser = userService.GetUserByLogin(userLoginRequestDTO);

        // Assert
        Assert.Equal(expectedUser, actualUser);
    }

    [Theory]
    [MemberData(
        nameof(UserServiceTestsData.UserLoginRequestDTOCollection),
        MemberType = typeof(UserServiceTestsData))]
    public void GetUserByLogin_NotExistsUserLoginRequestsDTO_ThrowsNotFoundException(
        UserLoginRequest userLoginRequest)
    {
        // Arrange
        var stubRepo = new Mock<IUserRepository>().Object;
        var userService = new UserService(stubRepo);

        // Act
        Action act = () => { userService.GetUserByLogin(userLoginRequest); };

        // Assert
        var actualException = Assert.Throws<NotFoundException>(act);
        Assert.Equal(string.Format(
            ExceptionMessage.USER_NOT_FOUND_WITH_LOGIN, userLoginRequest.Login),
            actualException.Message);
    }

    [Theory]
    [MemberData(
        nameof(UserServiceTestsData.UserRegisterRequestDTOSuitCollection),
        MemberType = typeof(UserServiceTestsData))]
    public void AddUser_UniqueNickname_SuccessAdd(
        UserRegisterRequest userRegisterRequest,
        User user)
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();

        var userService = new UserService(mockRepo.Object);

        // Act
        userService.AddUser(userRegisterRequest);

        // Assert
        mockRepo.Verify(ur => ur.Insert(user));
    }

    [Theory]
    [MemberData(
        nameof(UserServiceTestsData.UserRegisterRequestDTOSuitCollection),
        MemberType = typeof(UserServiceTestsData))]
    public void AddUser_ExistsUserWithNickname_ThrowsExistsException(
        UserRegisterRequest userRegisterRequest,
        User user)
    {
        // Arrange
        var stubRepo = UserServiceTestsFakes
            .CreateGetByUserLoginFromRegisterDTOFake(userRegisterRequest, user);

        var userService = new UserService(stubRepo.Object);

        // Act
        Action act = () => { userService.AddUser(userRegisterRequest); };

        // Assert
        ExistsException actualException = Assert.Throws<ExistsException>(act);
        Assert.Equal(string.Format(
            ExceptionMessage.USER_IS_EXISTS, userRegisterRequest.Nickname),
            actualException.Message);
    }
}
