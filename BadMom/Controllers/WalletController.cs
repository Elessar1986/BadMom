using BadMom.BLL.Interfaces;
using BadMom.Helpers;
using BadMom.Helpers.DataServiceHelper.Abstract;
using BadMom.Helpers.DataServiceHelper.Concrete;
using BadMom.Models.Shared;
using BadMom.Models.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BadMom.Controllers
{
    [Authorize]
    public class WalletController : Controller
    {
        PasswordHelper passwordHelper = new PasswordHelper(20);
        IDataHelper dataHelper;
        LoggerHelper logger;
        EmailHelper emailHelper = new EmailHelper();

        public WalletController(IBadMomDataService service)
        {
            logger = new LoggerHelper(service);
            dataHelper = new DataHelper(service);
            ViewBag.ActiveMenu = "Wallet";
        }

        public ActionResult Index()
        {

            return RedirectToAction("Income");
        }

        public ActionResult Income()
        {
            try
            {
                SetVievBag();
                var income = dataHelper.GetIncomeByUser(User.Identity.Name, DateTime.Now.AddMonths(-1), DateTime.Now, 0);
                return View(income);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Income(DateTime dateTo, DateTime dateFrom, int sourceId)
        {
            try
            {
                SetVievBag();
                var income = dataHelper.GetIncomeByUser(User.Identity.Name, dateFrom, dateTo, sourceId);
                return View(income);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIncome(Income income)
        {
            try
            {
                SetVievBag();
                var incomes = dataHelper.AddIncome(income, User.Identity.Name);
                return PartialView("_Incomes", incomes);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }


        [HttpGet]
        public ActionResult ConfirmIncome(long incomeId, DateTime date, long oldevent)
        {
            try
            {
                var old = dataHelper.GetIncomeById(incomeId);
                old.Date = date;
                old.IncomeReason = null;
                old.OperationType = null;
                old.Source = null;
                dataHelper.AddIncome(old, User.Identity.Name);
                dataHelper.DeleteEvent(oldevent);
                return RedirectToAction("Income");
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult ConfirmConsumption(long consumptionId, DateTime date, long oldevent)
        {
            try
            {
                var old = dataHelper.GetConsumptionById(consumptionId);
                old.Date = date;
                old.ConsumptionReason = null;
                old.OperationType = null;
                old.Source1 = null;
                dataHelper.AddConsumption(old, User.Identity.Name);
                dataHelper.DeleteEvent(oldevent);
                return RedirectToAction("Income");
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult DeleteIncome(int incomeId)
        {
            try
            {
                SetVievBag();
                var incomes = dataHelper.DeleteIncome(incomeId, User.Identity.Name);
                return PartialView("_Incomes", incomes);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        public ActionResult Resources()
        {
            try
            {
                ViewBag.ResourceType = dataHelper.GetResourceTypeList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

                return View(dataHelper.GetSourceByUser(User.Identity.Name));
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddResource(Source source)
        {
            ViewBag.ResourceType = dataHelper.GetResourceTypeList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            var resources = dataHelper.AddSource(source, User.Identity.Name);
            return PartialView("_Resources", resources);

        }

        [HttpPost]
        public ActionResult DeleteResource(long sourceId)
        {
            ViewBag.ResourceType = dataHelper.GetResourceTypeList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            var resources = dataHelper.DeleteSource(sourceId, User.Identity.Name);
            return PartialView("_Resources", resources);

        }

        public ActionResult Consumption()
        {
            try
            {
                SetVievBag();
                var income = dataHelper.GetConsumptionByUser(User.Identity.Name, DateTime.Now.AddMonths(-1), DateTime.Now, 0);
                return View(income);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Consumption(DateTime dateTo, DateTime dateFrom, int sourceId)
        {
            try
            {
                SetVievBag();
                var income = dataHelper.GetConsumptionByUser(User.Identity.Name, dateFrom, dateTo, sourceId);
                return View(income);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddConsumption(Consumption consumption)
        {
            try
            {
                SetVievBag();
                var consumptions = dataHelper.AddConsumption(consumption, User.Identity.Name);
                return PartialView("_Consumption", consumptions);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult DeleteConsumption(int consumptionId)
        {
            try
            {
                SetVievBag();
                var consumptions = dataHelper.DeleteConsumption(consumptionId, User.Identity.Name);
                return PartialView("_Consumption", consumptions);
            }
            catch (Exception ex)
            {
                logger.ErrorMessage("305", ex);
                return View("Error", new Error() { ExDescription = ex.Message });
            }
        }

        public ActionResult Statistics()
        {

            return View();
        }

        public ActionResult ShowSourceInfo(long sourceId)
        {
            var sourceInfo = dataHelper.GetSourceInfo(sourceId, DateTime.Now.AddMonths(-1), DateTime.Now, User.Identity.Name);
            return PartialView("_SourceInfo", sourceInfo);
        }

        private void SetVievBag()
        {
            ViewBag.IncomeReason = dataHelper.GetIncomeReasonList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.ConsumptionReason = dataHelper.GetConsumptionReasonList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.OperationType = dataHelper.GetOperationTypeList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.Sources = dataHelper.GetSourceByUser(User.Identity.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList(); ;
            var list = dataHelper.GetSourceByUser(User.Identity.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            list.Add(new SelectListItem { Text = "Все", Value = "0", Selected = true });
            ViewBag.SourcesFilter = list;
        }

    }
}