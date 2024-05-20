using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace DAL
{
    public class XmlRepository : IRepository
    {
        private readonly string filePath;
        private Dictionary<string, int> lastUsedIds;
        public Dictionary<string, int> LastUsedIds
        {
            get => lastUsedIds;
            set
            {
                if (value != null)
                {
                    foreach (var kvp in value)
                    {
                        lastUsedIds[kvp.Key] = kvp.Value;
                    }
                }
                else
                {
                    lastUsedIds = new Dictionary<string, int>();
                }
            }
        }
        private XDocument xLibrary;
        public XDocument XLibrary { get { return xLibrary; } }


        public XmlRepository()
        {
            filePath = "Library.xml";
            xLibrary = XmlUtils.ReadFromFile(filePath);
            lastUsedIds = new Dictionary<string, int>();

            foreach (var attribute in XLibrary.Root.Attributes())
            {
                var typeName = attribute.Name.LocalName.Replace("Last", "").Replace("Id", "");
                lastUsedIds[typeName] = int.Parse(attribute.Value);
            }

        }

        public IEnumerable<TEntity> FindAll<TEntity>() where TEntity : class, new()
        {
            List<TEntity> entities = new List<TEntity>();

            var document = XDocument.Load(filePath);
            var xElements = from el in xLibrary.Descendants(typeof(TEntity).Name) select el;
            foreach (var el in xElements)
            {
                TEntity? entity = el.ToEntity<TEntity>();
                entities.Add(entity);
            }
            return entities;

        }

        public TEntity? FindById<TEntity>(int id) where TEntity : class, new()
        {
            var document = XDocument.Load(filePath);
            TEntity entity;
            PropertyInfo? idProperty = typeof(TEntity).GetProperty("Id");

            if (idProperty == null || idProperty.PropertyType != typeof(int))
            {
                return default(TEntity);
            }

            return FindAll<TEntity>().FirstOrDefault(res => ((int)idProperty.GetValue(res)) == id);//.Equals(id));
        }


        public void Create<TEntity>(TEntity entity) where TEntity : class, new()
        {
            if (entity == null)
            {
                Console.WriteLine($"Could not add '{typeof(TEntity).Name}' object.");
            }
            else
            {
                // assign nextId to entity
                string entityType = typeof(TEntity).Name;
                XAttribute? idAttribute = xLibrary.Root.Attribute($"Last{entityType}Id");
                int nextId = LastUsedIds[entityType];
                LastUsedIds[typeof(TEntity).Name] = XmlUtils.UpdateNextId(ref nextId);

                idAttribute.Value = nextId.ToString();

                // convert entity to XElement
                var idProperty = typeof(TEntity).GetProperty("Id");
                idProperty.SetValue(entity, nextId);
                XElement xEntity = XmlUtils.FromEntity(entity);

                // locate node of TEntities and append new element
                var parentNode = xLibrary.Descendants(typeof(TEntity).Name + "s").FirstOrDefault();
                if (parentNode != null)
                {
                    parentNode.Add(xEntity);
                }
                else
                {
                    Console.WriteLine("ERROR: root is empty.");
                    return;
                }
                xLibrary.Save(filePath);
            }
        }

        public void Delete<TEntity>(int entityId) where TEntity : class, new()
        {
            var parentNode = xLibrary.Descendants(typeof(TEntity).Name);
            if (parentNode != null)
            {
                XElement? elementToDelete = parentNode
                    .FirstOrDefault(el => int.TryParse(el.Element("Id")?.Value, out int id) && id == entityId);

                if (elementToDelete is null)
                {
                    Console.WriteLine("Nothing to remove.");
                }
                else
                {
                    elementToDelete.Remove();
                    XLibrary.Save(filePath);
                }
            }
            else
            {
                Console.WriteLine("ERROR: document root is empty.");
            }

        }

        public void SaveChanges()
        {
            // No-op
        }


        public void Update<TEntity>(TEntity entity) where TEntity : class, new()
        {
            var elementToUpdate = XmlUtils.FromEntity(entity);
            Console.WriteLine(elementToUpdate);



            int entityId = int.Parse(typeof(TEntity).GetProperty("Id").GetValue(entity).ToString());
            var entityToUpdate = FindById<TEntity>(entityId);

            foreach(var prop in typeof(TEntity).GetProperties())
            {
                Console.WriteLine(prop.Name);
                Console.WriteLine(prop.GetValue(entityToUpdate));
                Console.WriteLine();
                prop.SetValue(entityToUpdate, prop.GetValue(entity));
            }

            //Delete<TEntity>(entityId);

            //Create(entityToUpdate);
        }
    }
}
