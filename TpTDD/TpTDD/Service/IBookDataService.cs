using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpTDD.Model;

namespace TpTDD.Service
{
    public interface IBookDataService
    {
        public List<Book> GetBooks();
        public Book GetBookByIsbn(string isbn);
        public bool CreateBook(string isbn, string title, string author, string editor, string formatName);
        public bool UpdateBook(string isbn, string title, string author, string editor, string formatName);
        public bool DeleteBook(string isbn);
    }
}
