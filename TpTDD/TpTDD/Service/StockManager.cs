using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            Book result = new Book();

            if (isbn != null)
            {
                result = databaseBookService.GetBookByIsbn(isbn);
                if (result == null || areBookInfoComplete(result) == false)
                {
                    result = webBookService.GetBookByIsbn(isbn);
                }
            }

            return result;
        }

        /// <summary>
        /// Verify if one property of the book is null 
        /// </summary>
        /// <param name="book"></param>
        /// <returns>true if no properties are null</returns>
        private bool areBookInfoComplete(Book book)
        {            
            bool result = true;

            if (book != null)
            {
                foreach (PropertyInfo prop in book.GetType().GetProperties())
                {
                    if (prop.GetValue(book) == null) { result = false; }
                }
            }

            return result;
        }


    }
}
