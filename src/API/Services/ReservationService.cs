using API.DTO;
using API.DTOs;
using API.Interfaces;
using API.Model;
using DataAccess;

namespace API.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> repository;
        private readonly LoggedUser loggedUser;

        public ReservationService(IRepository<Reservation> repository, LoggedUser loggedUser)
        {
            this.repository = repository;
            this.loggedUser = loggedUser;
        }
        public List<Reservation> GetReservationsByBookId(int bookId)
        {
            return repository.FindByEntityId(bookId, typeof(Book)).ToList();
        }

        public List<Reservation> GetReservationsByUserId(int userId)
        {
            int currentUserId = int.Parse(loggedUser.Id);
            try
            {
                if (loggedUser.IsAdmin && userId != currentUserId)
                {
                    return repository.FindByEntityId(userId, typeof(User)).ToList();
                }
                if (userId == currentUserId)
                {
                    return repository.FindByEntityId(currentUserId, typeof(User)).ToList();
                }
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }

            return new List<Reservation>();
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

        public void AddReservation(Reservation reservation)
        {
            repository.Add(reservation);
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
            List<Reservation> reservationsToDelete = repository.FindByEntityId(bookId, typeof(Book)).ToList();
            foreach (var reservation in reservationsToDelete)
            {
                repository.Delete(reservation);
            }
            repository.SaveChanges();
        }

        public void UpdateReservation(int reservationId)
        {
            var reservationToUpdate = repository.FindById(reservationId);
            reservationToUpdate.EndDate = DateTime.Now;
            repository.Update(reservationToUpdate);

        }

        private bool CanUserReserve(Book book, out BookErrorType message)
        {
            if (book is null)
            {
                message = BookErrorType.NonExistent;
                return false;
            }
            if (GetReservationsByBookId(book.BookId).Any(res => res.EndDate > DateTime.Now && res.UserId == int.Parse(loggedUser.Id)))
            {
                message = BookErrorType.AlreadyReservedByUser;
                return false;
            }
            if (GetReservationsByStatus(active: true).Where(res => res.BookId == book.BookId).Count() >= book.Quantity)
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
            return new Reservation(bookId: book.BookId, userId: int.Parse(loggedUser.Id), startDate: DateTime.Now);
        }
    }

}