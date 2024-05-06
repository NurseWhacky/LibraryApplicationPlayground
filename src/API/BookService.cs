﻿using API.DTOs;
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
                }
                else
                { UpdateQuantity(duplicate); }
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
        public bool CheckAvailability(Book b)
        {
            List<Reservation> activeReservations = reservationService.GetReservationsByBookId(b.BookId)
                .Where(res => res.EndDate > DateTime.Now)
                .ToList();

            return activeReservations.Count() < b.Quantity;

        }

        public void UpdateQuantity(Book duplicate)
        {
            Book bookToIncrement = bookRepository.FindById(duplicate.BookId);
            if (bookToIncrement != null && bookToIncrement.Equals(duplicate))
            {
                bookToIncrement.Quantity += 1;
                bookRepository.Update(bookToIncrement);
                Console.WriteLine($"Quantity of book '{bookToIncrement.Title}' incremented: total copies = {bookToIncrement.Quantity}");
            }
        }

        public void EditBook(Book book)
        {
            //if (authenticatedUser.Role == UserRole.Admin)
            if (currentUser.IsAdmin)
            {

                bookRepository.Update(book);
            }
        }

        public void DeleteBook(Book book)
        {
            //if (authenticatedUser.Role == UserRole.Admin)
            if (currentUser.IsAdmin)
            {
                bookRepository.Delete(book);
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
