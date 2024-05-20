using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DAL
{
    internal static class XmlUtils
    {
        // utilities methods for xml, extension methods etc

        // XElement extension method 
        public static T? ToEntity<T>(this XElement element) where T : class, new()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var xmlReader = element.CreateReader())
            {
                return (T)serializer.Deserialize(xmlReader);
            }
        }

        public static XElement? FromEntity<T>(T entity) where T : class, new()
        {
            try
            {
                if (entity is null)
                { throw new ArgumentNullException(nameof(entity)); }

                var element = new XElement(typeof(T).Name);
                var emptyNamespaces = new XmlSerializerNamespaces([XmlQualifiedName.Empty]);
                var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
                using (var memoryStream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(memoryStream))
                    {
                        var xmlSerializer = new XmlSerializer(typeof(T));
                        xmlSerializer.Serialize(writer, entity, emptyNamespaces);
                    }
                    element = XElement.Parse(Encoding.UTF8.GetString(memoryStream.ToArray()));

                }
                //Console.WriteLine($"entity {nameof(entity)} successfully converted to XElement");
                return element;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return new XElement(typeof(T).Name, entity);
        }


        public static void PrintEntityProperties<TEntity>(TEntity entity) where TEntity : class, new()
        {
            int id;
            var props = typeof(TEntity).GetProperties();
            foreach (var p in props)
            {
                Console.WriteLine($"{p.Name}: {p.GetValue(entity)}");
            }

        }

        public static int UpdateNextId(ref int lastId)
        {

            return ++lastId;
        }

        public static XDocument ReadFromFile(string filePath)
        {
            try
            {
                return XDocument.Load(filePath);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"File '{filePath}' not found.");
            }
            catch (XmlException)
            {
                throw new XmlException($"The file '{filePath}' is not a well-formed XML.");
            }
        }

        internal static int GetLastId<T>()
        {
            throw new NotImplementedException();
        }
    }
}
