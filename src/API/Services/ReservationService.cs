using API.Interfaces;
using API.Model;
using DAL;
using DataAccess;

namespace API.Services
{
    public class ReservationService : IReservationService
    {
        private readonly DAL.IRepository reservationRepo;
        private readonly IRepository<Reservation> repository; // TO DELETE!
        private readonly LoggedUser loggedUser;

        public ReservationService(DAL.IRepository reservationRepo, IRepository<Reservation> repository, LoggedUser loggedUser)
        {
            this.reservationRepo = reservationRepo;
            this.repository = repository; // TO DELETE!
            this.loggedUser = loggedUser;
        }
        public List<Reservation> GetReservationsByBookId(int bookId)
        {
            return repository.FindByEntityId(bookId, typeof(Book)).ToList();// TO DELETE!
        }

        public List<Reservation> GetReservationsByUserId(int userId)
        {
            try
            {
                int currentUserId = int.Parse(loggedUser.Id);
                if (loggedUser.IsAdmin && userId != currentUserId)
                {
                    return repository.FindByEntityId(userId, typeof(User)).ToList();// TO DELETE!
                }
                if (userId == currentUserId)
                {
                    return repository.FindByEntityId(currentUserId, typeof(User)).ToList();// TO DELETE!
                }
            }
            catch (FormatException ex)
            { Console.WriteLine(ex.Message); }

            return new List<Reservation>();
        }

        public List<Reservation> GetReservations()
        {
            return repository.FindAll().ToList();// TO DELETE!
        }


        public List<Reservation> GetReservationsByStatus(bool active)
        {
            if (active)
            {
                return repository.FindAll().Where(r => r.EndDate > DateTime.Now).ToList();// TO DELETE!
            }
            else
            {
                return repository.FindAll().Where(r => r.EndDate < DateTime.Now).ToList();// TO DELETE!
            }
        }

        public void AddReservation(Reservation reservation)
        {
            repository.Add(reservation);// TO DELETE!
            //if (!CanUserReserve(book, out BookErrorType type))
            //{
            //    //PrintErrorMessage(type); ==> for a future Printer class
            //}
            //else
            //{
            //repository.Add(new Reservation(repository.NextBookId(), book.BookId, loggedUser.Id, DateTime.Now));
            // TODO : check this crap!
            //repository.Add(new Reservation() { BookId = book.BookId, UserId = loggedUser.Id, StartDate = DateTime.Now, ReservationId = library.LastUsedIds["Reservation"] });
            //repository.SaveChanges();
        }


        public void DeleteReservations(int bookId)
        {
            List<Reservation> reservationsToDelete = repository.FindByEntityId(bookId, typeof(Book)).ToList();// TO DELETE!
            foreach (var reservation in reservationsToDelete)
            {
                repository.Delete(reservation);// TO DELETE!
            }
            repository.SaveChanges();// TO DELETE!
        }

        public void UpdateReservation(int reservationId)
        {
            var reservationToUpdate = repository.FindById(reservationId);// TO DELETE!
            reservationToUpdate.EndDate = DateTime.Now;
            repository.Update(reservationToUpdate);// TO DELETE!

        }

        private bool CanUserReserve(Book book, out BookErrorType message)
        {
            if (book is null)
            {
                message = BookErrorType.NonExistent;
                return false;
            }
            if (GetReservationsByBookId(book.Id).Any(res => res.EndDate > DateTime.Now && res.UserId == int.Parse(loggedUser.Id)))
            {
                message = BookErrorType.AlreadyReservedByUser;
                return false;
            }
            if (GetReservationsByStatus(active: true).Where(res => res.BookId == book.Id).Count() >= book.Quantity)
            {
                message = BookErrorType.NotAvailable;
                return false;
            }
            message = BookErrorType.None;
            return true;

        }

        public Reservation? CreateReservation(Book book)
        {
            bool reservable = CanUserReserve(book, out BookErrorType errorMessage);
            if (!reservable)
            {
                // PrintError(errorMessage); 
                return null;
            }
            // TODO: CHANGE repository and logic of assigning Id (DAL responsibility) 
            return new Reservation(bookId: book.Id, userId: int.Parse(loggedUser.Id), startDate: DateTime.Now);
        }

        // TODO : Implement reservationservice methods and test them!
        // TODO : logic in here is flawed, fix it
        public bool IsBookAvailable(Book book)
        {
            List<Reservation> activeReservations = GetReservationsByBookId(book.Id)
                .Where(res => res.EndDate > DateTime.Now)
                .ToList();

            return activeReservations.Count() < book.Quantity;

        }

        public bool IsBookDeletable(Book book)
        {
            return GetReservationsByBookId(book.Id).Any(res => res.EndDate > DateTime.Now);
        }
    }

}