using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DataAccess
{
    public static class Utilities
    {
        private static readonly string dataBase = "Library.xml";
        public static string DataBase { get => dataBase; }
        private static XDocument xDoc = XDocument.Load(dataBase);
        private static XAttribute? idAttribute;// = doc.Root.Attribute("LastBookId");            


        public static int GetLastId<T>()
        {
            string entityType = typeof(T).Name;
            idAttribute = xDoc.Root.Attribute($"Last{entityType}Id");

            int.TryParse(idAttribute.Value, out int lastId);
            return lastId;
        }

        public static void UpdateNextId<T>(ref int nextId)
        {
            // get lastAssignedId from Repository
            int lastEntityId = GetLastId<T>();

            // increment it by 1 and assign it to the ref parameter (===>>  XmlRepository.LastUsedId  <<===)
            nextId = lastEntityId + 1;

            // convert nextId to string and overwrite old value in xml
            string entityType = typeof(T).Name;
            idAttribute = xDoc.Root.Attribute($"Last{entityType}Id");
            idAttribute.Value = nextId.ToString();
            
            xDoc.Save(dataBase);
        }


        //public static T PopulateLibraryFromFile<T>() where T : class
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(T));
        //    doc = XDocument.Load(dataBase);
        //    var root = doc
            
        //}

        //public static XElement PopulateLibraryFromFile<T>()
        //{
        //    XElement node = new XElement(typeof(T).Name);

        //    var serializer = new XmlSerializer(typeof(T));
        //    // missing code
        //    doc = XDocument.Load(dataBase);

        //    // Get the root element for this type
        //    XElement root = doc.Element($"{typeof(T).Name}s");

        //    // Deserialize each element in the root into an object of type T
        //    foreach (XElement element in root.Elements())
        //    {
        //        using (var reader = element.CreateReader())
        //        {
        //            T entity = (T)serializer.Deserialize(reader);
        //            // Add the entity to your data structure here...
        //        }
        //    }

        //    return node;
        //}


        //public static XElement PopulateLibrary()
        //{
        //    XElement library = new XElement("Library");

        //    // Create Books elements with sample data
        //    XElement booksElement = new XElement("Books");
        //    booksElement.Add(
        //        new XElement("Book",
        //            new XElement("BookId", 1),
        //            new XElement("Title", "The Lord of the Rings"),
        //            new XElement("AuthorName", "J.R.R."),
        //            new XElement("AuthorSurname", "Tolkien"),
        //            new XElement("Publisher", "Allen & Unwin"),
        //            new XElement("Quantity", 5)
        //        )
        //    );
        //    booksElement.Add(
        //        new XElement("Book",
        //            new XElement("BookId", 2),
        //            new XElement("Title", "Pride and Prejudice"),
        //            new XElement("AuthorName", "Jane"),
        //            new XElement("AuthorSurname", "Austen"),
        //            new XElement("Publisher", "Penguin Classics"),
        //            new XElement("Quantity", 3)
        //        )
        //    );
        //    booksElement.Add(
        //        new XElement("Book",
        //            new XElement("BookId", 3),
        //            new XElement("Title", "To Kill a Mockingbird"),
        //            new XElement("AuthorName", "Harper"),
        //            new XElement("AuthorSurname", "Lee"),
        //            new XElement("Publisher", "J.B. Lippincott & Co."),
        //            new XElement("Quantity", 2)
        //        )
        //    );

        //    // Create Users elements with sample data
        //    XElement usersElement = new XElement("Users");
        //    usersElement.Add(
        //        new XElement("User",
        //            new XElement("UserId", 1),
        //            new XElement("Username", "user1"),
        //            new XElement("Password", "hashed_password1"), // Replace with hashed password
        //            new XElement("Role", "User")
        //        )
        //    );
        //    usersElement.Add(
        //        new XElement("User",
        //            new XElement("UserId", 2),
        //            new XElement("Username", "user2"),
        //            new XElement("Password", "hashed_password2"), // Replace with hashed password
        //            new XElement("Role", "Admin")
        //        )
        //    );
        //    usersElement.Add(
        //        new XElement("User",
        //            new XElement("Id", 3),
        //            new XElement("Username", "user3"),
        //            new XElement("Password", "hashed_password3"), // Replace with hashed password
        //            new XElement("Role", "User")
        //        )
        //    );

        //    // Create a single Reservation element (assuming one for now) with sample data
        //    XElement reservationsElement = new XElement("Reservations");
        //    reservationsElement.Add(
        //        new XElement("Reservation",
        //            new XElement("Id", 1),
        //            new XElement("UserId", 1),
        //            new XElement("BookId", 1),
        //            new XElement("StartDate", DateTime.Now.ToString("yyyy-MM-dd")), // Assuming reservation starts today
        //            new XElement("EndDate", DateTime.Now.AddDays(30).ToString("yyyy-MM-dd")) // Ends 30 days from now
        //        )
        //    );

        //    // Add child elements to Library element
        //    library.Add(booksElement);
        //    library.Add(usersElement);
        //    library.Add(reservationsElement);

        //    return library;
        //}


        public static void WriteToFile(XElement library)
        {
            try
            {
                // Use FileStream with FileMode.Create to create if not exists, or Open to overwrite
                using (FileStream fs = File.Open(dataBase, FileMode.Create))
                {
                    library.Save(fs); 
                }
                Console.WriteLine($"Library data written to '{dataBase}' successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing library data to file: {ex.Message}");
            }
        }

        public static XElement ReadFromFile()
        {
            try
            {

                return (dataBase is null || dataBase == "") ? new XElement("AvanadeLibrary") : XElement.Load(dataBase);
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


        public static T? ToEntity<T>(this XElement element) where T : class, new()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var xmlReader = element.CreateReader())
            {
                return (T)serializer.Deserialize(xmlReader);
            }
        }


        public static XElement FromEntity<T>(T entity) where T : class, new()
        {
            if (entity is null)
            { throw new ArgumentNullException(nameof(entity)); }

            var element = new XElement(typeof(T).Name);
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
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
            Console.WriteLine($"entity {nameof(entity)} successfully converted to XElement");
            return element;

        }
    }
}
