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
    public class SubjectController : Controller
    {
        ISubjectRepository subjectRepository = null;
        public SubjectController() => subjectRepository = new SubjectRepository();

        public IActionResult Index()
        {
            dynamic model = new ExpandoObject();
            model.userEmail = HttpContext.Session.GetString("email");
            model.subjectList = new List<Major>();
            model.subjectList = subjectRepository.GetSubjects();
            
            return View(model);
        }

        public IActionResult Detail(int? id)
        {
            if (id == null) {
                return NotFound();
            }
            var subject = subjectRepository.GetSubjectByID(id.Value);
            if (subject == null) {
                return NotFound();
            }
            return View(subject);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Subject subject)
        {
            try
            {
                if (ModelState.IsValid) {
                    subjectRepository.CreateSubject(subject);
                    return RedirectToAction(nameof(Index));
                }
            } catch (Exception) {
                
            }
            return View(subject);
        }

        

        public IActionResult Edit(int? id)
        {
   
            if (id ==null) return NotFound();
            Subject subject = subjectRepository.GetSubjectByID(Convert.ToInt32(id));
            if (subject == null) return NotFound();
            return View(subject);

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Subject subject)
        {
            try
            {
                var _Subject = subjectRepository.GetSubjectByID(subject.SubjectId);
                if(_Subject == null) return NotFound();
                if (ModelState.IsValid) {
                    subjectRepository.EditSubject(subject);
                    return RedirectToAction(nameof(Index));
                }
            } catch (Exception) {
            
            }
            return View(subject);
        }

        public IActionResult Delete(int? id)
        {
   
            if (id ==null) return NotFound();
            Subject subject = subjectRepository.GetSubjectByID(Convert.ToInt32(id));
            if (subject == null) return NotFound();
            return View(subject);

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Subject subject)
        {
            try
            {
                var _Subject = subjectRepository.GetSubjectByID(subject.SubjectId);
                if(_Subject == null) return NotFound();
                if (ModelState.IsValid) {
                    subjectRepository.DeleteSubject(subject.SubjectId);
                    return RedirectToAction(nameof(Index));
                }
            } catch (Exception) {
                //
            }
            return View(subject);
        }
    }
}