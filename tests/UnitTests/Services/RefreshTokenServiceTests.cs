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

        var refreshToken1Id = Guid.Parse("12222222-2222-2222-2222-222222222222");
        var refreshToken2Id = Guid.Parse("21111111-1111-1111-1111-111111111111");

        var refreshToken1 = new RefreshToken(refreshToken1Id, 1, "finger_print1", targetDate, targetDate.AddMinutes(1));
        var refreshToken2 = new RefreshToken(refreshToken2Id, 2, "finger_print2", targetDate.AddDays(2), targetDate.AddDays(3));

        mock.Setup(rr => rr.GetByUserId(1))
            .Returns(refreshToken1);

        mock.Setup(rr => rr.GetById(refreshToken1Id))
            .Returns(refreshToken1);

        mock.Setup(rr => rr.GetById(refreshToken2Id))
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

        var expected = new RefreshToken(Guid.Parse("12222222-2222-2222-2222-222222222222"), 1, "finger_print1", targetDate, targetDate.AddMinutes(1));
        var actual = refreshTokenService.GetRefreshTokenByUserId(1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetRefreshToken_ById_ReturnRefreshToken()
    {
        var refreshTokenService = CreateMock();

        var targetDate = new DateTime(2023, 1, 1);
        
        var refreshToken2Id = Guid.Parse("21111111-1111-1111-1111-111111111111");

        var expected = new RefreshToken(refreshToken2Id, 2, "finger_print2", targetDate.AddDays(2), targetDate.AddDays(3));
        var actual = refreshTokenService.GetRefreshToken(refreshToken2Id.ToString());

        Assert.Equal(expected, actual);
    }
}
