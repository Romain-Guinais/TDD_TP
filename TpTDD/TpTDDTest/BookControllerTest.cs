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
        Mock<BookDataService> _mockBookDataService;
        Mock<BookDataService> _mockBookWebService;

        [TestInitialize]
        public void initMocks()
        {
            manager = new StockManager();
            //manager.databaseBookService = _mockBookDataService.Object;
            //manager.webBookService = _mockBookWebService.Object;
        } 

        [TestMethod]
        public void GetDbBooksShouldReturnEmptyListIfNoBooksInDbResponse()
        {
            _mockBookDataService.Setup(m => m.GetBooks()).Returns(new List<Book>());
            
            setMockInManager(_mockBookDataService, _mockBookWebService);

            List<Book> books = manager.GetDbBooks();

            books.Should().NotBeNull();
            books.Should().BeEmpty();

        }

        public void setMockInManager(Mock<BookDataService> db, Mock<BookDataService> web)
        {
            manager.databaseBookService = db.Object;
            manager.webBookService = web.Object;
        }

    }
}
