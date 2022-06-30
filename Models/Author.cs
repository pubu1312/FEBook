using System;
using System.Collections.Generic;

#nullable disable

namespace FEBook.Models
{
    public partial class Author
    {
        public Author()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Profiles { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
