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
        private readonly ReservationService reservationService;
        private readonly LoggedUser currentUser;

        public BookService(IRepository<Book> bookRepository, LoggedUser currentUser)
        {
            this.bookRepository = bookRepository;
            this.currentUser = currentUser;
        }

        public void AddBook(Book book)
        {
            if (currentUser.IsAdmin)
            {
                Book? duplicate = bookRepository.FindAll()
                    .Where(d => book.Equals(d))
                    .FirstOrDefault();
                if (duplicate is null)
                {
                    bookRepository.Add(book);
                    bookRepository.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"Libro con id {duplicate.BookId} già presente in libreria");
                    UpdateQuantity(book);
                }
            }
            else
            {
                Console.WriteLine("Non autorizzato!");
            }

        }

        public List<Book> GetAllBooks()
        {
            return bookRepository.FindAll().ToList();
        }

        public Book? GetBook(int id)
        {
            return bookRepository.FindById(id);
        }

        public List<Book> GetBooksBySearchCriteria(BookSearchObject searchObject)
        {
            List<Book> allBooks = GetAllBooks();
            var filtered = allBooks.Where(b => searchObject.Title == null || b.Title.ToLowerInvariant().Contains(searchObject.Title.ToLowerInvariant())
            && searchObject.Author == null || (b.AuthorName + " " + b.AuthorSurname).ToLowerInvariant().Contains(searchObject.Author.ToLowerInvariant())
            && searchObject.Publisher == null || b.Publisher.ToLowerInvariant().Contains(searchObject.Publisher.ToLowerInvariant()))
                .SelectMany(b => new Book[] { b })
                .Distinct()
                .ToList();

            if (searchObject.IsAvailable)
            {
                return filtered.Where(b => CheckAvailability(b))
                    .SelectMany(b => new Book[] { b })
                    .ToList();
            }
            return filtered;
        }

        // TODO : Implement reservationservice methods and test them!
        // TODO : logic in here is flawed, fix it
        public bool CheckAvailability(Book b)
        {
            List<Reservation> activeReservations = reservationService.GetReservationsByBookId(b.BookId)
                .Where(res => res.EndDate > DateTime.Now)
                .ToList();

            return activeReservations.Count() < b.Quantity;

        }

        public void UpdateQuantity(Book duplicate)
        {
            // 1) create new book object from duplicate
            ++duplicate.Quantity;
            bookRepository.Update(duplicate);
            
            //if (duplicate != null)
            //{
            //    duplicate.Quantity += 1;
            //    bookRepository.Update(duplicate);
            //    bookRepository.SaveChanges();
            //    Console.WriteLine($"Quantity of book '{duplicate.Title}' incremented: total copies = {duplicate.Quantity}");
            //}
            //else
            //{
            //    throw new Exception("!!!!!!");

            //}
        }

        public void EditBook(Book book)
        {
            //if (authenticatedUser.Role == UserRole.Admin)
            if (currentUser.IsAdmin)
            {

                bookRepository.Update(book);
                bookRepository.SaveChanges();
            }
        }

        public void DeleteBook(Book book)
        {
            //if (authenticatedUser.Role == UserRole.Admin)
            if (currentUser.IsAdmin)
            {
                bookRepository.Delete(book);
                bookRepository.SaveChanges();
            }
        }

        public List<Book> GetAllBooksByPattern(BookSearchObject searchObject)
        {
            IEnumerable<Book> books = bookRepository.FindAll();
            List<Book> filteredList = new List<Book>();
            if (books.Count() > 0)
            {
                filteredList = books.ToList().Where(b => (searchObject.Title == null || b.Title.ToLowerInvariant().Contains(searchObject.Title.ToLowerInvariant())) && (searchObject.Author == null || $"{b.AuthorName} {b.AuthorSurname}".ToLowerInvariant().Contains(searchObject.Author.ToLowerInvariant()) && (searchObject.Publisher == null || b.Publisher.ToLowerInvariant().Contains(searchObject.Publisher.ToLowerInvariant()))))
                    .SelectMany(b => new Book[] { b })
                    .Distinct()
                    .ToList();
            }
            return filteredList;
        }

    }
}
