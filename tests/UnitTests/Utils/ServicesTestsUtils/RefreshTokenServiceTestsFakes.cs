using Microsoft.Extensions.Configuration;
using Moq;
using TodoList.Models.RefreshToken;
using TodoList.Services.DateTimeProvider;

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

    public static Mock<IDateTimeProvider> CreateDateTimeProviderFake(DateTime dateTime)
    {
        var fake = new Mock<IDateTimeProvider>();

        fake.Setup(dtp => dtp.DateTimeNow)
            .Returns(dateTime);

        return fake;
    }
}
