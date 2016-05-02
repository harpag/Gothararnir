namespace Mooshak2_Hopur5.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AssignmentPart")]
    public partial class AssignmentPart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AssignmentPart()
        {
            AssignmentTestCase = new HashSet<AssignmentTestCase>();
            Submission = new HashSet<Submission>();
        }

        public int assignmentPartId { get; set; }

        public int assignmentId { get; set; }

        [Required]
        public string assignmentPartName { get; set; }

        public string assignmentPartDescription { get; set; }

        public byte[] assignmentPartFile { get; set; }

        public int weight { get; set; }

        public int programmingLanguageId { get; set; }

        public virtual Assignment Assignment { get; set; }

        public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignmentTestCase> AssignmentTestCase { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Submission> Submission { get; set; }
    }
}
