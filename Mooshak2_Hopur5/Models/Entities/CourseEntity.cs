using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mooshak2_Hopur5.Models.Entities
{
    [Table("Course")]
    public class CourseEntity
    {
        [Key]
        public int courseId { get; set; }
        public int semesterId { get; set; }
        public string courseNumber { get; set; }
        public string courseName { get; set; }
    }
}