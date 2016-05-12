using Mooshak2_Hopur5.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public CourseViewModel getCourseById(int courseId)
        {
            var course = _db.Course.SingleOrDefault(x => x.courseId == courseId);
            var courseTeachers = getAllTeachersInCourse(courseId);

            if (course == null)
            {
                throw new Exception();
            }
            else
            {
                //Áfanginn settur inn í ViewModel
                var viewModel = new CourseViewModel
                {
                    CourseId = course.courseId,
                    CourseName = course.courseName,
                    CourseNumber = course.courseNumber,
                    SemesterId = course.semesterId,
                    TeacherList = courseTeachers.TeacherList
                };
                return viewModel;
            }
        }

        public CourseViewModel getAllCourses()
        {
            //Sækir allt um áfanga og setur í lista
            var allCourses = (from courses in _db.Course
                              join semester in _db.Semester on courses.semesterId equals semester.semesterId
                              select new { semester, courses }).Distinct().ToList().OrderByDescending(x => x.semester.dateFrom);

            List<CourseViewModel> courseList;
            courseList = new List<CourseViewModel>();

            foreach (var entity in allCourses)
            {
                var courseTeachers = getAllTeachersInCourse(entity.courses.courseId);
                var result = new CourseViewModel
                {
                    CourseId = entity.courses.courseId,
                    CourseName = entity.courses.courseName,
                    CourseNumber = entity.courses.courseNumber,
                    SemesterId = entity.courses.semesterId,
                    SemesterName = entity.semester.semesterName,
                    TeacherList = courseTeachers.TeacherList
                };
                courseList.Add(result);
            }

            CourseViewModel viewModel = new CourseViewModel
            {
                CourseList = courseList
            };
            return viewModel;
        }

        public CourseViewModel getAllCoursesOnSemester(int semesterId)
        {
            var courses = _db.Course
                .Where(x => x.semesterId == semesterId)
                .ToList();

            List<CourseViewModel> courseList;
            courseList = new List<CourseViewModel>();

            foreach (var entity in courses)
            {
                var courseTeachers = getAllTeachersInCourse(entity.courseId);
                var result = new CourseViewModel
                {
                    CourseId = entity.courseId,
                    CourseName = entity.courseName,
                    CourseNumber = entity.courseNumber,
                    SemesterId = entity.semesterId,
                    TeacherList = courseTeachers.TeacherList
                };
                courseList.Add(result);
            }

            //Áfangar settir í lista
            CourseViewModel viewModel = new CourseViewModel
            {
                CourseList = courseList
            };
            return viewModel;
        }

        //Sækir alla áfanga sem notandi er skráður í
        public CourseViewModel getAllUsersCourses(string userId)
        {
            //Sæki alla áfanga fyrir notanda
            var userCourses = (from courses in _db.Course
                               join userCourse in _db.UserCourse on courses.courseId equals userCourse.courseId
                               join semester in _db.Semester on courses.semesterId equals semester.semesterId
                               where userCourse.userId == userId
                               orderby semester.dateTo
                               select new { semester, courses }).Distinct().ToList().OrderByDescending(x => x.semester.dateFrom);

            List<CourseViewModel> courseList;
            courseList = new List<CourseViewModel>();

            foreach (var entity in userCourses)
            {
                var courseTeachers = getAllTeachersInCourse(entity.courses.courseId);
                var result = new CourseViewModel
                {
                    CourseId = entity.courses.courseId,
                    CourseName = entity.courses.courseName,
                    CourseNumber = entity.courses.courseNumber,
                    SemesterId = entity.courses.semesterId,
                    SemesterName = entity.semester.semesterNumber,
                    TeacherList = courseTeachers.TeacherList
                };
                courseList.Add(result);
            }

            CourseViewModel viewModel = new CourseViewModel
            {
                CourseList = courseList
            };
            return viewModel;
        }

        //Sækir alla áfanga sem notandi er skráður í á ákveðinni önn
        public CourseViewModel getAllUsersCoursesOnSemester(string userId, int semesterId)
        {
            //Sækir alla áfanga sem nemandi er skráður í, á önn ákveðinni önn
            var userCourses = (from courses in _db.Course
                               join userCourse in _db.UserCourse on courses.courseId equals userCourse.courseId
                               join semester in _db.Semester on courses.semesterId equals semester.semesterId
                               where userCourse.userId == userId
                               && semester.semesterId == semesterId
                               select courses).ToList();

            List<CourseViewModel> courseList;
            courseList = new List<CourseViewModel>();

            foreach (var entity in userCourses)
            {
                var result = new CourseViewModel
                {
                    CourseName = entity.courseName,
                    CourseNumber = entity.courseNumber,
                    SemesterId = entity.semesterId,
                    CourseId = entity.courseId
                };
                courseList.Add(result);
            }

            //Setur lista af áföngum í lista
            CourseViewModel viewModel = new CourseViewModel
            {
                CourseList = courseList
            };
            return viewModel;
        }

        //Sækir alla notendur sem eru skráðir í ákveðinn áfanga
        public SubmissionOverviewViewModel getAllUsersInCourse(int courseId, int assignmentId)
        {
            var users = (from courses in _db.Course
                         join userCourse in _db.UserCourse on courses.courseId equals userCourse.courseId
                         join anUser in _db.AspNetUsers on userCourse.userId equals anUser.Id
                         where courses.courseId == courseId
                         select anUser).ToList();

            var uniquePeople = from p in users
                               group p by new { p.Id }
                               into mygroup
                               select mygroup.FirstOrDefault();

            //Býr til lista af notendum (UserViewModel)
            List<SubmissionOverviewViewModel> userList;
            userList = new List<SubmissionOverviewViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í áfanga listann
            foreach (var entity in uniquePeople)
            {
                UserAssignment userAssignment = (from userAssignments in _db.UserAssignment
                                                 where userAssignments.assignmentId == assignmentId
                                                 && userAssignments.userId == entity.Id
                                                 select userAssignments).SingleOrDefault();

                int iCount = 0;
                bool bSuccess = false;

                if (userAssignment != null)
                {
                    var submissions = (from a in _db.Submission
                                       where a.userAssignmentId.Equals(userAssignment.userAssignmentId)
                                       select a);
                    iCount = submissions.Count();

                    int? iAcc = null;
                    if (iCount > 0)
                        iAcc = submissions.Sum(s => s.accepted);

                    if (iAcc != null)
                        bSuccess = iAcc > 0;
                }

                var result = new SubmissionOverviewViewModel
                {
                    Email = entity.Email,
                    UserName = entity.UserName,
                    UserId = entity.Id,
                    SubmissionCount = iCount,
                    Success = bSuccess
                };
                userList.Add(result);
            }

            SubmissionOverviewViewModel viewModel = new SubmissionOverviewViewModel();
            viewModel.UserList = userList;

            return viewModel;
        }

        public CourseViewModel editCourse(CourseViewModel courseToChange)
        {
            // Sækir færsluna sem á að breyta í gagnagrunninn
            var query = (from course in _db.Course
                         where course.courseId == courseToChange.CourseId
                         select course).SingleOrDefault();

            query.semesterId = courseToChange.SemesterId;
            query.courseName = courseToChange.CourseName;
            query.courseNumber = courseToChange.CourseNumber;
            query.courseId = 0;

            _db.SaveChanges();

            return courseToChange;
        }

        public Boolean addCourse(CourseViewModel courseToAdd)
        {
            var newCourse = new Course();

            newCourse.courseName = courseToAdd.CourseName;
            newCourse.courseNumber = courseToAdd.CourseNumber;
            newCourse.semesterId = courseToAdd.SemesterId;

            _db.Course.Add(newCourse);
            _db.SaveChanges();
            return true;
        }

        public Boolean addUsersToCourse(string userId, int courseId)
        {
            var newUserCourse = new UserCourse();

            newUserCourse.courseId = courseId;
            newUserCourse.userId = userId;

            _db.UserCourse.Add(newUserCourse);
            _db.SaveChanges();
            return true;
        }

        public CourseViewModel getAllTeachersInCourse(int courseId)
        {
            var teacher = (from courses in _db.Course
                           join courseTeacher in _db.CourseTeacher on courses.courseId equals courseTeacher.courseId

                           where courses.courseId == courseId
                           select new { courses, courseTeacher }).ToList();

            //Bý til lista af kennurum(CourseTeacherViewModel)
            List<CourseTeacherViewModel> teacherList;
            teacherList = new List<CourseTeacherViewModel>();

            //Fer í gegnum listann úr gagnagrunninum og setur inn í áfanga listann
            foreach (var entity in teacher)
            {
                var result = new CourseTeacherViewModel
                {
                    CourseTeacherId = entity.courseTeacher.courseTeacherId,
                    CourseId = entity.courses.courseId,
                    UserId = entity.courseTeacher.userId,
                    MainTeacher = entity.courseTeacher.mainTeacher,
                };
                teacherList.Add(result);
            }

            //Listi af kennurum settur í lista
            CourseViewModel viewModel = new CourseViewModel
            {
                TeacherList = teacherList
            };
            return viewModel;
        }

        // Allar annir settar í lista
        public SemesterViewModel getAllSemesters()
        {
            var semesters = _db.Semester.ToList();

            List<SemesterViewModel> semesterlist = new List<SemesterViewModel>();

            foreach (var entity in semesters)
            {
                var result = new SemesterViewModel
                {
                    SemesterId = entity.semesterId,
                    SemesterName = entity.semesterName
                };
                semesterlist.Add(result);
            }

            SemesterViewModel viewModel = new SemesterViewModel();
            viewModel.SemesterList = semesterlist;
            return viewModel;
        }

        public SemesterViewModel getSemesterById(int semesterId)
        {
            //Sækir önn með ákveðnu ID ofan í gagnagrunn
            var semester = (from Semester in _db.Semester
                            where Semester.semesterId == semesterId
                            select new { Semester }).SingleOrDefault();

            if (semester == null)
            {
                throw new Exception();
            }
            else
            {
                var viewModel = new SemesterViewModel
                {
                    SemesterName = semester.Semester.semesterName,
                    SemesterId = semester.Semester.semesterId,
                    SemesterNumber = semester.Semester.semesterNumber,
                    DateFrom = semester.Semester.dateFrom,
                    DateTo = semester.Semester.dateTo,
                };
                return viewModel;
            }
        }

        public Boolean addSemester(SemesterViewModel semesterToAdd)
        {
            var newSemester = new Semester();

            newSemester.semesterNumber = semesterToAdd.SemesterNumber;
            newSemester.semesterName = semesterToAdd.SemesterName;
            newSemester.dateFrom = semesterToAdd.DateFrom;
            newSemester.dateTo = semesterToAdd.DateTo;

            _db.Semester.Add(newSemester);
            _db.SaveChanges();
            return true;
        }

        //Breytir önn
        public SemesterViewModel editSemester(SemesterViewModel semesterToChange)
        {
            // Sækir færsluna sem á að breyta í gagnagrunninn
            var query = (from semester in _db.Semester
                         where semester.semesterId == semesterToChange.SemesterId
                         select semester).SingleOrDefault();

            // Setur inn breyttu upplýsingarnar
            query.semesterNumber = semesterToChange.SemesterNumber;
            query.semesterName = semesterToChange.SemesterName;
            query.dateFrom = semesterToChange.DateFrom;
            query.dateTo = semesterToChange.DateTo;

            _db.SaveChanges();
            return semesterToChange;
        }

        public int getCurrentSemester()
        {
            //Sæki núverandi önn
            var semester = (from Semester in _db.Semester
                            where DateTime.Now >= Semester.dateFrom && DateTime.Now <= Semester.dateTo
                            select Semester).SingleOrDefault();

            if (semester == null)
            {
                throw new Exception();
            }
            else
            {
                return semester.semesterId;
            }
        }
    }
}