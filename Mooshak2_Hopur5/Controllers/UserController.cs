using Mooshak2_Hopur5.Handlers;
using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Utilities;
using System.Web.Mvc;

namespace Mooshak2_Hopur5.Controllers
{
    [CustomHandleErrorAttribute]
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllUsers()
        {
            UserViewModel viewModel = new UserViewModel();
            viewModel.AllUsers = IdentityManager.GetUsers();
            return View(viewModel);
        }
    }
}