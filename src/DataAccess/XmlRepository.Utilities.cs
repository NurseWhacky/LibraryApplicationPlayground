using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DataAccess
{
    public class Utilities
    {
        private static readonly string dataBase = "Library.xml";
        private readonly string filePath = "lastUsedId.xml";

        public static XElement PopulateNode<T>(T entity)
        {
            XElement node = new XElement(typeof(T).Name);

            var props = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance); // only public members of T class
            foreach (var prop in props)
            {
                node.Add(new XElement(prop.Name, prop.GetValue(entity)));
            }
            return node;

        }
        public static XElement PopulateLibraryFromFile<T>(T entity)
        {
            XElement node = new XElement(typeof(T).Name);

            var serializer = new XmlSerializer(typeof(T));
            //using (var reader = new )
            return node;
        }


        public static XElement PopulateLibrary()
        {
            XElement library = new XElement("Library");

            // Create Books elements with sample data
            XElement booksElement = new XElement("Books");
            booksElement.Add(
                new XElement("Book",
                    new XElement("Id", 1),
                    new XElement("Title", "The Lord of the Rings"),
                    new XElement("AuthorName", "J.R.R."),
                    new XElement("AuthorSurname", "Tolkien"),
                    new XElement("Publisher", "Allen & Unwin"),
                    new XElement("Quantity", 5)
                )
            );
            booksElement.Add(
                new XElement("Book",
                    new XElement("Id", 2),
                    new XElement("Title", "Pride and Prejudice"),
                    new XElement("AuthorName", "Jane"),
                    new XElement("AuthorSurname", "Austen"),
                    new XElement("Publisher", "Penguin Classics"),
                    new XElement("Quantity", 3)
                )
            );
            booksElement.Add(
                new XElement("Book",
                    new XElement("Id", 3),
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
                    new XElement("Id", 1),
                    new XElement("Username", "user1"),
                    new XElement("Password", "hashed_password1"), // Replace with hashed password
                    new XElement("Role", "Member")
                )
            );
            usersElement.Add(
                new XElement("User",
                    new XElement("Id", 2),
                    new XElement("Username", "user2"),
                    new XElement("Password", "hashed_password2"), // Replace with hashed password
                    new XElement("Role", "Admin")
                )
            );
            usersElement.Add(
                new XElement("User",
                    new XElement("Id", 3),
                    new XElement("Username", "user3"),
                    new XElement("Password", "hashed_password3"), // Replace with hashed password
                    new XElement("Role", "Librarian")
                )
            );

            // Create a single Reservation element (assuming one for now) with sample data
            XElement reservationsElement = new XElement("Reservations");
            reservationsElement.Add(
                new XElement("Reservation",
                    new XElement("Id", 1),
                    new XElement("userid", 1),
                    new XElement("bookid", 1),
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


        public static void WriteLibraryToFile(XElement library)
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

        public static XDocument ReadLibraryFromFile()
        {
            if(dataBase is not null && File.Exists(dataBase))
            {
                return XDocument.Load(dataBase);
            }
            return new XDocument(PopulateLibrary()); // default content FOR NOW
        }

    }
}
