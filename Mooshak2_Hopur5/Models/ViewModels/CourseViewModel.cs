using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.ViewModels
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } 
        public string CourseNumber { get; set; }
        public List<CourseViewModel> CourseList { get; set; }
        public List<UserViewModel> UserList { get; internal set; }
    }
}