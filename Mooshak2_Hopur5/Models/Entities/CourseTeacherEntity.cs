using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak_Hopur5.Models.Entities
{
    public class CourseTeacherEntity
    {
        public int courseTeacherId { get; set; }
        public int courseId { get; set; }
        public int userId { get; set; }
        public int mainTeacher { get; set; }
    }
}