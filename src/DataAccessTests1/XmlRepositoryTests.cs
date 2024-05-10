using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Model;
using System.Xml.Linq;

namespace DataAccess.Tests
{
    [TestClass()]
    public class XmlRepositoryTests
    {
        public string path = "library.xml";
        public XDocument library;
        public XmlRepository<Book> bookRepo = new XmlRepository<Book>();
        public XmlRepository<Reservation> reservationRepo = new XmlRepository<Reservation>();
        public XmlRepository<User> userRepo = new XmlRepository<User>();

        [TestMethod()]
        public void DeleteExistingBookTest()
        {
            library = XDocument.Load(path);
            int bookId = 1;
            Book? bookToDelete = bookRepo.FindById(bookId);
            XElement bookToXElement = Utilities.FromEntity<Book>(bookToDelete);
            bookRepo.Delete(bookToDelete);
            Assert.IsFalse(library.Root.Descendants("Book").Contains(bookToXElement));
            //Assert.Fail();
        }
    }
}