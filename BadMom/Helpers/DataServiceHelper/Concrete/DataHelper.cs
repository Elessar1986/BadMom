using AutoMapper;
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

        public UserData ChangePass( UserPasswordData passData)
        {
            var changeUser = data.ChangePass(passData.Login, passData.passwordHash, passData.salt);
            if (changeUser != null)
            {
                var mapper = new MapperConfiguration(c => c.CreateMap<User, UserData>()).CreateMapper();
                var getUser = mapper.Map<User, UserData>(changeUser);
                return getUser;
            }
            return null;
        }

        public bool CheckUserToChangePass(string login, string pass)
        {
            return data.CheckUserToChangePass(login, pass);
        }

        public UserData ConfirmAuth(string login, string pass)
        {
            var user = data.ConfirmAuth(login, pass);
            if (user != null)
            {
                var mapper = new MapperConfiguration(c => c.CreateMap<User, UserData>()).CreateMapper();
                var getUser = mapper.Map<User, UserData>(user);
                return getUser;
            }
            return null;
        }

        public UserData CreateUser(RegistrUserVM user, UserPasswordData passData)
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
            var UserCreated = data.FindUserByLogin(user.Login);
            if (UserCreated != null)
            {
                var mapper = new MapperConfiguration(c => c.CreateMap<User, UserData>()).CreateMapper();
                var getUser = mapper.Map<User, UserData>(UserCreated);
                return getUser;
            }
            return null;
        }

        public UserPasswordData GetPasswordData(string login)
        {
            var user = data.FindUserByLogin(login);
            if (user != null)
            {
                return new UserPasswordData() { passwordHash = user.PasswordHash, salt = user.Salt, Login = user.Login };
            }
            return null;
        }

        public string[] GetUserRoles(string login)
        {
            var user = data.FindUserByLogin(login);
            if(user != null)
            {
                return user.Roles.Split('/');
            }
            return null;
        }
    }
}