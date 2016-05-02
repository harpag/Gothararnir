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
        public int userGroupId { get; set; }
        public int courseId { get; set; }
        public int userGroupName { get; set; }
        public int userCreate { get; set; }
    }
}