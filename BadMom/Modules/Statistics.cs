using BadMom.BLL.Interfaces;
using BadMom.BLL.Services;
using BadMom.Helpers;
using BadMom.Helpers.DataServiceHelper.Abstract;
using BadMom.Helpers.DataServiceHelper.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace BadMom.Modules
{
    public class Statistics : IHttpModule
    {
        static Timer timer;
        long interval = 30000; 
        static object synclock = new object();
        static bool sent = false;
        EmailHelper email;
        IBadMomDataService service;
        IDataHelper data;
        LoggerHelper logger;

        public void Init(HttpApplication app)
        {
            service = new BadMomDataService("BadMomResource");
            data = new DataHelper(service);
            logger = new LoggerHelper(service);
            timer = new Timer(new TimerCallback(SendEmail), null, 0, interval);
        }

        private void SendEmail(object obj)
        {
            lock (synclock)
            {
                //email = new EmailHelper();
                //email.SendMail("Test", "Body", "qweasd@zxc.com");
                DateTime dd = DateTime.Now;
                if (dd.Minute == 0 && sent == false)
                {
                    var today = DateTime.Now;
                    var date = new DateTime(today.Year, today.Month, today.Day);
                    var users = data.GetAllUsers(null, null, "confirmed", null, null).Where(x => x.Created > date).Count();
                    var posts = data.GetPostsByTheme(0).Where(x => x.Created > date).Count();
                    var adverts = data.GetAdvertsByCategoryId(0, "default").Where(x => x.Created > date).Count();
                    logger.InfoMessage("100", $"Statistic:\nUsers: {users}\nPosts: {posts}\nAdverts: {adverts}");
                    sent = true;
                }
                else if(dd.Minute != 0)
                {
                    sent = false;
                }
            }
        }
        public void Dispose()
        { }
    }
}