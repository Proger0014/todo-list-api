using TodoList.DTO.Token;
using TodoList.Models.RefreshToken;
using TodoList.Exceptions;

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
        var existsUser = _refreshTokenRepository.GetByUserId(userId);

        if (existsUser == null)
        {
            throw new NotFoundException("RefreshToken not found");
        }

        return existsUser;
    }

    public string GenerateRefreshToken(RefreshTokenCreate refreshTokenCreate)
    {
        var token = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            UserId = refreshTokenCreate.UserId,
            FingerPrint = refreshTokenCreate.FingerPrint,
            AddedTime = DateTime.Now,
            ExpirationTime = DateTime.Now.Add(TimeSpan.FromMinutes(10))
        };

        _refreshTokenRepository.Insert(token);

        return token.Id.ToString();
    }

    public void RemoveRefreshToken(RefreshToken refreshToken)
    {
        _refreshTokenRepository.Delete(refreshToken.Id);
    }
}