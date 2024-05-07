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
            return repository.FindByEntityId(bookId, typeof(Book)).ToList();
        }

        public List<Reservation> GetReservationsByUserId(int userId)
        {
            if (loggedUser.IsAdmin)
            {
                return repository.FindByEntityId(userId, typeof(User)).ToList();
            }
            // TODO: implement checks and error messages and stuff
            return new List<Reservation>();
        }

        public List<Reservation> GetAllReservations()
        {
            return repository.FindAll().ToList();
        }
    }
}