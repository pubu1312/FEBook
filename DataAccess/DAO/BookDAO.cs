using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Models;
namespace FEBook.DataAccess.DAO
{
    public class BookDAO
    {
        private static BookDAO instance = null;
        private static readonly object instanceLock = new object();
        public static BookDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Book> GetBookList()
        {
            var Books = new List<Book>();
            try
            {
                using var context = new EbookManagementContext();
                Books = context.Books.Where(s => s.DeleteStatus == false).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Books;
        }

        public IEnumerable<Book> GetBookDeletedList()
        {
            var Books = new List<Book>();
            try
            {
                using var context = new EbookManagementContext();
                Books = context.Books.Where(s => s.DeleteStatus == true).ToList();
                return Books;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Book GetBookByID(int BookID)
        {
            Book Book = null;
            try
            {
                using var context = new EbookManagementContext();
                Book = context.Books.SingleOrDefault(c => c.BookId == BookID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Book;
        }


        public void AddNew(Book Book)
        {
            try
            {
                Book _Book = GetBookByID(Book.BookId);
                //System.Console.WriteLine("Hello im in add new");
                if (_Book == null)
                {
                    using var context = new EbookManagementContext();
                    System.Console.WriteLine("This book is not existed");
                    context.Books.Add(Book);
                    System.Console.WriteLine("Add completed!");
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Book is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Book book)
        {
            try
            {
                Book _Book = GetBookByID(book.BookId);
                if (_Book != null)
                {
                    System.Console.WriteLine("toi day r");
                    using var context = new EbookManagementContext();
                    context.Books.Update(book);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Book does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int BookID)
        {
            try
            {
                Book Book = GetBookByID(BookID);
                if (Book != null)
                {
                    using var context = new EbookManagementContext();
                    context.Books.Remove(Book);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Book does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOnce(int BookID){
            try{
                Book Book = GetBookByID(BookID);
                if (Book != null)
                {
                    using var context = new EbookManagementContext();
                    Book.DeleteStatus=true;
                    context.Books.Update(Book);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Book does not not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal void Restore(int BookID)
        {
            try{
                Book Book = GetBookByID(BookID);
                if (Book != null)
                {
                    using var context = new EbookManagementContext();
                    Book.DeleteStatus=false;
                    context.Books.Update(Book);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Book does not not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

}