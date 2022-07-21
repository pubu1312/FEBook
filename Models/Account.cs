using System;
using System.Collections.Generic;

#nullable disable

namespace FEBook.Models
{
    public partial class Account
    {
        public Account(string username, string email, string password)
        {
            UserId=0;
            UserName = username;
            Roles = "User";
            FullName = username;
            Email = email;
            Passwords = password;
            Phone = "0000000000";
        }
        public Account()
        {
            BookDowndoads = new List<BookDowndoad>();
            BookRatings = new List<BookRating>();
            ManageBooks = new List<ManageBook>();
            ReadingHistories = new List<ReadingHistory>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Roles { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Passwords { get; set; }
        public int? MajorId { get; set; }
        public string Phone { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual Major Major { get; set; }
        public virtual ICollection<BookDowndoad> BookDowndoads { get; set; }
        public virtual ICollection<BookRating> BookRatings { get; set; }
        public virtual ICollection<ManageBook> ManageBooks { get; set; }
        public virtual ICollection<ReadingHistory> ReadingHistories { get; set; }
    }
}
