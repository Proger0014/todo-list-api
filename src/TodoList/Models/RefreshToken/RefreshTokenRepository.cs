namespace TodoList.Models.RefreshToken;

public class RefreshTokenRepository
{
    private ApplicationContext _context;

    public RefreshTokenRepository(ApplicationContext context)
    {
        _context = context;
    }

    public RefreshToken GetByRefreshToken(string refreshToken)
    {
        var rf = _context.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken);

        if (rf == null)
        {
            throw new Exception("Not found refresh token");
        }

        return rf;
    }

    public void AddRefreshToken(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Add(refreshToken);
        _context.SaveChanges();
    }

    public void RemoveRefreshToken(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Remove(refreshToken);
        _context.SaveChanges();
    }
}