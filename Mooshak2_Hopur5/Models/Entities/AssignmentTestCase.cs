namespace Mooshak2_Hopur5.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AssignmentTestCase")]
    public partial class AssignmentTestCase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AssignmentTestCase()
        {
            Submission = new HashSet<Submission>();
        }

        public int assignmentTestCaseId { get; set; }

        public int assignmentPartId { get; set; }

        public int testNumber { get; set; }

        [Required]
        public string input { get; set; }

        [Required]
        public string output { get; set; }

        public virtual AssignmentPart AssignmentPart { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Submission> Submission { get; set; }
    }
}
