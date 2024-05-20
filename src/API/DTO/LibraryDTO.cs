using API.DTO;
using API.Model;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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


        public LibraryDTO(out Library library)
        {
            Users = new List<User>();
            Books = new List<Book>();
            Reservations = new List<Reservation>();

            XElement xmlLibrary = Utilities.ReadFromFile();

            LastBookId = int.TryParse(xmlLibrary.Attribute("LastBookId").Value, out int lastBookId) ? lastBookId : 1;
            LastReservationId = int.TryParse(xmlLibrary.Attribute("LastReservationId").Value, out int lastReservationId) ? lastReservationId : 1;
            LastUserId = int.TryParse(xmlLibrary.Attribute("LastUserId").Value, out int lastUserId) ? lastUserId : 1;
            //LastUsedIds = new Dictionary<string, int>();

            Users = xmlLibrary.Element("Users")!.Elements().Select(e => new UserDTO(e).User).ToList();
            Books = xmlLibrary.Element("Books")!.Elements().Select(e => new BookDTO(e).Book).ToList();
            Reservations = xmlLibrary.Element("Reservations")!.Elements().Select(e => new ReservationDTO(e).Reservation).ToList();

            library = new Library(this);
        }


        public LibraryDTO(Library library)
        {
            Users = library.Users;
            Books = library.Books;
            Reservations = library.Reservations;

            LastBookId = library.LastUsedIds["Book"];
            LastUserId = library.LastUsedIds["User"];
            LastReservationId = library.LastUsedIds["Reservation"];
        }

        public Library ToLibrary()
        {
            return new Library(this);
        }
    }
}
