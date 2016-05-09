using Mooshak2_Hopur5.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mooshak2_Hopur5.Models.ViewModels;

namespace Mooshak2_Hopur5.Services
{
    public class UserGroupService
    {
        private DataModel _db;

        public UserGroupService()
        {
            _db = new DataModel();
        }

        public UserGroupViewModel GetViewWithUsers()
        {
            UserGroupViewModel res = new UserGroupViewModel();

            res.AllUsers = Utilities.IdentityManager.GetUsers();

            return res;
        }

        //Bæta við notendum í hópa
        public Boolean addUsersToGroup(string userId, int groupId)
        {
            var member = new UserGroupMember();

            //Setja property-in
            member.userGroupId = groupId;
            member.userId = userId;

            _db.UserGroupMember.Add(member);
            _db.SaveChanges();
            return true;
        }
    }
}