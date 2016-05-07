using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mooshak2_Hopur5.Models;
using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mooshak2_Hopur5.Utilities;

namespace Mooshak2_Hopur5.Controllers
{
    public class CourseController : Controller
    {
        private CourseService _service = new CourseService();
        private AssignmentService _assignmentService = new AssignmentService();
        private UserService _userService = new UserService();

        // GET: Course
        public ActionResult ViewCourse(int id)
        {
            string userId = User.Identity.GetUserId();
            var viewModel = _service.getCourseById(id);
            viewModel.AssignmentList = _assignmentService.getAllUserAssignmentsInCourse(userId, id).AssignmentList;
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
            string userId = User.Identity.GetUserId();
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
            string userID = User.Identity.GetUserId();
            var viewModel = new CourseViewModel();
            viewModel = _service.getAllUsersCourses(userID);
            return View(viewModel);
        }

        public ActionResult GetAllUserCoursesOnSemester()
        {
            string userId = "3";
            int semesterId = 1;
            var viewModel = new CourseViewModel();
            viewModel = _service.getAllUsersCoursesOnSemester(userId, semesterId);
            return View(viewModel);
        }

        //Stofna nýjan áfanga
        public ActionResult CreateCourse()
        {
            CourseViewModel viewModel = new CourseViewModel();
            viewModel.Semesters = new SelectList(_service.getAllSemesters().SemesterList , "SemesterId", "SemesterName");
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateCourse(CourseViewModel newCourse)
        {
            if (ModelState.IsValid)
            {
                Boolean course = _service.addCourse(newCourse);
                return RedirectToAction("Index", "Home");
            }
            
            return View(newCourse);
        }

        public ActionResult AddUsersToCourse()
        {
            CourseUsersViewModel viewModel = new CourseUsersViewModel();
            viewModel.AllUsers = IdentityManager.GetUsers();
            viewModel.Courses = new SelectList(_service.getAllCourses().CourseList, "CourseId", "CourseName");
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddUsersToCourse(CourseUsersViewModel newCourseUsers)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < newCourseUsers.AllUsers.Count; i++)
                {
                    if(newCourseUsers.CheckedUsers[i]) {
                        _service.addUsersToCourse(newCourseUsers.AllUsers[i].Id, newCourseUsers.CourseId);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}