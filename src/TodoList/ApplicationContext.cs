using Microsoft.EntityFrameworkCore;
using TodoList.Extensions;
using TodoList.Models.RefreshToken;
using TodoList.Models.User;

public class ApplicationContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<RefreshToken> RefreshTokens { get; set; }

	public ApplicationContext()
	{
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		Database.EnsureCreated();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var connectionString = ("DefaultConnection".GetConnectionString());

		optionsBuilder.UseNpgsql(connectionString);
	}
}