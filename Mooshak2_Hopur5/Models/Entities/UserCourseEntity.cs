using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak_Hopur5.Models.Entities
{
    public class UserCourseEntity
    {
        int userCourseId { get; set; }
        int userId { get; set; }
        int courseId { get; set; }
    }
}