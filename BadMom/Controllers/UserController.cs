using BadMom.BLL.Interfaces;
using BadMom.Helpers;
using BadMom.Helpers.DataServiceHelper.Abstract;
using BadMom.Helpers.DataServiceHelper.Concrete;
using BadMom.Models.Registration;
using BadMom.Models.Shared;
using BadMom.Models.User;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BadMom.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        //IBadMomDataService data;
        IDataHelper dataHelper;
        LoggerHelper logger;
        //EmailHelper emailHelper = new EmailHelper();
        PasswordHelper passwordHelper = new PasswordHelper(20);

        public UserController(IBadMomDataService service)
        {
            logger = new LoggerHelper(service);
            dataHelper = new DataHelper(service);
            ViewBag.ActiveMenu = "User";
        }


        
        public ActionResult Index()
        {
            try
            {
                var user = dataHelper.GetUserData(User.Identity.Name);
                ViewBag.User = user;
                return View(user);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }


        public ActionResult UserInfo(string UserName, long? id)
        {
            try
            {
                if (!string.IsNullOrEmpty(UserName))
                {
                    var user = dataHelper.GetUserData(UserName);
                    ViewBag.User = user;
                    return View(user);
                }
                else if (id.HasValue)
                {
                    var user = dataHelper.GetUserData(id.Value);
                    ViewBag.User = user;
                    return View(user);
                }
                else
                {
                    return View("Error", new Error { ExDescription = "Empty user name!" });
                }
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }


        public ActionResult MyMessages()
        {
            try
            {
                var user = dataHelper.GetUserData(User.Identity.Name);
                ViewBag.User = user;
                ViewBag.UsersMessages = dataHelper.GetMessagesByUsers(user.Id);
                return View(user);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        public ActionResult MyAdverts()
        {
            try
            {
                var user = dataHelper.GetUserData(User.Identity.Name);
                ViewBag.User = user;
                var adverts = dataHelper.GetAdvertsByUserId(user.Id);
                return View(adverts);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        public ActionResult FavoriteAdverts()
        {
            try
            {
                var user = dataHelper.GetUserData(User.Identity.Name);
                ViewBag.User = user;
                var adverts = dataHelper.GetFavoriteAdvertsByUserId(user.FavoriteAdverts);
                return View(adverts);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        public ActionResult MyPosts()
        {
            try
            {
                var user = dataHelper.GetUserData(User.Identity.Name);
                ViewBag.User = user;
                var posts = dataHelper.GetPostsByUser(user.Id).OrderByDescending(x => x.Created).ToList();
                return View(posts);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        public ActionResult UserMessages(long id, string userLogin)
        {
            try
            {
                var user = dataHelper.GetUserData(User.Identity.Name);
                ViewBag.User = user;
                if (user.Id == id) throw new Exception("Невозможно отправить сообщение самому себе!");
                var userMess = dataHelper.GetMessagesByUsers(user.Id).Where(x => x.Id == id).ToList();
                PrivateMessageByUserVM mess;
                if (userMess.Count != 0)
                {
                    mess = userMess.First();
                    mess.Messages = dataHelper.SetMessageStatusReaded(mess.Messages, User.Identity.Name);
                }
                else
                {
                    var userFrom = dataHelper.GetUserData(id);
                    mess = new PrivateMessageByUserVM
                    {
                        Avatar = userFrom.Photo,
                        Id = userFrom.Id,
                        Login = userFrom.Login,
                        Messages = new List<PersonalMessage>()
                    };
                }
                ViewBag.UserMessages = mess;
                return View(user);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendFastMessage(PersonalMessage message)
        {
            try
            {
                dataHelper.SendMessage(message, User.Identity.Name);
                return RedirectToAction("UserMessages", new { id = message.UserTo });
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }


        public ActionResult SendMessage(PersonalMessage message)
        {
            try
            {
                dataHelper.SendMessage(message, User.Identity.Name);
                return Json(new { test = "true" });
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        public ActionResult UserAdvert(long id, string userName)
        {
            try
            {
                ViewBag.User = dataHelper.GetUserData(id);
                var adverts = dataHelper.GetAdvertsByUserId(id);
                return View(adverts);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        public ActionResult ChangePassRegistered()
        {
            try
            {
                var user = dataHelper.GetUserData(User.Identity.Name);
                ViewBag.User = user;
                return View();
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult ChangePassRegistered(ChangePassRegistered passData)
        {
            try
            {
                var user = dataHelper.GetUserData(User.Identity.Name);
                ViewBag.User = user;
                if (ModelState.IsValid)
                {
                    var userPassData = dataHelper.GetPasswordData(user.Login);
                    if (passwordHelper.CheckPassword(passData.OldPassword, userPassData))
                    {
                        userPassData = passwordHelper.CryptPassword(passData.ConfirmPassword);
                        userPassData.Login = user.Login;
                        dataHelper.ChangePass(userPassData);
                       
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("OldPassword", "Не верный пароль");
                        return View(passData);
                    }
                }
                return View(passData);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        public ActionResult ChangeAvatar()
        {
            try
            {
                var user = dataHelper.GetUserData(User.Identity.Name);
                ViewBag.User = user;
                return View(user.Photo);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeAvatar(HttpPostedFileBase fileUpload)
        {
            try
            {
                var user = dataHelper.GetUserData(User.Identity.Name);
                ViewBag.User = user;
                if (fileUpload != null)
                {
                    var res = ImageHelper.ScaleImage(Image.FromStream(fileUpload.InputStream, true, true), 200, 200);
                    ImageConverter _imageConverter = new ImageConverter();
                    byte[] xByte = (byte[])_imageConverter.ConvertTo(res, typeof(byte[]));
                    dataHelper.ChangeAvatar(xByte, user.Id);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

    }
}