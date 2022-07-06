using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Controllers.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FEBook.Controllers
{
    public class SubjectController : Controller
    {
        ISubjectRepository subjectRepository = null;
        public SubjectController() => subjectRepository = new SubjectRepository();

        public IActionResult Index()
        {
            var subjectList = subjectRepository.GetSubjects();
            return View(subjectList);
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
    }
}