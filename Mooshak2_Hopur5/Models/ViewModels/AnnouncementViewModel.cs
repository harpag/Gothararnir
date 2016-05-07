using Mooshak2_Hopur5.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2_Hopur5.Models.ViewModels
{
    public class AnnouncementViewModel
    {
        public int AnnouncementId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Announcement { get; set; }
        public DateTime? DateCreate {get; set;}
        public List<AnnouncementViewModel> AnnouncementList { get; set; }
    }
}