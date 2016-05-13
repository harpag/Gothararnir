﻿using Mooshak2_Hopur5.Handlers;
using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Services;
using System.Web.Mvc;

namespace Mooshak2_Hopur5.Controllers
{
    [CustomHandleErrorAttribute]
    public class SemesterController : Controller
    {
        private CourseService _service = new CourseService();
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
            if (ModelState.IsValid)
            {
                _service.addSemester(newSemester);

                return RedirectToAction("GetAllSemesters");
            }

            return View();
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