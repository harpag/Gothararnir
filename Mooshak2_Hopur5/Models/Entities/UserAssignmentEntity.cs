using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mooshak2_Hopur5.Models.Entities
{
    public class UserAssignmentEntity
    {
        [Key]
        public int userAssignmentId { get; set; }
        public int userId { get; set; }
        public int userGroupId { get; set; }
        public double grade { get; set; }
        public double gradeComment { get; set; }
    }
}