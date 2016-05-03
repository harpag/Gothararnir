using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.ViewModels
{
    public class SemesterViewModel
    {
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<SemesterViewModel> SemesterList { get; set; }
        public List<CourseViewModel> CourseOnSemesterList { get; set; }
    }
}