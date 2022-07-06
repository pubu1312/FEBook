using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DataAccess.Repository;
using FEBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace FEBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IBookRepository bookRepository = null;
        public HomeController() => bookRepository = new BookRepository();

        public IActionResult Privacy()
        {
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
    }
}
