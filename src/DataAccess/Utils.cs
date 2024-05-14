using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DataAccess
{
    public static class Utils
    {
        private static readonly string dataBase = "library.xml";
        public static string DataBase { get => dataBase; }
        private static XDocument? doc = XDocument.Load(DataBase);
        private static XAttribute? idAttribute;
        //private static int nextBookId = 0;
        //public static int NextBookId
        //{
        //    get
        //    {
        //        if (nextBookId == 0)
        //        {
        //            GetLastUsedId(out int lastUsedId);
        //            nextBookId = ++lastUsedId;

        //        }
        //        return nextBookId;
        //    }
        //    set
        //    {
        //        nextBookId = value;
        //        UpdateLastUsedId(nextBookId);
        //    }
        //}

        private static int GetLastId<T>()
        {
            //XElement root = doc.Root;
            idAttribute = doc.Root.Attribute($"Last{typeof(T).Name}Id");
            int.TryParse(idAttribute.Value, out int lastEntityId);
            return lastEntityId;
        }

        private static void SetLastId<T>(ref int newId)
        {
            idAttribute = doc.Root.Attribute($"Last{typeof(T).Name}Id");
            ++newId;
            idAttribute.Value = newId.ToString();
            doc.Save(DataBase);
        }

        private static void UpdateLastUsedId<T>(ref int newId)
        {
            doc = XDocument.Load(dataBase);
            idAttribute = doc.Root.Attribute($"Last{typeof(T).Name}Id");
            //idAttribute = doc.Root.Attribute("LastBookId");
            idAttribute.SetValue(newId.ToString());
            doc.Save(dataBase);
        }



        public static XElement PopulateLibraryFromFile<T>()
        {
            XElement node = new XElement(typeof(T).Name);

            var serializer = new XmlSerializer(typeof(T));
            doc = XDocument.Load(dataBase);

            // Get the root element for this type
            XElement root = doc.Element($"{typeof(T).Name}s");

            // Deserialize each element in the root into an object of type T
            foreach (XElement element in root.Elements())
            {
                using (var reader = element.CreateReader())
                {
                    T entity = (T)serializer.Deserialize(reader);
                    // Add the entity to your data structure here...
                }
            }

            return node;
        }

        public static void WriteToFile(XElement library)
        {
            try
            {
                // Use FileStream with FileMode.Create to create if not exists, or Open to overwrite
                using (FileStream fs = File.Open(dataBase, FileMode.Create))
                {
                    library.Save(fs); // Save the XElement to the file stream
                }
                Console.WriteLine($"Library data written to '{dataBase}' successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing library data to file: {ex.Message}");
            }
        }

        public static DataSet ReadDataSetFromFile()
        {
            try
            {
                DataSet set = new();
                set.ReadXml(dataBase);
                return set;
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"File '{dataBase}' not found.");
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException($"Access to file '{dataBase}' is denied.");
            }
            catch (XmlException)
            {
                throw new XmlException($"The file '{dataBase}' is not a well-formed XML.");
            }
        }

        public static XElement ReadFromFile()
        {
            try
            {
                //return (dataBase is null || dataBase == "") ? new XElement(PopulateLibrary()) : XElement.Load(dataBase);
                return XElement.Load(dataBase);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"File '{dataBase}' not found.");
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException($"Access to file '{dataBase}' is denied.");
            }
            catch (XmlException)
            {
                throw new XmlException($"The file '{dataBase}' is not a well-formed XML.");
            }

        }

        public static void PopulateLibrary()
        {
            // populate file with default data
        }


        public static void PrintColumns(DataTable table)
        {
            // Loop through all the rows in the DataTableReader
            foreach (DataColumn col in table.Columns)
            {
                Console.Write($"{col.ColumnName}  ");
            }
            foreach (DataRow row in table.Rows)
            {

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Console.Write(row[i] + " ");
                }
                Console.WriteLine();
            }
        }


        //public static DataSet FillDataSetFromFile()
        //{
        //    return new DataSet("Library").Load(new StreamReader(dataBase), LoadOption.PreserveChanges, );
        //} 


        //public static T? ToEntity<T>(this XElement element) where T : class, new()
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(T));
        //    using (var xmlReader = element.CreateReader())
        //    {
        //        return (T)serializer.Deserialize(xmlReader);
        //    }
        //}


        //public static XElement FromEntity<T>(T entity) where T : class, new()
        //{
        //    if (entity is null)
        //    { throw new ArgumentNullException(nameof(entity)); }

        //    var element = new XElement(typeof(T).Name);
        //    var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
        //    var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        using (var writer = new StreamWriter(memoryStream))
        //        {
        //            var xmlSerializer = new XmlSerializer(typeof(T));
        //            xmlSerializer.Serialize(writer, entity, emptyNamespaces);
        //        }
        //        element = XElement.Parse(Encoding.UTF8.GetString(memoryStream.ToArray()));

        //    }
        //    Console.WriteLine($"entity {nameof(entity)} successfully converted to XElement");
        //    return element;

        //}
    }
}
