using Mooshak2_Hopur5.Utilities;
using Mooshak2_Hopur5.Models.Entities;
using Mooshak2_Hopur5.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using System.Web.Mvc;
using System.Diagnostics;

namespace Mooshak2_Hopur5.Services
{
    public class AssignmentService
    {
        private DataModel _db;

        public AssignmentService()
        {
            _db = new DataModel();
        }

        //Sæki verkefni með ákveðnu ID
        public AssignmentViewModel getAssignmentById(int assignmentId)
        {
            //Sæki verkefni með ákveðnu ID ofan í gagnagrunn
            var assignment = (from assign in _db.Assignment
                              join course in _db.Course on assign.courseId equals course.courseId
                              where assign.assignmentId == assignmentId
                              select new { assign, course }).SingleOrDefault();

            var assignmentPart = getAssignmentParts(assignmentId);
            var assignmentPartSelect = new SelectList(assignmentPart.AssignmentPartList, "AssignmentPartId", "AssignmentPartName");

            //Kasta villu ef ekki fannst verkefni með þessu ID-i
            if (assignment == null)
            {
                //TODO: Kasta villu
                return null;
            }
            else
            {
                //Set verkefni inn í ViewModelið
                var viewModel = new AssignmentViewModel
                {
                    AssignmentId = assignment.assign.assignmentId,
                    CourseId = assignment.assign.courseId,
                    CourseName = assignment.course.courseName,
                    CourseNumber = assignment.course.courseNumber,
                    AssignmentName = assignment.assign.assignmentName,
                    AssignmentDescription = assignment.assign.assignmentDescription,
                    AssignmentFile = assignment.assign.assignmentFile,
                    Weight = assignment.assign.weight,
                    MaxSubmission = assignment.assign.maxSubmission,
                    AssignDate = assignment.assign.assignDate,
                    DueDate = assignment.assign.dueDate,
                    GradePublished = assignment.assign.gradePublished,
                    AssignmentPartList = assignmentPart.AssignmentPartList,
                    AssignmentParts = assignmentPartSelect
                };

                //Returna ViewModelinu með áfanganum í
                return viewModel;
            }
        }

        //Sæki verkefni með ákveðnu ID
        public UserAssignment getUserAssignmentById(string userId, int assignmentId)
        {
            //Sæki verkefni með ákveðnu ID ofan í gagnagrunn ef það er til
            var userAssignment = (from assign in _db.Assignment
                                  join userAssign in _db.UserAssignment on assign.assignmentId equals userAssign.assignmentId
                                  where assign.assignmentId == assignmentId && userAssign.userId == userId
                                  select userAssign).SingleOrDefault();

            //Skila nýju tómu ef ekki fannst verkefni með þessu ID-i
            if (userAssignment == null)
            {
                UserAssignment newAssignment = new UserAssignment();
                newAssignment.userId = userId;
                return newAssignment;
            }
            else
            {
                //ef verkefnið fannst þá skil á því
                var viewModel = new UserAssignment
                {
                    userId = userAssignment.userId,
                    userGroupId = userAssignment.userGroupId,
                    grade = userAssignment.grade,
                    gradeComment = userAssignment.gradeComment,
                    assignmentId = userAssignment.assignmentId
                };

                return viewModel;
            }
        }

        //Sæki verkefni með ákveðnu ID
        public List<Submission> getUsersSubmissions(string userId, int assignmentId)
        {
            //Sæki verkefni með ákveðnu ID ofan í gagnagrunn ef það er til
            var userSubmissions = (from submission in _db.Submission
                                   join assignmentPart in _db.AssignmentPart on submission.assignmentPartId equals assignmentPart.assignmentPartId
                                   join userAssign in _db.UserAssignment on assignmentPart.assignmentId equals userAssign.assignmentId
                                   where assignmentPart.assignmentId == assignmentId && userAssign.userId == userId
                                   select submission).ToList();

            List<Submission> submissionList;
            submissionList = new List<Submission>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í verkefna hluta 
            foreach (var entity in userSubmissions)
            {
                var result = new Submission
                {
                    submissionId = entity.submissionId,
                    assignmentPartId = entity.assignmentPartId,
                    userAssignmentId = entity.userAssignmentId,
                    submissionComment = entity.submissionComment,
                    accepted = entity.accepted,
                    numberOfSucessTestCases = entity.numberOfSucessTestCases,
                    testCaseFailId = entity.testCaseFailId,
                    error = entity.error
                };
                submissionList.Add(result);
            }

            return submissionList;
        }

