using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadMom.BLL.Services;
using BadMom.DAL.Repositories;
using BadMom.BLL.DataTransferObjects;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            BadMomDataService service = new BadMomDataService(new EFUnitOfWork("data source=UAPSPC313-22\\SQLEXPRESS;initial catalog=BadMomResource;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"));
            //service.AddUser(new User()
            //{
            //    PasswordHash = "sdsdsdsdsd",
            //    Salt = "dsdsdsdsds",
            //    Created = DateTime.Now,
            //    Email = "test@test.com",
            //    Login = "TestUser",
            //    Roles = "admin",
            //    Photo = new byte[] { 4, 5, 5, 5, 6, 6, 6 },

            //});

            var user = service.GetUserById(1);

            //service.AddMessage(new Message()
            //{
            //    Body = "test test test",
            //    Created = DateTime.Now,
            //    Title = "Test",
            //    UserId = 1,
            //    Theme = 1
            //});

            var res = service.GetMessageByUserId(1);

            Console.ReadKey();
        }
    }
}
