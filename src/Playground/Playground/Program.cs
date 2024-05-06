
using API;
using API.Model;
using DataAccess;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;



List<Book> books = new List<Book>()
{
        new Book() { BookId=1, AuthorName = "ciccio", AuthorSurname = "pasticcio", Title = "Piccolo Pippo cucciolo eroico", Publisher = "Mondadori", Quantity = 1 },
        new Book() {BookId=2, AuthorName = "Papa", AuthorSurname = "Francesco", Title = "La buona novella", Publisher = "Mondadori", Quantity = 1 },
        new Book() {BookId=3, AuthorName = "Silvio", AuthorSurname = "Abberluschioni", Title = "I'll be back", Publisher = "Minimum fax", Quantity = 1 }
       };
List<Reservation> reservations = new List<Reservation>()
{
    new Reservation(){ BookId=2, ReservationId=1, UserId=1, StartDate=DateTime.Now},
    new Reservation(){ BookId=3, ReservationId=2, UserId=2, StartDate=DateTime.Now}
};
List<User> users = new List<User>() {
    new User() { UserId = 1, Username = "admin", Password = "pssw", Role = UserRole.Admin },
    new User() { UserId = 2, Username = "usr", Password = "pssw", Role = UserRole.User } };


Library lib = new Library() { Books = books, Users = users, Reservations = reservations, LastUsedBookId = 3 };
XElement xlibrary = Utilities.FromEntity(lib);

XElement myXBook = Utilities.FromEntity(new Book(8, "Poba", "MC", "Cavallo", "Seppiette", 20));

//Console.WriteLine(xlibrary);
//Console.WriteLine(myXBook);

var entityLibrary = Utilities.ToEntity<Library>(xlibrary);

//Console.WriteLine(entityLibrary.LastUsedBookId);
foreach (var book in entityLibrary.Books)
{
    Console.WriteLine($"Id: {book.BookId}, Title: {book.Title}, Author: {book.AuthorName} {book.AuthorSurname}");
}

Book cavalloBook = myXBook.ToEntity<Book>();
//Console.WriteLine($"Id: {cavalloBook.BookId}, Title: {cavalloBook.Title}, Author: {cavalloBook.AuthorName} {cavalloBook.AuthorSurname}");


//XmlRepository<Book> bookRepo = new XmlRepository<Book>();
BookService service = new(new XmlRepository<Book>(), new LoggedUser(new User() { Username = "minka Kelly", UserId = 555, Password = "porcone", Role = UserRole.Admin }));

//bookRepo.Add(cavalloBook);

//foreach(var b in books) bookRepo.Add(b);

//bookRepo.Add(cavalloBook);
var scarara = new Book(3, "Scarara", "Franco", "Cappai", "Ottaviu Pallietta", 4);
service.AddBook(cavalloBook);
service.AddBook(scarara);
//Console.WriteLine(xlibrary);
service.UpdateQuantity(scarara);
//service.AddBook(scarara);
service.AddBook(scarara);



//Reservation reservation = new Reservation(34, 55, 99, new DateTime(2024, 3, 14));

//IRepository<Reservation> reservationRepo = new XmlRepository<Reservation>();

//reservationRepo.Add(reservation);

//foreach(var res in reservations)
//{
//    reservationRepo.Add(res);
//}

//Console.WriteLine(xlibrary);


//var xLib = Utilities.FromEntity(lib);

//Console.WriteLine(xLib);
