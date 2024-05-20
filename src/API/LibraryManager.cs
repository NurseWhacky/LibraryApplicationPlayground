using API.Interfaces;
using API.Model;
using API.Services;

namespace API
{
    //public class LibraryManager(ILogger logger, IUserService userService, IReservationService reservationService, IBookService bookService)
    //public class LibraryManager(LoggedUser currentUser, IUserService userService, IReservationService reservationService, IBookService bookService)
    public class LibraryManager
    {
        private readonly LoggedUser currentUser; 
        private readonly IUserService userService;
        private readonly IReservationService reservationService;
        private readonly IBookService bookService;
        private static Library libraryInstance = Library.Instance;

        //public LibraryManager(ServiceFactory factory)
        //{
        //    // all the properties coming from the factory
        //}

        public void AddReservation(Reservation reservation)
        {
            reservation.Id = libraryInstance.LastUsedIds["Reservation"];
            libraryInstance.LastUsedIds["Reservation"]++;
            reservationService.AddReservation(reservation);
        }

        // more methods from services



    }
}