using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Data;
using FEBook.DataAccess.DAO;
using FEBook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEBook.Controllers
{
    public class HistoryController : Controller
    {
        public HistoryController()
        {

        }
        public void AddHistory(int userId, int bookId)
        {
            ReadingHistory rh = new ReadingHistory(DateTime.Now, userId, bookId);
            using var _db = new EbookManagementContext();
            _db.ReadingHistories.Add(rh);
            _db.SaveChanges();
        }
        public IActionResult Index()
        {
            string userEmail = HttpContext.Session.GetString("email");
            if (userEmail == null)
            {
                return RedirectToAction("Index", "Home");
            }
            dynamic model = new ExpandoObject();
            var _db = new EbookManagementContext();
            AccountDAO accountdao = new AccountDAO();
            var account = accountdao.GetAccountByEmail(userEmail);
            var historylist = _db.ReadingHistories.Where(x => x.UserId == account.UserId && x.DeleteStatus == false).OrderByDescending(r => r.DateRead);
            var booklist = _db.Books.ToList();
            foreach (ReadingHistory rh in historylist)
            {
                foreach (Book book in booklist)
                {
                    if (book.BookId == rh.BookId)
                    {
                        rh.Book = book;
                    }

                }
            }
            model.id = account.UserId;
            model.user = account.UserName;
            model.historyList = historylist.ToList();
            foreach (ReadingHistory rh in historylist)
            {
                System.Console.WriteLine("{0} {1} {2}", rh.DateRead, rh.Book.BookName, account.UserName);
            }
            return View(model);
        }

        public IActionResult Delete(int userId, int bookId, DateTime DateRead)
        {
            try
            {
                using var context = new EbookManagementContext();
                var test = context.ReadingHistories.ToList();
                ReadingHistory res = null;
                foreach (ReadingHistory x in test)
                {
                    if (x.DateRead.ToString() == DateRead.ToString() && x.BookId == bookId && x.UserId == userId)
                    {
                        res = x;
                        break;
                    }
                }

                if (res != null)
                {
                    res.DeleteStatus = true;
                    context.ReadingHistories.Update(res);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Reading History does not exist.");
                }
            }
            catch (Exception ex)
            {
                return View();
                throw new Exception(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAll(int userId)
        {
            try
            { 
                using var context = new EbookManagementContext();
                var test = context.ReadingHistories.Where(x => x.UserId == userId).ToList();
                if (test != null)
                {
                    foreach (ReadingHistory x in test)
                    {
                        x.DeleteStatus = true;
                        context.ReadingHistories.Update(x);
                        context.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("The Reading History does not exist.");
                }
            }
            catch (Exception ex)
            {
                return View();
                throw new Exception(ex.Message);
            }
            return RedirectToAction("Index");
        }

    }
}
