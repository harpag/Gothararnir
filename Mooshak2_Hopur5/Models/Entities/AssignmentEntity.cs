using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.Entities
{
    public class AssignmentEntity
    {
        [Key]
        public int assignmentId { get; set; }
        public int courseId { get; set; }
        public string assignmentName { get; set; } 
        public string assignmentDescription { get; set; }
        public string assignmentFile { get; set; }
        public int weight { get; set; }
        public int maxSubmission { get; set; }
        public DateTime assignDate { get; set; }
        public DateTime dueDate { get; set; }
        public int gradePublished { get; set; }
    }
}