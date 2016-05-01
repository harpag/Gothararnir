using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.Entities
{
    public class UserLoginEntity
    {
        int userLoginId { get; set; }
        int userId { get; set; }
        DateTime timeOfLogin { get; set; }
    }
}