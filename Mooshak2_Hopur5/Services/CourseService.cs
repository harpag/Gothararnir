using Mooshak2_Hopur5.Models.ViewModels;
using Mooshak2_Hopur5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mooshak2_Hopur5.Models.Entities;

namespace Mooshak2_Hopur5.Services
{
    public class CourseService
    {
        private DataModel _db;

        public CourseService()
        {
            _db = new DataModel();
        }

        public List<CourseViewModel> getAllCourses()
        {
            //Todo
            var courses = _db.Course.ToList();
            //var viewModel = new List<CourseViewModel>();
            List<CourseViewModel> CourseList;
            CourseList = new List<CourseViewModel>();

            foreach (var entity in courses)
            {
                var result = new CourseViewModel
                {
                    CourseName = entity.courseName,
                    CourseNumber = entity.courseNumber
                };
                CourseList.Add(result);
            }

            return CourseList;
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