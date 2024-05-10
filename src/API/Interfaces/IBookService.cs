using API.DTOs;
using API.Model;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Interfaces
{
    interface IBookService
    {
        List<Book> GetAllBooks();
        List<Book> GetBooksBySearchCriteria(BookSearchObject searchCriteria);
        Book GetBookById(int id);
        void AddBook(BookDTO book);
        void DeleteBook(int id);
        void UpdateBook(BookDTO book);

    }
}
