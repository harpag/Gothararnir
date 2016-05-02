using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mooshak2_Hopur5.Models.Entities
{
    public class UserLoginEntity
    {
        [Key]
        public int userLoginId { get; set; }
        public int userId { get; set; }
        public DateTime timeOfLogin { get; set; }
    }
}