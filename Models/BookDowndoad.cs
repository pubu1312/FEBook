using System;
using System.Collections.Generic;

#nullable disable

namespace FEBook.Models
{
    public partial class BookDowndoad
    {
        public int DownloadId { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual Book Book { get; set; }
        public virtual Account User { get; set; }
    }
}
