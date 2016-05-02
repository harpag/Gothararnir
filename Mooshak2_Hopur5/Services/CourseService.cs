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

        public CourseViewModel getAllCourses()
        {
            //Todo
            var courses = _db.Course.ToList();
            
            List<CourseViewModel> courseList;
            courseList = new List<CourseViewModel>();

            foreach (var entity in courses)
            {
                var result = new CourseViewModel
                {
                    CourseName = entity.courseName,
                    CourseNumber = entity.courseNumber
                };
                courseList.Add(result);
            }

            CourseViewModel viewModel = new CourseViewModel
            {
                CourseList = courseList
            };

            return viewModel;
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