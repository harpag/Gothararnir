using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak_Hopur5.Models.Entities
{
    public class DiscussionEntity
    {
        public int discussionId { get; set; }
        public int assignmentId { get; set; }
        public int userId { get; set; }
        public string discussionText { get; set; }
    }
}