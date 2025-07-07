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
    public class ReadingsController : Controller
    {
        ReadingManager rm = new ReadingManager();

        // GET: Readings
        public ActionResult Index()
        {
           
            return View(rm.GetReadings());
        }

        // GET: Readings/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reading = rm.GetReading(id);
            if (reading == null)
            {
                return HttpNotFound();
            }
            return View(reading);
        }


        // GET: Readings/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            var reading = rm.GetReading(id);
            return PartialView("_PartialReadingsDetails", reading);
        }


        // GET: Readings/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            var reading = rm.GetReading(id);
            return PartialView("_PartialReadingsDelete", reading);
        }



        // GET: Readings/Create
        public ActionResult Create()
        {
            ViewBag.ReadingTypeId = new SelectList(rm.ReadingTypes, "ReadingTypeId", "Name");
            ViewBag.ReadingUnitId = new SelectList(rm.ReadingUnits, "ReadingUnitId", "Name");
            ViewBag.StatusId = new SelectList(rm.Status, "StatusId", "Name");
            ViewBag.AssignedTeam = new SelectList(rm.Teams, "TeamId", "Name");
            ViewBag.AssignedUser = new SelectList(rm.Users, "UserId", "Name");
            return View();
        }

        // POST: Readings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReadingId,Value,ReadingTypeId,ReadingUnitId,MarginOfErrorInPercent,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] ReadingDto reading)
        {
            if (ModelState.IsValid)
            {
                bool condition = rm.SaveReading(reading);
                if (condition == true)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.ReadingTypeId = new SelectList(rm.ReadingTypes, "ReadingTypeId", "Name", reading.ReadingTypeId);
            ViewBag.ReadingUnitId = new SelectList(rm.ReadingUnits, "ReadingUnitId", "Name", reading.ReadingUnitId);
            ViewBag.StatusId = new SelectList(rm.Status, "StatusId", "Name", reading.StatusId);
            ViewBag.AssignedTeam = new SelectList(rm.Teams, "TeamId", "Name", reading.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(rm.Users, "UserId", "Name", reading.AssignedUser);
            return View(reading);
        }

        // GET: Readings/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reading = rm.GetReading(id);
            if (reading == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReadingTypeId = new SelectList(rm.ReadingTypes, "ReadingTypeId", "Name", reading.ReadingTypeId);
            ViewBag.ReadingUnitId = new SelectList(rm.ReadingUnits, "ReadingUnitId", "Name", reading.ReadingUnitId);
            ViewBag.StatusId = new SelectList(rm.Status, "StatusId", "Name", reading.StatusId);
            ViewBag.AssignedTeam = new SelectList(rm.Teams, "TeamId", "Name", reading.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(rm.Users, "UserId", "Name", reading.AssignedUser);
            return View(reading);
        }

        // POST: Readings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReadingId,Value,ReadingTypeId,ReadingUnitId,MarginOfErrorInPercent,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] ReadingDto reading)
        {
            if (ModelState.IsValid)
            {
                bool condition = rm.SaveReading(reading, true);
                if (condition == true)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.ReadingTypeId = new SelectList(rm.ReadingTypes, "ReadingTypeId", "Name", reading.ReadingTypeId);
            ViewBag.ReadingUnitId = new SelectList(rm.ReadingUnits, "ReadingUnitId", "Name", reading.ReadingUnitId);
            ViewBag.StatusId = new SelectList(rm.Status, "StatusId", "Name", reading.StatusId);
            ViewBag.AssignedTeam = new SelectList(rm.Teams, "TeamId", "Name", reading.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(rm.Users, "UserId", "Name", reading.AssignedUser);
            return View(reading);
        }

        // GET: Readings/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reading = rm.GetReading(id);
            if (reading == null)
            {
                return HttpNotFound();
            }
            return View(reading);
        }

        // POST: Readings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var reading = rm.GetReading(id);
            rm.SaveReading(reading, true, true);
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
