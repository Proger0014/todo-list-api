using Microsoft.EntityFrameworkCore;

namespace TodoList.Models.Base;

public abstract class BaseRepository<T>
    : IBaseRepository<T> where T : class
{
    private ApplicationContext _context;
    private DbSet<T> _table;

    public BaseRepository(ApplicationContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }

    public void Delete(object id)
    {
        var targetEntity = _table.Find(id);

        if (targetEntity == null)
        {
            throw new Exception("Entity not found");
        }

        _table.Remove(targetEntity);
        Save();
    }

    public IEnumerable<T> GetAll()
    {
        return _table.ToList();
    }

    public T GetById(object id)
    {
        var targetEntity = _table.Find(id);

        if (targetEntity == null)
        {
            throw new Exception("Entity not found");
        }

        return targetEntity;
    }

    public void Insert(T obj)
    {
        _table.Add(obj);
        Save();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public void Update(T obj)
    {
        _table.Attach(obj);
        _context.Entry(obj).State = EntityState.Modified;
        Save();
    }
}