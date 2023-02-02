using Microsoft.EntityFrameworkCore;
using TodoList.Models.Id;

namespace TodoList.Models.Base;

public interface ICommonProps<T, E>
    where T : class, ID<E>
{
    ApplicationDBContext _context { get; set; }
    DbSet<T> _table { get; set; }
}