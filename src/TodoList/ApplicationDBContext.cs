using Microsoft.EntityFrameworkCore;
using TodoList.Models.RefreshToken;
using TodoList.Models.User;

namespace TodoList;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<User>? Users { get; set; }
    public DbSet<RefreshToken>? RefreshTokens { get; set; }
}