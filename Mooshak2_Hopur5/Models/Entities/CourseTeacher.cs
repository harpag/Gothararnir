namespace Mooshak2_Hopur5.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseTeacher")]
    public partial class CourseTeacher
    {
        public int courseTeacherId { get; set; }

        public int courseId { get; set; }

        [Required]
        [StringLength(128)]
        public string userId { get; set; }

        public int? mainTeacher { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual Course Course { get; set; }
    }
}
