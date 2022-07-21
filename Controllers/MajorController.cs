using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DataAccess.DAO;
using FEBook.DataAccess.Repository;
using FEBook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEBook.Controllers
{
    public class MajorController : Controller
    {
        AccountRepository accountRepository = new AccountRepository();
        MajorDAO majorDAO = new MajorDAO();
        IMajorRepository majorRepository = null;
        public MajorController() => majorRepository = new MajorRepository();

        public IActionResult Index()
        {
            dynamic model = new ExpandoObject();
            string userEmail = HttpContext.Session.GetString("email");

            Account acc = null;
            if (userEmail != null)
            {
                accountRepository = new AccountRepository();
                acc = accountRepository.GetAccountByEmail(userEmail);
                if (acc.Roles == "Admin")
                {
                    model.userEmail = HttpContext.Session.GetString("email");
                    model.MajorList = new List<Major>();
                    model.MajorList = majorDAO.GetMajorList();

                    model.MajorDeletedList = new List<Major>();
                    model.MajorDeletedList = majorDAO.GetMajorDeletedList();

                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }


        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var major = majorRepository.GetMajorByID(id.Value);
            if (major == null)
            {
                return NotFound();
            }
            return View(major);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Major major)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    majorRepository.CreateMajor(major);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

            }
            return View(major);
        }



        public IActionResult Edit(int? id)
        {

            if (id == null) return NotFound();
            Major major = majorRepository.GetMajorByID(Convert.ToInt32(id));
            if (major == null) return NotFound();
            return View(major);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Major major)
        {
            try
            {
                var _major = majorRepository.GetMajorByID(major.MajorId);
                if (_major == null) return NotFound();
                if (ModelState.IsValid)
                {
                    majorRepository.EditMajor(major);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

            }
            return View(major);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            majorRepository.DeleteMajor(id.Value);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteOnce(int? id)
        {
            if (id == null) return NotFound();
            else
            {
                majorDAO.DeleteOnce(Convert.ToInt32(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Restore(int? id)
        {
            if (id == null) return NotFound();
            else
            {
                majorDAO.Restore(Convert.ToInt32(id));
            }
            return RedirectToAction(nameof(Index));
        }

    }
}