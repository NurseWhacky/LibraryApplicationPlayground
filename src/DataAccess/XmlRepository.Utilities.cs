using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess
{
    public class Utilities
    {
        private readonly string dataBase = "Library.xml";
        private readonly string filePath = "lastUsedId.xml";


        public static void NextId(Type type)
        {
            XElement lastIds = new XElement("LastUsedIds");
            

            
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
            string filePath = "library.xml";

            try
            {
                // Use FileStream with FileMode.Create to create if not exists, or Open to overwrite
                using (FileStream fs = File.Open(filePath, FileMode.Create))
                {
                    library.Save(fs); // Save the XElement to the file stream
                }
                Console.WriteLine("Library data written to 'library.xml' successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing library data to file: {ex.Message}");
            }
        }

        //private XElement CreateNode<T>(T entity)
        //{
        //    XElement newNode = new XElement($"{typeof(T).Name}");
        //    PropertyInfo[] props = entity.GetType().GetProperties();
        //    foreach (PropertyInfo prop in props)
        //    {
        //        newNode.Add(new XElement($"{prop.Name}", prop.GetValue(entity)));
        //    }
        //    return newNode;
        //}

        //public void InitializeLibrary()
        //{
        //    XDocument library = new XDocument(
        //        new XComment("Avanade Library App."),
        //        new XElement("Library",
        //            new XElement("Books", ));
        //}
    }
}
