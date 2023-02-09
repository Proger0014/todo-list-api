using Moq;
using TodoList.Models.RefreshToken;

namespace UnitTests.Utils.ServicesTestsUtils;

public static class RefreshTokenServiceTestsFakes
{
    public static Mock<IRefreshTokenRepository> CreateByUserIdFake(RefreshToken refreshToken)
    {
        var fake = new Mock<IRefreshTokenRepository>();

        fake.Setup(rt => rt.GetByUserId(refreshToken.UserId))
            .Returns(refreshToken);

        return fake;
    }

    public static Mock<IRefreshTokenRepository> CreateByIdFake(RefreshToken refreshToken)
    {
        var fake = new Mock<IRefreshTokenRepository>();

        fake.Setup(rt => rt.GetById(refreshToken.Id))
            .Returns(refreshToken);

        return fake;
    }
}
