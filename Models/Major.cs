using System;
using System.Collections.Generic;

#nullable disable

namespace FEBook.Models
{
    public partial class Major
    {
        public Major()
        {
            Accounts = new HashSet<Account>();
            Subjects = new HashSet<Subject>();
        }

        public string MajorName { get; set; }
        public int MajorId { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
