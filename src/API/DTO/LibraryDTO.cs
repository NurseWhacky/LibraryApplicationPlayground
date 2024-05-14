using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace API.DTOs
{
    [XmlRoot("AvanadeLibrary")]
    public class LibraryDTO
    {
        private string filePath = "library.xml";

        public List<User> Users { get; set; }
        public List<Book> Books { get; set; }
        public List<Reservation> Reservations { get; set; }

        [XmlAttribute("LastBookId")]
        public int LastBookId { get; set; }

        [XmlAttribute("LastUserId")]
        public int LastUserId { get; set; }

        [XmlAttribute("LastReservationId")]
        public int LastReservationId { get; set; }

        public LibraryDTO() { }

        public LibraryDTO(Library library)
        {
            Users = library.Users;
            Books = library.Books;
            Reservations = library.Reservations;

            LastBookId = library.LastUsedIds["Book"];
            LastUserId = library.LastUsedIds["User"];
            LastReservationId = library.LastUsedIds["Reservation"];
        }
    }
}
