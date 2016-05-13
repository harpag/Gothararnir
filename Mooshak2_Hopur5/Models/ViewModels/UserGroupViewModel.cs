using Mooshak2_Hopur5.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.ViewModels
{
    public class UserGroupViewModel
    {
        public int userGroupId { get; set; }
        public int courseId { get; set; }
        [Required]
        [DisplayName("User group name")]
        public string userGroupName { get; set; }
        public string userCreate { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<UserAssignment> UserAssignment { get; set; }
        public virtual ICollection<UserGroupMember> UserGroupMember { get; set; }
        public List<ApplicationUser> AllUsers { get; set; }
        public List<Boolean> CheckedUsers { get; set; }
    }
}