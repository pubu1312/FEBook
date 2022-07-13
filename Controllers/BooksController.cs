using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using FEBook.Models;
using FEBook.DataAccess.Repository;

namespace EbookProject.Controllers
{
    public class BooksController : Controller{
        IBookRepository BookRepository = null;
        public BooksController() => BookRepository = new BookRepository();
        public async Task<ActionResult> Index(string searchString)
        {
            var BookList = BookRepository.GetBooks();
            var searchBook = from book in BookList select book;
            if (!String.IsNullOrEmpty(searchString))
            {
                searchBook = searchBook.Where(c => c.BookName!.Contains(searchString));

            }
            return View(await Task.FromResult(searchBook.ToList()));
        }
        
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Book = BookRepository.GetBookByID(id.Value);
            if (Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }
        public ActionResult Create() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book Book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BookRepository.InsertBook(Book);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(Book);
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Book = BookRepository.GetBookByID(id.Value);
            if (Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Book Book)
        {
            try
            {
                if (id != Book.BookId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    BookRepository.UpdateBook(Book);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Book = BookRepository.GetBookByID(id.Value);
            if (Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                BookRepository.DeleteBook(id);
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