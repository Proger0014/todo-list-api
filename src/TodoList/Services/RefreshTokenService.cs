using TodoList.DTO.Token;
using TodoList.Models.RefreshToken;

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
        try
        {
            return _refreshTokenRepository.GetById(Guid.Parse(refreshToken));
        }
        catch (Exception)
        {
            return null;
        }
    }

    public RefreshToken GetRefreshTokenByUserId(long userId)
    {
        try
        {
            return _refreshTokenRepository.GetByUserId(userId);
        }
        catch (Exception)
        {
            return null;
        }

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