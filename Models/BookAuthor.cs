using System;
using System.Collections.Generic;

#nullable disable

namespace FEBook.Models
{
    public partial class BookAuthor
    {
        public DateTime AuthorUpdateDate { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
