using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
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
        IAccountRepository accountRepository = null;
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
            dynamic model = new ExpandoObject();
            string userEmail = HttpContext.Session.GetString("email");
            
             Account acc = null;
            if(userEmail != null){
                accountRepository = new AccountRepository();
                acc = accountRepository.GetAccountByEmail(userEmail);

            }

            var BookList = bookRepository.GetBooks();

            var searchBook = from book in BookList select book;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                searchBook = searchBook.Where(c => c.BookName!.Contains(searchString));

            }
            model.userSession = acc;
            model.searchBook = searchBook.Reverse();
            return View(await Task.FromResult(model));

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
      public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }


        public IActionResult Register(){
            return View();
        }

        public IActionResult ForgotPass(){
            return View();
        }
        
        public ActionResult ViewBook(int? id)
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
     
    }
}
