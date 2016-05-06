using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            var viewModel = new HomeViewModel();
            viewModel.CourseList = _courseService.getAllCourses().CourseList;
            viewModel.AssignmentList = _assignmentService.getAllAssignments().AssignmentList;
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