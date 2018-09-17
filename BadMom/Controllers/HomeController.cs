using BadMom.Models.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BadMom.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
            ViewBag.ActiveMenu = "Home";
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Error()
        {

            return View();
        }

    }
}