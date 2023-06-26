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
    }
}