        public AssignmentPartViewModel getAssignmentParts(int assignmentId)
        {
            //Sæki alla hluta úr verkefni
            var assignmentParts = (from assignmentPart in _db.AssignmentPart
                                   join assignment in _db.Assignment on assignmentPart.assignmentId equals assignment.assignmentId
                                   where assignmentPart.assignmentId == assignmentId
                                   select new { assignmentPart, assignment }).ToList();


            //Bý til lista af verkefna hlutum
            List<AssignmentPartViewModel> assignmentPartList;
            assignmentPartList = new List<AssignmentPartViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í verkefna hluta 
            foreach (var entity in assignmentParts)
            {
                var testCases = getAssignmentPartTestCases(entity.assignmentPart.assignmentPartId);
                var result = new AssignmentPartViewModel
                {
                    AssignmentPartId = entity.assignmentPart.assignmentPartId,
                    AssignmentId = entity.assignmentPart.assignmentId,
                    AssignmentName = entity.assignment.assignmentName,
                    AssignmentPartName = entity.assignmentPart.assignmentPartName,
                    AssignmentPartDescription = entity.assignmentPart.assignmentPartDescription,
                    AssignmentPartFile = entity.assignmentPart.assignmentPartFile,
                    Weight = entity.assignmentPart.weight,
                    ProgrammingLanguageId = entity.assignmentPart.programmingLanguageId,
                    AssignmentTestCaseList = testCases.AssignmentTestCaseList
                };
                assignmentPartList.Add(result);
            }

            //Bý til nýtt AssignmentPartViewModel og set listann inn í það
            AssignmentPartViewModel viewModel = new AssignmentPartViewModel
            {
                AssignmentPartList = assignmentPartList
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }

        public AssignmentPartViewModel getAssignmentPartTestCases(int assignmentPartId)
        {
            //Sæki öll prófunartilvik fyrir verkefnis hluta
            var testCases = (from assignmentTestCase in _db.AssignmentTestCase
                             join assignmentPart in _db.AssignmentPart on assignmentTestCase.assignmentPartId equals assignmentPart.assignmentPartId
                             where assignmentTestCase.assignmentPartId == assignmentPartId
                             select new { assignmentTestCase }).ToList();


            //Bý til lista af prófunartilvikum
            List<AssignmentTestCase> assignmentTestCaseList;
            assignmentTestCaseList = new List<AssignmentTestCase>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í verkefna hluta 
            foreach (var entity in testCases)
            {
                var result = new AssignmentTestCase
                {
                    assignmentTestCaseId = entity.assignmentTestCase.assignmentTestCaseId,
                    assignmentPartId = entity.assignmentTestCase.assignmentPartId,
                    testNumber = entity.assignmentTestCase.testNumber,
                    input = entity.assignmentTestCase.input,
                    output = entity.assignmentTestCase.output
                };
                assignmentTestCaseList.Add(result);
            }

            //Bý til nýtt AssignmentPartViewModel og set prófunartilvika listann inn í það
            AssignmentPartViewModel viewModel = new AssignmentPartViewModel
            {
                AssignmentTestCaseList = assignmentTestCaseList
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }

        //getAllAssignments()
        //getAllAssignmentsOnSemester()
        //getAllUserAssignments()
        //getAllUserAssignmentsOnSemester()
        //getAssignmentGrade()
        //getAssignmentStatistics()
        //editAssignment()
        //addAssignment()
        //addAssignmentTestCase()

        //Sækir öll verkefni
        public AssignmentViewModel getAllAssignments()
        {
            //Sæki öll gögn í verkefna töfluna
            var assignments = (from assign in _db.Assignment
                               join course in _db.Course on assign.courseId equals course.courseId
                               select new { assign, course }).ToList();

            //Bý til lista af verkefnum
            List<AssignmentViewModel> assignmentsList;
            assignmentsList = new List<AssignmentViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í verkefna listann
            foreach (var entity in assignments)
            {
                var assignmentParts = getAssignmentParts(entity.assign.assignmentId);
                var result = new AssignmentViewModel
                {
                    AssignmentId = entity.assign.assignmentId,
                    CourseId = entity.assign.courseId,
                    CourseName = entity.course.courseName,
                    CourseNumber = entity.course.courseNumber,
                    AssignmentName = entity.assign.assignmentName,
                    AssignmentDescription = entity.assign.assignmentDescription,
                    AssignmentFile = entity.assign.assignmentFile,
                    Weight = entity.assign.weight,
                    MaxSubmission = entity.assign.maxSubmission,
                    AssignDate = entity.assign.assignDate,
                    DueDate = entity.assign.dueDate,
                    GradePublished = entity.assign.gradePublished,
                    AssignmentPartList = assignmentParts.AssignmentPartList
                    //AssignmentSubmissionsList =
                    //DiscussionsList =
                };
                assignmentsList.Add(result);
            }

            //Bý til nýtt AssingmentViewModel og set listann inn í það
            AssignmentViewModel viewModel = new AssignmentViewModel
            {
                AssignmentList = assignmentsList
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }

        public AssignmentViewModel getAllAssignmentsOnSemester(int semesterId)
        {
            //Sæki öll gögn í verkefna töfluna
            var assignments = (from assign in _db.Assignment
                               join course in _db.Course on assign.courseId equals course.courseId
                               join semester in _db.Semester on course.semesterId equals semester.semesterId
                               select new { assign, course }).ToList();

            //Bý til lista af verkefnum
            List<AssignmentViewModel> assignmentsList;
            assignmentsList = new List<AssignmentViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í verkefna listann
            foreach (var entity in assignments)
            {
                var assignmentParts = getAssignmentParts(entity.assign.assignmentId);
                var result = new AssignmentViewModel
                {
                    AssignmentId = entity.assign.assignmentId,
                    CourseId = entity.assign.courseId,
                    CourseName = entity.course.courseName,
                    CourseNumber = entity.course.courseNumber,
                    AssignmentName = entity.assign.assignmentName,
                    AssignmentDescription = entity.assign.assignmentDescription,
                    AssignmentFile = entity.assign.assignmentFile,
                    Weight = entity.assign.weight,
                    MaxSubmission = entity.assign.maxSubmission,
                    AssignDate = entity.assign.assignDate,
                    DueDate = entity.assign.dueDate,
                    GradePublished = entity.assign.gradePublished,
                    AssignmentPartList = assignmentParts.AssignmentPartList
                    //AssignmentSubmissionsList =
                    //DiscussionsList =
                };
                assignmentsList.Add(result);
            }

            //Bý til nýtt AssingmentViewModel og set listann inn í það
            AssignmentViewModel viewModel = new AssignmentViewModel
            {
                AssignmentList = assignmentsList
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }

        public AssignmentViewModel getAllUserAssignments(int userId)
        {
            //Sæki öll gögn í verkefna töfluna
            var assignments = (from assign in _db.Assignment
                               join course in _db.Course on assign.courseId equals course.courseId
                               join userCourse in _db.UserCourse on course.courseId equals userCourse.courseId
                               where userCourse.userId.Equals(userId)
                               select new { assign, course }).ToList();

            //Bý til lista af verkefnum
            List<AssignmentViewModel> assignmentsList;
            assignmentsList = new List<AssignmentViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í verkefna listann
            foreach (var entity in assignments)
            {
                var assignmentParts = getAssignmentParts(entity.assign.assignmentId);
                var result = new AssignmentViewModel
                {
                    AssignmentId = entity.assign.assignmentId,
                    CourseId = entity.assign.courseId,
                    CourseName = entity.course.courseName,
                    CourseNumber = entity.course.courseNumber,
                    AssignmentName = entity.assign.assignmentName,
                    AssignmentDescription = entity.assign.assignmentDescription,
                    AssignmentFile = entity.assign.assignmentFile,
                    Weight = entity.assign.weight,
                    MaxSubmission = entity.assign.maxSubmission,
                    AssignDate = entity.assign.assignDate,
                    DueDate = entity.assign.dueDate,
                    GradePublished = entity.assign.gradePublished,
                    AssignmentPartList = assignmentParts.AssignmentPartList
                    //AssignmentSubmissionsList =
                    //DiscussionsList =
                };
                assignmentsList.Add(result);
            }

            //Bý til nýtt AssingmentViewModel og set listann inn í það
            AssignmentViewModel viewModel = new AssignmentViewModel
            {
                AssignmentList = assignmentsList
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }

        public AssignmentViewModel getAllUserAssignmentsInCourse(string userId, int courseId)
        {
            //Sæki öll gögn í verkefna töfluna
            var assignments = (from assign in _db.Assignment
                               join course in _db.Course on assign.courseId equals course.courseId
                               join userCourse in _db.UserCourse on course.courseId equals userCourse.courseId
                               where userCourse.userId == userId && course.courseId == courseId
                               select new { assign, course }).ToList();

            //Bý til lista af verkefnum
            List<AssignmentViewModel> assignmentsList;
            assignmentsList = new List<AssignmentViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í verkefna listann
            foreach (var entity in assignments)
            {
                var assignmentParts = getAssignmentParts(entity.assign.assignmentId);
                var result = new AssignmentViewModel
                {
                    AssignmentId = entity.assign.assignmentId,
                    CourseId = entity.assign.courseId,
                    CourseName = entity.course.courseName,
                    CourseNumber = entity.course.courseNumber,
                    AssignmentName = entity.assign.assignmentName,
                    AssignmentDescription = entity.assign.assignmentDescription,
                    AssignmentFile = entity.assign.assignmentFile,
                    Weight = entity.assign.weight,
                    MaxSubmission = entity.assign.maxSubmission,
                    AssignDate = entity.assign.assignDate,
                    DueDate = entity.assign.dueDate,
                    GradePublished = entity.assign.gradePublished,
                    AssignmentPartList = assignmentParts.AssignmentPartList
                    //AssignmentSubmissionsList =
                    //DiscussionsList =
                };
                assignmentsList.Add(result);
            }

            //Bý til nýtt AssingmentViewModel og set listann inn í það
            AssignmentViewModel viewModel = new AssignmentViewModel
            {
                AssignmentList = assignmentsList
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }

        public AssignmentViewModel getOpenAssignments()
        {
            var openAssignments = (from assign in _db.Assignment
                                   join course in _db.Course on assign.courseId equals course.courseId
                                   where assign.dueDate >= DateTime.Now
                                   select new { assign, course }).ToList();

            List<AssignmentViewModel> openAssignmentsList;
            openAssignmentsList = new List<AssignmentViewModel>();

            foreach (var entity in openAssignments)
            {
                var assignmentParts = getAssignmentParts(entity.assign.assignmentId);
                var result = new AssignmentViewModel
                {
                    AssignmentId = entity.assign.assignmentId,
                    CourseId = entity.assign.courseId,
                    CourseName = entity.course.courseName,
                    CourseNumber = entity.course.courseNumber,
                    AssignmentName = entity.assign.assignmentName,
                    AssignmentDescription = entity.assign.assignmentDescription,
                    AssignmentFile = entity.assign.assignmentFile,
                    Weight = entity.assign.weight,
                    MaxSubmission = entity.assign.maxSubmission,
                    AssignDate = entity.assign.assignDate,
                    DueDate = entity.assign.dueDate,
                    GradePublished = entity.assign.gradePublished,
                    AssignmentPartList = assignmentParts.AssignmentPartList
                };

                openAssignmentsList.Add(result);
            }

            //Bý til nýtt AssingmentViewModel og set listann inn í það
            AssignmentViewModel viewModel = new AssignmentViewModel
            {
                OpenAssignmentList = openAssignmentsList,
            };
            
            //Skila viewModelinu með listanum
            return viewModel;
        }

        public AssignmentViewModel getClosedAssignments()
        {
            var closedAssignments = (from assign in _db.Assignment
                                     join course in _db.Course on assign.courseId equals course.courseId
                                     where assign.dueDate < DateTime.Now
                                     select new { assign, course }).ToList();

            List<AssignmentViewModel> closedAssignmentsList;
            closedAssignmentsList = new List<AssignmentViewModel>();

            foreach (var entity in closedAssignments)
            {
                var assignmentParts = getAssignmentParts(entity.assign.assignmentId);
                var result = new AssignmentViewModel
                {
                    AssignmentId = entity.assign.assignmentId,
                    CourseId = entity.assign.courseId,
                    CourseName = entity.course.courseName,
                    CourseNumber = entity.course.courseNumber,
                    AssignmentName = entity.assign.assignmentName,
                    AssignmentDescription = entity.assign.assignmentDescription,
                    AssignmentFile = entity.assign.assignmentFile,
                    Weight = entity.assign.weight,
                    MaxSubmission = entity.assign.maxSubmission,
                    AssignDate = entity.assign.assignDate,
                    DueDate = entity.assign.dueDate,
                    GradePublished = entity.assign.gradePublished,
                    AssignmentPartList = assignmentParts.AssignmentPartList
                };

                closedAssignmentsList.Add(result);
            }

            //Bý til nýtt AssingmentViewModel og set listann inn í það
            AssignmentViewModel viewModel = new AssignmentViewModel
            {
                ClosedAssignmentList = closedAssignmentsList
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }

        public AssignmentViewModel getAllAssignmentsInCourse(int courseId)
        {
            //Sæki öll opin verkefni í verkefna töfluna
            var openAssignments = (from assign in _db.Assignment
                                   join course in _db.Course on assign.courseId equals course.courseId
                                   where course.courseId == courseId && assign.dueDate >= DateTime.Now
                                   select new { assign, course }).ToList();

            var closedAssignments = (from assign in _db.Assignment
                                     join course in _db.Course on assign.courseId equals course.courseId
                                     where course.courseId == courseId && assign.dueDate < DateTime.Now
                                     select new { assign, course }).ToList();

            //Bý til lista af verkefnum
            List<AssignmentViewModel> openAssignmentsList;
            openAssignmentsList = new List<AssignmentViewModel>();

            List<AssignmentViewModel> closedAssignmentsList;
            closedAssignmentsList = new List<AssignmentViewModel>();

            //Loopa í gegnum opin verkefni
            foreach (var entity in openAssignments)
            {
                var assignmentParts = getAssignmentParts(entity.assign.assignmentId);
                var result = new AssignmentViewModel
                {
                    AssignmentId = entity.assign.assignmentId,
                    CourseId = entity.assign.courseId,
                    CourseName = entity.course.courseName,
                    CourseNumber = entity.course.courseNumber,
                    AssignmentName = entity.assign.assignmentName,
                    AssignmentDescription = entity.assign.assignmentDescription,
                    AssignmentFile = entity.assign.assignmentFile,
                    Weight = entity.assign.weight,
                    MaxSubmission = entity.assign.maxSubmission,
                    AssignDate = entity.assign.assignDate,
                    DueDate = entity.assign.dueDate,
                    GradePublished = entity.assign.gradePublished,
                    AssignmentPartList = assignmentParts.AssignmentPartList
                };
                openAssignmentsList.Add(result);
            }

            foreach (var entity in closedAssignments)
            {
                var assignmentParts = getAssignmentParts(entity.assign.assignmentId);
                var result = new AssignmentViewModel
                {
                    AssignmentId = entity.assign.assignmentId,
                    CourseId = entity.assign.courseId,
                    CourseName = entity.course.courseName,
                    CourseNumber = entity.course.courseNumber,
                    AssignmentName = entity.assign.assignmentName,
                    AssignmentDescription = entity.assign.assignmentDescription,
                    AssignmentFile = entity.assign.assignmentFile,
                    Weight = entity.assign.weight,
                    MaxSubmission = entity.assign.maxSubmission,
                    AssignDate = entity.assign.assignDate,
                    DueDate = entity.assign.dueDate,
                    GradePublished = entity.assign.gradePublished,
                    AssignmentPartList = assignmentParts.AssignmentPartList
                };
                closedAssignmentsList.Add(result);
            }



            //Bý til nýtt AssingmentViewModel og set listann inn í það
            AssignmentViewModel viewModel = new AssignmentViewModel
            {
                OpenAssignmentList = openAssignmentsList,
                ClosedAssignmentList = closedAssignmentsList,
                CourseId = courseId
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }


        public AssignmentViewModel getAllUserAssignmentsOnSemester(string userId, int semesterId)
        {
            //Sæki öll gögn í verkefna töfluna
            var assignments = (from assign in _db.Assignment
                               join course in _db.Course on assign.courseId equals course.courseId
                               join userCourse in _db.UserCourse on course.courseId equals userCourse.courseId
                               join semester in _db.Semester on course.semesterId equals semester.semesterId
                               where semester.semesterId == semesterId && userCourse.userId == userId
                               select new { assign, course }).ToList();

            //Bý til lista af verkefnum
            List<AssignmentViewModel> assignmentsList;
            assignmentsList = new List<AssignmentViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í verkefna listann
            foreach (var entity in assignments)
            {
                var assignmentParts = getAssignmentParts(entity.assign.assignmentId);
                var result = new AssignmentViewModel
                {
                    AssignmentId = entity.assign.assignmentId,
                    CourseId = entity.assign.courseId,
                    CourseName = entity.course.courseName,
                    CourseNumber = entity.course.courseNumber,
                    AssignmentName = entity.assign.assignmentName,
                    AssignmentDescription = entity.assign.assignmentDescription,
                    AssignmentFile = entity.assign.assignmentFile,
                    Weight = entity.assign.weight,
                    MaxSubmission = entity.assign.maxSubmission,
                    AssignDate = entity.assign.assignDate,
                    DueDate = entity.assign.dueDate,
                    GradePublished = entity.assign.gradePublished,
                    AssignmentPartList = assignmentParts.AssignmentPartList
                    //AssignmentSubmissionsList =
                    //DiscussionsList =
                };
                assignmentsList.Add(result);
            }

            //Bý til nýtt AssingmentViewModel og set listann inn í það
            AssignmentViewModel viewModel = new AssignmentViewModel
            {
                AssignmentList = assignmentsList
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }

        public AssignmentViewModel getAssignmentGrade(string userId, int assignmentId)
        {
            //Sæki öll gögn í verkefna töfluna
            var assignment = (from assign in _db.Assignment
                              join userAssignment in _db.UserAssignment on assign.assignmentId equals userAssignment.assignmentId
                              where assign.assignmentId == assignmentId && userAssignment.userId == userId
                              select new { assign, userAssignment }).SingleOrDefault();

            UserAssignment userAssign = null;
            if (assignment.assign.gradePublished == 1)
            {
                userAssign = new UserAssignment
                {
                    userAssignmentId = assignment.userAssignment.userAssignmentId,
                    userId = assignment.userAssignment.userId,
                    userGroupId = assignment.userAssignment.userGroupId,
                    grade = assignment.userAssignment.grade,
                    gradeComment = assignment.userAssignment.gradeComment
                };
            }
            //Bý til nýtt AssingmentViewModel og set listann inn í það
            AssignmentViewModel viewModel = new AssignmentViewModel
            {
                UserAssignment = userAssign
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }


        public AssignmentViewModel editAssignment(AssignmentViewModel assignmentToChange)
        {
            // Sæki færsluna sem á að breyta í gagnagrunninn
            var query = (from assignment in _db.Assignment
                         where assignment.assignmentId == assignmentToChange.AssignmentId
                         select assignment).SingleOrDefault();

            // Set inn breyttu upplýsingarnar
            query.courseId = assignmentToChange.CourseId;
            query.assignmentName = assignmentToChange.AssignmentName;
            query.assignmentDescription = assignmentToChange.AssignmentDescription;
            query.assignmentFile = assignmentToChange.AssignmentFile;
            query.weight = assignmentToChange.Weight;
            query.maxSubmission = assignmentToChange.MaxSubmission;
            query.assignDate = assignmentToChange.AssignDate;
            query.dueDate = assignmentToChange.DueDate;
            query.gradePublished = assignmentToChange.GradePublished;

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
            return assignmentToChange;
        }

        public AssignmentViewModel addAssignment(AssignmentViewModel assignmentToAdd, string serverPath)
        {
            var newAssignment = new Assignment();

            //setja propery-in
            newAssignment.courseId = assignmentToAdd.CourseId;
            newAssignment.assignmentName = assignmentToAdd.AssignmentName;
            newAssignment.assignmentDescription = assignmentToAdd.AssignmentDescription;
            newAssignment.assignmentFile = assignmentToAdd.AssignmentFile;
            newAssignment.weight = assignmentToAdd.Weight;
            newAssignment.maxSubmission = assignmentToAdd.MaxSubmission;
            newAssignment.assignDate = assignmentToAdd.AssignDate;
            newAssignment.dueDate = assignmentToAdd.DueDate;
            newAssignment.gradePublished = assignmentToAdd.GradePublished;

            try
            {
                //Vista ofan í gagnagrunn
                newAssignment = _db.Assignment.Add(newAssignment);
                _db.SaveChanges();
                assignmentToAdd.AssignmentId = newAssignment.assignmentId;
                //Vista skránna sem fylgir verkefninu
                addAssignmentFile(serverPath, assignmentToAdd);
                addAssignmentPart(assignmentToAdd, serverPath);
                return assignmentToAdd;
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
        }

        public Boolean addAssignmentTestCase(AssignmentTestCase assignmentTestCaseToAdd)
        {
            var newAssignmentTestCase = new AssignmentTestCase();
            var testCaseCount = (from testCase in _db.AssignmentTestCase
                                 where testCase.assignmentPartId == assignmentTestCaseToAdd.assignmentPartId
                                 select testCase).Count();
            //setja propery-in
            newAssignmentTestCase.assignmentPartId = assignmentTestCaseToAdd.assignmentPartId;
            newAssignmentTestCase.testNumber = testCaseCount + 1;
            newAssignmentTestCase.input = assignmentTestCaseToAdd.input;
            newAssignmentTestCase.output = assignmentTestCaseToAdd.output;
            //Todo setja inn öll property

            try
            {
                //Vista ofan í gagnagrunn
                _db.AssignmentTestCase.Add(newAssignmentTestCase);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean addAssignmentFile(string serverPath, AssignmentViewModel assignment)
        {
            string filePathFull = serverPath + "\\Content\\Files\\Assignments\\";

            if (!Directory.Exists(filePathFull))
            {
                Directory.CreateDirectory(filePathFull);
            }

            if (assignment.AssignmentUploaded != null)
            {

                string fileExtension = System.IO.Path.GetExtension(assignment.AssignmentUploaded.FileName);
                string fileContentType = assignment.AssignmentUploaded.ContentType;

                AssignmentFile newFile = new AssignmentFile();
                newFile.assignmentId = assignment.AssignmentId;
                newFile.fileType = fileContentType;
                newFile.fileExtension = fileExtension;

                try
                {
                    newFile = _db.AssignmentFile.Add(newFile);
                    _db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                string fileName = newFile.assignmentFileId.ToString();
                newFile.path = filePathFull + fileName + fileExtension;
                newFile.fileExtension = fileExtension;

                _db.Entry(newFile).State = EntityState.Modified;
                _db.SaveChanges();

                assignment.AssignmentUploaded.SaveAs(newFile.path);

                return true;
            }

            return false;
        }

        public Boolean addAssignmentPartFile(string serverPath, AssignmentPartViewModel assignmentPart)
        {
            string filePathFull = serverPath + "\\Content\\Files\\AssignmentParts\\";

            if (!Directory.Exists(filePathFull))
            {
                Directory.CreateDirectory(filePathFull);
            }

            if (assignmentPart.AssignmentPartUploaded != null)
            {

                string fileExtension = System.IO.Path.GetExtension(assignmentPart.AssignmentPartUploaded.FileName);
                string fileContentType = assignmentPart.AssignmentPartUploaded.ContentType;

                AssignmentPartFile newFile = new AssignmentPartFile();
                newFile.assignmentPartId = assignmentPart.AssignmentPartId;
                newFile.fileType = fileContentType;
                newFile.fileExtension = fileExtension;

                try
                {
                    newFile = _db.AssignmentPartFile.Add(newFile);
                    _db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                string fileName = newFile.assignmentPartFileId.ToString();
                newFile.path = filePathFull + fileName + fileExtension;
                newFile.fileExtension = fileExtension;

                _db.Entry(newFile).State = EntityState.Modified;
                _db.SaveChanges();

                assignmentPart.AssignmentPartUploaded.SaveAs(newFile.path);

                return true;
            }

            return false;
        }

        public AssignmentViewModel getAllProgrammingLanguages()
        {
            //Sæki öll forritunarmál
            var programmingLanguages = _db.ProgrammingLanguage.ToList();
            var languages = new SelectList(programmingLanguages, "ProgrammingLanguageId", "ProgrammingLanguageName");
            //Returna viewModelinu með listanum
            AssignmentViewModel viewModel = new AssignmentViewModel
            {
                ProgrammingLanguages = languages
            };
            return viewModel;
        }

        public AssignmentViewModel addAssignmentPart(AssignmentViewModel assignmentToAdd, string serverPath)
        {
            //setja propery-in
            for (int i = 0; i < assignmentToAdd.AssignmentPartList.Count; i++)
            {
                var newAssignmentPart = new AssignmentPart();
                newAssignmentPart.assignmentId = assignmentToAdd.AssignmentId;
                newAssignmentPart.assignmentPartName = assignmentToAdd.AssignmentPartList[i].AssignmentPartName;
                newAssignmentPart.assignmentPartDescription = assignmentToAdd.AssignmentPartList[i].AssignmentPartDescription;
                newAssignmentPart.weight = assignmentToAdd.AssignmentPartList[i].Weight;
                newAssignmentPart.programmingLanguageId = assignmentToAdd.AssignmentPartList[i].ProgrammingLanguageId;

                newAssignmentPart = _db.AssignmentPart.Add(newAssignmentPart);
                _db.SaveChanges();
                assignmentToAdd.AssignmentPartList[i].AssignmentPartId = newAssignmentPart.assignmentPartId;
                addAssignmentPartTestCase(assignmentToAdd.AssignmentPartList[i]);
                addAssignmentPartFile(serverPath, assignmentToAdd.AssignmentPartList[i]);
            }
            return null;
        }

        public void addAssignmentPartTestCase(AssignmentPartViewModel assignmentPart)
        {
            //setja propery-in
            for (int i = 0; i < assignmentPart.AssignmentTestCaseList.Count; i++)
            {
                var newAssignmentTestCase = new AssignmentTestCase();
                newAssignmentTestCase.assignmentPartId = assignmentPart.AssignmentPartId;
                newAssignmentTestCase.input = assignmentPart.AssignmentTestCaseList[i].input;
                newAssignmentTestCase.output = assignmentPart.AssignmentTestCaseList[i].output;
                newAssignmentTestCase.testNumber = i + 1;

                newAssignmentTestCase = _db.AssignmentTestCase.Add(newAssignmentTestCase);
                _db.SaveChanges();
            }
        }

        public AssignmentViewModel studentSubmitsAssignment(AssignmentViewModel submission, string serverPath)
        {
            //Smíða haus færslu ef hún er ekki til nú þegar
            submission = userAssignmentCreate(submission);

            //Test submissionið
            //TODO harðkóðað eins og er
            submission.UserSubmission.accepted = 0;
            submission.UserSubmission.numberOfSucessTestCases = 0;
            submission.UserSubmission.testCaseFailId = 2;// submission.AssignmentPartList[0].AssignmentTestCaseList[0].assignmentTestCaseId;
            submission.UserSubmission.error = "Compile time error";
            submission = addSubmission(submission);

            //Vista skrána
            submission = submitFile(submission, serverPath);

            cppProgram(serverPath);

            return submission;
        }

        public AssignmentViewModel testSubmission(AssignmentViewModel submission)
        {
            submission.UserSubmission.accepted = 0;
            return submission;

        }


        public AssignmentViewModel userAssignmentCreate(AssignmentViewModel submission)
        {

            var userAssignmentExists = (from userAssignment in _db.UserAssignment
                                        where userAssignment.assignmentId == submission.AssignmentId
                                       && userAssignment.userId == submission.UserAssignment.userId
                                        select userAssignment).SingleOrDefault();

            if (userAssignmentExists == null)
            {
                var newUserAssignment = new UserAssignment();
                //setja propery-in
                newUserAssignment.userId = submission.UserAssignment.userId;
                newUserAssignment.assignmentId = submission.AssignmentId;
                //try
                //{
                //Vista ofan í gagnagrunn
                submission.UserAssignment = _db.UserAssignment.Add(newUserAssignment);
                _db.SaveChanges();
                return submission;
                //}
                //catch
                //{
                //return null;
                //}
            }
            else
            {
                submission.UserAssignment = userAssignmentExists;
                return submission;
            }

        }

        public AssignmentViewModel submitFile(AssignmentViewModel submission, string serverPath)
        {
            string filePathFull = serverPath + "\\Content\\Files\\Submissions\\";

            if (!Directory.Exists(filePathFull))
            {
                Directory.CreateDirectory(filePathFull);
            }

            string fileExtension = System.IO.Path.GetExtension(submission.SubmissionUploaded.FileName);
            string fileContentType = submission.SubmissionUploaded.ContentType;

            SubmissionFile newFile = new SubmissionFile();
            newFile.submissionId = submission.UserSubmission.submissionId;
            newFile.fileType = fileContentType;
            newFile.fileExtension = fileExtension;

            try
            {
                newFile = _db.SubmissionFile.Add(newFile);
                _db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            string fileName = newFile.submissionFileId.ToString();
            newFile.path = filePathFull + fileName + fileExtension;
            newFile.fileExtension = fileExtension;

            _db.Entry(newFile).State = EntityState.Modified;
            _db.SaveChanges();

            submission.SubmissionUploaded.SaveAs(newFile.path);

            return submission;
        }

        public AssignmentViewModel addSubmission(AssignmentViewModel submissionToChange)
        {
            // Bý til nýja færslu
            var submission = new Submission();

            // Set inn breyttu upplýsingarnar
            submission.userAssignmentId = submissionToChange.UserAssignment.userAssignmentId;
            submission.assignmentPartId = submissionToChange.UserSubmission.assignmentPartId;
            submission.accepted = submissionToChange.UserSubmission.accepted;
            submission.numberOfSucessTestCases = submissionToChange.UserSubmission.numberOfSucessTestCases;
            submission.testCaseFailId = submissionToChange.UserSubmission.testCaseFailId;
            submission.error = submissionToChange.UserSubmission.error;


            //Vista breytingar í gagnagrunn
            //try
            //{
            _db.Submission.Add(submission);
            _db.SaveChanges();
            //}
            //catch (Exception e)
            //{
            //  Console.WriteLine(e);
            // TODO
            //}
            submissionToChange.UserSubmission = submission;
            return submissionToChange;
        }

        public List<string> cppProgram(string serverPath)
        {
            var code = "#include <iostream>\n" +
                        "using namespace std;\n" +
                        "int main()\n" +
                        "{\n" +
                        "cout << \"Hello world\" << endl;\n" +
                        "cout << \"The output should only contain two lines\" << endl;\n" +
                        "return 0;\n" +
                        "}";

            var workingFolder = serverPath + "\\Content\\Files\\CPP\\";

            if (!Directory.Exists(workingFolder))
            {
                Directory.CreateDirectory(workingFolder);
            }

            var cppFileName = "Hello.cpp";
            var exeFilePath = workingFolder + "Hello.exe";

            File.WriteAllText(workingFolder + cppFileName, code);

            //Location of c++ compiler
            var compilerFolder = "C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\VC\\bin\\";

            Process compiler = new Process();
            compiler.StartInfo.FileName = "cmd.exe";
            compiler.StartInfo.WorkingDirectory = workingFolder;
            compiler.StartInfo.RedirectStandardInput = true;
            compiler.StartInfo.RedirectStandardOutput = true;
            compiler.StartInfo.UseShellExecute = false;

            compiler.Start();
            compiler.StandardInput.WriteLine("\"" + compilerFolder + "vcvars32.bat" + "\"");
            compiler.StandardInput.WriteLine("cl.exe /nologo /EHsc " + cppFileName);
            compiler.StandardInput.WriteLine("exit");
            string output = compiler.StandardOutput.ReadToEnd();
            compiler.WaitForExit();

            var lines = new List<string>();
            if (File.Exists(exeFilePath))
            {
                var processInfoExe = new ProcessStartInfo(exeFilePath, "");
                processInfoExe.UseShellExecute = false;
                processInfoExe.RedirectStandardOutput = true;
                processInfoExe.RedirectStandardError = true;
                processInfoExe.CreateNoWindow = true;
                using (var processExe = new Process())
                {
                    processExe.StartInfo = processInfoExe;
                    processExe.Start();

                    //Read the output of the program
                    while(!processExe.StandardOutput.EndOfStream)
                    {
                        lines.Add(processExe.StandardOutput.ReadLine());
                    }
                    
                }
            }

            return lines;
        }
    }
}