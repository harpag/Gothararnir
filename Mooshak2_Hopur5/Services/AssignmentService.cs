using Mooshak2_Hopur5.Models.Entities;
using Mooshak2_Hopur5.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

            //Kasta villu ef ekki fannst verkefni með þessu ID-i
            if (assignment == null)
            {
                //TODO: Kasta villu
            }

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
                AssignmentPartList = assignmentPart.AssignmentPartList
            };

            //Returna ViewModelinu með áfanganum í
            return viewModel;
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
                    AssignmentName =entity.assign.assignmentName,
                    AssignmentDescription =entity.assign.assignmentDescription,
                    AssignmentFile =entity.assign.assignmentFile,
                    Weight = entity.assign.weight,
                    MaxSubmission = entity.assign.maxSubmission,
                    AssignDate = entity.assign.assignDate,
                    DueDate = entity.assign.dueDate,
                    GradePublished =entity.assign.gradePublished,
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
                               where userCourse.userId == userId
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

        public AssignmentViewModel getAllUserAssignmentsOnSemester(int userId, int semesterId)
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

        public AssignmentViewModel getAssignmentGrade(int userId, int assignmentId)
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

        public Boolean addAssignment(AssignmentViewModel assignmentToAdd)
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
            //Todo setja inn öll property

            try
            {
                //Vista ofan í gagnagrunn
                _db.Assignment.Add(newAssignment);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public Boolean addAssignmentTestCase(AssignmentTestCase assignmentTestCaseToAdd)
        {
            var newAssignmentTestCase = new AssignmentTestCase();
            var testCaseCount = (from testCase in _db.AssignmentTestCase
                              where testCase.assignmentPartId == assignmentTestCaseToAdd.assignmentPartId select testCase).Count();
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

        //submittAssignment()

        //addAssignmentDiscussion()

        //addAssignmentGrade()

        //editAssignmentGrade()

        //getAssignmentStatistics() ætla að geyma þetta
    }
}