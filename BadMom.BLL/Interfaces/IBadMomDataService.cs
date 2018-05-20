using BadMom.BLL.DataTransferObjects;
using BadMom.DAL.Model.Abstract;
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

        User GetUserById(int id);

        User FindUserByLogin(string login);

        void AddMessage(Message mess);

        Message GetMessageByUserId(long id);

        void Dispose();
    }
}
