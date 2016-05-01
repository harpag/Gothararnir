using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Mooshak2_Hopur5.Models.Entities
{
    public class UserTypeEntity
    {
        [Key]
        int userTypeId { get; set; }
        string userTypeName { get; set; }
    }
}