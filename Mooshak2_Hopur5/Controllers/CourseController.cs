using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2_Hopur5.Controllers
{
    public class CourseController : Controller
    {
        private CourseService _service = new CourseService();

        // GET: Course
        public ActionResult GetCourse()
        {
            int courseId = 1;
            var viewModel = _service.getCourseById(courseId);
            return View(viewModel);
        }

        public ActionResult GetAllCourses()
        {
            var viewModel = new CourseViewModel();
            viewModel = _service.getAllCourses();
            return View(viewModel);
        }
    }
}