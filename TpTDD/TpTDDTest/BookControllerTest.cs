using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TpTDDApi;
using Moq;
using System.Collections.Generic;
using TpTDD.Model;
using TpTDD.Service;
using FluentAssertions;

namespace TpTDDTest
{
    [TestClass]
    public class BookControllerTest
    {
        StockManager manager;
        Mock<IBookDataService> _mockBookDataService;
        Mock<IBookDataService> _mockBookWebService;

        [TestInitialize]
        public void initMocks()
        {
            manager = new StockManager();

            _mockBookDataService = new Mock<IBookDataService>();
            _mockBookWebService = new Mock<IBookDataService>();

            //manager.databaseBookService = _mockBookDataService.Object;
            //manager.webBookService = _mockBookWebService.Object;
        } 

        /// <summary>
        /// Test if getDbBooks return a empty list when db have no books  
        /// </summary>
        [TestMethod]
        public void GetDbBooksShouldReturnEmptyListIfNoBooksInDbResponse()
        {
            _mockBookDataService.Setup(m => m.GetBooks()).Returns(new List<Book>());
            
            setMockInManager(_mockBookDataService, _mockBookWebService);

            List<Book> books = manager.GetDbBooks();

            books.Should().NotBeNull();
            books.Should().BeEmpty();

        }

        /// <summary>
        /// Book searched by ISBN.
        /// Test if a book is returned from the db.
        /// If all it's data are stored in db.
        /// </summary>
        [TestMethod]
        public void GetBookByIsbnfromOnlyDbShouldReturnBook()
        {
            Book testBook = new Book("XXXX", "book title", "Me", "also Me", new Format("Poche"));
            _mockBookDataService.Setup(m => m.GetBookByIsbn("XXXX")).Returns(testBook);

            setMockInManager(_mockBookDataService, _mockBookWebService);

            Book book = manager.GetBookByIsbn("XXXX");

            book.Should().NotBeNull();
            book.Should().BeEquivalentTo(testBook);
        }

        /// <summary>
        /// Book searched by ISBN.
        /// Test if a book is returned from the webService.
        /// If all it's data are stored in webService.
        /// </summary>
        [TestMethod]
        public void GetBookByIsbnfromOnlyWebServiceShouldReturnBook()
        {
            Book testBook = new Book("XXXX", "book title", "Me", "also Me", new Format("Poche"));
            _mockBookWebService.Setup(m => m.GetBookByIsbn("XXXX")).Returns(testBook);

            setMockInManager(_mockBookDataService, _mockBookWebService);

            Book book = manager.GetBookByIsbn("XXXX");

            book.Should().NotBeNull();
            book.Should().BeEquivalentTo(testBook);
        }

        public void setMockInManager(Mock<IBookDataService> db, Mock<IBookDataService> web)
        {
            manager.databaseBookService = db.Object;
            manager.webBookService = web.Object;
        }

    }
}
