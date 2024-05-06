using API.Model;
using DataAccess;

namespace API
{
    public class ReservationService
    {
        private readonly IRepository<Reservation> repository;
        private readonly LoggedUser loggedUser;
        private readonly UserService userService;
        private readonly BookService bookService;

        public ReservationService(IRepository<Reservation> repository, LoggedUser loggedUser, UserService userService, BookService bookService)
        {
            this.repository = repository;
            this.loggedUser = loggedUser;
            this.userService = userService;
            this.bookService = bookService;
        }

        public List<Reservation> GetReservationsByBookId(int bookId)
        {
            throw new NotImplementedException(); 
        }
    }
}