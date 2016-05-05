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
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateAssignment(AssignmentViewModel assignment)
        {
            if (ModelState.IsValid)
            {
                assignment = _service.addAssignment(assignment);

                if (ModelState.IsValid)
                {
                    //Save Picture

                    string serverPath = Server.MapPath("~");
                    bool fileUpload = _service.addAssignmentFile(serverPath,assignment);
                }

                return RedirectToAction("ViewCourse", "Course", new { id = assignment.CourseId });
            }

            //ViewBag.MovieCategoryID = new SelectList(db.MovieCategories, "ID", "Name", movie.MovieCategoryID);
            return View(assignment);
        }
    }
}