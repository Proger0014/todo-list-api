using UnitTests.TestDataCollections.ServicesTests.RefreshTokenServiceTests;
using UnitTests.Utils.ServicesTestsUtils;
using TodoList.Services.DateTimeProvider;
using TodoList.Models.RefreshToken;
using TodoList.Exceptions;
using TodoList.DTO.Token;
using TodoList.Constants;
using TodoList.Services;
using Moq;

namespace UnitTests.ServicesTests;

public class RefreshTokenServiceTests
{
    [Theory]
    [MemberData(
        nameof(RefreshTokenServiceTestsData.RefreshTokensSuit), 
        MemberType = typeof(RefreshTokenServiceTestsData))]
    public void GetRefreshTokenByUserId_ExistsUserId_ReturnRefreshToken(
        RefreshToken expectedRefreshToken,
        IDateTimeProvider dateTimeProvider,
        AuthCookieOptions authCookieOptions)
    {
        // Arrange
        var stubRepo = RefreshTokenServiceTestsFakes
            .CreateByUserIdFake(expectedRefreshToken);
        var refreshTokenService = new RefreshTokenService(
            stubRepo.Object, dateTimeProvider, authCookieOptions);

        // Act
        var actualRefreshToken = refreshTokenService
            .GetRefreshTokenByUserId(expectedRefreshToken.UserId);

        // Assert
        Assert.Equal(expectedRefreshToken, actualRefreshToken);
    }

    [Theory]
    [MemberData(
        nameof(RefreshTokenServiceTestsData.RefreshTokensSuit), 
        MemberType = typeof(RefreshTokenServiceTestsData))]
    public void GetRefreshTokenByUserId_NotExistsUserId_ThrowsNotFoundException(
        RefreshToken refreshToken,
        IDateTimeProvider dateTimeProvider,
        AuthCookieOptions authCookieOptions)
    {
        // Arrange
        var stubRepo = new Mock<IRefreshTokenRepository>();
        var refreshTokenService = new RefreshTokenService(
            stubRepo.Object, dateTimeProvider, authCookieOptions);

        // Act
        Action act = () => refreshTokenService.GetRefreshTokenByUserId(refreshToken.UserId);

        // Assert
        var actualException = Assert.Throws<NotFoundException>(act);
        Assert.Equal(ExceptionMessage.REFRESH_TOKEN_NOT_FOUND, actualException.Message);
    }

    [Theory]
    [MemberData(
        nameof(RefreshTokenServiceTestsData.RefreshTokensSuit), 
        MemberType = typeof(RefreshTokenServiceTestsData))]
    public void GetRefreshToken_ByExistsId_ReturnRefreshToken(
        RefreshToken expectedRefreshToken,
        IDateTimeProvider dateTimeProvider,
        AuthCookieOptions authCookieOptions)
    {
        // Arrange
        var stubRepo = RefreshTokenServiceTestsFakes
            .CreateByIdFake(expectedRefreshToken);
        var refreshTokenService = new RefreshTokenService(
            stubRepo.Object, dateTimeProvider, authCookieOptions);

        // Act
        var actualRefreshToken = refreshTokenService
            .GetRefreshToken(expectedRefreshToken.Id.ToString());

        // Assert
        Assert.Equal(expectedRefreshToken, actualRefreshToken);
    }

    [Theory]
    [MemberData(
        nameof(RefreshTokenServiceTestsData.RefreshTokensSuit), 
        MemberType = typeof(RefreshTokenServiceTestsData))]
    public void GetRefreshToken_ByNotExistsId_ThrowsNotFoundException(
        RefreshToken refreshToken,
        IDateTimeProvider dateTimeProvider,
        AuthCookieOptions authCookieOptions)
    {
        // Arrange
        var stubRepo = new Mock<IRefreshTokenRepository>();
        var refreshTokenService = new RefreshTokenService(
            stubRepo.Object, dateTimeProvider, authCookieOptions);

        // Act
        Action act = () => refreshTokenService.GetRefreshToken(refreshToken.Id.ToString());

        // Assert
        var actualException = Assert.Throws<NotFoundException>(act);
        Assert.Equal(ExceptionMessage.REFRESH_TOKEN_NOT_FOUND, actualException.Message);
    }

    [Theory]
    [MemberData(
        nameof(RefreshTokenServiceTestsData.CollectionDataForGenerateRefreshToken), 
        MemberType = typeof(RefreshTokenServiceTestsData))]
    public void GenerateRefreshToken_ValidRefreshTokenCreateDTO_ReturnValidRefreshToken(
        RefreshTokenCreate refreshTokenCreateDTO,
        IDateTimeProvider dateTimeProvider,
        AuthCookieOptions authCookieOptions)
    {
        // Arrange
        var mockRepo = new Mock<IRefreshTokenRepository>();
        var refreshTokenService = new RefreshTokenService(
            mockRepo.Object, dateTimeProvider, authCookieOptions);

        // Act
        RefreshToken newRefreshToken = refreshTokenService
            .GenerateRefreshToken(refreshTokenCreateDTO);

        // Assert
        mockRepo.Verify(rtr => rtr.Insert(newRefreshToken));
    }

    [Theory]
    [MemberData(
        nameof(RefreshTokenServiceTestsData.RefreshTokensSuit),
        MemberType = typeof(RefreshTokenServiceTestsData))]
    public void RemoveRefreshToken_ValidRefreshTokenId_Removed(
        RefreshToken refreshTokenForRemove,
        IDateTimeProvider dateTimeProvider,
        AuthCookieOptions authCookieOptions)
    {
        // Arrange
        var mockRepo = new Mock<IRefreshTokenRepository>();

        var refreshTokenService = new RefreshTokenService(
            mockRepo.Object, dateTimeProvider, authCookieOptions);

        // Act
        refreshTokenService.RemoveRefreshToken(refreshTokenForRemove);

        // Assert
        mockRepo.Verify(repo => repo.Delete(refreshTokenForRemove.Id));
    }
}
