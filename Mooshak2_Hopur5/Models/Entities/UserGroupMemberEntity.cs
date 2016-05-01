using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak_Hopur5.Models.Entities
{
    public class UserGroupMemberEntity
    {
        int userGroupMemberId { get; set; }
        int userId { get; set; }
        int userGroupId { get; set; }
    }
}