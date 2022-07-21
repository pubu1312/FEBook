using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DataAccess.DAO;
using FEBook.DataAccess.Repository;
using FEBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace FEBook.Controllers
{
    public class AuthorController : Controller
    {
        AuthorDAO authorDAO = new AuthorDAO();
        IAuthorRepository authorRepository = null;
        public AuthorController() => authorRepository = new AuthorRepository();

        //GET: Index
        public IActionResult Index() {
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author author)
        {
            try
            {
                if (ModelState.IsValid) {
                    authorRepository.CreateAuthor(author);
                    return RedirectToAction(nameof(Index));
                }
            } catch (Exception) {
                //
            }
            return View(author);
        }

         public IActionResult Edit(int? id)
        {
   
            if (id ==null) return NotFound();
            Author author = authorRepository.GetAuthorByID(Convert.ToInt32(id));
            if (author == null) return NotFound();
            return View(author);

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Author author)
        {
            try
            {
                var _Author = authorRepository.GetAuthorByID(author.AuthorId);
                if(_Author == null) return NotFound();
                if (ModelState.IsValid) {
                    authorRepository.EditAuthor(author);
                    return RedirectToAction(nameof(Index));
                }
            } catch (Exception) {
                
            }
            return View(author);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            authorRepository.DeleteAuthor(id.Value);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DeleteOnce(int? id)
        {
            if (id == null) return NotFound();
            else
            {
                authorDAO.DeleteOnce(Convert.ToInt32(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Restore(int? id)
        {
            if (id == null) return NotFound();
            else
            {
                authorDAO.Restore(Convert.ToInt32(id));
            }
            return RedirectToAction(nameof(Index));
        }
        
    }
}