using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ProductBox_Task.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        DbSet<T> dbset();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll();
        T GetById(object id);
        string Insert(T obj);
        string Update(T obj);
        string Delete(object id);
        string SaveChanges();
    }
}
