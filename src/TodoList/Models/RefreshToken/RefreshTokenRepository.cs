using TodoList.Models.Base;

namespace TodoList.Models.RefreshToken;

public class RefreshTokenRepository :
    BaseRepository<RefreshToken>,
    IRefreshTokenRepository 
{
    private ApplicationContext _context;

    public RefreshTokenRepository(ApplicationContext context)
        : base(context)
    {
        _context = context;
    }

    public RefreshToken GetByUserId(long userId)
    {
        var rf = _context.RefreshTokens.SingleOrDefault(rt => rt.UserId == userId);

        if (rf == null)
        {
            throw new Exception("Not found refresh token");
        }

        return rf;
    }
}