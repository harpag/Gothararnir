using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2_Hopur5.Controllers
{
    public class SemesterController : Controller
    {
        private CourseService _service = new CourseService();
        // GET: Semester
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddSemester()
        {
            SemesterViewModel viewModel = new SemesterViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddSemester(SemesterViewModel newSemester)
        {
            bool announcement = _service.addSemester(newSemester);

            return RedirectToAction("GetAllSemesters");

        }

        [HttpPost]
        public ActionResult EditSemester(SemesterViewModel newSemester)
        {
            _service.editSemester(newSemester);

            return RedirectToAction("GetAllSemesters");

        }

        public ActionResult GetAllSemesters()
        {
            var viewModel = new SemesterViewModel();
            viewModel = _service.getAllSemesters();
            return View(viewModel);
        }

        public ActionResult ViewSemester(int id)
        {
            var viewModel = _service.getSemesterById(id);
            return View(viewModel);
        }
    }
}