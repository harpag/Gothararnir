using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.Entities
{
    public class AssignmentTestCaseEntity
    {
        public int assignmentTestCaseId { get; set; }
        public int assignmentPartId { get; set; }   
        public int testNumber { get; set; }
        public string input { get; set; }
        public string output { get; set; }
    }
}