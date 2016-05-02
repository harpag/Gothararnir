namespace Mooshak2_Hopur5.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Assignment")]
    public partial class Assignment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Assignment()
        {
            AssignmentPart = new HashSet<AssignmentPart>();
            Discussion = new HashSet<Discussion>();
        }

        public int assignmentId { get; set; }

        public int courseId { get; set; }

        [Required]
        public string assignmentName { get; set; }

        [Required]
        public string assignmentDescription { get; set; }

        public byte[] assignmentFile { get; set; }

        public int weight { get; set; }

        public int? maxSubmission { get; set; }

        public DateTime? assignDate { get; set; }

        public DateTime? dueDate { get; set; }

        public int? gradePublished { get; set; }

        public virtual Course Course { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignmentPart> AssignmentPart { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Discussion> Discussion { get; set; }
    }
}
