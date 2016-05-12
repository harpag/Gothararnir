using Mooshak2_Hopur5.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.ViewModels
{
    public class SubmissionViewModel
    {
        public int SubmissionId { get; set; }
        public int AssignmentPartId { get; set; }
        public int UserAssignmentId { get; set; }
        public string SubmissionComment { get; set; }
        public int? Accepted { get; set; }
        public int? NumberOfSucessTestCases { get; set; }
        public int? TestCaseFailId { get; set; }
        public string Error { get; set; }
        public SubmissionFile File { get; set; }

    }
}