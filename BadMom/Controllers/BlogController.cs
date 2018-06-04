using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BadMom.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        //[Authorize(Roles ="admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}