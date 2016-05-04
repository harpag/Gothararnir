using Mooshak2_Hopur5.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.ViewModels
{
    public class AssignmentViewModel
    {
        public int AssignmentId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseNumber { get; set; }
        public string AssignmentName { get; set; }
        public string AssignmentDescription { get; set; }
        public byte[] AssignmentFile { get; set; }
        public int Weight { get; set; }
        public int? MaxSubmission { get; set; }
        public DateTime? AssignDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int? GradePublished { get; set; }
        public List<AssignmentViewModel> AssignmentList { get; set; }
        public List<AssignmentPartViewModel> AssignmentPartList { get; set; }
        public List<Submission> AssignmentSubmissionsList { get; set; }
        public List<Discussion> DiscussionsList { get; set; }
        public UserAssignment UserAssignment {get; set;}
    }
}