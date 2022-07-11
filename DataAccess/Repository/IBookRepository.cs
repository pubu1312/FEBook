using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Models;

namespace FEBook.DataAccess.Repository
{
    public interface IBookRepository
    {
        public IEnumerable<Book> GetBooks();

        Book GetBookByID(int BookID);
        void InsertBook(Book Book);
        void DeleteBook(int BookID);
        void UpdateBook(Book Book);
    }
}