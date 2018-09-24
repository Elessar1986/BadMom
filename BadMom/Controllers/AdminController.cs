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


        public ActionResult Logger(int? page, int days = 1)
        {
            try
            {
                var logs = dataHelper.GetLog(DateTime.Now.AddDays(-days)).OrderByDescending(x => x.Created);
                int pageSize = 20;
                int pageNumber = (page ?? 1);
                return View(logs.ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }


        public ActionResult Users(string find, int? page, string status, string conf, string role, string order)
        {
            try
            {
                ViewBag.User = dataHelper.GetUserData(User.Identity.Name);
                var users = dataHelper.GetAllUsers(find, status, conf, role, order);
                ViewBag.Role = role;
                ViewBag.Status = status;
                ViewBag.Conf = conf;
                ViewBag.Order = order;
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(users.ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        public ActionResult BlockUser(long id, string status)
        {
            try
            {
                switch (status)
                {
                    case "blocked":
                        dataHelper.ChangeUserStatus(id, 1);
                        break;
                    case "unblocked":
                        dataHelper.ChangeUserStatus(id, 0);
                        break;
                    default:
                        break;
                }
                return RedirectToAction("Users");
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        public ActionResult SetUserRole(long id, string role)
        {
            try
            {
                dataHelper.ChangeUserRole(id, role);
                return RedirectToAction("Users");
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        public ActionResult DeleteUser(long id)
        {
            try
            {
                dataHelper.DeleteUser(id);
                return RedirectToAction("Users");
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        public ActionResult SendEmail(long id)
        {
            try
            {
                // TODO: SendEmail
                return RedirectToAction("Users");
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        public ActionResult SendMessage(long id)
        {
            try
            {
                // TODO: SendMessage
                return RedirectToAction("Users");
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        public ActionResult ShowPost(long id)
        {
            try
            {
                // TODO: ShowPost
                return RedirectToAction("Users");
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        public ActionResult ShowAdvert(long id)
        {
            try
            {
                // TODO: ShowAdvert
                return RedirectToAction("Users");
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }


    }
}