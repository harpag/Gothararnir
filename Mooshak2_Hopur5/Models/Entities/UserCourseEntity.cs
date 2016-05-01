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
        int userCourseId { get; set; }
        int userId { get; set; }
        int courseId { get; set; }
    }
}