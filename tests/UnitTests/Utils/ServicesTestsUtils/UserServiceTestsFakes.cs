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
}
