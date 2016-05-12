using Mooshak2_Hopur5.Handlers;
using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Services;
using Mooshak2_Hopur5.Utilities;
using System.Web.Mvc;

namespace Mooshak2_Hopur5.Controllers
{
    [CustomHandleErrorAttribute]
    public class UserController : Controller
    {
        private UserService _service = new UserService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUser(int? userId)
        {
            UserViewModel viewModel = null;
            if (userId == null)
                viewModel = _service.getUserById(3);
            else
                viewModel = _service.getUserById((int)userId);

            return View(viewModel);
        }

        public ActionResult GetAllUsers()
        {
            UserViewModel viewModel = new UserViewModel();
            viewModel.AllUsers = IdentityManager.GetUsers();
            return View(viewModel);
        }
    }
}