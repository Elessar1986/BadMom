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
        void CreateUser(RegistrUserVM user, UserPasswordData passData);

        UserPasswordData GetPasswordData(string login);



    }
}
