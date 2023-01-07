using TodoList.DTO.Token;
using TodoList.Models.RefreshToken;

namespace TodoList.Services;

public class RefreshTokenService
{
    private RefreshTokenRepository _refreshTokenRepository;

	public RefreshTokenService(RefreshTokenRepository refreshTokenRepository)
	{
		_refreshTokenRepository = refreshTokenRepository;
	}

	public RefreshToken GetRefreshToken(string refreshToken)
	{
		try
		{
			return _refreshTokenRepository.GetById(refreshToken);
		} catch (Exception)
		{
			return null;
		}
    }

	public RefreshToken GetRefreshTokenByUserId(long userId)
	{
		try
		{
            return _refreshTokenRepository.GetByUserId(userId);
        } catch (Exception)
		{
			return null;
		}

    }

	public string GenerateRefreshToken(RefreshTokenCreate refreshTokenCreate)
	{
		var token = new RefreshToken(
            Guid.NewGuid().ToString(),
			refreshTokenCreate.UserId,
			refreshTokenCreate.FingerPrint,
			DateTime.Now,
			DateTime.Now.Add(TimeSpan.FromMinutes(10)));

		_refreshTokenRepository.Insert(token);

		return token.Token;
	}

	public void RemoveRefreshToken(RefreshToken refreshToken)
	{
		_refreshTokenRepository.Delete(refreshToken.Token);
	}
}