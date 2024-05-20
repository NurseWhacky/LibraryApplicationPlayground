using API.Model;
using DataAccess;
using System.Xml.Linq;

namespace API.DTO
{
    public class ReservationDTO
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public XElement? XmlReservation { get; set; }
        public Reservation? Reservation
        {
            get => XmlReservation?.ToEntity<Reservation>();
            set { Reservation = value != null ? value : new Reservation(); }
        }

        public ReservationDTO(XElement? xReservation)
        {
            XmlReservation = xReservation;
            Reservation = xReservation?.ToEntity<Reservation>();
        }

        public ReservationDTO(int userId, int bookId)
        {
            UserId = userId;
            BookId = bookId;
        }
    }
}
