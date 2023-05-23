using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpTDD.Model
{
    public class Book
    {

        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Editor { get; set; }
        public Format Format { get; set; }
        public bool IsFou { get; set; } //Fof : Free of use

        public Book(string isbn, string title, string author, string editor, Format format)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            Editor = editor;
            Format = format;
            IsFou = true;
        }

    }
}
