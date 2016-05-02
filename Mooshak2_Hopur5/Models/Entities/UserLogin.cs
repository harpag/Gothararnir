namespace Mooshak2_Hopur5.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserLogin")]
    public partial class UserLogin
    {
        public int userLoginId { get; set; }

        public int userId { get; set; }

        public DateTime timeOfLogin { get; set; }

        public virtual User User { get; set; }
    }
}
