using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess
{
    public class XmlRepository<T> : IRepository<T>  where T : class, new()
    {
        private XElement xLibrary;
        private readonly string filePath = "library.xml"; // debug folder in entry point project
        private List<T> entities = new();

        public XmlRepository()
        {
            xLibrary = LoadFile();
            var xList = from el in xLibrary.Elements(typeof(T).Name) select el;
            foreach(var el in xList)
            {
                entities.Add(el.ToEntity<T>())
            }
        }
        private XElement? LoadFile()
        {
            if (!File.Exists(filePath))
            {
                //return new XElement();
                return null;
            }
            return XElement.Load(filePath);
        }

        private void WriteFile(XElement doc)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    // create new file 
                    using (FileStream fs = File.Create(filePath))
                    { }
                }
                // write doc to new file
                doc.Save(filePath);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }


        }

        public void Add(T entity)
        {


        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        //TODO : check this -> https://learn.microsoft.com/en-us/dotnet/standard/linq/find-descendants-specific-element-name
        public List<T> FindAll()
        {
            List<T> list = new List<T>();
            IEnumerable<XElement> xElements = 
                from el in xLibrary.Descendants(typeof(T).Name)
                select el;
            //foreach (var node in xmlDB.Descendants().Where(d => d.Name == typeof(T).Name))
            //foreach (XElement element in xElements)
            //{
            //    T entity = element.ToEntity<T>();
            //    list.Add(entity);

            //}
            return list;

        }

        public T? FindById(object id)
        {
            T? result = FindAll().FirstOrDefault(r => r.GetType() == typeof(T) && r.GetType().GetProperty($"{r.GetType().Name}Id").GetValue(r) == id);
            return result;
        }

        public List<T> FindByPattern(string pattern)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            object? entityId = entity.GetType().GetProperty($"{entity.GetType()}Id").GetValue(entity);
            T? entityToUpdate = FindById(entityId);


        }

        IEnumerable<T> IRepository<T>.FindAll()
        {
            throw new NotImplementedException();
        }

        IEnumerable<T> IRepository<T>.FindByPattern(string pattern)
        {
            throw new NotImplementedException();
        }
        
        ////this is for serialization
        ///
        //public T Deserialize();
    }
}
