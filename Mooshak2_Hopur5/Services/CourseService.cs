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

        //getAllUsersInCourse() Á eftir að klára

        private DataModel _db;

        public CourseService()
        {
            _db = new DataModel();
        }

        //Sækir alla áfanga með ákveðnu ID
        public CourseViewModel getCourseById(int courseId)
        {
            //Sæki áfanga með ákveðnu ID ofan í gagnagrunn
            var course = _db.Course.SingleOrDefault(x => x.courseId == courseId);
            var courseUsers = getAllUsersInCourse(courseId);
            var courseTeachers = getAllTeachersInCourse(courseId);

            //Kasta villu ef ekki fannst áfangi með þessu ID-i
            if (course == null)
            {
                //TODO: Kasta villu
                return null;
            }

            else { 
                //Set áfangann inn í ViewModelið
                var viewModel = new CourseViewModel
                {
                    CourseId = course.courseId,
                    CourseName = course.courseName,
                    CourseNumber = course.courseNumber,
                    SemesterId = course.semesterId,
                    UserList = courseUsers.UserList,
                    TeacherList = courseTeachers.TeacherList
                };

                //Returna ViewModelinu með áfanganum í
                return viewModel;
            }
        }

        //Sækir alla áfanga
        public CourseViewModel getAllCourses()
        {
            //Sæki öll gögn í Course(áfanga) töfluna
            var courses = _db.Course.ToList();
            
            //Bý til lista af áföngum(CourseViewModel)
            List<CourseViewModel> courseList;
            courseList = new List<CourseViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í áfanga listann
            foreach(var entity in courses)
            {
                var courseUsers = getAllUsersInCourse(entity.courseId);
                var courseTeachers = getAllTeachersInCourse(entity.courseId);
                var result = new CourseViewModel
                {
                    CourseId = entity.courseId,
                    CourseName = entity.courseName,
                    CourseNumber = entity.courseNumber,
                    SemesterId = entity.semesterId,
                    UserList = courseUsers.UserList,
                    TeacherList = courseTeachers.TeacherList
                };
                courseList.Add(result);
            }

            //Bý til nýtt CourseViewModel og set listann inn í það
            CourseViewModel viewModel = new CourseViewModel
            {
                CourseList = courseList
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }

        //Sækir alla áfanga á ákveðninni önn
        public CourseViewModel getAllCoursesOnSemester(int semesterId)
        {
            //Sæki öll gögn á önn með ID-ið semesterId í Course(áfanga) töfluna
            var courses = _db.Course
                .Where(x => x.semesterId == semesterId)
                .ToList();

            //Bý til lista af áföngum(CourseViewModel)
            List<CourseViewModel> courseList;
            courseList = new List<CourseViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í áfanga listann
            foreach (var entity in courses)
            {
                var courseUsers = getAllUsersInCourse(entity.courseId);
                var courseTeachers = getAllTeachersInCourse(entity.courseId);
                var result = new CourseViewModel
                {
                    CourseId = entity.courseId,
                    CourseName = entity.courseName,
                    CourseNumber = entity.courseNumber,
                    SemesterId = entity.semesterId,
                    UserList = courseUsers.UserList,
                    TeacherList = courseTeachers.TeacherList
                };
                courseList.Add(result);
            }

            //Bý til nýtt CourseViewModel og set listann inn í það
            CourseViewModel viewModel = new CourseViewModel
            {
                CourseList = courseList
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }

        //Sækir alla áfanga sem notandi er skráður í
        public CourseViewModel getAllUsersCourses(int userId)
        {
            //Sæki alla áfanga sem nemandi með ID userId er skráður í 
            var userCourses =  (from courses in _db.Course
                                join userCourse in _db.UserCourse on courses.courseId equals userCourse.courseId
                                where userCourse.userId == userId
                                select courses).ToList();

            //Bý til lista af áföngum(CourseViewModel)
            List<CourseViewModel> courseList;
            courseList = new List<CourseViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í áfanga listann
            foreach (var entity in userCourses)
            {
                var courseUsers = getAllUsersInCourse(entity.courseId);
                var courseTeachers = getAllTeachersInCourse(entity.courseId);
                var result = new CourseViewModel
                {
                    CourseId = entity.courseId,
                    CourseName = entity.courseName,
                    CourseNumber = entity.courseNumber,
                    SemesterId = entity.semesterId,
                    UserList = courseUsers.UserList,
                    TeacherList = courseTeachers.TeacherList
                };
                courseList.Add(result);
            }

            //Bý til nýtt CourseViewModel og set listann inn í það
            CourseViewModel viewModel = new CourseViewModel
            {
                CourseList = courseList
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }

        //Sækir alla áfanga sem notandi er skráður í á ákveðinni önn
        public CourseViewModel getAllUsersCoursesOnSemester(int userId, int semesterId)
        {
            //Sæki alla áfanga sem nemandi með ID userId er skráður í á önn semesterId
            var userCourses = (from courses in _db.Course
                               join userCourse in _db.UserCourse on courses.courseId equals userCourse.courseId
                               join semester in _db.Semester on courses.semesterId equals semester.semesterId
                               where userCourse.userId == userId
                               && semester.semesterId == semesterId
                               select courses).ToList();

            //Bý til lista af áföngum(CourseViewModel)
            List<CourseViewModel> courseList;
            courseList = new List<CourseViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í áfanga listann
            foreach (var entity in userCourses)
            {
                var result = new CourseViewModel
                {
                    CourseName = entity.courseName,
                    CourseNumber = entity.courseNumber
                };
                courseList.Add(result);
            }

            //Bý til nýtt CourseViewModel og set listann inn í það
            CourseViewModel viewModel = new CourseViewModel
            {
                CourseList = courseList
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }

        //Sækir alla notendur sem eru skráðir í ákveðin áfanga
        public CourseViewModel getAllUsersInCourse(int courseId)
        {
            //Sæki alla áfanga sem nemandi með ID userId er skráður í á önn semesterId
            var users = (from courses in _db.Course
                               join userCourse in _db.UserCourse on courses.courseId equals userCourse.courseId
                               join user in _db.User on userCourse.userId equals user.userId
                               where courses.courseId == courseId
                               select user).ToList();

            //Bý til lista af notendur(UserViewModel)
            List<UserViewModel> userList;
            userList = new List<UserViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í áfanga listann
            foreach (var entity in users)
            {
                var result = new UserViewModel
                {
                    //Todo vildi ekki ger til að conflicta ekki við Bríet
                };
                userList.Add(result);
            }

            //Bý til nýtt CourseViewModel og set listann inn í það
            CourseViewModel viewModel = new CourseViewModel
            {
                UserList = userList
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }

        //Breyta ákveðnum áfanga
        public CourseViewModel editCourse(CourseViewModel courseToChange)
        {
            // Sæki færsluna sem á að breyta í gagnagrunninn
            var query = (from course in _db.Course
                where course.courseId == courseToChange.CourseId
                select course).SingleOrDefault();

            // Set inn breyttu upplýsingarnar
            query.courseName = courseToChange.CourseName;
            query.courseNumber = courseToChange.CourseNumber;
            //Todo setja inn rest

            //Vista breytingar í gagnagrunn
            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // TODO
            }
            return courseToChange;
        }

        //Búa til nýja áfanga
        public Boolean addCourse(CourseViewModel courseToAdd)
        {
            var newCourse = new Course();

            //setja propery-in
            newCourse.courseName = courseToAdd.CourseName;
            newCourse.courseNumber = courseToAdd.CourseNumber;
            //Todo setja inn öll property

            try
            {
                //Vista ofan í gagnagrunn
                _db.Course.Add(newCourse);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Bæta við notendum í áfanga
        public Boolean addUsersToCourse(int userId, int courseId)
        { 
            var newUserCourse = new UserCourse();

            //Setja property-in
            newUserCourse.courseId = courseId;
            newUserCourse.userId = userId;
            try
            {
                //Vista ofan í gagnagrunn
                _db.UserCourse.Add(newUserCourse);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public CourseViewModel getAllTeachersInCourse(int courseId)
        {
            //Sæki alla kennara námskeiðs
            var teacher = (from courses in _db.Course
                         join courseTeacher in _db.CourseTeacher on courses.courseId equals courseTeacher.courseId
                         join user in _db.User on courseTeacher.userId equals user.userId
                         where courses.courseId == courseId
                         select new { courses, user, courseTeacher }).ToList();

            //Bý til lista af kennurum(CourseTeacherViewModel)
            List<CourseTeacherViewModel> teacherList;
            teacherList = new List<CourseTeacherViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í áfanga listann
            foreach (var entity in teacher)
            {
                var result = new CourseTeacherViewModel
                {
                    CourseTeacherId = entity.courseTeacher.courseTeacherId,
                    CourseId = entity.courses.courseId,
                    UserId = entity.courseTeacher.userId,
                    MainTeacher = entity.courseTeacher.mainTeacher,
                    TeacherName = entity.user.name,
                    TeacherUserName = entity.user.userName

                };
                teacherList.Add(result);
            }

            //Bý til nýtt CourseViewModel og set listann inn í það
            CourseViewModel viewModel = new CourseViewModel
            {
                TeacherList = teacherList
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }

    }
}