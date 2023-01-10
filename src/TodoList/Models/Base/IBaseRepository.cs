using Microsoft.EntityFrameworkCore;
using TodoList.Models.Id;

namespace TodoList.Models.Base;

public interface IBaseRepository<T, E>
    where T : class, ID<E>
{
    public interface GetAll : CommonProps<T, E>
    {
        IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }
    }
    public interface GetById : CommonProps<T, E>
    {
        T? GetById(E id)
        {
            return _table.Find(id);
        }
    }
    public interface Insert : CommonProps<T, E>
    {
        void Insert(T obj)
        {
            _table.Add(obj);
            _context.SaveChanges();
        }
    }
    public interface Update : CommonProps<T, E>
    {
        void Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
    public interface Delete : CommonProps<T, E>
    {
        void Delete(E id)
        {
            var existsEntity = _table.Find(id);

            if (existsEntity == null)
            {
                return;
            }

            _table.Remove(existsEntity);
            _context.SaveChanges();
        }
    }
    public interface Save : CommonProps<T, E>
    {
        void Save()
        {
            _context.SaveChanges();
        }
    }
}
