using Moq;
using TodoList.Models.RefreshToken;
using TodoList.DTO.Token;
using TodoList.Services;
using TodoList.Exceptions;
using UnitTests.Utils.ServicesTestsUtils;
using UnitTests.TestDataCollections.ServicesTests.RefreshTokenServiceTests;
using TodoList.Constants;

namespace UnitTests.ServicesTests;

public class RefreshTokenServiceTests
{
    // Плохо, потому что одно состояние для нескольких тестов 
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

        foreach (object[] objs in RefreshTokenServiceTestData.RefreshTokens)
        {
            foreach (RefreshToken refreshToken in objs)
            {
                mock.Setup(rt => rt.GetById(refreshToken.Id))
                    .Returns(refreshToken);
                mock.Setup(rt => rt.GetByUserId(refreshToken.UserId))
                    .Returns(refreshToken);
            }
        }

        return mock;
    }

    // T - is type of exception
    private void TestTemplateWithThrowsException<T>(
        Action guessAction, 
        RefreshTokenService refreshTokenService,
        string expectedExceptionMessage)
        where T : Exception
    {
        T exception = Assert.Throws<T>(guessAction);
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [Theory]
    [MemberData(nameof(RefreshTokenServiceTestData.RefreshTokens), MemberType = typeof(RefreshTokenServiceTestData))]
    public void GetRefreshTokenByUserId_ExistsUserId_ReturnRefreshToken(RefreshToken expectedRefreshToken)
    {
        // Arrange
        var stubRepo = RefreshTokenServiceTestsFakes.CreateByUserIdFake(expectedRefreshToken);
        var refreshTokenService = new RefreshTokenService(stubRepo.Object);

        // Act
        var actualRefreshToken = refreshTokenService.GetRefreshTokenByUserId(expectedRefreshToken.UserId);

        // Assert
        Assert.Equal(expectedRefreshToken, actualRefreshToken);
    }

    [Theory]
    [MemberData(nameof(RefreshTokenServiceTestData.RefreshTokens), MemberType = typeof(RefreshTokenServiceTestData))]
    public void GetRefreshTokenByUserId_NotExistsUserId_ThrowsNotFoundException(RefreshToken refreshToken)
    {
        // Arrange
        var stubRepo = new Mock<IRefreshTokenRepository>();
        var refreshTokenService = new RefreshTokenService(stubRepo.Object);

        // Act
        Action act = () => refreshTokenService.GetRefreshTokenByUserId(refreshToken.UserId);

        // Assert
        var actualException = Assert.Throws<NotFoundException>(act);
        Assert.Equal(ExceptionMessage.REFRESH_TOKEN_NOT_FOUND, actualException.Message);
    }

    [Theory]
    [MemberData(nameof(RefreshTokenServiceTestData.RefreshTokens), MemberType = typeof(RefreshTokenServiceTestData))]
    public void GetRefreshToken_ByExistsId_ReturnRefreshToken(RefreshToken expectedRefreshToken)
    {
        // Arrange
        var stubRepo = RefreshTokenServiceTestsFakes.CreateByIdFake(expectedRefreshToken);
        var refreshTokenService = new RefreshTokenService(stubRepo.Object);

        // Act
        var actualRefreshToken = refreshTokenService.GetRefreshToken(expectedRefreshToken.Id.ToString());

        // Assert
        Assert.Equal(expectedRefreshToken, actualRefreshToken);
    }

    [Theory]
    [MemberData(nameof(RefreshTokenServiceTestData.RefreshTokens), MemberType = typeof(RefreshTokenServiceTestData))]
    public void GetRefreshToken_ByNotExistsId_ThrowsNotFoundException(RefreshToken refreshToken)
    {
        // Arrange
        var stubRepo = new Mock<IRefreshTokenRepository>();
        var refreshTokenService = new RefreshTokenService(stubRepo.Object);

        // Act
        Action act = () => refreshTokenService.GetRefreshToken(refreshToken.Id.ToString());

        // Assert
        var actualException = Assert.Throws<NotFoundException>(act);
        Assert.Equal(ExceptionMessage.REFRESH_TOKEN_NOT_FOUND, actualException.Message);
    }

    // TODO: Отрефакторить это
    // Также подумать об Инверсии зависимостей для CommonCookieOptions
    [Theory]
    [MemberData(nameof(RefreshTokenServiceTestData.RefreshTokenCreateDTOs), MemberType = typeof(RefreshTokenServiceTestData))]
    public void GenerateRefreshToken_ValidRefreshTokenCreateDTO_ReturnValidRefreshToken(RefreshTokenCreate refreshTokenCreateDTO)
    {
        // Arrange
        var mockRepo = new Mock<IRefreshTokenRepository>();
        var refreshTokenService = new RefreshTokenService(mockRepo.Object);

        // Act
        refreshTokenService.GenerateRefreshToken(refreshTokenCreateDTO);

        // Assert
        mockRepo.Verify(rtr => rtr.Insert);
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
