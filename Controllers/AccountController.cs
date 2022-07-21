using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DataAccess.DAO;
using FEBook.DataAccess.Repository;
using FEBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEBook.Controllers
{
    public class AccountController : Controller
    {
        IAccountRepository accountRepository = null;
        public AccountController() => accountRepository = new AccountRepository();

        public IActionResult Index()
        {
            string userEmail = HttpContext.Session.GetString("email");
            if (userEmail == null)
            {
                return RedirectToAction("Index", "Home");
            }
            AccountDAO accountdao = new AccountDAO();
            Account account = accountdao.GetAccountByEmail(userEmail);
            if (account == null || account.Roles != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var accountList = accountRepository.GetAccounts();
            return View(accountList);

        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    accountRepository.InsertAccount(account);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

            }
            return View(account);
        }

        public IActionResult Edit(int? id)
        {

            if (id == null) return NotFound();
            Account account = accountRepository.GetAccountByID(Convert.ToInt32(id));
            if (account == null) return NotFound();
            return View(account);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Account account)
        {
            try
            {
                var _account = accountRepository.GetAccountByID(Convert.ToInt32(account.UserId));
                if (_account == null) return NotFound();
                if (ModelState.IsValid)
                {
                    accountRepository.UpdateAccount(account);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

            }
            return View(account);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            accountRepository.DeleteAccount(id.Value);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProfileUser(Account account)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                string email = HttpContext.Session.GetString("email");
                var _account = accountRepository.GetAccountByEmail(email);
                
                return View(_account);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Account account)
        {
            try
            {
                var _account = accountRepository.GetAccountByID(Convert.ToInt32(account.UserId));
                if (ModelState.IsValid)
                {
                    accountRepository.UpdateAccount(account);
                    return RedirectToAction(nameof(ProfileUser));
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return View(account);
        }

    }
}

