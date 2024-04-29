using Microsoft.VisualStudio.TestTools.UnitTesting;
using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Model;
using DataAccess;

namespace API.Tests
{
    [TestClass()]
    public class BookServiceTests
    {
        BookService serviceTest = new BookService();
        List<Book> books = new List<Book>() {
            new Book() { AuthorName = "ciccio", AuthorSurname = "pasticcio", Title = "Piccolo Pippo cucciolo eroico", Publisher = "Mondadori" },
         new Book() { AuthorName = "Papa", AuthorSurname = "Francesco", Title = "La buona novella", Publisher = "Mondadori" },
         new Book() { AuthorName = "Silvio", AuthorSurname = "Abberluschioni", Title = "I'll be back", Publisher = "Minimum fax" }
        };

        [TestMethod()]
        public void GetAllBooksByPatternTest()
        {
            List<Book> should_contain_one_book = serviceTest.GetAllBooksByPattern(new DTOs.BookSearchObject(title: "be back", author: null, publisher: null, true));

            Console.WriteLine(should_contain_one_book.Count);

            Assert.IsTrue(should_contain_one_book.Count == 1);
        }
    }
}