namespace Mooshak2_Hopur5.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AssignmentFile")]
    public partial class AssignmentFile
    {
        public int assignmentFileId { get; set; }

        public int assignmentId { get; set; }

        public string path { get; set; }

        public string pathThumb { get; set; }

        [Required]
        public string fileType { get; set; }

        [Required]
        public string fileExtension { get; set; }

        public virtual Assignment Assignment { get; set; }
    }
}
