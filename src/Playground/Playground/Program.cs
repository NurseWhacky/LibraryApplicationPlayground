
using API.Model;
using DataAccess;
using System.Xml.Linq;
using System.Xml.Serialization;

XElement library = Utilities.PopulateLibrary();
Utilities.WriteLibraryToFile(library);

List<Book> books = new List<Book>()
{
        new Book() { BookId=1, AuthorName = "ciccio", AuthorSurname = "pasticcio", Title = "Piccolo Pippo cucciolo eroico", Publisher = "Mondadori" },
        new Book() {BookId=2, AuthorName = "Papa", AuthorSurname = "Francesco", Title = "La buona novella", Publisher = "Mondadori" },
        new Book() {BookId=3, AuthorName = "Silvio", AuthorSurname = "Abberluschioni", Title = "I'll be back", Publisher = "Minimum fax" }
       };
List<Reservation> reservations = new List<Reservation>()
{
    new Reservation(){ BookId=2, ReservationId=1, UserId=1, StartDate=DateTime.Now},
    new Reservation(){ BookId=3, ReservationId=2, UserId=2, StartDate=DateTime.Now}
};
List<User> users = new List<User>() {
    new User() { UserId = 1, UserName = "admin", Password = "pssw", Role = UserRole.Admin },
    new User() { UserId = 2, UserName = "usr", Password = "pssw", Role = UserRole.User } };


Library lib = new Library() { Books = books, Users = users, Reservations = reservations, LastUsedBookId = 3 };

var serializer = new XmlSerializer(typeof(Library));

using (var writer = new StreamWriter("prova.xml", false))
{
    serializer.Serialize(writer, lib);
}

Library deserLib = new();

using(var reader = new StreamReader("prova.xml"))
{
    deserLib = (Library) serializer.Deserialize(reader);
    foreach (Book book in deserLib.Books)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.AuthorName} {book.AuthorSurname}");
        }
}

