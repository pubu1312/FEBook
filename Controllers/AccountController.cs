using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DataAccess.Repository;
using FEBook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEAccount.Controllers
{
    public class AccountController: Controller
    {
      IAccountRepository AccountRepository = null;
        public AccountController() => AccountRepository = new AccountRepository();
        public async Task<ActionResult> Index(string searchString) {
    
            var AccountList = AccountRepository.GetAccounts();
            var searchAccount = from Account in AccountList select Account;
            if(!String.IsNullOrEmpty(searchString)){
                 searchAccount = searchAccount.Where(c => c.UserName!.Contains(searchString));   

            }
            return View(await Task.FromResult(searchAccount.ToList()));

        }
        public ActionResult Detail(int? id) {
            if(id == null){
                return NotFound();
            }
            var Account = AccountRepository.GetAccountByID(id.Value);
            if(Account == null) {
                return NotFound();
            }
            return View(Account);
        }
        public ActionResult Create() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (Account Account){
            try{
                if(ModelState.IsValid){
                    AccountRepository.InsertAccount(Account);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex){
                ViewBag.Message = ex.Message;
                return View(Account);
            }
        }
        public ActionResult Edit(int? id){
            if (id == null){
                return NotFound();
            }
            var Account = AccountRepository.GetAccountByID(id.Value);
            if(Account == null){
                return NotFound();
            }
            return View(Account);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Account Account){
            try{
                if (id != Account.UserId){
                    return NotFound();
                }
                if(ModelState.IsValid){
                    AccountRepository.UpdateAccount(Account);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex){
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        public ActionResult Delete(int? id){
            if (id == null){
                return NotFound();
            }
            var Account = AccountRepository.GetAccountByID(id.Value);
            if (Account == null){
                return NotFound();
            }
            return View(Account);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id){
            try {
                AccountRepository.DeleteAccount(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex){
                ViewBag.Message = ex.Message;
                return View();
            }
        }
          public IActionResult Detail()
        {
             if (HttpContext.Session.GetString("email") != null) {
                    ViewBag.userName = HttpContext.Session.GetString("UserName");
                    ViewBag.roles = HttpContext.Session.GetString("Role");
                    ViewBag.fullName = HttpContext.Session.GetString("FullName");
                    ViewBag.phone = HttpContext.Session.GetString("Phone");
                    
                       return View();
                    }
            else {
                return RedirectToAction("Index","Login");
            }
        }

        

    }
}