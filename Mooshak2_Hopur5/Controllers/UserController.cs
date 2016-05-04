using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2_Hopur5.Controllers
{
    public class UserController : Controller
    {
        private UserService _service = new UserService();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: Course
        public ActionResult GetUser(int? userId)
        {
            Models.ViewModels.UserViewModel viewModel = null;
            if ( userId == null)
                viewModel = _service.getUserById(3);
            else
                viewModel = _service.getUserById((int)userId);

            return View(viewModel);
        }

        public ActionResult GetAllUsers()
        {
            var viewModel = new UserViewModel();
            viewModel = _service.getAllUsers();
            return View(viewModel);
        }
    }
}