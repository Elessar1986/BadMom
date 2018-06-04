using BadMom.Models.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
