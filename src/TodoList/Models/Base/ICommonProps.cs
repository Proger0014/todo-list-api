using Microsoft.EntityFrameworkCore;
using TodoList.DB;
using TodoList.Models.Id;

namespace TodoList.Models.Base;

public interface ICommonProps<T, E>
    where T : class, ID<E>
{
    IApplicationDbContext _context { get; set; }
    DbSet<T> _table { get; set; }
}