using API.DTOs;
using API.Interfaces;
using API.Model;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    //public class LibraryManager(ILogger logger, IUserService userService, IReservationService reservationService, IBookService bookService)
    public class LibraryManager(LoggedUser currentUser, IUserService userService, IReservationService reservationService, IBookService bookService)
    {
        
        // = logger.Login(new UserLoginDTO("admin", "pssw")); // TODO: implement it for real!
        private static Library libraryInstance = Library.Instance;

        public void AddReservation(Reservation reservation)
        {
            reservation.ReservationId = libraryInstance.LastUsedIds["Reservation"];
            libraryInstance.LastUsedIds["Reservation"]++;
            reservationService.AddReservation(reservation);
        }



    }
}