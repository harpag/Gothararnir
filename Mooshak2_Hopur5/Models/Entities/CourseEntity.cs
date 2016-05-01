using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak_Hopur5.Models.Entities
{
    public class CourseEntity
    {
        public int courseId { get; set; }
        public int semesterId { get; set; }
        public string courseNumber { get; set; }
        public string courseName { get; set; }
    }
}