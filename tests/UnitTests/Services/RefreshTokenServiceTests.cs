using Moq;
using TodoList.Models.RefreshToken;
using TodoList.Services;

namespace UnitTests.Services;

public class RefreshTokenServiceTests
{
    private RefreshTokenService CreateMock()
    {
        var mock = new Mock<IRefreshTokenRepository>();

        var targetDate = new DateTime(2023, 1, 1);

        var refreshToken1 = new RefreshToken("refresh_token1", 1, "finger_print1", targetDate, targetDate.AddMinutes(1));
        var refreshToken2 = new RefreshToken("refresh_token2", 2, "finger_print2", targetDate.AddDays(2), targetDate.AddDays(3));

        mock.Setup(rr => rr.GetByUserId(1))
            .Returns(refreshToken1);

        mock.Setup(rr => rr.GetById("refresh_token1"))
            .Returns(refreshToken1);

        mock.Setup(rr => rr.GetById("refresh_token2"))
            .Returns(refreshToken2);

        mock.Setup(rr => rr.GetByUserId(2))
            .Returns(refreshToken2);

        return new RefreshTokenService(mock.Object);
    }

    [Fact]
    public void GetRefreshToken_ByUserId_ReturnRefreshToken()
    {
        var refreshTokenService = CreateMock();

        var targetDate = new DateTime(2023, 1, 1);

        var expected = new RefreshToken("refresh_token1", 1, "finger_print1", targetDate, targetDate.AddMinutes(1));
        var actual = refreshTokenService.GetRefreshTokenByUserId(1);

        Assert.Equal(expected, actual);
    }

    public void GetRefreshToken_ById_ReturnRefreshToken()
    {
        var refresTokenService = CreateMock();

        var targetDate = new DateTime(2023, 1, 1);
        
        var expected = new RefresToken("refresh_token2", 1, "finger_print2", targetDate.AddDays(2), targetDate.AddDays(3));
        var actual = refresTokenService.GetById();
    }
}
