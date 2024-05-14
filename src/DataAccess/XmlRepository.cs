using System.Reflection;
using System.Xml.Linq;

namespace DataAccess
{
    public class XmlRepository<T> : IRepository<T> where T : class, new()
    {
        private XElement xLibrary;
        public XElement XLibrary { get { return xLibrary; } }
        private List<T> entities; // TODO: MUST be updated after each operation that changes library state
        public List<T> Entities
        {
            get
            {
                return entities != null ? entities : new List<T>();
            }
            set
            {
                if (value is List<T> && (entities.Count() == 0 || entities is null))
                { entities = value; }
                if (entities != null && value.Count() != 0)
                {
                    entities.Clear(); 
                    foreach (T entity in value)
                    { entities.Add(entity); }
                }
                else
                { entities = new List<T>(); }
            }
        }
        private int nextBookId = 0;

        public int NextBookId()
        {
            if (nextBookId == 0)
            {
                // TODO: Da rivedere -> non modifica attributo su file!
                Utilities.GetLastUsedId<T>(out int lastUsedId);
                nextBookId = ++lastUsedId;
                Utilities.UpdateLastUsedId<T>(lastUsedId);
            }
            return nextBookId;

        }
            //get
            //{
            //    if (nextBookId == 0)
            //    {
            //        //GetLastUsedId(out int lastUsedId);
            //        Utilities.GetLastUsedId<T>(out int lastUsedId);
            //        nextBookId = lastUsedId++;
            //        //nextBookId++;

            //    }
            //    return nextBookId;
            //}
            //set
            //{
            //    nextBookId = value;
            //    Utilities.UpdateLastUsedId<T>(value);
            //}
        //}?

        public XmlRepository()
        {
            entities = new List<T>();
            xLibrary = LoadFile();
            // now it works!
            foreach (var el in xLibrary.Descendants($"{typeof(T).Name}"))
            {
                T entity = el.ToEntity<T>();
                entities.Add(entity);
            }
        }

        private XElement LoadFile()
        {
            return Utilities.ReadFromFile();
        }

        public void Add(T? entity)
        {
            if (entity is null)
            {
                Console.WriteLine($"Could not add {typeof(T).Name}");
            }
            entities.Add(entity);
        }

        public void Delete(T entity)
        {
            T? toDelete = FindById((int)typeof(T).GetProperty($"{typeof(T).Name}Id").GetValue(entity));

            // if null return
            if (toDelete is null)
            { return; }

            // Delete entity from entities object
            entities.Remove(toDelete);
        }

       
        //TODO : check this -> https://learn.microsoft.com/en-us/dotnet/standard/linq/find-descendants-specific-element-name

        public IEnumerable<T> FindAll()
        {
            List<T> allEntities = new List<T>();

            var xElements = from el in xLibrary.Descendants(typeof(T).Name) select el;
            foreach (var el in xElements)
            {
                T? entity = el.ToEntity<T>();
                allEntities.Add(entity);
            }
            return allEntities;
        }

        public T? FindById(int? id)
        {
            PropertyInfo? idProperty = typeof(T).GetProperty($"{typeof(T).Name}Id");

            bool isIdInteger = idProperty.PropertyType == typeof(int);

            if (idProperty == null || idProperty.PropertyType != typeof(int))
            {
                return null;
            }
            return FindAll().FirstOrDefault(result => isIdInteger && (int)idProperty.GetValue(result) == id);
        }

        public IEnumerable<T>? FindByEntityId(int? entityId, Type entityType)
        {
            PropertyInfo? entityIdProperty = entityType.GetProperty($"{entityType.Name}Id");

            if (entityIdProperty == null || entityIdProperty.PropertyType != typeof(int))
            {
                // error message specific for invalid id
                return null;
            }
            return FindAll().Where(result => entityId == (int)entityIdProperty.GetValue(result));
        }


        public void SaveChanges()
        {
            // Get the root element for this type
            XElement? root = xLibrary.Element($"{typeof(T).Name}s");

            // Delete existing xlibrary
            root.Elements().Remove();

            // Convert each entity in the entities list to an XElement and add it to the root
            foreach (var entity in entities)
            {
                XElement entityElement = Utilities.FromEntity(entity);
                root.Add(entityElement);
            }
            // Save the updated library back to the XML file
            xLibrary.Save(Utilities.DataBase);

        }

        public void Update(T entity)
        {
            int entityId = (int)entity.GetType().GetProperty($"{entity.GetType().Name}Id").GetValue(entity);
            T? entityToUpdate = FindById(entityId);

            //PropertyInfo[] properties = typeof(T).GetProperties();
            //foreach (var property in properties)
            //{
            //    property.SetValue(entityToUpdate, property.GetValue(entity));
            //}
            entities.Remove(entityToUpdate);
            entities.Add(entity);
        }

    }
}
