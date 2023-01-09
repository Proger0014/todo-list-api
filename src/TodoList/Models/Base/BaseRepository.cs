using Microsoft.EntityFrameworkCore;
using TodoList.Models.Id;

namespace TodoList.Models.Base;

public abstract class BaseRepository<T, E> : IBaseRepository<T, E>
    where T : class, ID<E>
{
    public ApplicationDBContext _context { get; set; }
    public DbSet<T> _table { get; set; }


    public BaseRepository(ApplicationDBContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }
}