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

        public ActionResult AddAnnouncement()
        {
            AnnouncementViewModel viewModel = new AnnouncementViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddAnnouncement(AnnouncementViewModel newAnnouncement)
        {
            newAnnouncement.UserId = 5;
            bool announcement = _service.addAnnouncement(newAnnouncement);
            
            return RedirectToAction("GetAllAnnouncements", "Announcement");

        }
        
        public ActionResult GetAllAnnouncements()
        {
            var viewModel = new AnnouncementViewModel();
            viewModel = _service.getAllAnnouncements();
            return View(viewModel);
        }
    }
}