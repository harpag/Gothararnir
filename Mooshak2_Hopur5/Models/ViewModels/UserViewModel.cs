using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Ssn { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public int? Valid { get; set; }
        public List<UserViewModel> UserList { get; set; }
    }
}