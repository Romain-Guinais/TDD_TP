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
        public BookDataService databaseBookService { get; set; }
        public BookDataService webBookService { get; set; }

        public List<Book> GetDbBooks()
        {
            // TODO
            return null;
        }


    }
}
