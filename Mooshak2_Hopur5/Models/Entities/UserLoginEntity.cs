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
        int userLoginId { get; set; }
        int userId { get; set; }
        DateTime timeOfLogin { get; set; }
    }
}