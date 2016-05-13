using Microsoft.AspNet.Identity;
using Mooshak2_Hopur5.Handlers;
using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Services;
using System.Web.Mvc;

namespace Mooshak2_Hopur5.Controllers
{
    [CustomHandleErrorAttribute]
    public class AnnouncementController : Controller
    {
        private AnnouncementService _service = new AnnouncementService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddAnnouncement()
        {
            AnnouncementViewModel viewModel = new AnnouncementViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddAnnouncement(AnnouncementViewModel newAnnouncement)
        {
            if (ModelState.IsValid)
            {
                newAnnouncement.UserId = User.Identity.GetUserId();
                bool bAnnouncement = _service.addAnnouncement(newAnnouncement);

                return RedirectToAction("GetAllAnnouncements", "Announcement");
            }

            return View();
        }

        public ActionResult GetAllAnnouncements()
        {
            var viewModel = new AnnouncementViewModel();
            viewModel = _service.getAllAnnouncements();
            return View(viewModel);
        }
    }
}