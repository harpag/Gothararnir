using Mooshak2_Hopur5.Models.Entities;
using System;
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

        public UserGroupViewModel GetUsers()
        {
            UserGroupViewModel result = new UserGroupViewModel();
            result.AllUsers = Utilities.IdentityManager.GetUsers();

            return result;
        }

        //Bætir við notendum í hópa
        public Boolean addUsersToGroup(string userId, int groupId)
        {
            var member = new UserGroupMember();

            member.userGroupId = groupId;
            member.userId = userId;

            _db.UserGroupMember.Add(member);
            _db.SaveChanges();
            return true;
        }
    }
}