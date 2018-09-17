using AutoMapper;
using BadMom.BLL.DataTransferObjects;
using BadMom.BLL.Interfaces;
using BadMom.Helpers.DataServiceHelper.Abstract;
using BadMom.Models.Advert;
using BadMom.Models.Blog;
using BadMom.Models.Planer;
using BadMom.Models.Registration;
using BadMom.Models.User;
using BadMom.Models.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadMom.Helpers.DataServiceHelper.Concrete
{
    public class DataHelper : IDataHelper
    {
        IBadMomDataService data;
        //LoggerHelper logger;

        public DataHelper(IBadMomDataService service)
        {
            data = service;
            //logger = new LoggerHelper(service);
        }

        public List<CommentVM> AddComment(string userName, string commentBody, int messageId, string targetUserName)
        {
            try
            {
                var comments = data.AddComment(userName, commentBody, messageId, targetUserName);
                var mapper = new MapperConfiguration(c => c.CreateMap<Comment, CommentVM>()).CreateMapper();
                var mapped_comments = mapper.Map<IEnumerable<Comment>, IEnumerable<CommentVM>>(comments).ToList();
                return mapped_comments;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public UserData ChangePass(UserPasswordData passData)
        {
            var changeUser = data.ChangePass(passData.Login, passData.passwordHash, passData.salt);
            if (changeUser != null)
            {
                var mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.User, UserData>()).CreateMapper();
                var getUser = mapper.Map<BLL.DataTransferObjects.User, UserData>(changeUser);
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
                var mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.User, UserData>()).CreateMapper();
                var getUser = mapper.Map<BLL.DataTransferObjects.User, UserData>(user);
                return getUser;
            }
            return null;
        }

        public UserData CreateUser(RegistrUserVM user, UserPasswordData passData)
        {
            var newUser = new BLL.DataTransferObjects.User()
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
                var mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.User, UserData>()).CreateMapper();
                var getUser = mapper.Map<BLL.DataTransferObjects.User, UserData>(UserCreated);
                return getUser;
            }
            return null;
        }

        public List<CommentVM> DeleteComment(int commentId, int messageId)
        {
            var comments = data.DeleteComment(commentId, messageId);
            var mapper = new MapperConfiguration(c => c.CreateMap<Comment, CommentVM>()).CreateMapper();
            var mapped_comments = mapper.Map<IEnumerable<Comment>, IEnumerable<CommentVM>>(comments).ToList();
            return mapped_comments;
        }



        public List<CommentVM> GetComments(int messageId)
        {
            var comments = data.GetComments(messageId);
            var mapper = new MapperConfiguration(c => c.CreateMap<Comment, CommentVM>().ForMember(
                d => d.Users, a => a.MapFrom(s => s.Users))).CreateMapper();
            var mapped_comments = mapper.Map<IEnumerable<Comment>, IEnumerable<CommentVM>>(comments).ToList();
            return mapped_comments;
        }

        public List<FeaturedMessage> GetFeaturedPosts()
        {
            var mess = data.GetFeaturedPosts();
            var mapper = new MapperConfiguration(c => c.CreateMap<Message, FeaturedMessage>().ForMember(
                d => d.UsersLikes, a => a.MapFrom(s => s.UsersLikes)).ForMember(
                s => s.Comments, a => a.MapFrom(s => s.Comments))).CreateMapper();  
            var mapped_mess = mapper.Map<IEnumerable<Message>, IEnumerable<FeaturedMessage>>(mess);
            return mapped_mess.ToList();
        }

        public List<LastMessage> GetLastPost()
        {
            var mess = data.GetLastPosts();
            var mapper = new MapperConfiguration(c => c.CreateMap<Message, LastMessage>().ForMember(
                d => d.UsersLikes, a => a.MapFrom(s => s.UsersLikes)).ForMember(
                s => s.Comments, a => a.MapFrom(s => s.Comments))).CreateMapper();  
            var mapped_mess = mapper.Map<IEnumerable<Message>, IEnumerable<LastMessage>>(mess);
            return mapped_mess.ToList();
        }

        public PostVM GetPostById(int id)
        {
            var mess = data.GetPostById(id);
            var mapper = new MapperConfiguration(c => c.CreateMap<Message, PostVM>().ForMember(
                d => d.User, a => a.MapFrom(s => s.User)).ForMember(
                d => d.UsersLikes, a => a.MapFrom(s => s.UsersLikes)).ForMember(
                s => s.ThemeObj, a => a.MapFrom(s => s.ThemeObj)).ForMember(
                s => s.Comments, a => a.MapFrom(s => s.Comments))).CreateMapper();  
            var mapped_mess = mapper.Map<Message, PostVM>(mess);
            return mapped_mess;
        }

        public List<PostVM> GetPostsByTheme(int themeId)
        {
            try
            {
                var mess = data.GetPostsByTheme(themeId);
                var mapper = new MapperConfiguration(c => c.CreateMap<Message, PostVM>().ForMember(
                    d => d.User, a => a.MapFrom(s => s.User)).ForMember(
                    d => d.UsersLikes, a => a.MapFrom(s => s.UsersLikes)).ForMember(
                    s => s.ThemeObj, a => a.MapFrom(s => s.ThemeObj)).ForMember(
                    s => s.Comments, a => a.MapFrom(s => s.Comments))).CreateMapper();
                var mapped_mess = mapper.Map<IEnumerable<Message>, IEnumerable<PostVM>>(mess);
                return mapped_mess.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<PostVM> GetPostsByUser(long userId)
        {
            try
            {
                var mess = data.GetPostsByUser(userId);
                var mapper = new MapperConfiguration(c => c.CreateMap<Message, PostVM>().ForMember(
                    d => d.User, a => a.MapFrom(s => s.User)).ForMember(
                    d => d.UsersLikes, a => a.MapFrom(s => s.UsersLikes)).ForMember(
                    s => s.ThemeObj, a => a.MapFrom(s => s.ThemeObj)).ForMember(
                    s => s.Comments, a => a.MapFrom(s => s.Comments))).CreateMapper();
                var mapped_mess = mapper.Map<IEnumerable<Message>, IEnumerable<PostVM>>(mess);
                return mapped_mess.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DeletePost(long id)
        {
            try
            {
                data.DeletePost(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PostVM EditPost(PostVM post)
        {
            try
            {
                var mapper = new MapperConfiguration(c => c.CreateMap<PostVM, Message>()).CreateMapper();
                var mappedRes = mapper.Map<PostVM, Message>(post);
                var message = data.EditPost(mappedRes);
                mapper = new MapperConfiguration(c => c.CreateMap<Message, PostVM>()).CreateMapper();
                var mapped_mess = mapper.Map<Message, PostVM>(message);
                return mapped_mess;
            }
            catch (Exception ex)
            {
                return null;
            }
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

        public List<ThemesVM> GetThemes()
        {
            var themes = data.GetAllThemes();
            var mapper = new MapperConfiguration(c => c.CreateMap<Theme, ThemesVM>()).CreateMapper();
            var mapped_themes = mapper.Map<IEnumerable<Theme>, IEnumerable<ThemesVM>>(themes);
            foreach (var item in mapped_themes)
            {
                item.MessageCount = data.GetThemePostsCount(item.Id);
            }
            return mapped_themes.ToList();
        }

        public UserData GetUserData(string login)
        {
            var User = data.FindUserByLogin(login);
            if (User != null)
            {
                var mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.User, UserData>().ForMember(
                    s => s.FavoriteAdverts, a => a.MapFrom(s => s.FavoriteAdvert))).CreateMapper();
                var getUser = mapper.Map<BLL.DataTransferObjects.User, UserData>(User);
                //getUser.ConnectedUsers = GetConectedUsers(login);
                return getUser;
            }
            return null;
        }

        public string[] GetUserRoles(string login)
        {
            var user = data.FindUserByLogin(login);
            if (user != null)
            {
                return user.Roles.Split('/');
            }
            return null;
        }

        public int LikeMessage(string userName, int messageId)
        {
            return data.LikeMessage(userName, messageId);
        }

        public int DislikeMessage(string userName, int messageId)
        {
            return data.DislikeMessage(userName, messageId);
        }

        public List<Models.Wallet.IncomeReason> GetIncomeReasonList()
        {

            var incomeReason = data.GetIncomeReasonList();
            var mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.IncomeReason, Models.Wallet.IncomeReason>()).CreateMapper();
            var mapped_income = mapper.Map<IEnumerable<BLL.DataTransferObjects.IncomeReason>, IEnumerable<Models.Wallet.IncomeReason>>(incomeReason).ToList();
            return mapped_income;

        }

        public List<Models.Wallet.ConsumptionReason> GetConsumptionReasonList()
        {
            var consumptionReason = data.GetConsumptionReasonList();
            var mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.ConsumptionReason, Models.Wallet.ConsumptionReason>()).CreateMapper();
            var mapped_consumption = mapper.Map<IEnumerable<BLL.DataTransferObjects.ConsumptionReason>, IEnumerable<Models.Wallet.ConsumptionReason>>(consumptionReason).ToList();
            return mapped_consumption;
        }

        public List<Models.Wallet.OperationType> GetOperationTypeList()
        {
            var OperationType = data.GetOperationTypeList();
            var mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.OperationType, Models.Wallet.OperationType>()).CreateMapper();
            var mapped_OperationType = mapper.Map<IEnumerable<BLL.DataTransferObjects.OperationType>, IEnumerable<Models.Wallet.OperationType>>(OperationType).ToList();
            return mapped_OperationType;
        }

        public List<Models.Wallet.ResourceType> GetResourceTypeList()
        {
            var ResourceType = data.GetResourceTypeList();
            var mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.ResourceType, Models.Wallet.ResourceType>()).CreateMapper();
            var mapped_ResourceType = mapper.Map<IEnumerable<BLL.DataTransferObjects.ResourceType>, IEnumerable<Models.Wallet.ResourceType>>(ResourceType).ToList();
            return mapped_ResourceType;
        }

        public List<Models.Wallet.Source> AddSource(Models.Wallet.Source source, string userName)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<BadMom.Models.Wallet.Source, BLL.DataTransferObjects.Source>()).CreateMapper();
            var mapped_Source = mapper.Map<BadMom.Models.Wallet.Source, BLL.DataTransferObjects.Source>(source);
            if (data.AddSource(mapped_Source, userName))
            {
                return GetSourceByUser(userName);
            }
            return null;
        }

        public List<Models.Wallet.Source> GetSourceByUser(string userName)
        {
            var source = data.GetSourceByUser(userName);
            var mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.Source, BadMom.Models.Wallet.Source>()).CreateMapper();
            var mapped_Source = mapper.Map<IEnumerable<BLL.DataTransferObjects.Source>, IEnumerable<BadMom.Models.Wallet.Source>>(source);
            return mapped_Source.OrderBy(x => x.Created).ToList();
        }

        public List<Models.Wallet.Source> DeleteSource(long sourceId, string userName)
        {
            if (data.DeleteSource(sourceId))
            {
                return GetSourceByUser(userName);
            }
            return null;
        }

        public List<Models.Wallet.Income> AddIncome(Models.Wallet.Income income, string userName)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<BadMom.Models.Wallet.Income, BLL.DataTransferObjects.Income>()).CreateMapper();
            var mapped_Income = mapper.Map<BadMom.Models.Wallet.Income, BLL.DataTransferObjects.Income>(income);
            if (data.AddIncome(mapped_Income, userName))
            {
                return GetIncomeByUser(userName, DateTime.Now.AddMonths(-1), DateTime.Now, 0);
            }
            return null;
        }

        public List<Models.Wallet.Income> GetIncomeByUser(string userName, DateTime from, DateTime to, long sourceId = 0)
        {
            var Income = data.GetIncomeByUser(userName, from, to, sourceId);
            var mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.Income, BadMom.Models.Wallet.Income>()).CreateMapper();
            var mapped_Income = mapper.Map<IEnumerable<BLL.DataTransferObjects.Income>, IEnumerable<BadMom.Models.Wallet.Income>>(Income);
            return mapped_Income.ToList();
        }

        public List<Models.Wallet.Income> DeleteIncome(long incomeId, string userName)
        {
            if (data.DeleteIncome(incomeId))
            {
                return GetIncomeByUser(userName, DateTime.Now.AddMonths(-1), DateTime.Now, 0);
            }
            return null;
        }

        public List<Models.Wallet.Consumption> AddConsumption(Models.Wallet.Consumption consumption, string userName)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<BadMom.Models.Wallet.Consumption, BLL.DataTransferObjects.Consumption>()).CreateMapper();
            var mapped_Consumption = mapper.Map<BadMom.Models.Wallet.Consumption, BLL.DataTransferObjects.Consumption>(consumption);
            if (data.AddConsumption(mapped_Consumption, userName))
            {
                return GetConsumptionByUser(userName, DateTime.Now.AddMonths(-1), DateTime.Now, 0);
            }
            return null;
        }

        public List<Models.Wallet.Consumption> GetConsumptionByUser(string userName, DateTime from, DateTime to, long sourceId = 0)
        {
            var consumption = data.GetConsumptionByUser(userName, from, to, sourceId);
            var mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.Consumption, BadMom.Models.Wallet.Consumption>()).CreateMapper();
            var mapped_Consumption = mapper.Map<IEnumerable<BLL.DataTransferObjects.Consumption>, IEnumerable<BadMom.Models.Wallet.Consumption>>(consumption);
            return mapped_Consumption.ToList();
        }

        public List<Models.Wallet.Consumption> DeleteConsumption(long consumptionId, string userName)
        {
            if (data.DeleteConsumption(consumptionId))
            {
                return GetConsumptionByUser(userName, DateTime.Now.AddMonths(-1), DateTime.Now, 0);
            }
            return null;
        }

        public List<Models.Wallet.SourceInfoVM> GetSourceInfo(long sourceId, DateTime from, DateTime to, string userName)
        {
            var consumption = data.GetConsumptionByUser(userName, from, to, sourceId);
            var mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.Consumption, BadMom.Models.Wallet.SourceInfoVM>().ForMember(
                d => d.Source, a => a.MapFrom(s => s.Source1))).CreateMapper();
            var mapped_Consumption = mapper.Map<IEnumerable<BLL.DataTransferObjects.Consumption>, IEnumerable<BadMom.Models.Wallet.SourceInfoVM>>(consumption).ToList();
            var Income = data.GetIncomeByUser(userName, from, to, sourceId);
            mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.Income, BadMom.Models.Wallet.SourceInfoVM>()).CreateMapper();
            var mapped_Income = mapper.Map<IEnumerable<BLL.DataTransferObjects.Income>, IEnumerable<BadMom.Models.Wallet.SourceInfoVM>>(Income).ToList();
            mapped_Income.AddRange(mapped_Consumption);
            return mapped_Income.OrderByDescending(x => x.Date).ToList();
        }

        public bool AddMessage(PostVM mess, string userName)
        {
            try
            {
                var mapper = new MapperConfiguration(c => c.CreateMap<PostVM, BLL.DataTransferObjects.Message>()).CreateMapper();
                var message = mapper.Map<PostVM, BLL.DataTransferObjects.Message>(mess);
                data.AddMessage(message, userName);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddAdvert(AdvertVM advert, string userName)
        {
            try
            {
                var mapper = new MapperConfiguration(c => c.CreateMap<AdvertVM, Advert>()).CreateMapper();  //TODO:
                var mapperRes = mapper.Map<AdvertVM, BLL.DataTransferObjects.Advert>(advert);
                data.AddAdvert(mapperRes, userName);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SendMessage(Models.User.PersonalMessage personalMessage, string userFrom)
        {
            try
            {
                var mapper = new MapperConfiguration(c => c.CreateMap<Models.User.PersonalMessage, BLL.DataTransferObjects.PersonalMessage>()).CreateMapper();
                var mapped_personalMessage = mapper.Map<Models.User.PersonalMessage, BLL.DataTransferObjects.PersonalMessage>(personalMessage);

                data.SendMessage(mapped_personalMessage, userFrom);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PrivateMessageByUserVM> GetMessagesByUsers(string userName)
        {
            var usersMess = data.GetMessagesByUsers(userName);
            var mapper = new MapperConfiguration(c => c.CreateMap<PrivateMessageByUser, PrivateMessageByUserVM>()).CreateMapper();
            var mapped_res = mapper.Map<List<PrivateMessageByUser>, List< PrivateMessageByUserVM >> (usersMess);
            return mapped_res;
        }

        public List<Models.User.PersonalMessage> SetMessageStatus(List<Models.User.PersonalMessage> messages, PersonalMessageStatus status)
        {
            try
            {
                var mapper = new MapperConfiguration(c => c.CreateMap<Models.User.PersonalMessage, BLL.DataTransferObjects.PersonalMessage>()).CreateMapper();
                var mapped_res = mapper.Map<List<Models.User.PersonalMessage>, List<BLL.DataTransferObjects.PersonalMessage>>(messages);
                var res = data.SetMessageStatus(mapped_res, (Int32)status);
                mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.PersonalMessage, Models.User.PersonalMessage>()).CreateMapper();
                var mapped_result = mapper.Map<List<BLL.DataTransferObjects.PersonalMessage>, List<Models.User.PersonalMessage>>(res);
                return mapped_result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Models.User.PersonalMessage> SetMessageStatusReaded(List<Models.User.PersonalMessage> messages, string userName)
        {
            try
            {
                var mapper = new MapperConfiguration(c => c.CreateMap<Models.User.PersonalMessage, BLL.DataTransferObjects.PersonalMessage>()).CreateMapper();
                var mapped_res = mapper.Map<List<Models.User.PersonalMessage>, List<BLL.DataTransferObjects.PersonalMessage>>(messages);
                var res = data.SetMessageStatusReaded(mapped_res, userName);
                mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.PersonalMessage, Models.User.PersonalMessage>()).CreateMapper();
                var mapped_result = mapper.Map<List<BLL.DataTransferObjects.PersonalMessage>, List<Models.User.PersonalMessage>>(res);
                return mapped_result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AdvertVM> GetAdvertsByUserId(long id)
        {
            try
            {
                var advert = data.GetAdvertsByUserId(id);
                var mapper = new MapperConfiguration(c => c.CreateMap<Advert, AdvertVM>().ForMember(
                d => d.UserName, a => a.MapFrom(s => s.User.Login))).CreateMapper();

                var mappedRes = mapper.Map<List<Advert>, List<AdvertVM>>(advert);
                return mappedRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAdvert(long id)
        {
            try
            {
                data.DeleteAdvert(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AdvertVM GetAdvert(long id)
        {
            try
            {
                var advert = data.GetAdvert(id);
                var mapper = new MapperConfiguration(c => c.CreateMap<Advert, AdvertVM>().ForMember(
                d => d.UserName, a => a.MapFrom(s => s.User.Login))).CreateMapper();

                var mappedRes = mapper.Map<Advert, AdvertVM>(advert);
                return mappedRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Categories> GetAllCategories()
        {
            try
            {
                var cat = data.GetAllCategories();
                var mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.Category, Categories>()).CreateMapper();
                var mappedRes = mapper.Map<IEnumerable<BLL.DataTransferObjects.Category>, IEnumerable<Categories>>(cat).ToList();
                return mappedRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AdvertVM> GetAdvertsByCategoryId(int id, string order)
        {
            try
            {
                var advert = data.GetAdvertsByCategoryId(id);
                switch (order)
                {
                    case "price_down":
                        advert = advert.OrderByDescending(x => x.Price).ToList();
                        break;
                    case "price_up":
                        advert = advert.OrderBy(x => x.Price).ToList();
                        break;
                    case "date_up":
                        advert = advert.OrderBy(x => x.Created).ToList();
                        break;
                    default:
                        advert = advert.OrderByDescending(x => x.Created).ToList();
                        break;
                }

                var mapper = new MapperConfiguration(c => c.CreateMap<Advert, AdvertVM>().ForMember(
                d => d.UserName, a => a.MapFrom(s => s.User.Login))).CreateMapper();

                var mappedRes = mapper.Map<List<Advert>, List<AdvertVM>>(advert);
                return mappedRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddToFavorite(long advertId, long userId)
        {
            try
            {
                data.AddToFavorite(advertId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteFromFavorite(long advertId, long userId)
        {
            try
            {
                data.DeleteFromFavorite(advertId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AdvertVM> GetFavoriteAdvertsByUserId(List<long> favorites)
        {
            try
            {
                var advert = data.GetFavoriteAdvertsByUserId(favorites);
                var mapper = new MapperConfiguration(c => c.CreateMap<Advert, AdvertVM>().ForMember(
                d => d.UserName, a => a.MapFrom(s => s.User.Login))).CreateMapper();

                var mappedRes = mapper.Map<List<Advert>, List<AdvertVM>>(advert);
                return mappedRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Models.Planer.EventType> GetEventTypes()
        {
            try
            {
                var eventTypes = data.GetEventTypes();
                var mapper = new MapperConfiguration(c => c.CreateMap<BLL.DataTransferObjects.EventType, Models.Planer.EventType>()).CreateMapper();

                var mappedRes = mapper.Map<List<BLL.DataTransferObjects.EventType>, List<Models.Planer.EventType>>(eventTypes);
                return mappedRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddEvent(EventVM eventItem, string userName)
        {
                var mapper = new MapperConfiguration(c => c.CreateMap<EventVM, Events>()).CreateMapper();
                var mappedRes = mapper.Map<EventVM, Events>(eventItem);
                data.AddEvent(mappedRes, userName);
        }

        public void DeleteEvent(long eventId)
        {
            try
            {
                data.DeleteEvent(eventId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventVM> GetEventsByUser(string userName)
        {
            try
            {
                var events = data.GetEventsByUser(userName);
                var mapper = new MapperConfiguration(c => c.CreateMap<Events, EventVM>()).CreateMapper();

                var mappedRes = mapper.Map<List<Events>, List<EventVM>>(events);
                return mappedRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}