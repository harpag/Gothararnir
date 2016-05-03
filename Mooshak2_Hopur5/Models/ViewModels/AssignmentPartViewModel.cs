using Mooshak2_Hopur5.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.ViewModels
{
    public class AssignmentPartViewModel
    {
        public int AssignmentPartId { get; set; }
        public int AssignmentId { get; set; }
        public string AssignmentName { get; set; }
        public string AssignmentPartName { get; set; }
        public string AssignmentPartDescription { get; set; }
        public byte[] AssignmentPartFile { get; set; }
        public double Weight { get; set; }
        public int ProgrammingLanguageId { get; set;  }
        public List<AssignmentPartViewModel> AssignmentPartList { get; set; }
        public List<AssignmentTestCase> AssignmentTestCaseList { get; set; }
    }
}