using BadMom.BLL.Interfaces;
using BadMom.Helpers;
using BadMom.Helpers.DataServiceHelper.Abstract;
using BadMom.Helpers.DataServiceHelper.Concrete;
using BadMom.Models.Shared;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BadMom.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        
        IDataHelper dataHelper;
        LoggerHelper logger;

        public AdminController(IBadMomDataService service)
        {
            logger = new LoggerHelper(service);
            dataHelper = new DataHelper(service);
            ViewBag.ActiveMenu = "User";
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Logger()
        {
            return View();
        }


        public ActionResult Users(int? page)
        {
            try
            {
                ViewBag.User = dataHelper.GetUserData(User.Identity.Name);

                var users = dataHelper.GetAllUsers().OrderByDescending(x => x.Created);

                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(users.ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", $"Error: {ex.Message} \n Inner: {ex.InnerException.Message} \n InnerInner: {ex.InnerException.InnerException.Message}");
                return View("Error", new Error() { ExDescription = ex.Message, ExInnerDescription = ex.InnerException.Message });
            }
        }




    }
}