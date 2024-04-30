using API.DTOs;
using API.Model;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class BookService
    {
        private readonly IRepository<Book> bookRepository;

        public BookService()
        {
            bookRepository = new XmlRepository<Book>();
        }

        public BookService(IRepository<Book> bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public void AddBook(Book book, User user)
        {
            if (user.Role == User.UserRole.Admin)
            {
                bookRepository.Add(book);
            }
            else
            {
                Console.WriteLine("Non autorizzato!");
            }

        }

        public void EditBook(Book book, User user)
        {
            if (user.Role == User.UserRole.Admin)
            {

                bookRepository.Update(book);
            }
        }

        public void DeleteBook(Book book, User user)
        {
            if (user.Role == User.UserRole.Admin)
            {
                bookRepository.Delete(book);
            }
        }

        public List<Book> GetAllBooks()
        {
            return bookRepository.FindAll().ToList();
        }

        public List<Book> GetAllBooksByPattern(BookSearchObject searchObject)
        {
            IEnumerable<Book> books = bookRepository.FindAll();
            List<Book> filteredList = new List<Book>();
            if (books.Count() > 0)
            {
                filteredList = books.ToList().Where(b => (searchObject.Title == null || b.Title.ToLowerInvariant().Contains(searchObject.Title.ToLowerInvariant())) && (searchObject.Author == null || $"{b.AuthorName} {b.AuthorSurname}".ToLowerInvariant().Contains(searchObject.Author)) && (searchObject.Publisher == null || b.Publisher.ToLowerInvariant().Contains(searchObject.Publisher.ToLowerInvariant())))
                    .SelectMany(b => new Book[] { b })
                    .Distinct()
                    .ToList();
            }

            return filteredList;
        }

    }
}
