namespace Mooshak2_Hopur5.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Announcement")]
    public partial class Announcement
    {
        public int announcementId { get; set; }

        public int userId { get; set; }

        [Column("announcement")]
        [Required]
        public string announcement1 { get; set; }

        public DateTime dateCreate { get; set; }

        public virtual User User { get; set; }
    }
}
