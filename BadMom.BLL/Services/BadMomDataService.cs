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

        public void AddMessage(DataTransferObjects.Message mess, string userName)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<DataTransferObjects.Message, DAL.Model.Messages>()).CreateMapper();
            var message = mapper.Map<DataTransferObjects.Message, DAL.Model.Messages>(mess);
            message.Created = DateTime.Now;
            message.UserId = GetUserId(userName);
            data.Messages.Create(message);
            data.Save();
        }

        public void AddAdvert(DataTransferObjects.Advert advert, string userName)
        {
            try
            {
                var mapper = new MapperConfiguration(c => c.CreateMap<DataTransferObjects.Advert, DAL.Model.Advert>()).CreateMapper();
                var mappedRes = mapper.Map<DataTransferObjects.Advert, DAL.Model.Advert>(advert);
                mappedRes.Created = DateTime.Now;
                mappedRes.UserId = GetUserId(userName);
                data.Advert.Create(mappedRes);
                data.Save();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public User GetUserById(long id)
        {
            var user = data.Users.Get(id);
            if (user != null)
            {
                var mapper = new MapperConfiguration(c => c.CreateMap<Users, User>()).CreateMapper();
                var getUser = mapper.Map<Users, User>((Users)user);
                return getUser;
            }
            return null;
        }


        public List<DataTransferObjects.Advert> GetAdvertsByUserId(long id)
        {
            var adverts = data.Advert.Find(c => c.UserId == id).ToList();
            var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.Advert, DataTransferObjects.Advert>().ForMember(
                d => d.User, a => a.MapFrom(s => s.Users))).CreateMapper();

            var mappedRes = mapper.Map<List<DAL.Model.Advert>, List<DataTransferObjects.Advert>>(adverts);
            return mappedRes;
        }



        public User FindUserByLogin(string login)
        {
            var user = data.Users.Find(u => u.Login == login || u.Email == login).FirstOrDefault();

            if (user != null)
            {
                var mapper = new MapperConfiguration(c => c.CreateMap<Users, User>().ForMember(
                d => d.FavoriteAdvert, a => a.MapFrom(s => s.FavoriteAdvert.Select(x => x.AdvertId)))).CreateMapper();
                var getUser = mapper.Map<Users, User>((Users)user);
                return getUser;
            }
            return null;
        }

        public User ConfirmAuth(string login, string pass)
        {
            var user = data.Users.Find(u => u.Login == login).FirstOrDefault();
            if (user != null && !user.Confirmed && user.PasswordHash.Equals(pass))
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
            var user = data.Users.Find(u => u.Login == login && u.PasswordHash == pass).FirstOrDefault();
            if (user != null)
            {
                if (!user.Confirmed) throw new ValidationException("Пользователь не активировал свой аккаунт!", "");
                return true;
            }
            return false;
        }

        public User ChangePass(string login, string pass, string salt)
        {
            var user = data.Users.Find(u => u.Login == login).FirstOrDefault();
            if (user != null)
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
            var mapper = new MapperConfiguration(c => c.CreateMap<DataTransferObjects.logEvents, DAL.Model.logEvents>().ForMember(
            x => x.Type, a => a.MapFrom(d => d.Type))).CreateMapper();
            var newEvent = mapper.Map<DataTransferObjects.logEvents, DAL.Model.logEvents>(logEvents);
            //if (newEvent.Description.Length > 490) newEvent.Description = newEvent.Description.Substring(0, 490);

            data.logEvents.Create(newEvent);
            data.Save();
        }

        public List<Theme> GetAllThemes()
        {
            var themes = data.Themes.GetAll();
            var mapper = new MapperConfiguration(c => c.CreateMap<Themes, Theme>()).CreateMapper();
            var mapped_themes = mapper.Map<IEnumerable<Themes>, IEnumerable<Theme>>(themes);
            return mapped_themes.ToList();
        }

        public List<DataTransferObjects.Category> GetAllCategories()
        {
            var cat = data.Category.GetAll();
            var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.Category, DataTransferObjects.Category>()).CreateMapper();
            var mappedRes = mapper.Map<IEnumerable<DAL.Model.Category>, IEnumerable<DataTransferObjects.Category>>(cat);
            return mappedRes.ToList();
        }

        public int GetThemePostsCount(int themeId)
        {
            var mess = data.Messages.Find(x => x.Theme == themeId);
            return mess.Count();
        }

        public List<Message> GetPostsByTheme(int themeId)
        {
            IEnumerable<Messages> mess;
            if (themeId > 0) mess = data.Messages.Find(x => x.Theme == themeId);
            else mess = data.Messages.GetAll();
            var mapper = new MapperConfiguration(c => c.CreateMap<Messages, Message>().ForMember(
                d => d.User, a => a.MapFrom(s => s.Users)).ForMember(
                d => d.UsersLikes, a => a.MapFrom(s => s.Users1)).ForMember(
                s => s.ThemeObj, a => a.MapFrom(s => s.Themes)).ForMember(
                s => s.Comments, a => a.MapFrom(s => s.Comment))).CreateMapper();
            var mapped_mess = mapper.Map<IEnumerable<Messages>, IEnumerable<Message>>(mess);
            return mapped_mess.OrderByDescending(x => x.Created).ToList();
        }

        public List<Message> GetFeaturedPosts()
        {
            var mess = data.Messages.Find(x => x.Users1.Count > 0).OrderByDescending(x => x.Users1.Count).Take(3);
            var mapper = new MapperConfiguration(c => c.CreateMap<Messages, Message>().ForMember(
                d => d.User, a => a.MapFrom(s => s.Users)).ForMember(
                d => d.UsersLikes, a => a.MapFrom(s => s.Users1)).ForMember(
                s => s.ThemeObj, a => a.MapFrom(s => s.Themes)).ForMember(
                s => s.Comments, a => a.MapFrom(s => s.Comment))).CreateMapper();
            var mapped_mess = mapper.Map<IEnumerable<Messages>, IEnumerable<Message>>(mess);
            return mapped_mess.ToList();
        }

        public List<Message> GetLastPosts()
        {
            var mess = data.Messages.GetAll().OrderByDescending(x => x.Created).Take(3);
            var mapper = new MapperConfiguration(c => c.CreateMap<Messages, Message>().ForMember(
                d => d.User, a => a.MapFrom(s => s.Users)).ForMember(
                d => d.UsersLikes, a => a.MapFrom(s => s.Users1)).ForMember(
                s => s.ThemeObj, a => a.MapFrom(s => s.Themes)).ForMember(
                s => s.Comments, a => a.MapFrom(s => s.Comment))).CreateMapper();
            var mapped_mess = mapper.Map<IEnumerable<Messages>, IEnumerable<Message>>(mess);
            return mapped_mess.ToList();
        }

        public List<Message> GetPostsByUser(long userId)
        {
            var mess = data.Messages.Find(c => c.UserId == userId);
            var mapper = new MapperConfiguration(c => c.CreateMap<Messages, Message>().ForMember(
                d => d.User, a => a.MapFrom(s => s.Users)).ForMember(
                d => d.UsersLikes, a => a.MapFrom(s => s.Users1)).ForMember(
                s => s.ThemeObj, a => a.MapFrom(s => s.Themes)).ForMember(
                s => s.Comments, a => a.MapFrom(s => s.Comment))).CreateMapper();
            var mapped_mess = mapper.Map<IEnumerable<Messages>, IEnumerable<Message>>(mess);
            return mapped_mess.ToList();
        }

        public void DeletePost(long id)
        {
            data.Messages.Delete(id);
            data.Save();
        }

        public Message EditPost(Message mess)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<Message, Messages>()).CreateMapper();
            var mappedRes = mapper.Map<Message, Messages>(mess);
            data.Messages.Update(mappedRes);
            data.Save();
            var message = data.Messages.Find(c => c.Id == mappedRes.Id).First();
            mapper = new MapperConfiguration(c => c.CreateMap<Messages, Message>().ForMember(
                d => d.User, a => a.MapFrom(s => s.Users)).ForMember(
                d => d.UsersLikes, a => a.MapFrom(s => s.Users1)).ForMember(
                s => s.ThemeObj, a => a.MapFrom(s => s.Themes)).ForMember(
                s => s.Comments, a => a.MapFrom(s => s.Comment))).CreateMapper();
            var mapped_mess = mapper.Map<Messages, Message>(message);
            return mapped_mess;
        }

        public Message GetPostById(int id)
        {
            var mess = data.Messages.Get(id);
            var mapper = new MapperConfiguration(c => c.CreateMap<Messages, Message>().ForMember(
                d => d.User, a => a.MapFrom(s => s.Users)).ForMember(
                d => d.UsersLikes, a => a.MapFrom(s => s.Users1)).ForMember(
                s => s.ThemeObj, a => a.MapFrom(s => s.Themes)).ForMember(
                s => s.Comments, a => a.MapFrom(s => s.Comment))).CreateMapper();
            var mapped_mess = mapper.Map<Messages, Message>(mess);
            return mapped_mess;
        }

        public List<BLL.DataTransferObjects.Comment> AddComment(string userName, string commentBody, int messageId, string targetUserName = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(commentBody))
                {
                    long userId = data.Users.Find(c => c.Login == userName).First().Id;
                    data.Comment.Create(new DAL.Model.Comment()
                    {
                        Created = DateTime.Now,
                        Body = commentBody,
                        MessageId = messageId,
                        UserId = userId,
                        TargetUserName = targetUserName == null ? "test" : targetUserName
                    });
                    data.Save();
                }
                return GetComments(messageId);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<DataTransferObjects.Comment> DeleteComment(int commentId, int messageId)
        {
            try
            {
                data.Comment.Delete(commentId);
                data.Save();
                return GetComments(messageId);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<DataTransferObjects.Comment> GetComments(int messageId)
        {
            try
            {
                var comm = data.Messages.Get(messageId);
                var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.Comment, DataTransferObjects.Comment>()).CreateMapper();
                var mapped_comments = mapper.Map<IEnumerable<DAL.Model.Comment>, IEnumerable<DataTransferObjects.Comment>>(comm.Comment).OrderBy(x => x.Created).ToList();
                return mapped_comments;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public int LikeMessage(string userName, int messageId)
        {
            try
            {
                var user = data.Users.Find(c => c.Login == userName).First();
                var mess = data.Messages.Get(messageId);
                if (!mess.Users1.Contains(user))
                {
                    mess.Users1.Add(user);
                    data.Messages.Update(mess);
                    data.Save();
                }
                return mess.Users1.Count;
            }
            catch (Exception)
            {

                return -1;
            }
        }

        public int DislikeMessage(string userName, int messageId)
        {
            try
            {
                var user = data.Users.Find(c => c.Login == userName).First();
                var mess = data.Messages.Get(messageId);
                if (mess.Users1.Contains(user))
                {
                    mess.Users1.Remove(user);
                    data.Messages.Update(mess);
                    data.Save();
                }
                return mess.Users1.Count;
            }
            catch (Exception)
            {

                return -1;
            }
        }

        public List<DataTransferObjects.IncomeReason> GetIncomeReasonList()
        {
            try
            {
                var incomeReason = data.IncomeReason.GetAll();
                var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.IncomeReason, DataTransferObjects.IncomeReason>()).CreateMapper();
                var mapped_income = mapper.Map<IEnumerable<DAL.Model.IncomeReason>, IEnumerable<DataTransferObjects.IncomeReason>>(incomeReason).ToList();
                return mapped_income;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<DataTransferObjects.ConsumptionReason> GetConsumptionReasonList()
        {
            try
            {
                var consumptionReason = data.ConsumptionReason.GetAll();
                var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.ConsumptionReason, DataTransferObjects.ConsumptionReason>()).CreateMapper();
                var mapped_consumption = mapper.Map<IEnumerable<DAL.Model.ConsumptionReason>, IEnumerable<DataTransferObjects.ConsumptionReason>>(consumptionReason).ToList();
                return mapped_consumption;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<DataTransferObjects.OperationType> GetOperationTypeList()
        {
            try
            {
                var OperationType = data.OperationType.GetAll();
                var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.OperationType, DataTransferObjects.OperationType>()).CreateMapper();
                var mapped_OperationType = mapper.Map<IEnumerable<DAL.Model.OperationType>, IEnumerable<DataTransferObjects.OperationType>>(OperationType).ToList();
                return mapped_OperationType;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<DataTransferObjects.ResourceType> GetResourceTypeList()
        {
            try
            {
                var ResourceType = data.ResourceType.GetAll();
                var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.ResourceType, DataTransferObjects.ResourceType>()).CreateMapper();
                var mapped_ResourceType = mapper.Map<IEnumerable<DAL.Model.ResourceType>, IEnumerable<DataTransferObjects.ResourceType>>(ResourceType).ToList();
                return mapped_ResourceType;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool AddSource(DataTransferObjects.Source source, string userName)
        {
            try
            {
                source.UserId = GetUserId(userName) > 0 ? GetUserId(userName) : throw new Exception();
                source.Created = DateTime.Now;
                var mapper = new MapperConfiguration(c => c.CreateMap<DataTransferObjects.Source, DAL.Model.Source>()).CreateMapper();
                var mapped_Source = mapper.Map<DataTransferObjects.Source, DAL.Model.Source>(source);
                data.Source.Create(mapped_Source);
                data.Save();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        private long GetUserId(string userName)
        {
            try
            {
                long userId = data.Users.Find(c => c.Login == userName).First().Id;
                return userId;
            }
            catch (Exception ex)
            {

                return -1; ;
            }
        }

        public List<DataTransferObjects.Source> GetSourceByUser(string userName)
        {
            try
            {
                var source = data.Source.Find(x => x.UserId == GetUserId(userName));
                var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.Source, DataTransferObjects.Source>()).CreateMapper();
                var mapped_Source = mapper.Map<IEnumerable<DAL.Model.Source>, IEnumerable<DataTransferObjects.Source>>(source);
                return mapped_Source.OrderByDescending(x => x.Created).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bool DeleteSource(long sourceId)
        {
            try
            {
                data.Income.Find(x => x.Source.Id == sourceId).Select(x => x.Id).ToList().ForEach(x => data.Income.Delete(x));
                data.Consumption.Find(x => x.Source1.Id == sourceId).Select(x => x.Id).ToList().ForEach(x => data.Consumption.Delete(x));
                data.Source.Delete(sourceId);
                data.Save();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool AddIncome(DataTransferObjects.Income income, string userName)
        {
            try
            {
                income.UserId = GetUserId(userName) > 0 ? GetUserId(userName) : throw new Exception();
                income.Created = DateTime.Now;
                var mapper = new MapperConfiguration(c => c.CreateMap<DataTransferObjects.Income, DAL.Model.Income>()).CreateMapper();
                var mapped_income = mapper.Map<DataTransferObjects.Income, DAL.Model.Income>(income);
                data.Income.Create(mapped_income);
                UpdateSource(income.Destination, income.Sum);
                data.Save();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public List<DataTransferObjects.Income> GetIncomeByUser(string userName, DateTime from, DateTime to, long sourceId = 0)
        {
            try
            {
                var income = data.Income.Find(x => x.Users.Login == userName).Where(x => ((x.Date.Date >= from.Date) && (x.Date.Date <= to.Date)));
                if (sourceId != 0) income = income.Where(x => x.Destination == sourceId);
                var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.Income, DataTransferObjects.Income>()).CreateMapper();
                var mapped_Income = mapper.Map<IEnumerable<DAL.Model.Income>, IEnumerable<DataTransferObjects.Income>>(income);
                return mapped_Income.OrderByDescending(x => x.Date).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bool DeleteIncome(long incomeId)
        {
            try
            {
                var income = data.Income.Get(incomeId);
                UpdateSource(income.Destination, -income.Sum);
                data.Income.Delete(incomeId);
                data.Save();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool AddConsumption(DataTransferObjects.Consumption consumption, string userName)
        {
            try
            {
                consumption.UserId = GetUserId(userName) > 0 ? GetUserId(userName) : throw new Exception();
                consumption.Created = DateTime.Now;
                var mapper = new MapperConfiguration(c => c.CreateMap<DataTransferObjects.Consumption, DAL.Model.Consumption>()).CreateMapper();
                var mapped_consumption = mapper.Map<DataTransferObjects.Consumption, DAL.Model.Consumption>(consumption);
                data.Consumption.Create(mapped_consumption);
                UpdateSource(consumption.Source, -consumption.Sum);
                data.Save();
                return true;
            }
            catch (Exception ex)
            {
                var e = ex.InnerException;
                return false;
            }
        }

        public List<DataTransferObjects.Consumption> GetConsumptionByUser(string userName, DateTime from, DateTime to, long sourceId = 0)
        {

            var consumption = data.Consumption.Find(x => x.Users.Login == userName && (x.Date.Date >= from.Date) && (x.Date.Date <= to.Date));
            if (sourceId != 0) consumption = consumption.Where(x => x.Source == sourceId);
            var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.Consumption, DataTransferObjects.Consumption>()).CreateMapper();
            var mapped_consumption = mapper.Map<IEnumerable<DAL.Model.Consumption>, IEnumerable<DataTransferObjects.Consumption>>(consumption);
            return mapped_consumption.OrderByDescending(x => x.Date).ToList();
        }

        public bool DeleteConsumption(long consumptionId)
        {
            try
            {
                var cons = data.Consumption.Get(consumptionId);
                UpdateSource(cons.Source, cons.Sum);
                data.Consumption.Delete(consumptionId);
                data.Save();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool UpdateSource(long sourceId, decimal sumChange)
        {
            try
            {
                var source = data.Source.Get(sourceId);
                source.Sum = source.Sum + sumChange;
                data.Source.Update(source);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool SendMessage(DataTransferObjects.PersonalMessage personalMessage, string userFrom)
        {
            try
            {
                personalMessage.UserFrom = GetUserId(userFrom);
                personalMessage.Created = DateTime.Now;
                var mapper = new MapperConfiguration(c => c.CreateMap<DataTransferObjects.PersonalMessage, DAL.Model.PersonalMessage>()).CreateMapper();
                var mapped_personalMessage = mapper.Map<DataTransferObjects.PersonalMessage, DAL.Model.PersonalMessage>(personalMessage);

                data.PersonalMessage.Create(mapped_personalMessage);
                data.Save();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        //public Dictionary<long, User> GetConectedUsers(string userName)
        //{
        //    try
        //    {
        //        var res = new Dictionary<long, Users>();
        //        var user = data.Users.Find(x => x.Login == userName).First();
        //        var mess = user.PersonalMessage.ToList();
        //        mess.AddRange(user.PersonalMessage1);
        //        foreach (var item in mess)
        //        {
        //            if (item.UserFrom != user.Id && !res.ContainsKey(item.UserFrom)) res.Add(item.UserFrom, data.Users.Get(item.UserFrom));
        //            if (item.UserTo != user.Id && !res.ContainsKey(item.UserTo)) res.Add(item.UserTo, data.Users.Get(item.UserTo));
        //        }
        //        var mapper = new MapperConfiguration(c => c.CreateMap<Users, User>()).CreateMapper();
        //        var mapped_res = mapper.Map<Dictionary<long, Users>, Dictionary<long, User>>(res);
        //        return mapped_res;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public List<PrivateMessageByUser> GetMessagesByUsers(string userName)
        {
            try
            {
                var res = new List<PrivateMessageByUser>();
                var user = FindUserByLogin(userName);
                var mess = user.PersonalMessage.ToList();
                mess.AddRange(user.PersonalMessage1);
                foreach (var item in mess)
                {
                    if (item.UserFrom != user.Id && !res.Exists(x => x.Id == item.UserFrom))
                    {
                        var userFrom = GetUserById(item.UserFrom);
                        res.Add(new PrivateMessageByUser
                        {
                            Id = item.UserFrom,
                            Login = userFrom.Login,
                            Avatar = userFrom.Photo,
                            Messages = GetPersonalMessageByUserFromId(item.UserFrom, user.PersonalMessage, user.PersonalMessage1)
                        });
                    }
                    if (item.UserTo != user.Id && !res.Exists(x => x.Id == item.UserTo))
                    {
                        var userTo = GetUserById(item.UserTo);
                        res.Add(new PrivateMessageByUser
                        {
                            Id = item.UserTo,
                            Login = userTo.Login,
                            Avatar = userTo.Photo,
                            Messages = GetPersonalMessageByUserFromId(item.UserTo, user.PersonalMessage, user.PersonalMessage1)
                        });
                    }
                }

                return res.OrderByDescending(x => x.Messages.First().Created).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public List<BadMom.BLL.DataTransferObjects.PersonalMessage> GetPersonalMessageByUserFromId(long id, List<BadMom.BLL.DataTransferObjects.PersonalMessage> personalMessage, List<BadMom.BLL.DataTransferObjects.PersonalMessage> personalMessage1)
        {
            if (personalMessage != null && personalMessage1 != null)
            {
                var outMessages = personalMessage.Where(x => x.UserTo == id).ToList();
                var inMessages = personalMessage1.Where(x => x.UserFrom == id).ToList();
                outMessages.AddRange(inMessages);
                outMessages = outMessages.OrderByDescending(x => x.Created).ToList();
                return outMessages;
            }
            else
            {
                return null;
            }
        }

        public List<DataTransferObjects.PersonalMessage> SetMessageStatus(List<DataTransferObjects.PersonalMessage> messages, int status)
        {
            try
            {
                foreach (var item in messages)
                {
                    data.PersonalMessage.Get(item.Id).Status = status;
                    item.Status = status;
                }
                data.Save();
                return messages;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<DataTransferObjects.PersonalMessage> SetMessageStatusReaded(List<DataTransferObjects.PersonalMessage> messages, string login)
        {
            try
            {
                var id = GetUserId(login);
                foreach (var item in messages)
                {
                    if (item.UserTo == id)
                    {
                        data.PersonalMessage.Get(item.Id).Status = 1;
                        item.Status = 1;
                    }
                }
                data.Save();
                return messages;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteAdvert(long id)
        {
            data.Advert.Delete(id);
            data.Save();
        }

        public DataTransferObjects.Advert GetAdvert(long id)
        {
            var advert = data.Advert.Find(c => c.Id == id).FirstOrDefault();
            var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.Advert, DataTransferObjects.Advert>().ForMember(
                d => d.User, a => a.MapFrom(s => s.Users))).CreateMapper();

            var mappedRes = mapper.Map<DAL.Model.Advert, DataTransferObjects.Advert>(advert);
            return mappedRes;
        }

        public List<DataTransferObjects.Advert> GetAdvertsByCategoryId(int id)
        {
            var adverts = data.Advert.GetAll().ToList();
            if (id != 0) adverts = adverts.Where(c => c.Category == id).ToList();
            var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.Advert, DataTransferObjects.Advert>().ForMember(
                d => d.User, a => a.MapFrom(s => s.Users))).CreateMapper();

            var mappedRes = mapper.Map<List<DAL.Model.Advert>, List<DataTransferObjects.Advert>>(adverts);
            return mappedRes;
        }

        public void AddToFavorite(long advertId, long userId)
        {
            var favor = new DAL.Model.FavoriteAdvert
            {
                Created = DateTime.Now,
                UserId = userId,
                AdvertId = advertId
            };
            data.FavoriteAdvert.Create(favor);
            data.Save();
        }

        public void DeleteFromFavorite(long advertId, long userId)
        {
            var f = data.FavoriteAdvert.Find(x => x.AdvertId == advertId && x.UserId == userId).First();
            data.FavoriteAdvert.Delete(f.Id);
            data.Save();
        }

        public List<DataTransferObjects.Advert> GetFavoriteAdvertsByUserId(List<long> favorites)
        {
            var adverts = data.Advert.Find(c => favorites.Contains(c.Id)).ToList();
            var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.Advert, DataTransferObjects.Advert>().ForMember(
                d => d.User, a => a.MapFrom(s => s.Users))).CreateMapper();

            var mappedRes = mapper.Map<List<DAL.Model.Advert>, List<DataTransferObjects.Advert>>(adverts);
            return mappedRes;
        }

        public List<DataTransferObjects.EventType> GetEventTypes()
        {
            var eventTypes = data.EventType.GetAll().ToList();
            var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.EventType, DataTransferObjects.EventType>()).CreateMapper();

            var mappedRes = mapper.Map<List<DAL.Model.EventType>, List<DataTransferObjects.EventType>>(eventTypes);
            return mappedRes;
        }

        public void AddEvent(DataTransferObjects.Events eventItem , string userName)
        {
            long userId = GetUserId(userName);
            var mapper = new MapperConfiguration(c => c.CreateMap<DataTransferObjects.Events, DAL.Model.Events>()).CreateMapper();
            var mappedRes = mapper.Map<DataTransferObjects.Events, DAL.Model.Events>(eventItem);
            mappedRes.Created = DateTime.Now;
            mappedRes.LastUpdate = mappedRes.Created;
            mappedRes.UserId = userId;
            data.Events.Create(mappedRes);
            data.Save();
        }

        public void DeleteEvent(long eventId)
        {
            data.Events.Delete(eventId);
            data.Save();
        }

        public List<DataTransferObjects.Events> GetEventsByUser(string userName)
        {
            long userId = GetUserId(userName);
            var events = data.Events.Find(x => x.UserId == userId).ToList();
            var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.Events, DataTransferObjects.Events>()).CreateMapper();

            var mappedRes = mapper.Map<List<DAL.Model.Events>, List<DataTransferObjects.Events>>(events);
            return mappedRes;
        }

        public List<AdminUserData> GetAllUsers()
        {
            var users = data.Users.GetAll().ToList();
            var mapper = new MapperConfiguration(c => c.CreateMap<DAL.Model.Users, DataTransferObjects.AdminUserData>()).CreateMapper();

            var mappedRes = mapper.Map<List<DAL.Model.Users>, List<DataTransferObjects.AdminUserData>>(users);
            return mappedRes;
        }
    }
}
