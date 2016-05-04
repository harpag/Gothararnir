namespace Mooshak2_Hopur5.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        internal int courseId;
        internal int announcementId;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Announcement = new HashSet<Announcement>();
            CourseTeacher = new HashSet<CourseTeacher>();
            Discussion = new HashSet<Discussion>();
            UserAssignment = new HashSet<UserAssignment>();
            UserCourse = new HashSet<UserCourse>();
            UserGroupMember = new HashSet<UserGroupMember>();
            UserLogin = new HashSet<UserLogin>();
        }

        public int userId { get; set; }

        public int userTypeId { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string userName { get; set; }

        [Required]
        public string ssn { get; set; }

        [Required]
        public string password { get; set; }

        public string salt { get; set; }

        [Required]
        public string email { get; set; }

        public int? valid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Announcement> Announcement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseTeacher> CourseTeacher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Discussion> Discussion { get; set; }

        public virtual UserType UserType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAssignment> UserAssignment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCourse> UserCourse { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserGroupMember> UserGroupMember { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserLogin> UserLogin { get; set; }
    }
}
