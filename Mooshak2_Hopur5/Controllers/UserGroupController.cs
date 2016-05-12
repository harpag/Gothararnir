using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Mooshak2_Hopur5.Models.Entities;
using Microsoft.AspNet.Identity;
using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Services;
using Mooshak2_Hopur5.Handlers;

namespace Mooshak2_Hopur5.Controllers
{
    [CustomHandleErrorAttribute]
    public class UserGroupController : Controller
    {
        private UserGroupService _service = new UserGroupService();
        private DataModel db = new DataModel();
        
        public ActionResult Index()
        {
            var userGroup = db.UserGroup.Include(u => u.Course);
            return View(userGroup.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGroup userGroup = db.UserGroup.Find(id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            return View(userGroup);
        }

        public ActionResult Create(int? id)
        {
            throw new Exception("Villa villa");
            if (id == null)
                throw new NotImplementedException();

            ViewData["courseId"] = id;

            CourseService crs = new CourseService();
            var course = crs.getCourseById((int)id);
            ViewData["courseName"] = course.CourseNumber + "-" + course.CourseName;

            var viewModel = _service.GetViewWithUsers();
            return View(viewModel);
        }

        // Býr til hóp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userGroupId,courseId,userGroupName,AllUsers,CheckedUsers")] UserGroupViewModel userGroup)
        {
            if (ModelState.IsValid)
            {
                UserGroup grp = new UserGroup();
                grp.courseId = userGroup.courseId;
                grp.userCreate = User.Identity.GetUserId();
                grp.userGroupName = userGroup.userGroupName;

                UserGroup newUG = db.UserGroup.Add(grp);
                db.SaveChanges();
                
                for (int i = 0; i < userGroup.AllUsers.Count; i++)
                {
                    if (userGroup.CheckedUsers[i])
                    {
                        _service.addUsersToGroup(userGroup.AllUsers[i].Id, newUG.userGroupId);
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.courseId = new SelectList(db.Course, "courseId", "courseNumber", userGroup.courseId);
            return View(userGroup);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGroup userGroup = db.UserGroup.Find(id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.courseId = new SelectList(db.Course, "courseId", "courseNumber", userGroup.courseId);

            return View(userGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userGroupId,courseId,userGroupName")] UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.courseId = new SelectList(db.Course, "courseId", "courseName", userGroup.courseId);

            return View(userGroup);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGroup userGroup = db.UserGroup.Find(id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            return View(userGroup);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var members = from a in db.UserGroupMember
                          where a.userGroupId.Equals(id)
                          select a;
            
            db.UserGroupMember.RemoveRange(members);

            UserGroup userGroup = db.UserGroup.Find(id);
            db.UserGroup.Remove(userGroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}