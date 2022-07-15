using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DataAccess.Repository;
using FEBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace FEBook.Controllers
{
    public class BookController : Controller
    {
        IBookRepository bookRepository = null;
        public BookController() => bookRepository = new BookRepository();
        public IActionResult Index()
        {
            var bookList = bookRepository.GetBooks();
            return View(bookList);

        }

        public IActionResult Detail(int? id)
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

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book Book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bookRepository.InsertBook(Book);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(Book);
            }
        }
        public IActionResult Edit(int? id)
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
        public IActionResult Edit(int id, Book Book)
        {
            try
            {
                if (id != Book.BookId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    bookRepository.UpdateBook(Book);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
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
