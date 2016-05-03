using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.ViewModels
{
    public class UserViewModel
    {
        public int userId { get; set; }

        public int userTypeId { get; set; }

        public string name { get; set; }

        public string userName { get; set; }

        public string ssn { get; set; }

        public string password { get; set; }

        public string salt { get; set; }

        public string email { get; set; }

        public int? valid { get; set; }
    }
}