using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace API.Model
{
    [XmlRoot("AvanadeLibrary")]
    public class Library
    {
        public List<User> Users { get; set; }
        public List<Book> Books { get; set; }
        public List<Reservation> Reservations { get; set; }
        [XmlAttribute("LastBookId")]
        public int LastUsedBookId { get; set; }
    }
}
