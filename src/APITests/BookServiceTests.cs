using Microsoft.VisualStudio.TestTools.UnitTesting;
using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Model;
using DataAccess;
using API.DTOs;

namespace API.Tests
{
    [TestClass()]
    public class BookServiceTests
    {
        BookService serviceTest = new BookService(new XmlRepository<Book>(), new LoggedUser("usr", "User"));
        //List<Book> books = new List<Book>() {
        //    new Book() { AuthorName = "ciccio", AuthorSurname = "pasticcio", Title = "Piccolo Pippo cucciolo eroico", Publisher = "Mondadori" },
        // new Book() { AuthorName = "Papa", AuthorSurname = "Francesco", Title = "La buona novella", Publisher = "Mondadori" },
        // new Book() { AuthorName = "Silvio", AuthorSurname = "Abberluschioni", Title = "I'll be back", Publisher = "Minimum fax" }
        //};

        [TestMethod()]
        public void GetAllBooksByPatternTest()
        {
            List<Book> should_contain_ToKillAMockingBird = serviceTest.GetAllBooksByPattern(new BookSearchObject(title: "mocking", author: null, publisher: null, true));

            Console.WriteLine(should_contain_ToKillAMockingBird.Count());
            foreach (var b in should_contain_ToKillAMockingBird)
            {
                Console.WriteLine(b.Title);

            }

            Assert.IsTrue(should_contain_ToKillAMockingBird.Count() == 1);
        }


        //[TestMethod()]
        //public void GetAllBooksTest()
        //{
        //    List<Book> books = serviceTest.GetAllBooks();
        //    foreach (Book book in books)
        //    {
        //        Console.WriteLine(book.Title);
        //    }

        //    Console.WriteLine(books.Count());
        //    Assert.IsTrue(books.Count > 0);
        //}
        //}
    }
}