using TodoList.Models.Base;

namespace TodoList.Models.RefreshToken;

public class RefreshTokenRepository :
    BaseRepository<RefreshToken, Guid>,
    IRefreshTokenRepository
{
    public RefreshTokenRepository(ApplicationDBContext context)
        : base(context) { }

    public RefreshToken? GetByUserId(long userId)
    {
        return _context.RefreshTokens?
            .SingleOrDefault(rt => rt.UserId == userId);
    }
}