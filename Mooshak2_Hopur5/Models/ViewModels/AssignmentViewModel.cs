using Mooshak2_Hopur5.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [DisplayName("Name")]
        public string AssignmentName { get; set; }
        [DisplayName("Description")]
        public string AssignmentDescription { get; set; }
        [DisplayName("File")]
        public byte[] AssignmentFile { get; set; }
        [DisplayName("Weight")]
        public int Weight { get; set; }
        [DisplayName("Maximum submissions")]
        public int? MaxSubmission { get; set; }
        [DisplayName("Open date of assignment")]
        public DateTime? AssignDate { get; set; }
        [DisplayName("Deadline")]
        public DateTime? DueDate { get; set; }
        public int? GradePublished { get; set; }
        public List<AssignmentViewModel> AssignmentList { get; set; }
        public List<AssignmentPartViewModel> AssignmentPartList { get; set; }
        public List<Submission> AssignmentSubmissionsList { get; set; }
        public List<Discussion> DiscussionsList { get; set; }
        public UserAssignment UserAssignment {get; set;}

        #region extra properties


        [DisplayName("Attachment")]
        public HttpPostedFileBase ImageUploaded { get; set; }


        #endregion
    }
}