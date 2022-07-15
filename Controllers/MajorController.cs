using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DataAccess.Repository;
using FEBook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEBook.Controllers
{
    public class MajorController : Controller
    {
        IMajorRepository majorRepository = null;
        public MajorController() => majorRepository = new MajorRepository();

        public IActionResult Index()
        {
            dynamic model = new ExpandoObject();
            model.userEmail = HttpContext.Session.GetString("email");
            model.majorList = new List<Major>();
            model.majorList = majorRepository.GetMajors();
            return View(model);
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
            Major major = majorRepository.GetMajorByID(Convert.ToInt32(id));
            majorRepository.DeleteMajor(major.MajorId);
            if (major == null) return NotFound();
            return RedirectToAction(nameof(Index));
        }

    }
}