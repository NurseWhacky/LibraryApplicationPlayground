using API.DTOs;
using API.Interfaces;
using API.Model;
using DataAccess;

namespace API
{
    public partial class ReservationService : IReservationService
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
            throw new UnauthorizedAccessException("You are not authorized to perform this action");
            //return new List<Reservation>();
        }

        public List<Reservation> GetReservations()
        {
            return repository.FindAll().ToList();
        }


        public List<Reservation> GetReservationsByStatus(bool active)
        {
            if (active)
            {
                return repository.FindAll().Where(r => r.EndDate > DateTime.Now).ToList();
            }
            else
            {
                return repository.FindAll().Where(r => r.EndDate < DateTime.Now).ToList();
            }
        }

        public void AddReservation(Book? book)
        {
            if(!CanUserReserve(book, out ErrorType type))
            {
                //PrintErrorMessage(type); ==> for a future Printer class
            }
            else
            {
                repository.Add(new Reservation(Utilities.NextBookId, book.BookId, loggedUser.Id, DateTime.Now));
                repository.SaveChanges();
            }
        }

        public void DeleteReservations(int bookId)
        {
            List<Reservation> reservationsToDelete = repository.FindByEntityId(bookId, typeof(Book)).ToList();
            foreach (var reservation in reservationsToDelete)
            {
                repository.Delete(reservation);
            }
        }

        public void UpdateReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        private bool CanUserReserve(Book book, out ErrorType message)
        {
            if (book is null)
            {
                message = ErrorType.NonExistent;
                return false;
            }
            if (GetReservationsByBookId(book.BookId).Any(res => ((res.EndDate > DateTime.Now && res.UserId == loggedUser.Id))))
            {
                message = ErrorType.AlreadyReservedByUser;
                return false;
            }
            if (GetReservationsByStatus(active: true).Where(res => res.BookId == book.BookId).Count() >= book.Quantity)
            {
                message = ErrorType.NotAvailable;
                return false;
            }
            message = ErrorType.None;
            return true;

        }
    }

}