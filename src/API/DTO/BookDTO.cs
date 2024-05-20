using API.Model;
using DataAccess;
using System.Xml.Linq;

namespace API.DTOs
{
    //[XmlElement("Book")]
    public class BookDTO
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        //public bool IsAvailable { get; set; }
        public int Qty { get; set; }

        //[XmlAttribute("LastBookId")]
        //public int LastBookId { get; init; }

        public Book Book { get; set; }
        public XElement? XmlBook { get; set; }

        /// <summary>
        /// Constructor used for adding new books to the database. Creates a new instance of Book calling the Book constructor that accepts a BookDTO object
        /// </summary>
        /// <param name="lastUsedId">Value of the last id assigned to a new Book. It is retrieved from the LastBookId attribute in the Xml implementation</param>
        /// <param name="title">Value provided by a user with Admin privileges</param>
        /// <param name="publisher">Value provided by a user with Admin privileges</param>
        /// <param name="authorName">Value provided by a user with Admin privileges</param>
        /// <param name="authorSurname">Value provided by a user with Admin privileges</param>
        /// <param name="quantity">Value provided by a user with Admin privileges</param>
        public BookDTO(int lastUsedId, string title, string publisher, string authorName, string authorSurname, int quantity)
        {
            Title = title;
            Publisher = publisher;
            AuthorName = authorName;
            AuthorSurname = authorSurname;
            Qty = quantity;
            Book = new Book(id: lastUsedId, this);
            XmlBook = Utilities.FromEntity(Book);
        }

        public BookDTO(Book book)
        {
            Book = book;
            Title = book.Title;
            Publisher = book.Publisher;
            AuthorName = book.AuthorName;
            AuthorSurname = book.AuthorSurname;
            Qty = book.Quantity;
            XmlBook = Utilities.FromEntity(book);
        }

        public BookDTO(XElement xmlBook)
        {
            XmlBook = xmlBook;
            Book = xmlBook.ToEntity<Book>() ?? new Book();
        }
    }
}
