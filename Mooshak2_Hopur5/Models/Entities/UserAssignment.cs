namespace Mooshak2_Hopur5.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAssignment")]
    public partial class UserAssignment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserAssignment()
        {
            Submission = new HashSet<Submission>();
        }

        public int userAssignmentId { get; set; }

        public int userId { get; set; }

        public int userGroupId { get; set; }

        public double? grade { get; set; }

        public string gradeComment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Submission> Submission { get; set; }

        public virtual User User { get; set; }

        public virtual UserGroup UserGroup { get; set; }
        public int assignmentId { get; set; }
    }
}
