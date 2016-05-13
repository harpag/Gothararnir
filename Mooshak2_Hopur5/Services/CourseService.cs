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
            //var courseUsers = getAllUsersInCourse(courseId);
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
                    //UserList = courseUsers.UserList,
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
            var allCourses = (from courses in _db.Course
                               join semester in _db.Semester on courses.semesterId equals semester.semesterId
                               select new { semester, courses }).Distinct().ToList().OrderByDescending(x => x.semester.dateFrom);
            //Bý til lista af áföngum(CourseViewModel)
            List<CourseViewModel> courseList;
            courseList = new List<CourseViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í áfanga listann
            foreach(var entity in allCourses)
            {
                //var courseUsers = getAllUsersInCourse(entity.courseId);
                var courseTeachers = getAllTeachersInCourse(entity.courses.courseId);
                var result = new CourseViewModel
                {
                    CourseId = entity.courses.courseId,
                    CourseName = entity.courses.courseName,
                    CourseNumber = entity.courses.courseNumber,
                    SemesterId = entity.courses.semesterId,
                    SemesterName = entity.semester.semesterName,
                    //UserList = courseUsers.UserList,
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
                //var courseUsers = getAllUsersInCourse(entity.courseId);
                var courseTeachers = getAllTeachersInCourse(entity.courseId);
                var result = new CourseViewModel
                {
                    CourseId = entity.courseId,
                    CourseName = entity.courseName,
                    CourseNumber = entity.courseNumber,
                    SemesterId = entity.semesterId,
                    //UserList = courseUsers.UserList,
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
        public CourseViewModel getAllUsersCourses(string userId)
        {
            //Sæki alla áfanga sem nemandi með ID userId er skráður í 
            var userCourses =  (from courses in _db.Course
                                join userCourse in _db.UserCourse on courses.courseId equals userCourse.courseId
                                join semester in _db.Semester on courses.semesterId equals semester.semesterId
                                where userCourse.userId == userId
                                orderby semester.dateTo
                                select new { semester, courses }).Distinct().ToList().OrderByDescending(x => x.semester.dateFrom); 

            //Bý til lista af áföngum(CourseViewModel)
            List<CourseViewModel> courseList;
            courseList = new List<CourseViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í áfanga listann
            foreach (var entity in userCourses)
            {
                //var courseUsers = getAllUsersInCourse(entity.courseId);
                var courseTeachers = getAllTeachersInCourse(entity.courses.courseId);
                var result = new CourseViewModel
                {
                    CourseId = entity.courses.courseId,
                    CourseName = entity.courses.courseName,
                    CourseNumber = entity.courses.courseNumber,
                    SemesterId = entity.courses.semesterId,
                    SemesterName = entity.semester.semesterNumber,
                    //UserList = courseUsers.UserList,
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
        public CourseViewModel getAllUsersCoursesOnSemester(string userId, int semesterId)
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
                    CourseNumber = entity.courseNumber,
                    SemesterId = entity.semesterId,
                    CourseId = entity.courseId
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
        public SubmissionOverviewViewModel getAllUsersInCourse(int courseId, int assignmentId)
        {
            //Sæki alla áfanga sem nemandi með ID userId er skráður í á önn semesterId
            var users = (from courses in _db.Course
                         join userCourse in _db.UserCourse on courses.courseId equals userCourse.courseId
                         join anUser in _db.AspNetUsers on userCourse.userId equals anUser.Id
                         where courses.courseId == courseId
                         select anUser).ToList();

            //Fix þegar mörgum sinnum skráður í sama áfangann
            var uniquePeople = from p in users
                               group p by new { p.Id } //or group by new {p.ID, p.Name, p.Whatever}
                               into mygroup
                               select mygroup.FirstOrDefault();

            //Bý til lista af notendur(UserViewModel)
            List < SubmissionOverviewViewModel > userList;
            userList = new List<SubmissionOverviewViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í áfanga listann
            foreach (var entity in uniquePeople)
            {
                UserAssignment userAssignment = (from userAssignments in _db.UserAssignment
                                                where userAssignments.assignmentId == assignmentId
                                               && userAssignments.userId == entity.Id
                                                select userAssignments).SingleOrDefault();

                // = userAssignment.userAssignmentList == null
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

            //Bý til nýtt CourseViewModel og set listann inn í það
            /*CourseViewModel viewModel = new CourseViewModel
            {
                UserList = userList
            };*/

            SubmissionOverviewViewModel viewModel = new SubmissionOverviewViewModel();
            viewModel.UserList = userList;

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
            query.semesterId = courseToChange.SemesterId;
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
            newCourse.semesterId = courseToAdd.SemesterId;
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
        public Boolean addUsersToCourse(string userId, int courseId)
        { 
            var newUserCourse = new UserCourse();

            //Setja property-in
            newUserCourse.courseId = courseId;
            newUserCourse.userId = userId;
            //try
            //{
                //Vista ofan í gagnagrunn
                _db.UserCourse.Add(newUserCourse);
                _db.SaveChanges();
                return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        public CourseViewModel getAllTeachersInCourse(int courseId)
        {
            //Sæki alla kennara námskeiðs
            var teacher = (from courses in _db.Course
                         join courseTeacher in _db.CourseTeacher on courses.courseId equals courseTeacher.courseId
                         //join user in _db.User on courseTeacher.userId equals user.userId
                         where courses.courseId == courseId
                         select new { courses, courseTeacher }).ToList();

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
                    //TeacherName = entity.user.name,
                    //TeacherUserName = entity.user.userName

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

        //Sækir allar annir
        public SemesterViewModel getAllSemesters()
        {
            var semesters = _db.Semester.ToList();
            
            List<SemesterViewModel> semesterlist = new List<SemesterViewModel>();

            foreach (var entity in semesters)
            {
                var result = new SemesterViewModel
                {
                    SemesterId = entity.semesterId,
                    SemesterName = entity.semesterName,
                    DateFrom = entity.dateFrom,
                    DateTo = entity.dateTo
                };
                semesterlist.Add(result);
            }

            SemesterViewModel viewModel = new SemesterViewModel();
            viewModel.SemesterList = semesterlist;
            return viewModel;
        }

        public SemesterViewModel getSemesterById(int semesterId)
        {
            //Sæki verkefni með ákveðnu ID ofan í gagnagrunn
            var semester = (from Semester in _db.Semester
                              where Semester.semesterId == semesterId
                              select new { Semester }).SingleOrDefault();

            //Kasta villu ef ekki fannst verkefni með þessu ID-i
            if (semester == null)
            {
                //TODO: Kasta villu
                return null;
            }
            else
            {
                //Set verkefni inn í ViewModelið
                var viewModel = new SemesterViewModel
                {
                    SemesterName = semester.Semester.semesterName,
                    SemesterId = semester.Semester.semesterId,
                    SemesterNumber = semester.Semester.semesterNumber,
                    DateFrom = semester.Semester.dateFrom,
                    DateTo = semester.Semester.dateTo,
                   
                };

                //Returna ViewModelinu með áfanganum í
                return viewModel;
            }
        }

        public Boolean addSemester(SemesterViewModel semesterToAdd)
        {
            var newSemester = new Semester();

            //setja propery-in
            newSemester.semesterNumber = semesterToAdd.SemesterNumber;
            newSemester.semesterName = semesterToAdd.SemesterName;
            newSemester.dateFrom = semesterToAdd.DateFrom;
            newSemester.dateTo = semesterToAdd.DateTo;
            

            try
            {
                //Vista ofan í gagnagrunn
                _db.Semester.Add(newSemester);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Breyta önn
        public SemesterViewModel editSemester(SemesterViewModel semesterToChange)
        {
            // Sæki færsluna sem á að breyta í gagnagrunninn
            var query = (from semester in _db.Semester
                         where semester.semesterId == semesterToChange.SemesterId
                         select semester).SingleOrDefault();

            // Set inn breyttu upplýsingarnar
            query.semesterNumber = semesterToChange.SemesterNumber;
            query.semesterName = semesterToChange.SemesterName;
            query.dateFrom = semesterToChange.DateFrom;
            query.dateTo = semesterToChange.DateTo;


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
            return semesterToChange;
        }

        public int getCurrentSemester()
        {
            var date = DateTime.Now;
            //Sæki núverandi önn
            var semester = (from Semester in _db.Semester
                            where date >= Semester.dateFrom && date <= Semester.dateTo
                            select Semester).SingleOrDefault();

            //Kasta villu ef ekki fannst verkefni með þessu ID-i
            if (semester == null)
            {
                //TODO: Kasta villu
                return 0;
            }
            else
            {
                return semester.semesterId;
            }
        }


    }
}