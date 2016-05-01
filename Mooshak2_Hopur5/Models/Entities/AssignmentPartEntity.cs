using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mooshak2_Hopur5.Models.Entities
{
    public class AssignmentPartEntity
    {
        [Key]
        public int assignmentPartId { get; set; }
        public int assignmentId { get; set; }
        public string assignmentPartName { get; set; }
        public string assignmentPartDescription { get; set; }
        public string assignmentPartFile { get; set; }
        public int weight { get; set; } 
        public int programmingLanguageId { get; set; }
    }
}