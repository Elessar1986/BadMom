using BadMom.BLL.Interfaces;
using BadMom.Helpers;
using BadMom.Helpers.DataServiceHelper.Abstract;
using BadMom.Helpers.DataServiceHelper.Concrete;
using BadMom.Models.Advert;
using BadMom.Models.Shared;
using BadMom.Models.Wallet;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace BadMom.Controllers
{
    public class AdvertController : Controller
    {

        PasswordHelper passwordHelper = new PasswordHelper(20);
        IDataHelper dataHelper;
        LoggerHelper logger;
        EmailHelper emailHelper = new EmailHelper();

        public AdvertController(IBadMomDataService service)
        {
            logger = new LoggerHelper(service);
            dataHelper = new DataHelper(service);
            ViewBag.ActiveMenu = "Advert";
            
        }

        public ActionResult Index(string search, int? page, int categoryId = 0, string order = "default",int onpage = 6, string filter = "all")
        {
            try
            {
                var adverts = dataHelper.GetAdvertsByCategoryId(categoryId, order); 
                if (!string.IsNullOrEmpty(search))
                {
                    adverts = adverts.Where(x => x.Title.Contains(search)).ToList();
                    ViewBag.search = search;
                    onpage = adverts.Count > 0 ? adverts.Count : 1;
                    //order = "default";
                    //filter = "all";
                }
                ViewBag.categoryId = categoryId;
                ViewBag.order = order;
                ViewBag.onpage = onpage;
                ViewBag.filter = filter;
                switch (filter)
                {
                    case "new":
                        adverts = adverts.Where(x => x.New == true).ToList();
                        break;
                    case "old":
                        adverts = adverts.Where(x => x.New == false).ToList();
                        break;  
                    default:
                        break;
                }
                
                SetViewBag();
                int pageSize = onpage;
                int pageNumber = (page ?? 1);
                return View(adverts.ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", $"Error: {ex.Message} | Inner: {ex.InnerException.Message}");
                return View("Error", new Error() { ExDescription = ex.Message, ExInnerDescription = ex.InnerException.Message });
            }
        }

        public ActionResult ShowAdvert(long? id)
        {
            try
            {
                var advert = dataHelper.GetAdvert(id.HasValue ? id.Value : throw new Exception("Method: 'ShowAdvert' -> id == null", new Exception("none")));
                SetViewBag();
                return View(advert);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", $"Error: {ex.Message} | Inner: {ex.InnerException.Message}");
                return View("Error", new Error() { ExDescription = ex.Message, ExInnerDescription = ex.InnerException.Message });
            }
        }

        [Authorize]
        public ActionResult AddAdvert()
        {
            try
            {
                SetViewBag();
                return View();
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", $"Error: {ex.Message} | Inner: {ex.InnerException.Message}");
                return View("Error", new Error() { ExDescription = ex.Message, ExInnerDescription = ex.InnerException.Message });
            }
        }

        [Authorize]
        public ActionResult DeleteAdvert(long id, bool? admin)
        {
            try
            {
                dataHelper.DeleteAdvert(id);
                if(admin.HasValue && admin.Value == true) return RedirectToAction("Index", "Advert");
                return RedirectToAction("MyAdverts","User");
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", $"Error: {ex.Message} | Inner: {ex.InnerException.Message}");
                return View("Error", new Error() { ExDescription = ex.Message, ExInnerDescription = ex.InnerException.Message });
            }
        }



        [Authorize]
        [HttpPost]
        public ActionResult AddAdvert(AdvertVM advert, HttpPostedFileBase fileUpload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (fileUpload != null)
                    {
                        var res = ImageHelper.ScaleImage(Image.FromStream(fileUpload.InputStream, true, true), 300, 200);
                        ImageConverter _imageConverter = new ImageConverter();
                        byte[] xByte = (byte[])_imageConverter.ConvertTo(res, typeof(byte[]));
                        advert.Photo = xByte;
                    }
                    if (dataHelper.AddAdvert(advert, User.Identity.Name))
                    {
                        return RedirectToAction("Index", "Advert");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {
                    SetViewBag();
                    return View(advert);
                }
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", $"Error: {ex.Message} | Inner: {ex.InnerException.Message}");
                return View("Error", new Error() { ExDescription = ex.Message, ExInnerDescription = ex.InnerException.Message });
            }
        }

        [Authorize]
        public ActionResult EditAdvert(long id)
        {
            try
            {
                var advert = dataHelper.GetAdvert(id);
                ViewBag.Edit = true;
                SetViewBag();
                return View("AddAdvert",advert);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", $"Error: {ex.Message} | Inner: {ex.InnerException.Message}");
                return View("Error", new Error() { ExDescription = ex.Message, ExInnerDescription = ex.InnerException.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditAdvert(AdvertVM advert, HttpPostedFileBase fileUpload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (fileUpload != null)
                    {
                        var res = ImageHelper.ScaleImage(Image.FromStream(fileUpload.InputStream, true, true), 300, 200);
                        ImageConverter _imageConverter = new ImageConverter();
                        byte[] xByte = (byte[])_imageConverter.ConvertTo(res, typeof(byte[]));
                        advert.Photo = xByte;
                    }
                    if (dataHelper.EditAdvert(advert))
                    {
                        return RedirectToAction("Index", "Advert");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {
                    SetViewBag();
                    return View(advert);
                }
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", $"Error: {ex.Message} | Inner: {ex.InnerException.Message}");
                return View("Error", new Error() { ExDescription = ex.Message, ExInnerDescription = ex.InnerException.Message });
            }
        }

        //[Authorize]
        public ActionResult ToFavorite(long advertId, long userId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated) return Json(new { error = "В избранное может добавлять только зарегистрированный пользователь!" }, JsonRequestBehavior.AllowGet);
                dataHelper.AddToFavorite(advertId, userId);
                SetViewBag();
                return PartialView("Favorite", new Favorite() { AdvertId = advertId, UserId = userId });
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", $"Error: {ex.Message} | Inner: {ex.InnerException.Message}");
                return View("Error", new Error() { ExDescription = ex.Message, ExInnerDescription = ex.InnerException.Message });
            }
            
        }

        [Authorize]
        public ActionResult FromFavorite(long advertId, long userId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated) return Json(new { error = "В избранное может добавлять только зарегистрированный пользователь!" }, JsonRequestBehavior.AllowGet);
                dataHelper.DeleteFromFavorite(advertId, userId);
                SetViewBag();
                return PartialView("Favorite", new Favorite() { AdvertId = advertId, UserId = userId });
                
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", $"Error: {ex.Message} | Inner: {ex.InnerException.Message}");
                return View("Error", new Error() { ExDescription = ex.Message, ExInnerDescription = ex.InnerException.Message });
            }

        }

        private void SetViewBag()
        {
            var user = dataHelper.GetUserData(User.Identity.Name);
            ViewBag.User = user;
            ViewBag.Categories = dataHelper.GetAllCategories().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == 1 ? true : false }).ToList();
        }
    }
}