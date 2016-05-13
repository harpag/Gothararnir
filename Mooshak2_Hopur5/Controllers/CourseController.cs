using Microsoft.AspNet.Identity;
using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Services;
using System;
using System.Web.Mvc;
using Mooshak2_Hopur5.Utilities;
using Mooshak2_Hopur5.Handlers;

namespace Mooshak2_Hopur5.Controllers
{
    [CustomHandleErrorAttribute]
    public class CourseController : Controller
    {
        //Upphafstilling á service
        private CourseService _service = new CourseService();
        private AssignmentService _assignmentService = new AssignmentService();

        public ActionResult ViewCourse(int id)
        {
            string userId = User.Identity.GetUserId();
            var viewModel = new AssignmentViewModel();
            viewModel = _assignmentService.getAllAssignmentsInCourse(id);
            return View(viewModel);
        }

        //Sækir alla áfanga sem notandi hefur aðgang að
        public ActionResult GetAllCourses()
        {
            var viewModel = new CourseViewModel();
            if (User.IsInRole("Teacher") || User.IsInRole("Student"))
            {
                string suserId = User.Identity.GetUserId();

                viewModel = _service.getAllUsersCourses(suserId);
            }
            else if (User.IsInRole("Admin"))
            {
                viewModel = _service.getAllCourses();
            }
            else
            {
                return View("NotFound");
            }
            return View(viewModel);
        }

        //Sækir alla áfanga notanda til að birta í navigation barnum
        public ActionResult ViewAllMyCourses()
        {
            string userId = User.Identity.GetUserId();
            var viewModel = new CourseViewModel();
            Session["CurrentSemesterId"] = _service.getCurrentSemester();
            viewModel = _service.getAllUsersCoursesOnSemester(userId, int.Parse(Session["CurrentSemesterId"].ToString()));
            return View(viewModel);
        }

        //Stofnar nýjan áfanga
        public ActionResult CreateCourse()
        {
            if (User.IsInRole("Admin"))
            {
                CourseViewModel viewModel = new CourseViewModel();
                viewModel.Semesters = new SelectList(_service.getAllSemesters().SemesterList, "SemesterId", "SemesterName");
                return View(viewModel);
            }
            else
            {
                return View("NotFound");
            }
        }

        //Vistar nýjan áfanga
        [HttpPost]
        public ActionResult CreateCourse(CourseViewModel newCourse)
        {
            if (ModelState.IsValid)
            {
                Boolean course = _service.addCourse(newCourse);
                return RedirectToAction("GetAllCourses");
            }
            return View(newCourse);
        }

        //Bæta notendum við áfanga
        public ActionResult AddUsersToCourse(int? id)
        {
            if (User.IsInRole("Admin"))
            {
                CourseUsersViewModel viewModel = new CourseUsersViewModel();
                viewModel.AllUsers = IdentityManager.GetUsers();
                viewModel.Courses = new SelectList(_service.getAllCourses().CourseList, "CourseId", "CourseName");
                if (id.HasValue)
                {
                    viewModel.CourseId = id.Value;
                }
                return View(viewModel);
            }
            else
            {
                return View("NotFound");
            }
        }

        //Vistar notendur í áfanga
        [HttpPost]
        public ActionResult AddUsersToCourse(CourseUsersViewModel newCourseUsers)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < newCourseUsers.AllUsers.Count; i++)
                {
                    if (newCourseUsers.CheckedUsers[i])
                    {
                        _service.addUsersToCourse(newCourseUsers.AllUsers[i].Id, newCourseUsers.CourseId);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var viewModel = _service.getCourseById(id);
            viewModel.Semesters = new SelectList(_service.getAllSemesters().SemesterList, "SemesterId", "SemesterName");
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditCourse(CourseViewModel newCourse)
        {
            if (ModelState.IsValid)
            {
                _service.editCourse(newCourse);
                return RedirectToAction("GetAllCourses");
            }
            return View(newCourse);
        }
    }
}