using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TICRM.BuisnessLayer;
using TICRM.CRMFilters;
using TICRM.DTOs;
using static TICRM.ViewModels.JSTreeViewModel;

namespace TICRM.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private AccountManager am = new AccountManager();
        private DeviceManager deviceManager = new DeviceManager();
        private OpportunityManager om = new OpportunityManager();
        // GET: Accounts
      

        public ActionResult Index()
        {
            //;
            //var accounts = db.Accounts.Include(a => a.AccountSize).Include(a => a.AccountType).Include(a => a.Address).Include(a => a.Address1).Include(a => a.Industry).Include(a => a.Status).Include(a => a.Team).Include(a => a.User);
            // ignore deleted entries
            return View(am.GetAccounts());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountDto account = am.GetAccount(id);
            AccountViewModel accWithDetail = am.GetAccountAndDetails(id.Value);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(accWithDetail);
        }

        public JsonResult GetTreeOfDevices(Guid assetId)
        {
            
            List<DeviceDto> deviceList = deviceManager.GetDevicesOnAssetsId(assetId);
            if (deviceList.Count == 0)
            {

                return Json("NoData", JsonRequestBehavior.AllowGet);
            }
            return Json(deviceList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetAllAccountAssociate(Guid accountId)
        {
            //var data = am.GetAccount();
            AccountViewModel accWithDetail = am.GetAccountAndDetails(accountId);
            return Json(accWithDetail, JsonRequestBehavior.AllowGet);
        }


        // GET: Accounts/AccountsDetail/5
        public ActionResult AccountsDetail(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountDto account = am.GetAccount(id);
            AccountViewModel accWithDetail = am.GetAccountAndDetails(id.Value);

            if (account == null)
            {
                return HttpNotFound();
            }
            return View(accWithDetail);
        }


        public JsonResult GetAssetsOfLocation(Guid locationId)
        {
            CustomerAssetManager customerAsset = new CustomerAssetManager();
            return Json(customerAsset.GetLocationAssets(locationId), JsonRequestBehavior.AllowGet);
        }


        public JsonResult DSGonDeviceId(Guid deviceId)
        {

            DeviceSensorGraphManager deviceSensor = new DeviceSensorGraphManager();

            return Json(deviceSensor.GetDeviceSensorGraphList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Accounts/Details/5
        public JsonResult GetAccountDetails(Guid? id)
        {
            if (id == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            AccountDto account = am.GetAccount(id);
            AccountViewModel accWithDetail = am.GetAccountAndDetails(id.Value);
            if (account == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            return Json(accWithDetail, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AccountDetailsPartial(Guid? id)
        {
            AccountDto account = am.GetAccount(id);
            AccountViewModel accWithDetail = am.GetAccountAndDetails(id.Value);
            return PartialView("_PartialAccountDetailOnId", accWithDetail);
        }

        public ActionResult GetOppertunityDetailOnId(Guid? id)
        {
            OpportunityDto opportunity = om.GetOpportunity(id);
            return PartialView("~/Views/Opportunities/_PartialRightSideDetail.cshtml", opportunity);
        }

        public JsonResult GetAllRepairedDevices()
        {
            return Json(deviceManager.GetDevices(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllServicesDevices()
        {
            return Json(deviceManager.GetDevices(), JsonRequestBehavior.AllowGet);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            ViewBag.AccountSizeId = new SelectList(am.AccountSizes, "AccountSizeId", "Name");
            ViewBag.AccountTypeId = new SelectList(am.AccountTypes, "AccountTypeId", "Name");
            ViewBag.ShippingAddress = new SelectList(am.Addresses, "AddressId", "Street1");
            ViewBag.BillingAddress = new SelectList(am.Addresses, "AddressId", "Street1");
            ViewBag.IndustryId = new SelectList(am.Industries, "IndustryId", "Name");
            ViewBag.StatusId = new SelectList(am.Status, "StatusId", "Name");
            ViewBag.AssignedTeam = new SelectList(am.Teams, "TeamId", "Name");
            ViewBag.AssignedUser = new SelectList(am.Users, "UserId", "Name");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AccountActionFilter]
        public ActionResult Create([Bind(Include = "AccountId,Name,ShippingAddress,BillingAddress,AccountTypeId,PhoneOffice,Email,Fax,WebSite,AccountSizeId,IndustryId,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] AccountDto account, bool IsEventSchedule)
        {
            if (ModelState.IsValid)
            {
                // pass current userid
                string CurrentUserId = User.Identity.GetUserId();
                am.SaveAccount(account, CurrentUserId);
                return RedirectToAction("Index");
            }

            ViewBag.AccountSizeId = new SelectList(am.AccountSizes, "AccountSizeId", "Name", account.AccountSizeId);
            ViewBag.AccountTypeId = new SelectList(am.AccountTypes, "AccountTypeId", "Name", account.AccountTypeId);
            ViewBag.ShippingAddress = new SelectList(am.Addresses, "AddressId", "Street1", account.ShippingAddress);
            ViewBag.BillingAddress = new SelectList(am.Addresses, "AddressId", "Street1", account.BillingAddress);
            ViewBag.IndustryId = new SelectList(am.Industries, "IndustryId", "Name", account.IndustryId);
            ViewBag.StatusId = new SelectList(am.Status, "StatusId", "Name", account.StatusId);
            ViewBag.AssignedTeam = new SelectList(am.Teams, "TeamId", "Name", account.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(am.Users, "UserId", "Name", account.AssignedUser);
            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountDto account = am.GetAccount(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountSizeId = new SelectList(am.AccountSizes, "AccountSizeId", "Name", account.AccountSizeId);
            ViewBag.AccountTypeId = new SelectList(am.AccountTypes, "AccountTypeId", "Name", account.AccountTypeId);
            ViewBag.ShippingAddress = new SelectList(am.Addresses, "AddressId", "Street1", account.ShippingAddress);
            ViewBag.BillingAddress = new SelectList(am.Addresses, "AddressId", "Street1", account.BillingAddress);
            ViewBag.IndustryId = new SelectList(am.Industries, "IndustryId", "Name", account.IndustryId);
            ViewBag.StatusId = new SelectList(am.Status, "StatusId", "Name", account.StatusId);
            ViewBag.AssignedTeam = new SelectList(am.Teams, "TeamId", "Name", account.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(am.Users, "UserId", "Name", account.AssignedUser);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AccountActionFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountId,Name,ShippingAddress,BillingAddress,AccountTypeId,PhoneOffice,Email,Fax,WebSite,AccountSizeId,IndustryId,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] AccountDto account)
        {
            if (ModelState.IsValid)
            {
                // pass current userid
                string CurrentUserId = User.Identity.GetUserId();
                am.SaveAccount(account, CurrentUserId, true);
                return RedirectToAction("Index");
            }
            ViewBag.AccountSizeId = new SelectList(am.AccountSizes, "AccountSizeId", "Name", account.AccountSizeId);
            ViewBag.AccountTypeId = new SelectList(am.AccountTypes, "AccountTypeId", "Name", account.AccountTypeId);
            ViewBag.ShippingAddress = new SelectList(am.Addresses, "AddressId", "Street1", account.ShippingAddress);
            ViewBag.BillingAddress = new SelectList(am.Addresses, "AddressId", "Street1", account.BillingAddress);
            ViewBag.IndustryId = new SelectList(am.Industries, "IndustryId", "Name", account.IndustryId);
            ViewBag.StatusId = new SelectList(am.Status, "StatusId", "Name", account.StatusId);
            ViewBag.AssignedTeam = new SelectList(am.Teams, "TeamId", "Name", account.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(am.Users, "UserId", "Name", account.AssignedUser);
            return View(account);
        }

        // GET: Devices/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            AccountDto account = am.GetAccount(id);
            return PartialView("_PartialAccountsDelete", account);
        }


        // GET: Accounts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountDto account = am.GetAccount(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AccountDto account = am.GetAccount(id);
            // pass current userid
            string CurrentUserId = User.Identity.GetUserId();
            //soft delete for account
            am.SaveAccount(account, CurrentUserId, true, true);
            //db.Accounts.Remove(account);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // db.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
    }
}