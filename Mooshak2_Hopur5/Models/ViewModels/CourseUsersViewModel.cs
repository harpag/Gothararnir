using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.ViewModels
{
    public class CourseUsersViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public List<string> UserList { get; set; }
        public List<ApplicationUser> AllUsers {get; set; }
        public List<Boolean> CheckedUsers { get;set; }
    }
}