using BadMom.Models.Registration;
using BadMom.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadMom.Models.Wallet;
using BadMom.Models.User;
using BadMom.Models.Advert;
using BadMom.Models.Planer;
using BadMom.Models.Admin;

namespace BadMom.Helpers.DataServiceHelper.Abstract
{
    interface IDataHelper
    {
        UserData CreateUser(RegistrUserVM user, UserPasswordData passData);

        UserPasswordData GetPasswordData(string login);

        string[] GetUserRoles(string login);

        UserData ConfirmAuth(string login, string pass);

        bool CheckUserToChangePass(string login, string pass);

        UserData ChangePass(UserPasswordData passData);

        UserData GetUserData(string login);

        List<ThemesVM> GetThemes();

        List<PostVM> GetPostsByTheme(int themeId);

        PostVM GetPostById(int id);

        List<FeaturedMessage> GetFeaturedPosts();

        List<LastMessage> GetLastPost();

        List<PostVM> GetPostsByUser(long userId);

        PostVM EditPost(PostVM post);

        void DeletePost(long id);

        List<CommentVM> AddComment(string userName, string commentBody, int messageId, string targetUserName);

        List<CommentVM> DeleteComment(int commentId, int messageId);

        List<CommentVM> GetComments( int messageId);

        int LikeMessage(string userName, int messageId);

        int DislikeMessage(string userName, int messageId);

        List<IncomeReason> GetIncomeReasonList();

        List<ConsumptionReason> GetConsumptionReasonList();

        List<OperationType> GetOperationTypeList();

        List<ResourceType> GetResourceTypeList();

        List<Source> AddSource(Source source, string userName);

        List<Source> GetSourceByUser(string userName);

        List<Source> DeleteSource(long sourceId, string userName);

        List<Income> AddIncome(Income income, string userName);

        List<Income> GetIncomeByUser(string userName, DateTime from, DateTime to, long sourceId = 0);

        List<Income> DeleteIncome(long incomeId, string userName);

        List<Consumption> AddConsumption(Consumption consumption, string userName);

        List<Consumption> GetConsumptionByUser(string userName, DateTime from, DateTime to, long sourceId = 0);

        List<Consumption> DeleteConsumption(long consumptionId, string userName);

        List<Models.Wallet.SourceInfoVM> GetSourceInfo(long sourceId, DateTime from, DateTime to, string userName);

        bool AddMessage(PostVM mess,string userName);

        bool SendMessage(PersonalMessage personalMessage, string userFrom);

        List<PrivateMessageByUserVM> GetMessagesByUsers(string userName);

        List<PersonalMessage> SetMessageStatus(List<PersonalMessage> messages, PersonalMessageStatus status);

        List<PersonalMessage> SetMessageStatusReaded(List<PersonalMessage> messages, string userName);

        bool AddAdvert(AdvertVM advert, string userName);

        List<AdvertVM> GetAdvertsByUserId(long id);

        List<AdvertVM> GetFavoriteAdvertsByUserId(List<long> favorites);

        void DeleteAdvert(long id);

        AdvertVM GetAdvert(long id);

        List<Categories> GetAllCategories();

        List<AdvertVM> GetAdvertsByCategoryId(int id, string order);

        void AddToFavorite(long advertId, long userId);

        void DeleteFromFavorite(long advertId, long userId);

        List<EventType> GetEventTypes();

        void AddEvent(EventVM eventItem, string userName);

        void DeleteEvent(long eventId);

        List<EventVM> GetEventsByUser(string userName);

        List<AdminUserDataVM> GetAllUsers(string find, string status, string conf, string role, string order);

        bool DeleteUser(long id);

        bool ChangeUserStatus(long id, int status);

        bool ChangeUserRole(long id, string role);

        List<LoggerEvent> GetLog(DateTime from);

        bool EditAdvert(AdvertVM advert);
    }
}
