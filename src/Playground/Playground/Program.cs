using API;
using API.DTOs;
using API.Interfaces;
using API.Model;
using API.Services;
using DataAccess;


var userRepo = new XmlRepository<User>();
var logger = new LoginService(userRepo);
LoggedUser? currentUser = logger.Login(credentials: new UserLoginDTO("admin", "pssw"));
var bookRepo = new XmlRepository<Book>();
var reservationRepo = new XmlRepository<Reservation>();
IUserService userService = new UserService(userRepo);
IBookService bookService = new BookService(bookRepo, currentUser);
IReservationService reservationService = new ReservationService(reservationRepo, currentUser);

var manager = new LibraryManager(currentUser, userService, reservationService, bookService);




Console.WriteLine(currentUser.Username);
//Console.WriteLine(bookService.GetBookById(4).Title);
foreach (var book in bookService.GetAllBooks())
{

    Console.WriteLine($"Title: {book.Title}, publisher: {book.Publisher}");
    Console.WriteLine();
}

bookService.DeleteBook(4);
