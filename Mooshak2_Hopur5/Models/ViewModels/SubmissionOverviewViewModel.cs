using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2_Hopur5.Models.ViewModels
{
    public class SubmissionOverviewViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Success { get; set; }
        public int SubmissionCount { get; set; }
        public DateTime LastDate { get; set; }
        public int UserGroupId { get; set; }
        public string UserGroupName { get; set; }
        public List<SubmissionOverviewViewModel> UserList { get; set; }
    }
}