using Mooshak2_Hopur5.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mooshak2_Hopur5.Models.ViewModels;

namespace Mooshak2_Hopur5.Services
{
    public class UserService
    {
        private DataModel _db;

        public UserService()
        {
            _db = new DataModel();
        }

        public UserViewModel getUserById(int userId)
        {
            // Búa til 1 user object út frá 1 færslu í gagnagrunni
            User user = _db.User.SingleOrDefault(x => x.userId == userId);
            if (user == null)
            {
                //TODO: Kasta villu
            }

            // Búa til UserViewModel object
            UserViewModel viewModel = new UserViewModel();

            // Afrita frá user objecti (entity) yfir í viewModel objectið
            viewModel.name = user.name;
            viewModel.userName = user.userName;
            viewModel.email = user.email;
            viewModel.password = user.password;
            viewModel.ssn = user.ssn;
            viewModel.userTypeId = user.userTypeId;
            viewModel.salt = user.salt;
            viewModel.valid = user.valid;
            viewModel.userId = user.userId;

            // Skila viewModel objectinu til baka
            return viewModel;
        }
    }
}