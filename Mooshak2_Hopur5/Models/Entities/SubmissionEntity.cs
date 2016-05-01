using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.Entities
{
    public class SubmissionEntity
    {
        public int submissionId { get; set; }
        public int assignmentPartId { get; set; }
        public int userAssignmentId { get; set; }
        public string submissionFile { get; set; }
        public string submissionComment { get; set; }
        public int accepted { get; set; }
        public int numberOfSucessTestCases { get; set; }
        public int testCaseFailId { get; set; }
        public string error { get; set; }
    }
}