using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpTDD.Model;

namespace TpTDD.Service
{
    public class StockManager
    {
        public IBookDataService databaseBookService { get; set; }
        public IBookDataService webBookService { get; set; }

        /// <summary>
        /// Return the list of all the books in the Database
        /// </summary>
        /// <returns></returns>
        public List<Book> GetDbBooks()
        {            
            return databaseBookService.GetBooks();
        }


        /// <summary>
        /// Return the book corresponding to the ISBN
        /// </summary>
        /// <returns></returns>
        public Book GetBookByIsbn(string isbn)
        {
            return null;
        }

    }
}
