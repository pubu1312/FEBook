using System;
using System.Collections.Generic;
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
        IBookRepository bookRepository = null;
        public BookController() => bookRepository = new BookRepository();
        public IActionResult Index()
        {
            //System.Console.WriteLine("Im here");
            var bookList = bookRepository.GetBooks();
            return View(bookList);

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

                    string bookContentSrc = String.Format("pdf/{0}", bookContent.FileName);
                    book.Content = bookContentSrc;
                    // using (var file = new FileStream(bookCoverPath, FileMode.Create))
                    // {
                    //     bookCover.CopyTo(file);
                    // }
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

                    string bookContentSrc = String.Format("pdf/{0}", bookContent.FileName);
                    book.Content = bookContentSrc;
                    
                    
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
                System.Console.WriteLine("Src: " + imgSrc);
            }
            return imgSrc;
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Book = bookRepository.GetBookByID(id.Value);
            if (Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                bookRepository.DeleteBook(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
