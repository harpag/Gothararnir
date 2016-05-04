using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2_Hopur5.Controllers
{
    public class AnnouncementController : Controller
    {
        private AnnouncementService _service = new AnnouncementService();

        // GET: Announcement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAnnouncement()
        {
            int announcementId = 5;
            var viewModel = _service.getAnnouncementById(announcementId);
            return View(viewModel);
        }

        public ActionResult GetAllAnnouncements()
        {
            var viewModel = new AnnouncementViewModel();
            viewModel = _service.getAllAnnouncements();
            return View(viewModel);
        }
    }
}