using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class XmlRepository<T> : IRepository<T> where T : class, new()
    {
        private IEnumerable<T> entities;
        private readonly string xmlDatabase = "Library.xml"; // debug folder in entry point project
        //private Utilities util;

        public void Add(T entity)
        {

        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll()
        {
            //throw new NotImplementedException();
            return entities;

        }

        public T FindById(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindByPattern(string pattern)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
