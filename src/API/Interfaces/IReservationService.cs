using API.DTOs;
using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Interfaces
{
    interface IReservationService
    {
        List<Reservation> GetReservations();
        List<Reservation> GetReservationsByBookId(int bookId);
        List<Reservation> GetReservationsByUserId(int userId);
        List<Reservation> GetReservationsByStatus(bool status);
        void AddReservation(Book book);
        void DeleteReservations(int bookId);
        void UpdateReservation(Reservation reservation);

    }
}
