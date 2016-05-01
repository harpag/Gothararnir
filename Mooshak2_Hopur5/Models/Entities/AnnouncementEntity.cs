using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mooshak2_Hopur5.Models.Entities
{
    public class AnnouncementEntity
    {
        [Key]
        int announcementId { get; set; }
        int userId { get; set; }
        string announcement { get; set; }
        DateTime dateCreate { get; set; }
    }
}