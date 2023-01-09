using Microsoft.EntityFrameworkCore;
using TodoList.Extensions;
using TodoList.Models.RefreshToken;
using TodoList.Models.User;

namespace TodoList;

public class ApplicationDBContext : DbContext
{
	/**
	 * Сначала поля, потом конструктор, потом свойства, потом методы
	 * 
	 * Смотреть 1:09:00 на стриме
	 */
	public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
		: base(options)
	{
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		// Database.EnsureCreated(); // плохое решение, для создание бд использовать миграции или другое решение
	}

	public DbSet<User> Users { get; set; }
	public DbSet<RefreshToken> RefreshTokens { get; set; }
}