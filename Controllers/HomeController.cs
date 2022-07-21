using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
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
        BookDAO bookDAO = new BookDAO();
        public HomeController()
        {
            bookRepository = new BookRepository();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<ActionResult> Index(string searchString)
        {
            dynamic model = new ExpandoObject();
            string userEmail = HttpContext.Session.GetString("email");

            Account acc = null;
            if (userEmail != null)
            {
                accountRepository = new AccountRepository();
                acc = accountRepository.GetAccountByEmail(userEmail);

            }
            var BookList = bookRepository.GetBooks();

            var searchBook = from book in BookList select book;


            if (!String.IsNullOrEmpty(searchString))
            {
                searchBook = searchBook.Where(c => c.BookName!.ToLower().Contains(searchString.ToLower()));
                model.searchrs = String.Format("There are {0} result(s) of keyword {1}", searchBook.Count(), searchString);

            }
            else
            {
                model.searchrs = "There are no result of search book ";
            }

            model.userSession = acc;
            model.searchBook = searchBook.Reverse();
            return View(await Task.FromResult(model));

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginReal(Account account)
        {
            HttpContext.Session.SetString("email", "");
            try
            {
                if (ModelState.IsValid)
                {
                    //session here
                    HttpContext.Session.SetString("email", accountDAO.LoginAccount(account.Email, account.Passwords).Email);
                    if (HttpContext.Session.GetString("email") != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Account account)
        {
            try
            {
                Account _acc = new Account();
                if (ModelState.IsValid)
                {
                    //session here
                    if (_acc.Email == account.Email)
                    {
                        ViewBag.Message = "This email is already in use! Please sign in";
                    }
                    else
                    {
                        accountDAO.RegisterAccount(account.UserName, account.Email, account.Passwords);
                    }
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Register", "Home");
        }

        public IActionResult ForgotPass()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPass(Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    accountDAO.ForgotPass(account.Email, account.Passwords);
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return View();
        }

         public ActionResult ViewBook(int? id)
        {
            dynamic model = new ExpandoObject();
            string userEmail = HttpContext.Session.GetString("email");
            var Book = bookRepository.GetBookByID(id.Value);
            Account acc = null;
            if (userEmail != null)
            {
                accountRepository = new AccountRepository();
                acc = accountRepository.GetAccountByEmail(userEmail);
            }
            else{
                 model.userSession = acc;
                 model.book = Book;
            ViewBag.title = Book.BookName;
            return View(model);

            }
            if (id == null)
            {
                return NotFound();
            }
           
            if (Book == null)
            {
                return NotFound();
            }
            AccountDAO accountdao = new AccountDAO();
            Account account = accountdao.GetAccountByEmail(HttpContext.Session.GetString("email"));
            HistoryController hc = new HistoryController();
            hc.AddHistory(account.UserId,Book.BookId);
            model.userSession = acc;
            model.book = Book;
            ViewBag.title = Book.BookName;
            return View(model);
        }

        [HttpGet]
        public IActionResult DownloadBook(int? id)
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

            string filePath = "~/pdf/" + Book.Content;
            Response.Headers.Add("Content-Disposition", "inline; filename=" + Book.Content);
            return File(filePath, "application/pdf");
        }

    }
}
