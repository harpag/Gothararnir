namespace Mooshak2_Hopur5.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubmissionFile")]
    public partial class SubmissionFile
    {
        public int submissionFileId { get; set; }

        public int submissionId { get; set; }

        public string path { get; set; }

        public string pathThumb { get; set; }

        [Required]
        public string fileType { get; set; }

        [Required]
        public string fileExtension { get; set; }

        public virtual Submission Submission { get; set; }
    }
}
