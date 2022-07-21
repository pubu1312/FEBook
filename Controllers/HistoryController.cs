// using System;
// using System.Collections.Generic;
// using System.Dynamic;
// using System.Linq;
// using System.Threading.Tasks;
// using FEBook.Data;
// using FEBook.DataAccess.DAO;
// using FEBook.Models;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;

// namespace FEBook.Controllers
// {
//     public class HistoryController : Controller
//     {
//         public HistoryController(){

//         }
//         public void AddHistory(int userId, int bookId){
//                 ReadingHistory rh = new ReadingHistory(DateTime.Now, userId,bookId);
//                  using var  _db = new EbookManagementContext();
//                 _db.ReadingHistories.Add(rh);
//                 _db.SaveChanges();      
//         }
//         public IActionResult Index(int userId){
//               string userEmail = HttpContext.Session.GetString("email");
//               if(userEmail == null){
//                 return RedirectToAction("Index", "Home");
//               }
//             dynamic model = new ExpandoObject();
//             var _db = new EbookManagementContext();
//             AccountDAO accountdao = new AccountDAO();
//             var account =  accountdao.GetAccountByEmail(userEmail);
//             var historylist = _db.ReadingHistories.Where(x=>x.UserId == account.UserId).ToList();
//             var booklist = _db.Books.ToList();
//             foreach (ReadingHistory rh in historylist){
//                 foreach (Book book in booklist){
//                     if(book.BookId == rh.BookId){
//                         rh.Book = book;
//                     }

//                 }
//             }
//             model.user = account.UserName;
//             model.historyList = historylist.ToList(); 
//             foreach (ReadingHistory rh in historylist){
//                 System.Console.WriteLine("{0} {1} {2}",rh.DateRead,rh.Book.BookName,account.UserName);
//             }
//             return View(model);
//         }
        
//     }
// }