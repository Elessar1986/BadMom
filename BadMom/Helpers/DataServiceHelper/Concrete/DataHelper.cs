using BadMom.BLL.DataTransferObjects;
using BadMom.BLL.Interfaces;
using BadMom.Helpers.DataServiceHelper.Abstract;
using BadMom.Models.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadMom.Helpers.DataServiceHelper.Concrete
{
    public class DataHelper : IDataHelper
    {
        IBadMomDataService data;

        public DataHelper(IBadMomDataService service)
        {
            data = service;
        }

        public void CreateUser(RegistrUserVM user, UserPasswordData passData)
        {
            var newUser = new User()
            {
                Email = user.Email,
                Login = user.Login,
                Photo = user.Photo,
                PasswordHash = passData.passwordHash,
                Salt = passData.salt,
                Roles = "user",
                Created = DateTime.Now
            };
            data.AddUser(newUser);
        }

        public UserPasswordData GetPasswordData(string login)
        {
            var user = data.FindUserByLogin(login);
            if(user != null)
            {
                return new UserPasswordData() { passwordHash = user.PasswordHash, salt = user.Salt };
            }
            return null;
        }
    }
}