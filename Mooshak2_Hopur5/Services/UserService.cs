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
            viewModel.Name = user.name;
            viewModel.UserName = user.userName;
            viewModel.Email = user.email;
            viewModel.Password = user.password;
            viewModel.Ssn = user.ssn;
            viewModel.UserTypeId = user.userTypeId;
            viewModel.Salt = user.salt;
            viewModel.Valid = user.valid;
            viewModel.UserId = user.userId;

            // Skila viewModel objectinu til baka
            return viewModel;
        }

        //Sækir alla notendur
        public UserViewModel getAllUsers()
        {
            //Sækir allt um User
            var users = _db.User.ToList();

            //Bý til lista af notendum(UserViewModel)
            List<UserViewModel> userList;
            userList = new List<UserViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og set inn í notenda listann
            foreach (var entity in users)
            {
                var result = new UserViewModel
                {
                    Name = entity.name,
                    UserName = entity.userName,
                    Ssn = entity.ssn,
                    Email = entity.email,
                    Password = entity.password
                };
                userList.Add(result);
            }

            //Bý til nýtt UserViewModel og set listann inn í það
            UserViewModel viewModel = new UserViewModel
            {
                UserList = userList
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }

        //Breyta ákveðnum notanda
        public UserViewModel editUser(UserViewModel userToChange)
        {
            // Sæki færsluna sem á að breyta í gagnagrunninn
            var query = (from user in _db.User
                         where user.userId == userToChange.UserId
                         select user).SingleOrDefault();

            // Set inn breyttu upplýsingarnar
            query.name = userToChange.Name;
            query.userName = userToChange.UserName;
            query.ssn = userToChange.Ssn;
            query.password = userToChange.Password;
            query.email = userToChange.Email;
            //Todo setja inn rest

            //Vista breytingar í gagnagrunn
            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // TODO
            }
            return userToChange;
        }

        //Búa til nýja notendur
        public Boolean addUser(UserViewModel userToAdd)
        {
            var newUser = new User();

            //setja propery-in
            newUser.name = userToAdd.Name;
            newUser.password = userToAdd.Password;
            newUser.ssn = userToAdd.Ssn;
            newUser.email = userToAdd.Email;
            newUser.userName = userToAdd.UserName;
            newUser.valid = userToAdd.Valid;

            try
            {
                //Vista ofan í gagnagrunn
                _db.User.Add(newUser);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}