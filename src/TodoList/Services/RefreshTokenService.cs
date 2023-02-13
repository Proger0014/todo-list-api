using TodoList.DTO.Token;
using TodoList.Models.RefreshToken;
using TodoList.Exceptions;
using TodoList.Constants;
using TodoList.Services.DateTimeProvider;

namespace TodoList.Services;

public class RefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly AuthCookieOptions _authCookieOptions;

    public RefreshTokenService(
        IRefreshTokenRepository refreshTokenRepository,
        IDateTimeProvider dateTimeProvider,
        AuthCookieOptions authCookieOptions)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _dateTimeProvider = dateTimeProvider;
        _authCookieOptions = authCookieOptions;
    }

    public RefreshToken GetRefreshToken(string refreshToken)
    {
        var existsRefreshToken = _refreshTokenRepository.GetById(Guid.Parse(refreshToken));

        if (existsRefreshToken == null)
        {
            throw new NotFoundException(ExceptionMessage.REFRESH_TOKEN_NOT_FOUND);
        }

        return existsRefreshToken;
    }

    public RefreshToken GetRefreshTokenByUserId(long userId)
    {
        var existsRefreshToken = _refreshTokenRepository.GetByUserId(userId);

        if (existsRefreshToken == null)
        {
            throw new NotFoundException(ExceptionMessage.REFRESH_TOKEN_NOT_FOUND);
        }

        return existsRefreshToken;
    }

    public RefreshToken GenerateRefreshToken(RefreshTokenCreate refreshTokenCreate)
    {
        int expires = _authCookieOptions.MaxAgeRefreshToken;

        var token = new RefreshToken()
        {
            Id = refreshTokenCreate.Id,
            UserId = refreshTokenCreate.UserId,
            FingerPrint = refreshTokenCreate.FingerPrint,
            AddedTime = _dateTimeProvider.DateTimeNow,
            ExpirationTime = _dateTimeProvider.DateTimeNow.Add(TimeSpan.FromMinutes(expires))
        };

        _refreshTokenRepository.Insert(token);

        return token;
    }

    public void RemoveRefreshToken(RefreshToken refreshToken)
    {
        _refreshTokenRepository.Delete(refreshToken.Id);
    }
}