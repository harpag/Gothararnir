using Mooshak2_Hopur5.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.ViewModels
{
    public class HomeViewModel
    {
        public UserViewModel LoggedInUser { get; set; }
        public List<UserLogin> NewestUserLogin { get; set; }
        public List<AnnouncementViewModel> AnnouncementList { get; set; }
    }
}