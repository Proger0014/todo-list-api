using TodoList.Models.RefreshToken;
using TodoList.Models.User;

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
		return _refreshTokenRepository.GetByRefreshToken(refreshToken);
    }

	public string GenerateRefreshToken(User user)
	{
		var token = new RefreshToken(
            Guid.NewGuid().ToString(),
			user.Id,
			DateTime.Now,
			DateTime.Now.Add(TimeSpan.FromMinutes(10)));

		_refreshTokenRepository.AddRefreshToken(token);

		return token.Token;
	}

	public void RemoveRefreshToken(RefreshToken refreshToken)
	{
		_refreshTokenRepository.RemoveRefreshToken(refreshToken);
	}
}