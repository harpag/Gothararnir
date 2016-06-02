using Microsoft.AspNet.Identity;
using Mooshak2_Hopur5.Handlers;
using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Services;
using System;
using System.Web.Mvc;

namespace Mooshak2_Hopur5.Controllers
{
    [CustomHandleErrorAttribute]
    public class AssignmentController : Controller
    {
        private AssignmentService _service = new AssignmentService();
        private CourseService _courseService = new CourseService();

        //Notandi býr til nýtt assignment
        public ActionResult AddAssignment(int? id)
        {
            AssignmentViewModel viewModel = new AssignmentViewModel();

            //Ef notandi kemur frá ákveðnum áfanga er það frumstillt
            if (id.HasValue)
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

            string userId = User.Identity.GetUserId();
            assignment.UserCourses = new SelectList(_courseService.getAllUsersCourses(userId).CourseList, "CourseId", "CourseName");
            assignment.ProgrammingLanguages = _service.getAllProgrammingLanguages().ProgrammingLanguages;

            return View(assignment);
        }

        //Notandi breytir verkefni
        public ActionResult EditAssignment(int id)
        {
            AssignmentViewModel viewModel = _service.getAssignmentById(id);

            if (viewModel == null)
            {
                View("NotFound");
            }
            string userId = User.Identity.GetUserId();
            viewModel.UserCourses = new SelectList(_courseService.getAllUsersCourses(userId).CourseList, "CourseId", "CourseName");
            viewModel.ProgrammingLanguages = _service.getAllProgrammingLanguages().ProgrammingLanguages;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditAssignment(AssignmentViewModel assignment)
        {
            if (ModelState.IsValid)
            {
                string serverPath = Server.MapPath("~");
                assignment = _service.editAssignment(serverPath, assignment);

                return RedirectToAction("ViewAssignment", "Assignment", new { id = assignment.AssignmentId });
            }

            string userId = User.Identity.GetUserId();
            assignment.UserCourses = new SelectList(_courseService.getAllUsersCourses(userId).CourseList, "CourseId", "CourseName");
            assignment.ProgrammingLanguages = _service.getAllProgrammingLanguages().ProgrammingLanguages;

            return View(assignment);
        }

        //Notandi breytir verkefnahluta
        public ActionResult EditAssignmentPart(int? id)
        {
            if (id.HasValue == false)
            {
                return View("NotFound");
            }

            string serverPath = Server.MapPath("~");
            var assignment = _service.getAssignmentPartById(serverPath, id.Value);
            if (assignment == null)
            {
                return View("NotFound");
            }

            assignment.ProgrammingLanguages = _service.getAllProgrammingLanguages().ProgrammingLanguages;

            return View(assignment);
        }

        [HttpPost]
        public ActionResult EditAssignmentPart(AssignmentPartViewModel assignment)
        {
            if (ModelState.IsValid)
            {
                string serverPath = Server.MapPath("~");
                assignment = _service.editAssignmentPart(serverPath,assignment);

                return RedirectToAction("ViewAssignment", "Assignment", new { id = assignment.AssignmentId });
            }

            assignment.ProgrammingLanguages = _service.getAllProgrammingLanguages().ProgrammingLanguages;

            return View(assignment);
        }

        public ActionResult AddPartToAssignment(int? id)
        {
            if (id.HasValue == false)
            {
                return View("NotFound");
            }

            var assignment = _service.getAssignmentById(id.Value);
            if (assignment == null)
            {
                return View("NotFound");
            }

            AssignmentPartViewModel model = new AssignmentPartViewModel();
            model.AssignmentId = id.Value;
            string userId = User.Identity.GetUserId();
            model.ProgrammingLanguages = _service.getAllProgrammingLanguages().ProgrammingLanguages;

            return View(model);
        }

        [HttpPost]
        public ActionResult AddPartToAssignment(AssignmentPartViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                model.ProgrammingLanguages = _service.getAllProgrammingLanguages().ProgrammingLanguages;

                var assignment = _service.getAssignmentById(model.AssignmentId);
                if (assignment == null)
                {
                    return View("NotFound");
                }
                string serverPath = Server.MapPath("~");
                model = _service.addAssignmentPart(model, serverPath);
                return RedirectToAction("ViewAssignment", "Assignment", new { id = model.AssignmentId });
            }
            else
            {
                model.ProgrammingLanguages = _service.getAllProgrammingLanguages().ProgrammingLanguages;
                return View(model);
            }
        }

