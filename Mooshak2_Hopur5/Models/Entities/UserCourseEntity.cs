using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mooshak2_Hopur5.Models.Entities
{
    public class UserCourseEntity
    {
        [Key]
        public int userCourseId { get; set; }
        public int userId { get; set; }
        public int courseId { get; set; }
    }
}