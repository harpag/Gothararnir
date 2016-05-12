using Microsoft.AspNet.Identity;
using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Services;
using System;
using System.Web.Mvc;

namespace Mooshak2_Hopur5.Controllers
{
    public class HomeController : Controller
    {
        private CourseService _courseService = new CourseService();
        private AssignmentService _assignmentService = new AssignmentService();
        private AnnouncementService _announcementService = new AnnouncementService();

        public ActionResult Index()
        {
            Session["CurrentSemesterId"] = _courseService.getCurrentSemester();
            var viewModel = new HomeViewModel();
            string userId = User.Identity.GetUserId();
            viewModel.CourseList = _courseService.getAllUsersCoursesOnSemester(userId, int.Parse(Session["CurrentSemesterId"].ToString())).CourseList;
            viewModel.AssignmentList = _assignmentService.getAllUserAssignmentsOnSemester(userId, int.Parse(Session["CurrentSemesterId"].ToString())).AssignmentList;
            viewModel.AnnouncementList = _announcementService.getAllAnnouncements().AnnouncementList;

            return View(viewModel);
        }

        public ActionResult AdminIndex()
        {
            var viewModel = new HomeViewModel();
            viewModel.AnnouncementList = _announcementService.getAllAnnouncements().AnnouncementList;

            return View(viewModel);
        }
    }
}