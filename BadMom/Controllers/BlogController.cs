using BadMom.BLL.Interfaces;
using BadMom.Helpers;
using BadMom.Helpers.DataServiceHelper.Abstract;
using BadMom.Helpers.DataServiceHelper.Concrete;
using BadMom.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using BadMom.Models.Blog;
using System.Drawing;

namespace BadMom.Controllers
{
    public class BlogController : Controller
    {

        PasswordHelper passwordHelper = new PasswordHelper(20);
        IDataHelper dataHelper;
        LoggerHelper logger;
        EmailHelper emailHelper = new EmailHelper();

        public BlogController(IBadMomDataService service)
        {
            logger = new LoggerHelper(service);
            dataHelper = new DataHelper(service);
            ViewBag.ActiveMenu = "Blog";
        }


        public ActionResult Index(int? page, int themeId = 0)
        {
            List<PostVM> mess = dataHelper.GetPostsByTheme(themeId);
            SetViewBags();
            mess.ForEach(x => { if (x.Body.Length >= 200) x.Body = x.Body.Substring(0, 200); });
            //return View(mess);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.ThemeId = themeId;
            return View(mess.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Post(int message = 0)
        {
            try
            {
                var mess = dataHelper.GetPostById(message);
                if (mess == null) return View("Error", new Error() { ExDescription = "Нет сообщения с таким ID" });
                SetViewBags();
                return View(mess);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { ExDescription = ex.Message, ExInnerDescription = ex.InnerException.Message });
            }
        }

        public ActionResult DeletePost(long id)
        {
            try
            {
                dataHelper.DeletePost(id);
                return RedirectToAction("MyPosts", "User");
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", $"Error: {ex.Message} | Inner: {ex.InnerException.Message}");
                return View("Error", new Error() { ExDescription = ex.Message, ExInnerDescription = ex.InnerException.Message });
            }
        }


        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddComment( string Comment, int MessageId)
        {
            var comments = dataHelper.AddComment(User.Identity.Name, Comment, MessageId, null);
            if (comments == null) logger.ErrorMessage("301", "Empty data. Method: AddCommentes / dataHelper.AddComment returned null.");
            return PartialView("Comments", comments);

        }

        [Authorize]
        public ActionResult DeleteComment( int commentId, int messageId)
        {
            var comments = dataHelper.DeleteComment(commentId, messageId);
            if (comments == null) logger.ErrorMessage("301", "Empty data. Method: AddCommentes / dataHelper.AddComment returned null.");
            return PartialView("Comments", comments);

        }

        //[Authorize]
        public ActionResult LikeMessage(int messageId)
        {
            if (!User.Identity.IsAuthenticated) return Json(new { error = "Лайк может ставить только зарегистрированный пользователь!" }, JsonRequestBehavior.AllowGet);
            int likes = dataHelper.LikeMessage(User.Identity.Name, messageId);
            return PartialView("Like", new Like() { likes = likes, myLike = true, messageId = messageId });
        }

        [Authorize]
        public ActionResult DislikeMessage(int messageId)
        {
            int likes = dataHelper.DislikeMessage(User.Identity.Name, messageId);
            return PartialView("Like", new Like() { likes = likes, myLike = false, messageId = messageId });
        }

        [Authorize]
        public ActionResult AddPost()
        {
            var themes = dataHelper.GetThemes();
            if (!User.IsInRole("admin")) themes.RemoveAt(0);
            ViewBag.Themes = themes.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPost(PostVM post, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fileUpload != null)
                {
                    var res = ImageHelper.ScaleImage(Image.FromStream(fileUpload.InputStream, true, true), 600, 400);
                    ImageConverter _imageConverter = new ImageConverter();
                    byte[] xByte = (byte[])_imageConverter.ConvertTo(res, typeof(byte[]));
                    post.Photo = xByte;
                }
                if (dataHelper.AddMessage(post, User.Identity.Name))
                {
                    return RedirectToAction("Index", new { page = 1, themeId = post.Theme });
                }
                else
                {
                    return View("Error");
                }
            }
            else
            {
                var themes = dataHelper.GetThemes();
                if (!User.IsInRole("admin")) themes.RemoveAt(0);
                ViewBag.Themes = themes.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                return View(post);
            }
        }

        private void SetViewBags()
        {
            ViewBag.Themes = dataHelper.GetThemes();
            ViewBag.FeaturedMessage = dataHelper.GetFeaturedPosts();
            ViewBag.LastMessage = dataHelper.GetLastPost();
        }



    }
}