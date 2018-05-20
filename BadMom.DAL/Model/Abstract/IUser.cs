using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMom.DAL.Model.Abstract
{
    public interface IUser
    {
         string Login { get; set; }

         string Email { get; set; }

    }
}
