using API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace API.Model
{
    public class Library
    {
        public List<User> Users { get; set; }
        public List<Book> Books { get; set; }
        public List<Reservation> Reservations { get; set; }
        public Dictionary<string, int> LastUsedIds { get; set; }

        public Library(List<User> users, List<Reservation> reservations, List<Book> books)
        {
            Users = users;
            Books = books;
            Reservations = reservations;


            LastUsedIds = new Dictionary<string, int>();
            LastUsedIds["Book"] = Books != null && Books.Any() ? Books.Max(b => b.BookId) : 1;
            LastUsedIds["Reservation"] = Reservations != null && Reservations.Any() ? Reservations.Max(r => r.ReservationId) : 1;
            LastUsedIds["User"] = Users != null && Users.Any() ? Users.Max(b => b.UserId) : 1;
        }

        public LibraryDTO ToDTO()
        {
            return new LibraryDTO(this);
        }
    }
}
