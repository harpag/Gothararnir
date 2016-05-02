using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Services
{
    public class CourseService
    {
        private ApplicationDbContext _db;

        public CourseService()
        {
            _db = new ApplicationDbContext();
        }

        public List<CourseViewModel> getAllCourses()
        {
            //Todo
            return null;
        }

        public CourseViewModel getCourseById(int courseId)
        {
            var course = _db.Course.SingleOrDefault(x => x.courseId == courseId);
            if(course == null)
            {
                //TODO: Kasta villu
            }

            var viewModel = new CourseViewModel
            {
                CourseName = course.courseName,
                CourseNumber = course.courseNumber
            };

            return viewModel;
        }

    }
}