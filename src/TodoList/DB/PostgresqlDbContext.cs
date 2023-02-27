using Microsoft.EntityFrameworkCore;
using TodoList.Models.RefreshToken;
using TodoList.Models.User;
using TodoList.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TodoList.DB;

public class PostgresqlDbContext : DbContext, IApplicationDbContext
{
    public PostgresqlDbContext(DbContextOptions<PostgresqlDbContext> options) 
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }


    public override DatabaseFacade Database =>
        base.Database;

    public DbSet<User>? Users { get; set; }
    public DbSet<RefreshToken>? RefreshTokens { get; set; }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }

    public override DbSet<TEntity> Set<TEntity>()
        where TEntity : class
    {
        return base.Set<TEntity>();
    }

    public override EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
        where TEntity : class
    {
        return base.Entry(entity);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        new UserTypeConfiguration()
            .Configure(modelBuilder.Entity<User>());
        new RefreshTokenTypeConfiguration()
            .Configure(modelBuilder.Entity<RefreshToken>());
    }
}