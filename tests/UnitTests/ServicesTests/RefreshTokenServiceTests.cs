using Moq;
using TodoList.Models.RefreshToken;
using TodoList.DTO.Token;
using TodoList.Services;
using TodoList.Exceptions;

namespace UnitTests.ServicesTests;

public class RefreshTokenServiceTests
{
    private static readonly DateTime ADDED_TIME_FOR_RT1 = new DateTime(2023, 1, 1);
    private static readonly DateTime EXPIRATION_TIME_FOR_RT1 = ADDED_TIME_FOR_RT1.AddMinutes(1);
    private const int USER_ID_FOR_RT1 = 1;
    private const string FINGER_PRINT_FOR_RT1 = "finger_print1";
    private const string REFRESH_TOKEN1_ID_STRING = "12222222-2222-2222-2222-222222222222";

    private static readonly DateTime ADDED_TIME_FOR_RT2 = ADDED_TIME_FOR_RT1.AddDays(2);
    private static readonly DateTime EXPIRATION_TIME_FOR_RT2 = ADDED_TIME_FOR_RT1.AddDays(3);
    private const int USER_ID_FOR_RT2 = 2;
    private const string FINGER_PRINT_FOR_RT2 = "finger_print2";
    private const string REFRESH_TOKEN2_ID_STRING = "21111111-1111-1111-1111-111111111111";

    private const string NOT_EXISTS_RT_ID_STRING = "21111111-1111-1111-1111-211311111111";
    private const int NOT_EXISTS_USER_ID_FOR_RT = 3;

    // exception messages
    private const string REFRESH_TOKEN_NOT_FOUND = "refresh token not found";

    private Mock<IRefreshTokenRepository> CreateMock()
    {
        var mock = new Mock<IRefreshTokenRepository>();

        var targetDate = new DateTime(2023, 1, 1);

        var refreshToken1Id = Guid.Parse(REFRESH_TOKEN1_ID_STRING);
        var refreshToken2Id = Guid.Parse(REFRESH_TOKEN2_ID_STRING);

        var refreshToken1 = new RefreshToken(refreshToken1Id, USER_ID_FOR_RT1, 
            FINGER_PRINT_FOR_RT1, ADDED_TIME_FOR_RT1, EXPIRATION_TIME_FOR_RT1);
        var refreshToken2 = new RefreshToken(refreshToken2Id, USER_ID_FOR_RT2, 
            FINGER_PRINT_FOR_RT2, ADDED_TIME_FOR_RT2, EXPIRATION_TIME_FOR_RT2);

        mock.Setup(rt => rt.GetByUserId(USER_ID_FOR_RT1))
            .Returns(refreshToken1);

        mock.Setup(rt => rt.GetById(refreshToken1Id))
            .Returns(refreshToken1);

        mock.Setup(rt => rt.GetByUserId(USER_ID_FOR_RT2))
            .Returns(refreshToken2);

        mock.Setup(rt => rt.GetById(refreshToken2Id))
            .Returns(refreshToken2);

        return mock;
    }

    // T - is type of exception
    private void TestTemplate_With_ThrowException<T>(Action<RefreshTokenService> guessAction, string expectedExceptionMessage)
        where T : Exception
    {
        var refreshTokenService = new RefreshTokenService(CreateMock().Object);

        var exception = Assert.Throws<T>(() => guessAction(refreshTokenService));

        var actualExceptionMessage = exception.Message;

        Assert.Equal(expectedExceptionMessage, actualExceptionMessage);
    }

    [Fact]
    public void GetRefreshToken_ByUserId_ReturnRefreshToken()
    {
        var refreshTokenService = new RefreshTokenService(CreateMock().Object);

        var refreshToken1Id = Guid.Parse(REFRESH_TOKEN1_ID_STRING);

        var expected = new RefreshToken(refreshToken1Id, USER_ID_FOR_RT1, 
            FINGER_PRINT_FOR_RT1, ADDED_TIME_FOR_RT1, EXPIRATION_TIME_FOR_RT1);
        var actual = refreshTokenService.GetRefreshTokenByUserId(1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetRefreshToken_NotExistsUserId_ThrowException()
    {
        TestTemplate_With_ThrowException<NotFoundException>(
            (refreshTokenService) => refreshTokenService.GetRefreshTokenByUserId(NOT_EXISTS_USER_ID_FOR_RT),
            REFRESH_TOKEN_NOT_FOUND);
    }

    [Fact]
    public void GetRefreshToken_ById_ReturnRefreshToken()
    {
        var refreshTokenService = new RefreshTokenService(CreateMock().Object);

        var refreshToken2Id = Guid.Parse(REFRESH_TOKEN2_ID_STRING);

        var expected = new RefreshToken(refreshToken2Id, USER_ID_FOR_RT2, 
            FINGER_PRINT_FOR_RT2, ADDED_TIME_FOR_RT2, EXPIRATION_TIME_FOR_RT2);
        var actual = refreshTokenService.GetRefreshToken(refreshToken2Id.ToString());

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetRefreshToken_NotExistsId_ThrowException()
    {
        TestTemplate_With_ThrowException<NotFoundException>(
            (refreshTokenService) => refreshTokenService.GetRefreshToken(NOT_EXISTS_RT_ID_STRING),
            REFRESH_TOKEN_NOT_FOUND);
    }

    [Fact]
    public void GenerateRefreshToken_ReturnValidRefreshToken()
    {
        var mock = CreateMock();

        var refreshTokenService = new RefreshTokenService(mock.Object);

        var newRefreshToken = refreshTokenService.GenerateRefreshToken(new RefreshTokenCreate()
        {
            UserId = USER_ID_FOR_RT1,
            FingerPrint = FINGER_PRINT_FOR_RT1
        });

        // проверяет по ссылке
        mock.Verify(repo => repo.Insert(newRefreshToken));

        Assert.True(true);
    }

    [Fact]
    public void DeleteRefreshToken()
    {
        var mock = CreateMock();

        var refreshTokenService = new RefreshTokenService(mock.Object);

        var newRefreshToken = refreshTokenService.GenerateRefreshToken(new RefreshTokenCreate()
        {
            UserId = USER_ID_FOR_RT1,
            FingerPrint = FINGER_PRINT_FOR_RT1
        });

        refreshTokenService.RemoveRefreshToken(newRefreshToken);

        mock.Verify(repo => repo.Delete(newRefreshToken.Id));

        Assert.True(true);
    }
}
