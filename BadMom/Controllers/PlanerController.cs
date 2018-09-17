using BadMom.BLL.Interfaces;
using BadMom.Helpers;
using BadMom.Helpers.DataServiceHelper.Abstract;
using BadMom.Helpers.DataServiceHelper.Concrete;
using BadMom.Models.Planer;
using BadMom.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BadMom.Controllers
{
    [Authorize]
    public class PlanerController : Controller
    {

        PasswordHelper passwordHelper = new PasswordHelper(20);
        IDataHelper dataHelper;
        LoggerHelper logger;
        EmailHelper emailHelper = new EmailHelper();

        public PlanerController(IBadMomDataService service)
        {
            logger = new LoggerHelper(service);
            dataHelper = new DataHelper(service);
            ViewBag.ActiveMenu = "Planer";
        }

        public ActionResult Index(DateTime? showDate)
        {
            try
            {
                //showDate.HasValue ? showDate.Value :
                ViewBag.ShowDate =  DateTime.Now.AddMonths(-2).ToShortDateString();
                var Remind = dataHelper.GetEventsByUser(User.Identity.Name).Where(x => x.DateStart.ToShortDateString() == DateTime.Now.ToShortDateString() && x.Remind).ToList();
                ViewBag.Remind = Remind;
                return View();
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", $"Error: {ex.Message} | Inner: {ex.InnerException.Message}");
                return View("Error", new Error() { ExDescription = ex.Message, ExInnerDescription = ex.InnerException.Message + " ||| " + ex.InnerException.InnerException});
            }
        }

        public JsonResult GetUserEvents()
        {
            try
            {
                var events = dataHelper.GetEventsByUser(User.Identity.Name);
                //object e = events.Select(x => new { Title = x.Title, Description = x.Description, DateStart = x.DateStart, DateEnd = x.DateEnd.Value , Id = x.Id, Color = x.EventType.Color });
                return Json(events, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", $"Error: {ex.Message} | Inner: {ex.InnerException.Message}");
                return Json(new { error = ex.Message, JsonRequestBehavior.AllowGet });
            }
        }

        public ActionResult GetEventById(long id)
        {
            try
            {
                var showevent = dataHelper.GetEventsByUser(User.Identity.Name).Where(x => x.Id == id).First();
                return PartialView("ShowEvent", showevent);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", $"Error: {ex.Message} | Inner: {ex.InnerException.Message}");
                return View("Error", new Error() { ExDescription = ex.Message, ExInnerDescription = ex.InnerException.Message + " ||| " + ex.InnerException.InnerException });
            }
        }

        public ActionResult AddEvent(DateTime dateStart, long? advertId, string title)
        {
            ViewBag.Sources = dataHelper.GetSourceByUser(User.Identity.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.EventType = dataHelper.GetEventTypes().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return PartialView("AddEvent", new EventVM { DateStart = dateStart, AdvertId = advertId , Title = string.IsNullOrEmpty(title) ? null : title });
        }

        [HttpPost]
        public ActionResult AddEvent(EventVM eventVM, DateTime? TimeStart, DateTime? TimeEnd)
        {
            try
            {
                if (TimeStart.HasValue) eventVM.DateStart = eventVM.DateStart.AddHours(TimeStart.Value.Hour).AddMinutes(TimeStart.Value.Minute);
                if (eventVM.DateEnd.HasValue && TimeEnd.HasValue) eventVM.DateEnd = eventVM.DateEnd.Value.AddHours(TimeEnd.Value.Hour).AddMinutes(TimeEnd.Value.Minute);
                dataHelper.AddEvent(eventVM, User.Identity.Name);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", $"Error: {ex.Message} | Inner: {ex.InnerException.Message}");
                return View("Error", new Error() { ExDescription = ex.Message, ExInnerDescription = ex.InnerException.Message + " ||| " + ex.InnerException.InnerException });
            }
        }


        public ActionResult DeleteEvent(long id)
        {
            try
            {
                dataHelper.DeleteEvent(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", $"Error: {ex.Message} | Inner: {ex.InnerException.Message}");
                return View("Error", new Error() { ExDescription = ex.Message, ExInnerDescription = ex.InnerException.Message + " ||| " + ex.InnerException.InnerException });
            }
        }


    }
}