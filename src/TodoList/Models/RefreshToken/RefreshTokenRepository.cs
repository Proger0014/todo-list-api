using TodoList.DB;
using TodoList.Models.Base;

namespace TodoList.Models.RefreshToken;

public class RefreshTokenRepository :
    CommonProps<RefreshToken, Guid>,
    IRefreshTokenRepository
{
    public RefreshTokenRepository(IApplicationDbContext context)
        : base(context) { }

    public RefreshToken? GetByUserId(long userId)
    {
        return _context.RefreshTokens?
            .SingleOrDefault(rt => rt.UserId == userId);
    }
}