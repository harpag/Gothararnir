using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak_Hopur5.Models.Entities
{
    public class AssignmentPartEntity
    {
        public int assignmentPartId { get; set; }
        public int assignmentId { get; set; }
        public string assignmentPartName { get; set; }
        public string assignmentPartDescription { get; set; }
        public string assignmentPartFile { get; set; }
        public int weight { get; set; } 
        public int programmingLanguageId { get; set; }
    }
}