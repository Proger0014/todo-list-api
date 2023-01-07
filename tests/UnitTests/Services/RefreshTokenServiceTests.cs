using Moq;
using TodoList.Models.RefreshToken;
using TodoList.Services;

namespace UnitTests.Services;

public class RefreshTokenServiceTests
{
    private RefreshTokenService CreateMock()
    {
        var mock = new Mock<RefreshTokenRepository>();

        var targetDate = new DateTime(2023, 1, 1);

        mock.Setup(rr => rr.GetByUserId(1))
            .Returns(new RefreshToken("refresh_token1", 1, "finger_print1", targetDate, targetDate.AddMinutes(1)));

        mock.Setup(rr => rr.GetByRefreshToken("refresh_token2"))
            .Returns(new RefreshToken("refresh_token2", 2, "finger_print2", targetDate.AddDays(2), targetDate.AddDays(3)));

        return new RefreshTokenService(mock.Object);
    }

    [Fact]
    public void GetRefreshToken_ExistsRefreshTokenId_ReturnRefreshToken()
    {
        var refreshTokenService = CreateMock();

        var targetDate = new DateTime(2023, 1, 1);

        var expected = new RefreshToken("refresh_token1", 1, "finger_print1", targetDate, targetDate.AddMinutes(1));
        var actual = refreshTokenService.GetRefreshToken("refresh_token2");

        Assert.Equal(expected, actual);
    }
}
