using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.ViewModels
{
    public class SemesterViewModel
    {
        public int SemesterId { get; set; }
        [DisplayName("Semester name")]
        public string SemesterName { get; set; }
        [DisplayName("Semester number")]
        public string SemesterNumber { get; set; }
        [DisplayName("Date from")]
        public DateTime DateFrom { get; set; }
        [DisplayName("Date to")]
        public DateTime DateTo { get; set; }
        public List<SemesterViewModel> SemesterList { get; set; }
        public List<CourseViewModel> CourseOnSemesterList { get; set; }
    }
}