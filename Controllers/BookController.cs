using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DataAccess.DAO;
using FEBook.DataAccess.Repository;
using FEBook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEBook.Controllers
{
    public class BookController : Controller
    {
        BookDAO bookDAO = new BookDAO();
        IBookRepository bookRepository = null;
        public BookController() => bookRepository = new BookRepository();
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                dynamic model = new ExpandoObject();
                model.userEmail = HttpContext.Session.GetString("email");
                model.BookList = new List<Book>();
                model.BookList = bookDAO.GetBookList();

                model.BookDeletedList = new List<Book>();
                model.BookDeletedList = bookDAO.GetBookDeletedList();

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = bookRepository.GetBookByID(id.Value);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book, IFormFile bookCover, IFormFile bookContent)
        {
            var errors = new string[6];
            if (book.BookName == null )
            {
                ViewBag.errorNull_BookName = "This input is not null!";
            }

            if (book.BookAuthors == null )
            {
                ViewBag.errorNull_Author = "This input is not null!";
            }

            if (book.Content == null )
            {
                ViewBag.errorFormat_Content = "File format incorrect";
            }

            if (book.BookCover == null )
            {
                ViewBag.errorFormat_Cover = "File format incorrect";
            }

            if (book.YearOfPublic < 1500 || book.YearOfPublic > DateTime.Now.Year)
            {
                ViewBag.errorNull__YOP = "This input should be from 1500 up to now!!!!!!! ";
            }
            if (book.Languages == null )
            {
                ViewBag.errorNull_Languages = "This input is not null!";
            }

            if (book.Summary == null )
            {
                ViewBag.errorNull_Summary = "This input is not null!";
            }
            try
            {
                if (bookCover != null)
                {
                    string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/BookCovers/");
                    if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
                    //chi dinh duong dan se luu
                    //string bookCoverPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images", bookCover.FileName);

                    // copy file vao thu muc chi dinh
                    var bookCoverPath = Path.Combine(dirPath, bookCover.FileName);

                    using var fileStream = new FileStream(bookCoverPath, FileMode.Create);
                    bookCover.CopyTo(fileStream);

                    string bookCoverSrc = String.Format("images/BookCovers/{0}", bookCover.FileName);
                    book.BookCover = bookCoverSrc;

                    /* Book content*/
                    string dirPath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdf/");
                    if (!Directory.Exists(dirPath2)) Directory.CreateDirectory(dirPath2);

                    // copy file vao thu muc chi dinh
                    var bookContentPath = Path.Combine(dirPath2, bookContent.FileName);

                    using var fileStream2 = new FileStream(bookContentPath, FileMode.Create);
                    bookContent.CopyTo(fileStream2);

                    string bookContentSrc = String.Format(bookContent.FileName);
                    book.Content = bookContentSrc;

                    book.UpdateDate = DateTime.Now.Date;

                    bookRepository.InsertBook(book);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                //
            }
            return View(book);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = bookRepository.GetBookByID(id.Value);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormFile bookCover, IFormFile bookContent)
        {
            try
            {
                Book book = bookRepository.GetBookByID(id);
                if (ModelState.IsValid)
                {
                    string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/BookCovers/");
                    string bookCoverSource = UploadedFile(bookCover);
                    book.BookCover = bookCoverSource;
                    string dirPath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdf/");
                    var bookContentPath = Path.Combine(dirPath2, bookContent.FileName);
                    using var fileStream2 = new FileStream(bookContentPath, FileMode.Create);
                    bookContent.CopyTo(fileStream2);
                    string bookContentSrc = String.Format(bookContent.FileName);
                    book.Content = bookContentSrc;
                    book.UpdateDate = DateTime.Now.Date;
                    bookRepository.UpdateBook(book);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                System.Console.WriteLine(ex.Message);
                return View();
            }
        }

        private string UploadedFile(IFormFile file)
        {
            string imgSrc = null;

            if (file != null)
            {
                string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/BookCovers/");

                //create folder if not exist
                if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

                var filePath = Path.Combine(dirPath, file.FileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);

                imgSrc = String.Format("images/BookCovers/{0}", file.FileName);
            }
            return imgSrc;
        }


        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            bookRepository.DeleteBook(Convert.ToInt32(id));
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteOnce(int? id)
        {
            if (id == null) return NotFound();
            else
            {
                bookDAO.DeleteOnce(Convert.ToInt32(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Restore(int? id)
        {
            if (id == null) return NotFound();
            else
            {
                bookDAO.Restore(Convert.ToInt32(id));
            }
            return RedirectToAction(nameof(Index));
        }

        

        

    }
}