        public ActionResult AddTestCaseToPart(int? assignmentId, int? partId)
        {
            if (partId.HasValue == false)
            {
                return View("NotFound");
            }

            var assignment = _service.getAssignmentById(assignmentId.Value);
            if (assignment == null)
            {
                return View("NotFound");
            }

            AssignmentPartViewModel model = new AssignmentPartViewModel();
            model.AssignmentId = assignmentId.Value;
            string userId = User.Identity.GetUserId();
            model = _service.getAssignmentPartById(Server.MapPath("~"), partId.Value);
            model.AssignmentPartId = partId.Value;

            return View(model);
        }

        [HttpPost]
        public ActionResult AddTestCaseToPart(AssignmentPartViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                var assignment = _service.getAssignmentById(model.AssignmentId);

                if (assignment == null)
                {
                    return View("NotFound");
                }
                string serverPath = Server.MapPath("~");
                _service.addAssignmentPartTestCase(model);

                return RedirectToAction("ViewAssignment", "Assignment", new { id = model.AssignmentId });
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult OpenAssignments()
        {
            string userId = User.Identity.GetUserId();
            var viewModel = new AssignmentViewModel();
            Boolean bIsStudent = false;
            if(User.IsInRole("Student"))
            {
                bIsStudent = true;
            }
            viewModel = _service.getOpenAssignments(bIsStudent,userId);

            return View(viewModel);
        }

        public ActionResult ClosedAssignments()
        {
            string userId = User.Identity.GetUserId();
            var viewModel = new AssignmentViewModel();
            Boolean bIsStudent = User.IsInRole("Student");
            viewModel = _service.getClosedAssignments(bIsStudent,userId);

            return View(viewModel);
        }

        public ActionResult GetAllAssignments()
        {
            string userId = User.Identity.GetUserId();
            var viewModel = new AssignmentViewModel();
            Boolean bIsStudent = User.IsInRole("Student");
            viewModel = _service.getAllUserAssignments(bIsStudent,userId);

            return View(viewModel);
        }

        public ActionResult ViewAssignment(int? id)
        {
            if (id.HasValue == false)
            {
                return View("NotFound");
            }

            var viewModel = _service.getAssignmentById(id.Value);
            string userId = User.Identity.GetUserId();
            viewModel.File = _service.getAssignmentFile(id.Value);
            if (User.IsInRole("Student"))
            {
                viewModel.UserAssignment = _service.getUserAssignmentById(userId, id.Value);
                viewModel.AssignmentSubmissionsList = _service.getUsersSubmissions(userId, id.Value);
                viewModel.File = _service.getAssignmentFile(id.Value);
                viewModel.UserGroups = _service.getUserGroups(userId, viewModel.CourseId).UserGroups;
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

        public ActionResult DownloadFile(string path, string contentType, string fileName)
        {
            return File(path, contentType, Server.UrlEncode(fileName + "." + path.Split('.')[1]));
        }

        public ActionResult ViewSubmissions(int? assignmentId, int? courseId)
        {
            if (assignmentId.HasValue == false)
            {
                return View("NotFound");
            }

            var viewModel = _courseService.getAllUsersInCourse((int)courseId, (int)assignmentId);
            return View(viewModel);
        }

        public ActionResult ViewUserSubmissions(int? assignmentId, string userId)
        {
            if ((assignmentId == null) || (userId == null))
            {
                return View("NotFound");
            }

            var submissionList = _service.getUsersSubmissions(userId, (int)assignmentId);
            var assignmentParts = _service.getAssignmentParts(assignmentId.Value);
            AssignmentViewModel viewModel = new AssignmentViewModel();
            viewModel.UserName = Utilities.IdentityManager.GetUserById(userId).UserName;
            viewModel.AssignmentSubmissionsList = submissionList;
            viewModel.AssignmentPartList = assignmentParts.AssignmentPartList;

            return View(viewModel);
        }
    }
}