using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace API.Utils
{
    public static class XmlHelper
    {
        public static T? ToEntity<T>(this XElement element) where T : class, new()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var xmlReader = element.CreateReader())
            {
                return (T)serializer.Deserialize(xmlReader);
            }
        }


        public static XElement? FromEntity<T>(this T entity) where T : class, new()
        {
            try
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
            catch (ArgumentNullException ex) { Console.WriteLine(ex.Message); }

            return null;
        }

        public static void SaveToFile(XElement xLibrary, string path)
        {
            try
            {
                xLibrary.Save(path);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static XElement LoadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                return XElement.Load(path);
            }
            Console.WriteLine("File not found! Library populated with default data");
            return PopulateLibrary();
        }


        public static XElement PopulateLibrary()
        {
            XElement library = new XElement("Library");

            // Create Books elements with sample data
            XElement booksElement = new XElement("Books");
            booksElement.Add(
                new XElement("Book",
                    new XElement("BookId", 1),
                    new XElement("Title", "The Lord of the Rings"),
                    new XElement("AuthorName", "J.R.R."),
                    new XElement("AuthorSurname", "Tolkien"),
                    new XElement("Publisher", "Allen & Unwin"),
                    new XElement("Quantity", 5)
                )
            );
            booksElement.Add(
                new XElement("Book",
                    new XElement("BookId", 2),
                    new XElement("Title", "Pride and Prejudice"),
                    new XElement("AuthorName", "Jane"),
                    new XElement("AuthorSurname", "Austen"),
                    new XElement("Publisher", "Penguin Classics"),
                    new XElement("Quantity", 3)
                )
            );
            booksElement.Add(
                new XElement("Book",
                    new XElement("BookId", 3),
                    new XElement("Title", "To Kill a Mockingbird"),
                    new XElement("AuthorName", "Harper"),
                    new XElement("AuthorSurname", "Lee"),
                    new XElement("Publisher", "J.B. Lippincott & Co."),
                    new XElement("Quantity", 2)
                )
            );

            // Create Users elements with sample data
            XElement usersElement = new XElement("Users");
            usersElement.Add(
                new XElement("User",
                    new XElement("UserId", 1),
                    new XElement("Username", "user1"),
                    new XElement("Password", "password"),
                    new XElement("Role", "User")
                )
            );
            usersElement.Add(
                new XElement("User",
                    new XElement("UserId", 2),
                    new XElement("Username", "user2"),
                    new XElement("Password", "password"),
                    new XElement("Role", "Admin")
                )
            );
            usersElement.Add(
                new XElement("User",
                    new XElement("Id", 3),
                    new XElement("Username", "user3"),
                    new XElement("Password", "password"),
                    new XElement("Role", "User")
                )
            );

            // Create a single Reservation element (assuming one for now) with sample data
            XElement reservationsElement = new XElement("Reservations");
            reservationsElement.Add(
                new XElement("Reservation",
                    new XElement("Id", 1),
                    new XElement("UserId", 1),
                    new XElement("BookId", 1),
                    new XElement("StartDate", DateTime.Now.ToString("yyyy-MM-dd")), // Assuming reservation starts today
                    new XElement("EndDate", DateTime.Now.AddDays(30).ToString("yyyy-MM-dd")) // Ends 30 days from now
                )
            );

            // Add child elements to Library element
            library.Add(booksElement);
            library.Add(usersElement);
            library.Add(reservationsElement);

            return library;
        }
    }
}
