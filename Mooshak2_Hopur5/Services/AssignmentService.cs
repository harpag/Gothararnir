//using Mooshak2_Hopur5.Models.Entities;
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
        //submittAssignment()

        //addAssignmentDiscussion()

        //addAssignmentGrade()

        //editAssignmentGrade()
    }
}