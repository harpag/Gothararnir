using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.ViewModels
{
    public class CourseTeacherViewModel
    {
        public int CourseTeacherId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public int? MainTeacher { get; set; }
        public string TeacherName { get; set; }
        public string TeacherUserName { get; set; }
        public List<CourseTeacherViewModel> CourseTeacherList { get; set; }
    }
}