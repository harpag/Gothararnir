namespace Mooshak2_Hopur5.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Discussion")]
    public partial class Discussion
    {
        public int discussionId { get; set; }

        public int assignmentId { get; set; }

        public int userId { get; set; }

        [Required]
        public string discussionText { get; set; }

        public virtual Assignment Assignment { get; set; }

        public virtual User User { get; set; }
    }
}
