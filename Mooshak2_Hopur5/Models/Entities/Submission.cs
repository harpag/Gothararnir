namespace Mooshak2_Hopur5.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Submission")]
    public partial class Submission
    {
        public int submissionId { get; set; }

        public int assignmentPartId { get; set; }

        public int userAssignmentId { get; set; }

        [Required]
        public byte[] submissionFile { get; set; }

        [Required]
        public string submissionComment { get; set; }

        public int? accepted { get; set; }

        public int? numberOfSucessTestCases { get; set; }

        public int? testCaseFailId { get; set; }

        public string error { get; set; }

        public virtual AssignmentPart AssignmentPart { get; set; }

        public virtual AssignmentTestCase AssignmentTestCase { get; set; }

        public virtual UserAssignment UserAssignment { get; set; }
    }
}
