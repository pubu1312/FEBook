using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DataAccess.Repository;
using FEBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace FEBook.Controllers
{
    public class AuthorController : Controller
    {
        IAuthorRepository authorRepository = null;
        public AuthorController() => authorRepository = new AuthorRepository();

        public IActionResult Index()
        {
            var authorList = authorRepository.GetAuthors();
            return View(authorList);
        }

        public IActionResult Detail(int? id)
        {
            if (id == null) {
                return NotFound();
            }
            var author = authorRepository.GetAuthorByID(id.Value);
            if (author == null) {
                return NotFound();
            }
            return View(author);
        }

        public IActionResult Create() 
        {
            //System.Console.WriteLine("HI");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (Author author)
        {
            System.Console.WriteLine(author.AuthorName);
            try 
            {
                if (ModelState.IsValid) {
                    authorRepository.CreateAuthor(author);
                    return RedirectToAction(nameof(Index));
                }
            } catch (Exception) {
                //
            }
            return View (author);
        }


        public IActionResult Edit(int? id) 
        {
            if (id==null) return NotFound();
            Author author = authorRepository.GetAuthorByID(Convert.ToInt32(id));
            if (author==null) return NotFound();
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Author author)
        {
            try 
            {
                var _author = authorRepository.GetAuthorByID(author.AuthorId);
                if (_author == null) return NotFound();
                if (ModelState.IsValid) 
                {
                    authorRepository.EditAuthor(author);
                    return RedirectToAction(nameof(Index));
                }

            } catch (Exception) {
                //
            }
            return View (author);
        }

        public IActionResult Delete(int? id) 
        {
            if (id == null) return NotFound();
            Author author = authorRepository.GetAuthorByID(Convert.ToInt32(id));
            if (author == null) return NotFound();
            return View (author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Author author)
        {
            try 
            {
                var _author = authorRepository.GetAuthorByID(author.AuthorId);
                if (_author == null) return NotFound();
                if (ModelState.IsValid) {
                    authorRepository.DeleteAuthor(author.AuthorId);
                    return RedirectToAction(nameof(Index));
                }

            } catch (Exception) {
                //
            }
            return View(author);
        }
    }
}