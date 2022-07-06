using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Controllers.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FEBook.Controllers
{
    public class MajorController : Controller
    {
        IMajorReposity majorRepository = null;
        public MajorController() => majorRepository = new MajorRepository();

        public IActionResult Index()
        {
            var majorList = majorRepository.GetMajors();
            return View(majorList);
        }
    }
}