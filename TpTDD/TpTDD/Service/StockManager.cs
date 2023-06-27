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
        /// Create a new book
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="editor"></param>
        /// <param name="FormatName"></param>
        /// <returns>true if book was ceated</returns>
        public bool CreateBook(string isbn, string title, string author, string editor, string formatName)
        {
            bool result = false;

            if (isbn != null && isbn != "")
            {
                result = databaseBookService.CreateBook(isbn, title, author, editor, formatName);
                if (result == false)
                {
                    Book book = webBookService.GetBookByIsbn(isbn);
                    result = webBookService.CreateBook(book.ISBN, book.Title, book.Author, book.Editor, book.Format.Name);
                }
            }

            return result;
        }


        /// <summary>
        /// Udate a book
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="editor"></param>
        /// <param name="FormatName"></param>
        /// <returns>true if book was updated</returns>
        public bool UpdateBook(string isbn, string title, string author, string editor, string formatName)
        {
            bool result = false;

            if (isbn != null && isbn != "")
            {
                result = databaseBookService.UpdateBook(isbn, title, author, editor, formatName);
            }

            return result;
        }

        public bool DeleteBook(string isbn)
        {
            bool result = false;

            if (isbn != null && isbn != "")
            {
                result = databaseBookService.DeleteBook(isbn);
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
