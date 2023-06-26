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

        [TestMethod]
        public void GetDbBooksShouldReturnEmptyListIfNoBooksInDbResponse()
        {
            _mockBookDataService.Setup(m => m.GetBooks()).Returns(new List<Book>());
            
            setMockInManager(_mockBookDataService, _mockBookWebService);

            List<Book> books = manager.GetDbBooks();

            books.Should().NotBeNull();
            books.Should().BeEmpty();

        }

        public void setMockInManager(Mock<IBookDataService> db, Mock<IBookDataService> web)
        {
            manager.databaseBookService = db.Object;
            manager.webBookService = web.Object;
        }

    }
}
