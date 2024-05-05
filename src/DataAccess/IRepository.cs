using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepository<T>
    {
        IEnumerable<T> FindAll();
        T FindById(object id);
        IEnumerable<T> FindByPattern(string pattern);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
