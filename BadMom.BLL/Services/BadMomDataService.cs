using BadMom.BLL.DataTransferObjects;
using BadMom.BLL.Interfaces;
using BadMom.DAL.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadMom.DAL.Model;
using BadMom.DAL.Repositories;
using AutoMapper.Configuration;
using BadMom.DAL.Model.Abstract;
using BadMom.BLL.Infrastrutcure;

namespace BadMom.BLL.Services
{
    public class BadMomDataService : IBadMomDataService
    {

        IUnitOfWork data { get; set; }

        //public BadMomDataService(IUnitOfWork unit)
        //{
        //    data = unit;
        //}

        public BadMomDataService(string connectionString)
        {
            data = new EFUnitOfWork(connectionString);
        }

        public void AddUser(User user)
        {
            if (data.Users.Find(x => x.Email == user.Email).Count() != 0) throw new ValidationException("Пользователь с таким e-mail уже зарегистрирован!", "Email");
            if (data.Users.Find(x => x.Login == user.Login).Count() != 0) throw new ValidationException("Пользователь с таким логином уже зарегистрирован!", "Login");
            var mapper = new MapperConfiguration(c => c.CreateMap<User, Users>()).CreateMapper();
            var newUser = mapper.Map<User, Users>(user);
            data.Users.Create(newUser);
            data.Save();
        }

        public void Dispose()
        {
            data.Dispose();
        }

        public void AddMessage(DataTransferObjects.Message mess)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<DataTransferObjects.Message, DAL.Model.Messages>()).CreateMapper();
            var message = mapper.Map<DataTransferObjects.Message, DAL.Model.Messages>(mess);
            data.Messages.Create(message);
            data.Save();
        }

        public User GetUserById(int id)
        {
            var user = data.Users.Get(id);
            if(user != null)
            {
                var mapper = new MapperConfiguration(c => c.CreateMap<Users, User>()).CreateMapper();
                var getUser = mapper.Map<Users, User>((Users)user);
                return getUser;
            }
            return null;
        }

        public Message GetMessageByUserId(long id)
        {
            var mes = data.Messages.Find(c => c.UserId == id).FirstOrDefault();
            var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.Messages, DataTransferObjects.Message>().ForMember(d => d.User, a => a.MapFrom(s =>s.Users))).CreateMapper();
            
            var message = mapper.Map<DAL.Model.Messages, DataTransferObjects.Message>(mes);
            return message;
        }

        public User FindUserByLogin(string login)
        {
            var user = data.Users.Find(u => u.Login == login || u.Email == login).FirstOrDefault();

           if(user != null)
            {
                var mapper = new MapperConfiguration(c => c.CreateMap<Users, User>()).CreateMapper();
                var getUser = mapper.Map<Users, User>((Users)user);
                return getUser;
            }
            return null;
        }

        public User ConfirmAuth(string login, string pass)
        {
            var user = data.Users.Find(u => u.Login == login || u.Email == login).FirstOrDefault();
            if(user != null && !user.Confirmed && user.PasswordHash.Equals(pass))
            {
                user.Confirmed = true;
                data.Users.Update(user);
                data.Save();
                var mapper = new MapperConfiguration(c => c.CreateMap<Users, User>()).CreateMapper();
                var getUser = mapper.Map<Users, User>(user);
                return getUser;
            }
            return null;
        }

        public bool CheckUserToChangePass(string login, string pass)
        {
            var user = data.Users.Find(u => u.Login == login || u.PasswordHash == pass).FirstOrDefault();
            if(user != null)
            {
                if (!user.Confirmed) throw new ValidationException("Пользователь не активировал свой аккаунт!", "");
                return true;
            }
            return false;
        }

        public User ChangePass(string login, string pass, string salt)
        {
            var user = data.Users.Find(u => u.Login == login).FirstOrDefault();
            if(user != null)
            {
                user.PasswordHash = pass;
                user.Salt = salt;
                data.Users.Update(user);
                data.Save();
                var mapper = new MapperConfiguration(c => c.CreateMap<Users, User>()).CreateMapper();
                var getUser = mapper.Map<Users, User>(user);
                return getUser;
            }
            return null;
        }

        public void SendLoggerMessage(DataTransferObjects.logEvents logEvents)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<DataTransferObjects.logEvents, DAL.Model.logEvents>()).CreateMapper();
            var newEvent = mapper.Map<DataTransferObjects.logEvents, DAL.Model.logEvents>(logEvents);
            data.logEvents.Create(newEvent);
            data.Save();
        }
    }
}
