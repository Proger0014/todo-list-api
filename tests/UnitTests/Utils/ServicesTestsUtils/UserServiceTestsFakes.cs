    using Moq;
using TodoList.DTO.User;
using TodoList.Models.User;

namespace UnitTests.Utils.ServicesTestsUtils;

public static class UserServiceTestsFakes
{
    public static Mock<IUserRepository> CreateGetByIdFake(
        UserAccessDeniedCheck userAccessDeniedCheck, User user)
    {
        var fake = new Mock<IUserRepository>();

        fake.Setup(ur => ur.GetById(userAccessDeniedCheck.UserId))
            .Returns(user);

        return fake;
    }

    public static Mock<IUserRepository> CreateGetByUserLoginFake(
        UserLoginRequest userLoginRequest,
        User user)
    {
        var fake = new Mock<IUserRepository>();

        fake.Setup(ur => ur.GetByUserLogin(
                userLoginRequest.Login, userLoginRequest.Password))
            .Returns(user);

        return fake;
    }

    public static Mock<IUserRepository> CreateGetByUserLoginFromRegisterDTOFake(
        UserRegisterRequest userRegisterRequest,
        User user)
    {
        var fake = new Mock<IUserRepository>();

        fake.Setup(ur => ur.GetByUserLogin(
                userRegisterRequest.Login))
            .Returns(user);

        return fake;
    }
}
