using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TodoList.Models.RefreshToken;
using TodoList.Models.User;

namespace TodoList.DB;

public interface IApplicationDbContext : IDisposable
{
    DatabaseFacade Database { get; }

    DbSet<User>? Users { get; set; }
    DbSet<RefreshToken>? RefreshTokens { get; set; }

    int SaveChanges();
    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
        where TEntity : class;
}