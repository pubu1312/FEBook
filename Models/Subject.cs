using System;
using System.Collections.Generic;

#nullable disable

namespace FEBook.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Books = new HashSet<Book>();
        }

        public int SubjectId { get; set; }
        public string Subname { get; set; }
        public int? MajorId { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual Major Major { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
