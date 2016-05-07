namespace Mooshak2_Hopur5.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserGroupMember")]
    public partial class UserGroupMember
    {
        public int userGroupMemberId { get; set; }

        [Required]
        [StringLength(128)]
        public string userId { get; set; }

        public int userGroupId { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
