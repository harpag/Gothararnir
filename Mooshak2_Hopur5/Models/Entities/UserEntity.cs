using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.Entities
{
    public class UserEntity
    {
        int userId { get; set; }
        int userTypeId { get; set; }
        string name { get; set; }
        string userName { get; set; }
        string ssn { get; set; }
        string password { get; set; }
        string salt { get; set; }
        string email { get; set; }
        int valid { get; set; }
    }
}