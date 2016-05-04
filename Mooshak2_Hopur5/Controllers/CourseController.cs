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
        public ActionResult ViewCourse(int id)
        {
            var viewModel = _service.getCourseById(id);
            return View(viewModel);
        }

        public ActionResult GetAllCourses()
        {
            var viewModel = new CourseViewModel();
            viewModel = _service.getAllCourses();
            return View(viewModel);
        }

        public ActionResult ViewAllMyCourses()
        {
            int userId = 3;
            var viewModel = new CourseViewModel();
            viewModel = _service.getAllUsersCourses(userId);
            return View(viewModel);
        }

        public ActionResult GetAllCoursesOnSemester()
        {
            int semesterId = 2;
            var viewModel = new CourseViewModel();
            viewModel = _service.getAllCoursesOnSemester(semesterId);
            return View(viewModel);
        }

        public ActionResult GetAllUserCourses()
        {
            int userId = 3;
            var viewModel = new CourseViewModel();
            viewModel = _service.getAllUsersCourses(userId);
            return View(viewModel);
        }

        public ActionResult GetAllUserCoursesOnSemester()
        {
            int userId = 3;
            int semesterId = 1;
            var viewModel = new CourseViewModel();
            viewModel = _service.getAllUsersCoursesOnSemester(userId, semesterId);
            return View(viewModel);
        }
    }
}