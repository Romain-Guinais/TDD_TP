using Microsoft.AspNetCore.Mvc;
using TpTDD.Model;

namespace TpTDDApi
{
    [ApiController]
    [Route("[book]")]
    public class BookController : ControllerBase
    {
        public List<Book> GetBooks()
        {
            // TODO
            return null;
        }
    }
}
