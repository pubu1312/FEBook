using System;
using System.Collections.Generic;

#nullable disable

namespace FEBook.Models
{
    public partial class BookRating
    {
        public int RateId { get; set; }
        public DateTime DateRating { get; set; }
        public int NoOfRate { get; set; }
        public string CmtContent { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual Book Book { get; set; }
        public virtual Account User { get; set; }
    }
}
