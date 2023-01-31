using TodoList.DTO.Token;
using TodoList.Models.RefreshToken;
using TodoList.Exceptions;
using TodoList.Utils;

namespace TodoList.Services;

public class RefreshTokenService
{
    private IRefreshTokenRepository _refreshTokenRepository;

    public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public RefreshToken GetRefreshToken(string refreshToken)
    {
        var existsRefreshToken = _refreshTokenRepository.GetById(Guid.Parse(refreshToken));

        if (existsRefreshToken == null)
        {
            throw new NotFoundException("RefreshToken not found");
        }

        return existsRefreshToken;
    }

    public RefreshToken GetRefreshTokenByUserId(long userId)
    {
        var existsRefreshToken = _refreshTokenRepository.GetByUserId(userId);

        if (existsRefreshToken == null)
        {
            throw new NotFoundException("RefreshToken not found");
        }

        return existsRefreshToken;
    }

    public RefreshToken GenerateRefreshToken(RefreshTokenCreate refreshTokenCreate)
    {
        int expires = CommonCookieOptions.MaxAgeRefreshToken();

        var token = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            UserId = refreshTokenCreate.UserId,
            FingerPrint = refreshTokenCreate.FingerPrint,
            AddedTime = DateTime.Now,
            ExpirationTime = DateTime.Now.Add(TimeSpan.FromMinutes(expires))
        };

        _refreshTokenRepository.Insert(token);

        return token;
    }

    public void RemoveRefreshToken(RefreshToken refreshToken)
    {
        _refreshTokenRepository.Delete(refreshToken.Id);
    }
}