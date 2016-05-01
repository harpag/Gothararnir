using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mooshak2_Hopur5.Models.Entities
{
    public class SemesterEntity
    {
        [Key]
        public int semesterId { get; set; }
        public string semesterNumber { get; set; }
        public string semesterName { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
    }
}