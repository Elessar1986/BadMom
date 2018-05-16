using BadMom.BLL.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMom.BLL.Interfaces
{
    interface IBadMomDataService
    {
        void AddUser(User user);

        User GetUserById(int id);

        void AddMessage(Message mess);

        Message GetMessageByUserId(long id);

        void Dispose();
    }
}
