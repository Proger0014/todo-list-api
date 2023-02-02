using Microsoft.EntityFrameworkCore;
using TodoList.Models.Id;

namespace TodoList.Models.Base;

public abstract class CommonProps<T, E> : ICommonProps<T, E>
    where T : class, ID<E>
{
    public ApplicationDBContext _context { get; set; }
    public DbSet<T> _table { get; set; }


    public CommonProps(ApplicationDBContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }
}