using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepository<T>
    {
        int NextBookId();
        IEnumerable<T> FindAll();
        T FindById(int? id);
        IEnumerable<T> FindByEntityId(int? id, Type type);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}
