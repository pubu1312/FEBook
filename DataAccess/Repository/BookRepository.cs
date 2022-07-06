using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DataAccess.DAO;
using FEBook.Models;

namespace FEBook.DataAccess.Repository
{
    public class BookRepository : IBookRepository
    {
        public IEnumerable<Book> GetBooks()
        {
            return BookDAO.Instance.GetBookList();
        }             

        public Book GetBookByID(int BookID)
        {
            return BookDAO.Instance.GetBookByID(BookID);
        }
      
        public void InsertBook(Book Book)
        {
            BookDAO.Instance.AddNew(Book);
        }

        public void UpdateBook(Book Book)
        {
            BookDAO.Instance.Update(Book);
        }

        public void DeleteBook(int BookID)
        {
            BookDAO.Instance.Remove(BookID);
        }
    }
}