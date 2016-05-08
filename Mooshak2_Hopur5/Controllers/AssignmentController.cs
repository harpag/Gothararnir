using Microsoft.AspNet.Identity;
using Mooshak2_Hopur5.Models.Entities;
using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2_Hopur5.Controllers
{
    public class AssignmentController : Controller
    {
        private AssignmentService _service = new AssignmentService();
        private CourseService _courseService = new CourseService();

        // GET: Assignment
        public ActionResult Index()
        {
            //TODO:
            return View();
        }

        //Notandi býr til nýtt assignment
        public ActionResult AddAssignment(int? id)
        {
            AssignmentViewModel viewModel = new AssignmentViewModel();
            //Ef notandi kemur frá ákveðnum áfanga þá frumstilli ég það
            if(id.HasValue)
            { 
                viewModel.CourseId = id.Value;
            }
            string userId = User.Identity.GetUserId();
            viewModel.UserCourses = new SelectList(_courseService.getAllUsersCourses(userId).CourseList, "CourseId", "CourseName");
            viewModel.ProgrammingLanguages = _service.getAllProgrammingLanguages().ProgrammingLanguages;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddAssignment(AssignmentViewModel assignment)
        {
            if (ModelState.IsValid)
            {
                string serverPath = Server.MapPath("~");
                assignment = _service.addAssignment(assignment, serverPath);
                
                return RedirectToAction("ViewCourse", "Course", new { id = assignment.CourseId });
            }

            return View(assignment);
        }

        public ActionResult OpenAssignments()
        {
            string userId = User.Identity.GetUserId();
            var viewModel = new AssignmentViewModel();
            viewModel = _service.getOpenAssignments();
            return View(viewModel);
        }

        public ActionResult ClosedAssignments()
        {
            string userId = User.Identity.GetUserId();
            var viewModel = new AssignmentViewModel();
            viewModel = _service.getClosedAssignments();
            return View(viewModel);
        }

        

        public ActionResult ViewAssignment(int id)
        {
            var viewModel = _service.getAssignmentById(id);
            string userId = User.Identity.GetUserId();
            if(User.IsInRole("Student"))
            {
                viewModel.UserAssignment =_service.getUserAssignmentById(userId, id);
                viewModel.AssignmentSubmissionsList = _service.getUsersSubmissions(userId, id);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ViewAssignment(AssignmentViewModel viewModel)
        {
            if (ModelState.IsValid && viewModel.SubmissionUploaded != null && viewModel.SubmissionUploaded.ContentLength > 0)
            {
                string serverPath = Server.MapPath("~");
                _service.studentSubmitsAssignment(viewModel, serverPath);
            }

            return RedirectToAction("ViewAssignment", "Assignment", new { id = viewModel.AssignmentId });
        }
    }
}