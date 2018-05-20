using BadMom.BLL.Interfaces;
using BadMom.Helpers;
using BadMom.Helpers.DataServiceHelper.Abstract;
using BadMom.Helpers.DataServiceHelper.Concrete;
using BadMom.Models.Registration;
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

        public RegistrationController(IBadMomDataService service)
        {
            //data = service;
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
                dataHelper.CreateUser(user, passwordHelper.CryptPassword(user.Password));
                return RedirectToAction("Index", "Home");
            }
            else
            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginData loginData)
        {
            if (ModelState.IsValid)
            {
                var passData = dataHelper.GetPasswordData(loginData.Login);
                if (passwordHelper.CheckPassword(loginData.Password, passData))
                {
                    FormsAuthentication.SetAuthCookie(loginData.Login, true);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Error");
        }
    }
}