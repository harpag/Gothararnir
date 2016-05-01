using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mooshak2_Hopur5.Models.Entities
{
    public class UserGroupEntity
    {
        [Key]
        int userGroupId { get; set; }
        int courseId { get; set; }
        int userGroupName { get; set; }
        int userCreate { get; set; }
    }
}