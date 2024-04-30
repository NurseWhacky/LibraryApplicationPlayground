using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess
{
    public class XmlRepository<T> : IRepository<T> where T : class, new()
    {
        //private List<T> entities;
        private XDocument xmlDB;
        private readonly string filePath = "library.xml"; // debug folder in entry point project
        //private Utilities util;

        public XmlRepository()
        {
            //xmlDB = LoadFile();
        }
        public XDocument LoadFile()
        {
            if (!File.Exists(filePath))
            {
                return new XDocument(new XElement("Library"));
            }
            return XDocument.Load(filePath);
        }

        public void Add(T entity)
        {


        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll()
        {
            List<T> list = new List<T>();
            xmlDB = LoadFile();
            foreach (var node in xmlDB.Descendants().Where(d => d.Name == typeof(T).Name))
            {
                if (node is XElement element)
                {
                    T entity = element.ToEntity<T>();
                    list.Add(entity);
                }
            }
            return list;

        }

        public T? FindById(object id)
        {
            T? result = FindAll().FirstOrDefault(r => r.GetType() == typeof(T) && r.GetType().GetProperty("Id").GetValue(r) == id);
            return result;
            //throw new NotImplementedException();
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
