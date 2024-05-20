using API.DTOs;
using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IReservationService
    {
        List<Reservation> GetReservations();
        List<Reservation> GetReservationsByBookId(int bookId);
        List<Reservation> GetReservationsByUserId(int userId);
        List<Reservation> GetReservationsByStatus(bool status);
        void AddReservation(Reservation reservation);
        Reservation? CreateReservation(Book book);
        void DeleteReservations(int bookId);
        void UpdateReservation(int reservationId);
        bool IsBookAvailable(Book book);
        bool IsBookDeletable(Book book);

    }
}
