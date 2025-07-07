using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TICRM;
using TICRM.BuisnessLayer;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.Controllers
{
    [Authorize]
    public class CustomerAssetsController : Controller
    {
        CustomerAssetManager cam = new CustomerAssetManager();
        LocationManager locationManager = new LocationManager();

        // GET: CustomerAssets
        public ActionResult Index()
        {
            ViewBag.AccountId = cam.Accounts;
            ViewBag.LocationId = locationManager.GetLocations();
            return View(cam.GetCustomerAssets());
        }

        // GET: CustomerAssets/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customerAsset = cam.GetCustomerAsset(id);
            if (customerAsset == null)
            {
                return HttpNotFound();
            }
            return View(customerAsset);
        }

        // GET: CustomerAssets/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            var customerAsset = cam.GetCustomerAsset(id);
            
            ViewBag.AccountId = (string)cam.Accounts.Where(x=>x.AccountId == customerAsset.AccountId).FirstOrDefault().Name;
            ViewBag.LocationId = (string)locationManager.GetLocation(customerAsset.LocationId).Name;

            return PartialView("_PartialCustomerAssetsDetails", customerAsset);
        }
        // GET: CustomerAssets/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            var customerAsset = cam.GetCustomerAsset(id); // get customer assets on id
            ViewBag.AccountId = (string)cam.Accounts.Where(x => x.AccountId == customerAsset.AccountId).FirstOrDefault().Name; // get name of account
            ViewBag.LocationId = (string)locationManager.GetLocation(customerAsset.LocationId).Name; // get name of location which is associate with account
            return PartialView("_PartialCustomerAssetsDelete", customerAsset);
        }



        // Get: GetLocationOfAccount
        public JsonResult GetLocationOfAccount(Guid accountId)
        {
            
            SelectList data = new SelectList(locationManager.GetLocations(accountId), "LocationId", "Name");
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        // GET: CustomerAssets/Create
        public ActionResult Create()
        {
            ViewBag.CustomerAssetTypeId = new SelectList(cam.CustomerAssetTypes, "CustomerAssetTypeId", "Name");
            ViewBag.StatusId = new SelectList(cam.Status, "StatusId", "Name");
            ViewBag.AssignedTeam = new SelectList(cam.Teams, "TeamId", "Name");
            ViewBag.AssignedUser = new SelectList(cam.Users, "UserId", "Name");

            ViewBag.AccountId = new SelectList(cam.Accounts, "AccountId", "Name");
            ViewBag.LocationId = new SelectList(new List<LocationDto>(), "LocationId", "Name");
            return View();
        }

        // POST: CustomerAssets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerAssetId,Title,CustomerAssetTypeId,Manufacture,Model,YearOfManufacture,Value,DepriciatedValue,SKU,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,AccountId,LocationId")] CustomerAssetDto customerAsset)
        {
            if (ModelState.IsValid)
            {
                customerAsset.CustomerAssetId = Guid.NewGuid();
                bool condition = cam.SaveCustomerAsset(customerAsset);
                if (condition == true)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.CustomerAssetTypeId = new SelectList(cam.CustomerAssetTypes, "CustomerAssetTypeId", "Name", customerAsset.CustomerAssetTypeId);
            ViewBag.StatusId = new SelectList(cam.Status, "StatusId", "Name", customerAsset.StatusId);
            ViewBag.AssignedTeam = new SelectList(cam.Teams, "TeamId", "Name", customerAsset.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(cam.Users, "UserId", "Name", customerAsset.AssignedUser);
            ViewBag.AccountId = new SelectList(cam.Accounts, "AccountId", "Name",customerAsset.AccountId);
            ViewBag.LocationId = new SelectList(locationManager.GetLocations((Guid)customerAsset.AccountId), "LocationId", "Name",customerAsset.LocationId);
            return View(customerAsset);
        }

        // GET: CustomerAssets/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customerAsset = cam.GetCustomerAsset(id);
            if (customerAsset == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerAssetTypeId = new SelectList(cam.CustomerAssetTypes, "CustomerAssetTypeId", "Name", customerAsset.CustomerAssetTypeId);
            ViewBag.StatusId = new SelectList(cam.Status, "StatusId", "Name", customerAsset.StatusId);
            ViewBag.AssignedTeam = new SelectList(cam.Teams, "TeamId", "Name", customerAsset.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(cam.Users, "UserId", "Name", customerAsset.AssignedUser);
            ViewBag.AccountId = new SelectList(cam.Accounts, "AccountId", "Name", customerAsset.AccountId);
            ViewBag.LocationId = new SelectList(locationManager.GetLocations((Guid)customerAsset.AccountId), "LocationId", "Name", customerAsset.LocationId);
            return View(customerAsset);
        }

        // POST: CustomerAssets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerAssetId,Title,CustomerAssetTypeId,Manufacture,Model,YearOfManufacture,Value,DepriciatedValue,SKU,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,AccountId,LocationId")] CustomerAssetDto customerAsset)
        {
            if (ModelState.IsValid)
            {
                bool condition = cam.SaveCustomerAsset(customerAsset, true);
                if (condition == true)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CustomerAssetTypeId = new SelectList(cam.CustomerAssetTypes, "CustomerAssetTypeId", "Name", customerAsset.CustomerAssetTypeId);
            ViewBag.StatusId = new SelectList(cam.Status, "StatusId", "Name", customerAsset.StatusId);
            ViewBag.AssignedTeam = new SelectList(cam.Teams, "TeamId", "Name", customerAsset.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(cam.Users, "UserId", "Name", customerAsset.AssignedUser);
            ViewBag.AccountId = new SelectList(cam.Accounts, "AccountId", "Name", customerAsset.AccountId);
            ViewBag.LocationId = new SelectList(locationManager.GetLocations((Guid)customerAsset.AccountId), "LocationId", "Name", customerAsset.LocationId);
            return View(customerAsset);
        }

        // GET: CustomerAssets/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customerAsset = cam.GetCustomerAsset(id);
            if (customerAsset == null)
            {
                return HttpNotFound();
            }
            return View(customerAsset);
        }

        // POST: CustomerAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var customerAsset = cam.GetCustomerAsset(id);
            cam.SaveCustomerAsset(customerAsset, true, true);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               // db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
