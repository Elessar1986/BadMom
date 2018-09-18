using BadMom.BLL.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMom.BLL.Interfaces
{
    public interface IBadMomDataService
    {
        void AddUser(User user);

        User GetUserById(long id);

        User FindUserByLogin(string login);

        void AddMessage(Message mess, string userName);

        User ConfirmAuth(string login, string pass);

        bool CheckUserToChangePass(string login, string pass);

        User ChangePass(string login, string pass, string salt);

        void SendLoggerMessage(logEvents logEvents);

        List<Theme> GetAllThemes();

        int GetThemePostsCount(int themeId);

        List<Message> GetPostsByTheme(int themeId);

        Message GetPostById(int id);

        List<Message> GetPostsByUser(long userId);

        void DeletePost(long id);

        Message EditPost(Message mess);

        List<Message> GetFeaturedPosts();

        List<Message> GetLastPosts();

        List<Comment> AddComment(string userName, string commentBody, int messageId, string targetUserName);

        List<Comment> DeleteComment(int commentId, int messageId);

        List<Comment> GetComments(int messageId);

        int LikeMessage(string userName, int messageId);

        int DislikeMessage(string userName, int messageId);

        List<IncomeReason> GetIncomeReasonList();

        List<ConsumptionReason> GetConsumptionReasonList();

        List<OperationType> GetOperationTypeList();

        List<ResourceType> GetResourceTypeList();

        bool AddSource(Source source, string userName);

        List<Source> GetSourceByUser(string userName);

        bool DeleteSource(long sourceId);

        bool AddIncome(Income income, string userName);

        List<Income> GetIncomeByUser(string userName, DateTime from, DateTime to, long sourceId = 0);

        bool DeleteIncome(long incomeId);

        bool AddConsumption(Consumption consumption, string userName);

        List<Consumption> GetConsumptionByUser(string userName, DateTime from, DateTime to, long sourceId = 0);

        bool DeleteConsumption(long consumptionId);

        bool SendMessage(DataTransferObjects.PersonalMessage personalMessage, string userFrom);

        List<PrivateMessageByUser> GetMessagesByUsers(string userName);

        List<DataTransferObjects.PersonalMessage> SetMessageStatus(List<DataTransferObjects.PersonalMessage> messages, int status);

        List<DataTransferObjects.PersonalMessage> SetMessageStatusReaded(List<DataTransferObjects.PersonalMessage> messages, string login);

        void AddAdvert(DataTransferObjects.Advert advert, string userName);

        List<DataTransferObjects.Advert> GetAdvertsByUserId(long id);

        List<DataTransferObjects.Advert> GetFavoriteAdvertsByUserId(List<long> favorites);

        List<DataTransferObjects.Advert> GetAdvertsByCategoryId(int id);

        void DeleteAdvert(long id);

        Advert GetAdvert(long id);

        List<DataTransferObjects.Category> GetAllCategories();

        void AddToFavorite(long advertId, long userId);

        void DeleteFromFavorite(long advertId, long userId);

        List<EventType> GetEventTypes();

        void AddEvent(Events eventItem, string userName);

        void DeleteEvent(long eventId);

        List<Events> GetEventsByUser(string userName);

        List<AdminUserData> GetAllUsers(string find, string status, string conf, string role, string order );

        bool DeleteUser(long id);

        bool ChangeUserStatus(long id, int status);

        bool ChangeUserRole(long id, string role);

        List<logEvents> GetLog(DateTime from);

        bool EditAdvert(Advert advert);

        void Dispose();
    }
}
