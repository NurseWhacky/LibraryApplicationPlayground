using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataSetRepositoryForXml<T> : IRepository<T> where T : class, new()
    {
        private DataSet dataset;
        private DataTable table;
        private readonly string path = "library.xml";

        public DataSetRepositoryForXml()
        {
            dataset = new DataSet();
            if (File.Exists(path))
            {

            dataset.ReadXml(path);
            }
            else
            {
                Utils.PopulateLibrary();
            }
            table = dataset.Tables[typeof(T).Name];
        }




        public void Add(T entity)
        {
            DataTable entityTable = new(typeof(T).Name);
            entityTable.Rows.Add(entity);
            dataset.Tables.Add(entityTable);
            dataset.WriteXml(path);
            
            
        }

        public void Delete(T entity)
        {
           
        }

        public IEnumerable<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindByEntityId(int? id, Type type)
        {
            throw new NotImplementedException();
        }

        public T FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public int NextBookId()
        {
            throw new NotImplementedException();
        }
    }
}
