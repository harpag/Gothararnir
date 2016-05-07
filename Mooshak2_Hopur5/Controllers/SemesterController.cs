using Mooshak2_Hopur5.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2_Hopur5.Controllers
{
    public class SemesterController : Controller
    {
        private SemesterService _service = new SemesterService();
        // GET: Semester
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllSemester()
        {
            var viewModel = new SemesterViewModel();
            viewModel = _service.getAllSemester();
            return View(viewModel);
        }
    }
}