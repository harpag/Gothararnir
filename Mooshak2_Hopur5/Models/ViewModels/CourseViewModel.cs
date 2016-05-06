using Mooshak2_Hopur5.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2_Hopur5.Models.ViewModels
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        [DisplayName("Course name")]
        public string CourseName { get; set; }
        [DisplayName("Course number")]
        public string CourseNumber { get; set; }
        [DisplayName("Semester")]
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public List<CourseViewModel> CourseList { get; set; }
        public List<UserViewModel> UserList { get; set; }
        public List<CourseTeacherViewModel> TeacherList { get; set; }
        public List<AssignmentViewModel> AssignmentList { get; set; }
        public IEnumerable<SelectListItem> Semesters { get; set; }
        public IEnumerable<SelectListItem> Teachers { get; set; }
        public IEnumerable<SelectListItem> UsersSelect { get; set; }
    }
}
