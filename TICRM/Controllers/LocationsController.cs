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
    public class LocationsController : Controller
    {
        LocationManager lm = new LocationManager();

        // GET: Locations
        public ActionResult Index()
        {
            ViewBag.AccountId = lm.GetAccounts();
            return View(lm.GetLocations());
        }

        // GET: Locations/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var location = lm.GetLocation(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }


        // GET: Locations/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            var location = lm.GetLocation(id);
            ViewBag.AccountId = lm.GetAccounts().FirstOrDefault(x => x.AccountId == location.AccountId).Name;
            return PartialView("_PartialLocationDetails", location);
        }


        // GET: Locations/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            var location = lm.GetLocation(id);
            ViewBag.AccountId = lm.GetAccounts().FirstOrDefault(x=>x.AccountId == location.AccountId).Name;
            return PartialView("_PartialLocationDelete", location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            ViewBag.AddressId = new SelectList(lm.Addresses, "AddressId", "Street1");
            ViewBag.LocationTypeId = new SelectList(lm.LocationTypes, "LocationTypeId", "Name");
            ViewBag.StatusId = new SelectList(lm.Status, "StatusId", "Name");
            ViewBag.AssignedTeam = new SelectList(lm.Teams, "TeamId", "Name");
            ViewBag.AssignedUser = new SelectList(lm.Users, "UserId", "Name");
            ViewBag.AccountId = new SelectList(lm.GetAccounts(), "AccountId", "Name");
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationId,Name,AddressId,AccountId,LocationTypeId,Description,Latitude,Longitude,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] LocationDto location)
        {
            if (ModelState.IsValid)
            {
                bool condition = lm.SaveLocation(location);
                if (condition == true)
                {
                    TempData["FormSubmissionMessage"] = "Location Created successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            TempData["FormSubmissionMessage"] = "Location is not Created";
            TempData["FormSubmissionStatus"] = "success";
            ViewBag.AddressId = new SelectList(lm.Addresses, "AddressId", "Street1", location.AddressId);
            ViewBag.LocationTypeId = new SelectList(lm.LocationTypes, "LocationTypeId", "Name", location.LocationTypeId);
            ViewBag.StatusId = new SelectList(lm.Status, "StatusId", "Name", location.StatusId);
            ViewBag.AssignedTeam = new SelectList(lm.Teams, "TeamId", "Name", location.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(lm.Users, "UserId", "Name", location.AssignedUser);
            ViewBag.AccountId = new SelectList(lm.GetAccounts(), "AccountId", "Name");
            return View(location);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var location = lm.GetLocation(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressId = new SelectList(lm.Addresses, "AddressId", "Street1", location.AddressId);
            ViewBag.LocationTypeId = new SelectList(lm.LocationTypes, "LocationTypeId", "Name", location.LocationTypeId);
            ViewBag.StatusId = new SelectList(lm.Status, "StatusId", "Name", location.StatusId);
            ViewBag.AssignedTeam = new SelectList(lm.Teams, "TeamId", "Name", location.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(lm.Users, "UserId", "Name", location.AssignedUser);
            ViewBag.AccountId = new SelectList(lm.GetAccounts(), "AccountId", "Name",location.AccountId);
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationId,Name,AddressId,AccountId,LocationTypeId,Description,Latitude,Longitude,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] LocationDto location)
        {
            if (ModelState.IsValid)
            {
                bool condition = lm.SaveLocation(location, true);
                if (condition == true)
                {
                    TempData["FormSubmissionMessage"] = "Location Updated successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            TempData["FormSubmissionMessage"] = "Location is not Updated.";
            TempData["FormSubmissionStatus"] = "error";
            ViewBag.AddressId = new SelectList(lm.Addresses, "AddressId", "Street1", location.AddressId);
            ViewBag.LocationTypeId = new SelectList(lm.LocationTypes, "LocationTypeId", "Name", location.LocationTypeId);
            ViewBag.StatusId = new SelectList(lm.Status, "StatusId", "Name", location.StatusId);
            ViewBag.AssignedTeam = new SelectList(lm.Teams, "TeamId", "Name", location.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(lm.Users, "UserId", "Name", location.AssignedUser);
            ViewBag.AccountId = new SelectList(lm.GetAccounts(), "AccountId", "Name", location.AccountId);
            return View(location);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var location = lm.GetLocation(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var location = lm.GetLocation(id);
            lm.SaveLocation(location, true, true);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
