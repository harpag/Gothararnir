using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak_Hopur5.Models.Entities
{
    public class AnnouncementEntity
    {
        int announcementId { get; set; }
        int userId { get; set; }
        string announcement { get; set; }
        DateTime dateCreate { get; set; }
    }
}