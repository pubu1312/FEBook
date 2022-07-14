using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DataAccess.DAO;
using FEBook.DataAccess.Repository;
using FEBook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace FEBook.Controllers
{
    public class HomeController : Controller
    {
        AccountDAO accountDAO = new AccountDAO();
        IBookRepository bookRepository = null;
        public HomeController() => bookRepository = new BookRepository();

        public IActionResult Privacy(){
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(){
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public async Task<ActionResult> Index(string searchString)
        {
            var BookList = bookRepository.GetBooks();
            var searchBook = from book in BookList select book;
            if (!String.IsNullOrEmpty(searchString))
            {
                searchBook = searchBook.Where(c => c.BookName!.Contains(searchString));

            }
            return View(await Task.FromResult(searchBook.ToList()));

        }

        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        public IActionResult LoginReal(Account account)
        {
            System.Console.WriteLine(account.Email + " " + account.Passwords);
            HttpContext.Session.SetString("email", "");            
            try {
                if (ModelState.IsValid) {
                    //session here
                    HttpContext.Session.SetString("email", accountDAO.LoginAccount(account.Email, account.Passwords).Email);   
                    if (HttpContext.Session.GetString("email") != null) {
                        return RedirectToAction("Index", "Home");
                    }
                }
            } catch (Exception) {

            }
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Register(){
            return View();
        }

        public IActionResult ForgotPass(){
            return View();
        }
        
        
    }
}
