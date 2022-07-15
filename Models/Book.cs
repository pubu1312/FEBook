using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

#nullable disable

namespace FEBook.Models
{
    public partial class Book
    {
        public Book()
        {
            BookAuthors = new List<BookAuthor>();
            BookDowndoads = new List<BookDowndoad>();
            BookRatings = new List<BookRating>();
            ManageBooks = new List<ManageBook>();
            ReadingHistories = new List<ReadingHistory>();
            //images="~/wwwroot/images/cleancode.png/";
        }

        

        public Book(int bookId, string bookName, string bookCover, int yearOfPublic, string summary, string laguages, string content, DateTime updateDate){
            BookId = bookId;
            BookName = bookName;
            BookCover = bookCover;
            YearOfPublic = yearOfPublic;
            Summary = summary;
            Languages = laguages;
            Content = content;
            UpdateDate = updateDate;
        }

        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookCover { get; set; }
        public int YearOfPublic { get; set; }
        public string Summary { get; set; }
        public string Languages { get; set; }
        public string Content { get; set; }
        public DateTime UpdateDate { get; set; }
        //[NotMapped]
        //public HttpPostedFileBase ImageUpload { get; set; }
        public int? SubjectId { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<BookDowndoad> BookDowndoads { get; set; }
        public virtual ICollection<BookRating> BookRatings { get; set; }
        public virtual ICollection<ManageBook> ManageBooks { get; set; }
        public virtual ICollection<ReadingHistory> ReadingHistories { get; set; }
    }
}
