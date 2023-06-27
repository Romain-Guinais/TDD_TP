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
    public class StockManagerTest
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

        /// <summary>
        /// Book searched by ISBN.
        /// Test if a book is returned from both DB and webService.
        /// If it's data are partially stored in the Db and in webService.
        /// </summary>
        [TestMethod]
        public void GetBookByIsbnShouldReturnBook()
        {
            Book testBook = new Book("XXXX", "book title", "Me", "also Me", new Format("Poche"));
            _mockBookDataService.Setup(m => m.GetBookByIsbn("XXXX")).Returns(new Book("XXXX", "book title", null, null, new Format("Poche")));
            _mockBookWebService.Setup(m => m.GetBookByIsbn("XXXX")).Returns(testBook);

            setMockInManager(_mockBookDataService, _mockBookWebService);

            Book book = manager.GetBookByIsbn("XXXX");

            book.Should().NotBeNull();
            book.Should().BeEquivalentTo(testBook);
        }

        /// <summary>
        /// CreateBook with all parameters filled
        /// Should return true
        /// </summary>
        [TestMethod]
        public void CreateBookWithFullParamShouldReturnTrue()
        {
            _mockBookDataService.Setup(m => m.CreateBook("XXXX", "createTitle", "createAuthor", "createEditor", "Broché")).Returns(true);

            setMockInManager (_mockBookDataService, _mockBookWebService);

            bool result = manager.CreateBook("XXXX", "createTitle", "createAuthor", "createEditor", "Broché");

            result.Should().BeTrue();
        }

        /// <summary>
        /// CreateBook with part of the param
        /// Should return true because missing data are taken from WebServ
        /// </summary>
        [TestMethod]
        public void CreateBookWithPartialParamShouldReturnTrue()
        {
            Book testBook = new Book("XXXX", "createTitle", "createAuthor", "createEditor", new Format("Broché"));
            _mockBookDataService.Setup(m => m.CreateBook("XXXX", "createTitle", null, null, "Broché")).Returns(false);
            _mockBookWebService.Setup(m => m.CreateBook("XXXX", "createTitle", "createAuthor", "createEditor", "Broché")).Returns(true);
            _mockBookWebService.Setup(m => m.GetBookByIsbn("XXXX")).Returns(testBook);

            setMockInManager(_mockBookDataService, _mockBookWebService);

            bool result = manager.CreateBook("XXXX", "createTitle", null, null, "Broché");

            result.Should().BeTrue();
        }

        /// <summary>
        /// CreateBook without isbn
        /// Should return false
        /// </summary>
        [TestMethod]
        public void CreateBookWhitoutIsbnShouldReturnFalse()
        {
            _mockBookDataService.Setup(m => m.CreateBook(null, "title", "author", "editor", "Poche" )).Returns(false);
            _mockBookWebService.Setup(m => m.CreateBook(null, "title", "author", "editor", "Poche")).Returns(false);

            setMockInManager (_mockBookDataService, _mockBookWebService);

            bool result = manager.CreateBook(null, "title", "author", "editor", "Poche");

            result.Should().BeFalse();
        }

        /// <summary>
        /// Update a book whitout is isbn
        /// Should return false
        /// </summary>
        [TestMethod]
        public void UpdateBookWhitoutIsbnShouldReturnFalse()
        {
            _mockBookDataService.Setup(m => m.UpdateBook(null, "title", "author", "editor", "Poche")).Returns(false);
            _mockBookWebService.Setup(m => m.UpdateBook(null, "title", "author", "editor", "Poche")).Returns(false);

            setMockInManager(_mockBookDataService, _mockBookWebService);

            bool result = manager.UpdateBook(null, "title", "author", "editor", "Poche");

            result.Should().BeFalse();
        }

        /// <summary>
        /// Update a book
        /// Should return true
        /// </summary>
        [TestMethod]
        public void UpdateBookShouldReturnTrue()
        {

            _mockBookDataService.Setup(m => m.UpdateBook("XXXX", "title", "author", "editor", "Poche")).Returns(false);
            _mockBookWebService.Setup(m => m.UpdateBook("XXXX", "title", "author", "editor", "Poche")).Returns(false);

            setMockInManager(_mockBookDataService, _mockBookWebService);            

            bool result = manager.UpdateBook(null, "title", "author", "editor", "Poche");

            result.Should().BeTrue();
        }

        public void setMockInManager(Mock<IBookDataService> db, Mock<IBookDataService> web)
        {
            manager.databaseBookService = db.Object;
            manager.webBookService = web.Object;
        }

    }
}
