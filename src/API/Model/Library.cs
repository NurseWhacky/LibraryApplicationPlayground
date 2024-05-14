using API.DTO;
using API.DTOs;
using API.Utils;
using System.Xml.Linq;

namespace API.Model
{
    public class Library
    {
        private readonly string filePath = "library.xml";
        private static Library instance;
        public static Library Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Library();
                }
                return instance;
            }
        }

        public List<User> Users { get; set; }
        public List<Book> Books { get; set; }
        public List<Reservation> Reservations { get; set; }
        public Dictionary<string, int> LastUsedIds { get; set; }
        private Library()
        {
            Users = new List<User>();
            Books = new List<Book>();
            Reservations = new List<Reservation>();
            LastUsedIds = new Dictionary<string, int>();

            XElement xmlLibrary = XmlHelper.LoadFromFile(filePath);
            Users = xmlLibrary.Element("Users").Elements().Select(e => new UserDTO(e).User).ToList();
            Books = xmlLibrary.Element("Books").Elements().Select(e => new BookDTO(e).Book).ToList();
            Reservations = xmlLibrary.Element("Reservations").Elements().Select(e => new ReservationDTO(e).Reservation).ToList();
            LastUsedIds["Book"] = int.TryParse(xmlLibrary.Attribute("LastBookId").Value, out int lastBookId) ? lastBookId : 1;
            LastUsedIds["Reservation"] = int.TryParse(xmlLibrary.Attribute("LastReservationId").Value, out int lastReservationId) ? lastReservationId : 1;
            LastUsedIds["User"] = int.TryParse(xmlLibrary.Attribute("LastUserId").Value, out int lastUserId) ? lastUserId : 1;
        }

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
