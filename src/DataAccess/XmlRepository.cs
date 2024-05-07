using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DataAccess
{
    public class XmlRepository<T> : IRepository<T> where T : class, new()
    {
        private XElement xLibrary; // ==>> Maybe use XDocument to represent ENTIRE library??
        private readonly string filePath = "Library.xml"; // debug folder in entry point project
        private List<T> entities; // List of ONLY T entities. TODO: MUST be updated after each operation that changes library state

        public XmlRepository()
        {
            entities = new List<T>();
            xLibrary = LoadFile();
            var xList = from el in xLibrary.Elements(typeof(T).Name) select el;
            foreach (var el in xList)
            {
                entities.Add(el.ToEntity<T>());
            }
        }
        private XElement LoadFile()
        {
            return Utilities.ReadFromFile(filePath);
        }

        public void Add(T? entity)
        {
            if (entity is null)
            {
                Console.WriteLine($"Could not add {typeof(T).Name}");
            }
            entities.Add(entity);

            //SaveChanges();
        }

        public void Delete(T entity)
        {
            // FindById(entityToDeleteId);
            T ToDelete = FindById((int)typeof(T).GetProperty($"{typeof(T).Name}Id").GetValue(entity));

            // if null return
            if (ToDelete is null)
            { return; }

            // Delete entity from entities object
            entities.Remove(ToDelete);

            //SaveChanges();
        }

        //TODO : check this -> https://learn.microsoft.com/en-us/dotnet/standard/linq/find-descendants-specific-element-name

        public IEnumerable<T> FindAll()
        {
            List<T> entities = new List<T>();

            var xElements = from el in xLibrary.Descendants(typeof(T).Name) select el;
            foreach (var el in xElements)
            {
                T? entity = el.ToEntity<T>();
                entities.Add(entity);
            }
            return entities;
        }

        public T? FindById(int? id)
        {
            PropertyInfo? idProperty = typeof(T).GetProperty($"{typeof(T).Name}Id");

            bool isIdInteger = idProperty.PropertyType == typeof(int);

            if (idProperty == null || idProperty.PropertyType != typeof(int))
            {
                return null;
            }
            return FindAll().FirstOrDefault(r => isIdInteger && (int)idProperty.GetValue(r) == id);
        }

        public IEnumerable<T>? FindByEntityId(int? entityId, Type entityType)
        {
            PropertyInfo? entityIdProperty = entityType.GetProperty($"{entityType.Name}Id");

            if (entityIdProperty == null || entityIdProperty.PropertyType != typeof(int))// || !typeof(T).GetProperties().Contains(typeof(T).GetProperty(entityIdProperty.Name)))
            {
                // error message specific for invalid id
                return null;
            }
            return FindAll().Where(result => entityId == (int?) entityIdProperty.GetValue(result));
        }


        public void SaveChanges()
        {
            // Get the root element for this type
            var root = xLibrary.Element(typeof(T).Name + "s");

            // Remove the existing elements of this type
            //root.Elements(typeof(T).Name).Remove();

            // Convert each entity in the entities list to an XElement and add it to the root
            foreach (var entity in entities)
            {
                XElement entityElement = Utilities.FromEntity(entity);
                root.Add(entityElement);
            }

            // Save the updated library back to the XML file
            xLibrary.Save(filePath);

        }

        public void Update(T entity)
        {
            int entityId = (int)entity.GetType().GetProperty($"{entity.GetType().Name}Id").GetValue(entity);
            T? entityToUpdate = FindById(entityId);

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                property.SetValue(entityToUpdate, property.GetValue(entity));
            }

            //SaveChanges();
        }
    }
}
