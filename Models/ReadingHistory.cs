using System;
using System.Collections.Generic;

#nullable disable

namespace FEBook.Models
{
    public partial class ReadingHistory
    {
        public ReadingHistory()
        {

        }
        public ReadingHistory(DateTime DateRead, int UserId, int BookId)
        {
            this.DateRead = DateRead;
            this.UserId = UserId;
            this.BookId = BookId;
            DeleteStatus = false;


        }
        public DateTime DateRead { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual Book Book { get; set; }
        public virtual Account User { get; set; }
    }
}