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
        //Upphafstilli hvaða service þarf að nota í þessum controller
        private CourseService _service = new CourseService();
        private AssignmentService _assignmentService = new AssignmentService();
        private UserService _userService = new UserService();


        //Sæki ákveðinn áfanga
        public ActionResult ViewCourse(int id)
        {
            string userId = User.Identity.GetUserId();
            var viewModel = new AssignmentViewModel();
            viewModel = _assignmentService.getAllAssignmentsInCourse(id);
            return View(viewModel);
        }

        //Sæki alla áfanga sem notandi hefur aðgang að
        public ActionResult GetAllCourses()
        {
            var viewModel = new CourseViewModel();
            if (User.IsInRole("Teacher") || User.IsInRole("Student"))
            {
                string userId = User.Identity.GetUserId();

                viewModel = _service.getAllUsersCourses(userId);
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

        //Sæki alla áfanga notanda til að birta í nav bar
        public ActionResult ViewAllMyCourses()
        {
            string userId = User.Identity.GetUserId();
            var viewModel = new CourseViewModel();
            Session["CurrentSemesterId"] = _service.getCurrentSemester();
            viewModel = _service.getAllUsersCoursesOnSemester(userId, int.Parse(Session["CurrentSemesterId"].ToString()));
            return View(viewModel);
        }

        public ActionResult GetAllCoursesOnSemester()
        {
            int semesterId = 2;
            var viewModel = new CourseViewModel();
            viewModel = _service.getAllCoursesOnSemester(semesterId);
            return View(viewModel);
        }

        //public ActionResult GetAllUserCourses()
        //{
        //    string userID = User.Identity.GetUserId();
        //    var viewModel = new CourseViewModel();
        //    viewModel = _service.getAllUsersCourses(userID);
        //    return View(viewModel);
        //}

        //public ActionResult GetAllUserCoursesOnSemester()
        //{
        //    string userId = "3";
        //    int semesterId = 1;
        //    var viewModel = new CourseViewModel();
        //    viewModel = _service.getAllUsersCoursesOnSemester(userId, semesterId);
        //    return View(viewModel);
        //}

        //Stofna nýjan áfanga
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

        //Vista nýjan áfanga
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
        public ActionResult AddUsersToCourse()
        {
            if(User.IsInRole("Admin"))
            { 
            CourseUsersViewModel viewModel = new CourseUsersViewModel();
            viewModel.AllUsers = IdentityManager.GetUsers();
            viewModel.Courses = new SelectList(_service.getAllCourses().CourseList, "CourseId", "CourseName");
            return View(viewModel);
            }
            else
            {
                return View("NotFound");
            }
        }

        //Vista notendur saman við áfanga
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

        //Breyta áfanga
        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var viewModel = _service.getCourseById(id);
            viewModel.Semesters = new SelectList(_service.getAllSemesters().SemesterList, "SemesterId", "SemesterName");
            return View(viewModel);
        }

        //Vista breytingar á áfanga
        [HttpPost]
        public ActionResult EditCourse(CourseViewModel newCourse)
        {
            if(ModelState.IsValid)
            { 
                _service.editCourse(newCourse);
                return RedirectToAction("GetAllCourses");
            }
        
            return View(newCourse);

        }

       
    }
}