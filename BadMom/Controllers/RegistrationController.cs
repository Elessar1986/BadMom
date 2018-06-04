using BadMom.BLL.Infrastrutcure;
using BadMom.BLL.Interfaces;
using BadMom.Helpers;
using BadMom.Helpers.DataServiceHelper.Abstract;
using BadMom.Helpers.DataServiceHelper.Concrete;
using BadMom.Models.Registration;
using BadMom.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BadMom.Controllers
{
    public class RegistrationController : Controller
    {

        //IBadMomDataService data;
        PasswordHelper passwordHelper = new PasswordHelper(20);
        IDataHelper dataHelper;
        LoggerHelper logger;
        EmailHelper emailHelper = new EmailHelper();

        public RegistrationController(IBadMomDataService service)
        {
            logger = new LoggerHelper(service);
            dataHelper = new DataHelper(service);
        }
        // GET: Registration
        public ActionResult New()
        {
            return View(new RegistrUserVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(RegistrUserVM user)
        {

            if (ModelState.IsValid)
            {
                UserData newUser = new UserData();
                try
                {
                    newUser = dataHelper.CreateUser(user, passwordHelper.CryptPassword(user.Password));
                }
                catch (ValidationException ve)
                {
                    ModelState.AddModelError(ve.Property, ve.Message);
                    return View(user);
                }
                emailHelper.SendAuthLink(newUser.Login, newUser.PasswordHash, newUser.Email); 
                return View("RegistrationLink",newUser);        //for test
            }
            else
            return View(user);
        }

        [HttpGet]
        public ActionResult Login(bool wrongPass = false)
        {
            if (wrongPass) ViewBag.WrongPassOrLog = Resources.Resource.WrongPassOrLogin;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginDataVM loginData)
        {
            if (ModelState.IsValid)
            {
                var passData = dataHelper.GetPasswordData(loginData.Login);
                if (passwordHelper.CheckPassword(loginData.Password, passData))
                {
                    FormsAuthentication.SetAuthCookie(passData.Login, true);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Login", "Registration", new { wrongPass = true });
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ConfirmAuth(string login, string pass)
        {
            var user = dataHelper.ConfirmAuth(login, pass);
            if(user != null) {
                return View(user);
            }
            return View("Error");
        }

        public ActionResult ForgotPass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPass(UserEmail userEmail)
        {
            if (ModelState.IsValid)
            {
                UserPasswordData passData;
                try
                {
                    passData = dataHelper.GetPasswordData(userEmail.Email);
                    if(passData == null) return View("Error", new Error() { ExDescription = "Incorect email!" });
                }
                catch (Exception ex)
                {
                    throw;
                }
                if (emailHelper.SendChangePassLink(passData))
                {

                }
                return View("ChangePassLink", passData);
            }
            return View();
        }

        public ActionResult ChangePass(string login, string pass)
        {
            try
            {
                if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(pass) && dataHelper.CheckUserToChangePass(login, pass))
                {
                    Session["cp_Login"] = login;
                    Session["cp_Pass"] = pass;
                    return View();
                }
            }
            catch (ValidationException ve)
            {
                return View("UserError", ve);
            }
            return View("Error", new Error() { ExDescription = "Error in login data!" });
        }

        [HttpPost]
        public ActionResult ChangePass(ChangePass changePass)
        {
            if (ModelState.IsValid)
            {
                if(Session["cp_Login"] != null && Session["cp_Pass"] != null)
                {
                    var userPassData = passwordHelper.CryptPassword(changePass.Password);
                    userPassData.Login = Session["cp_Login"].ToString();
                    try
                    {
                        var user = dataHelper.ChangePass(userPassData);
                        if(user != null)
                        {
                            FormsAuthentication.SetAuthCookie(user.Login, true);
                            return View("PassChangeSuccess");
                        }
                        else
                        {
                            return View("Error", new Error() { ExDescription = "Error to find User!" });
                        }
                    }
                    catch (ValidationException ve)
                    {
                        return View("UserError", ve);
                    }
                }

            }
            return View(changePass);
        }
    }
}