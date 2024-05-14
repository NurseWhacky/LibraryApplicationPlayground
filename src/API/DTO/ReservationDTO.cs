using API.Model;
using API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            get => XmlReservation.ToEntity<Reservation>();
            set { Reservation = value != null ? value : null; }
        }

        public ReservationDTO(XElement? xReservation)
        {
            XmlReservation = xReservation;
            Reservation = XmlReservation == null ? new Reservation() : XmlReservation.ToEntity<Reservation>();
        }

        public ReservationDTO(int userId, int bookId)
        {
            UserId = userId;
            BookId = bookId;
        }
    }
}
