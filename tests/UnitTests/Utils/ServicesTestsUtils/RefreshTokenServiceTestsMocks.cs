using Moq;
using TodoList.Models.RefreshToken;

namespace UnitTests.Utils.ServicesTestsUtils;

public static class RefreshTokenServiceTestsMocks
{
    public static Mock<IRefreshTokenRepository> CreateByUserIdMock(RefreshToken refreshToken)
    {
        var mock = new Mock<IRefreshTokenRepository>();

        mock.Setup(rt => rt.GetByUserId(refreshToken.UserId))
            .Returns(refreshToken);

        return mock;
    }

    public static Mock<IRefreshTokenRepository> CreateByIdMock(RefreshToken refreshToken)
    {
        var mock = new Mock<IRefreshTokenRepository>();

        mock.Setup(rt => rt.GetById(refreshToken.Id))
            .Returns(refreshToken);

        return mock;
    }
}
