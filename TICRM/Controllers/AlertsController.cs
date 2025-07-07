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
    public class AlertsController : Controller
    {
        AlertManager am = new AlertManager();

        // GET: Alerts
        public ActionResult Index()
        {
            // var alerts = db.Alerts.Include(a => a.Status).Include(a => a.Team).Include(a => a.Urgency).Include(a => a.User);
            return View(am.GetAlerts());
        }

        // GET: Alerts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var alert = am.GetAlert(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            return View(alert);
        }



        // GET: Alerts/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            var alert = am.GetAlert(id);
            return PartialView("_PartialAlertsDetails", alert);
        }

        // GET: Alerts/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            var alert = am.GetAlert(id);
            return PartialView("_PartialAlertsDelete", alert);
        }


        // GET: Alerts/Create
        public ActionResult Create()
        {
            ViewBag.StatusId = new SelectList(am.Status, "StatusId", "Name");
            ViewBag.AssignedTeam = new SelectList(am.Teams, "TeamId", "Name");
            ViewBag.UrgencyId = new SelectList(am.Urgencies, "UrgencyId", "Name");
            ViewBag.AssignedUser = new SelectList(am.Users, "UserId", "Name");
            return View();
        }

        // POST: Alerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlertId,Title,UrgencyId,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] AlertDto alert)
        {
            if (ModelState.IsValid)
            {
                // alert.AlertId = Guid.NewGuid();
                alert.CreatedBy = User.Identity.Name;
                alert.CreatedDate = DateTime.Now;
                bool condition = am.SaveAlert(alert);
                if (condition == true)
                {
                    TempData["FormSubmissionMessage"] = "Alert Created successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            TempData["FormSubmissionMessage"] = "Alert is not Created.";
            TempData["FormSubmissionStatus"] = "error";
            ViewBag.StatusId = new SelectList(am.Status, "StatusId", "Name", alert.StatusId);
            ViewBag.AssignedTeam = new SelectList(am.Teams, "TeamId", "Name", alert.AssignedTeam);
            ViewBag.UrgencyId = new SelectList(am.Urgencies, "UrgencyId", "Name", alert.UrgencyId);
            ViewBag.AssignedUser = new SelectList(am.Users, "UserId", "Name", alert.AssignedUser);
            return View(alert);
        }

        // GET: Alerts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var alert = am.GetAlert(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusId = new SelectList(am.Status, "StatusId", "Name", alert.StatusId);
            ViewBag.AssignedTeam = new SelectList(am.Teams, "TeamId", "Name", alert.AssignedTeam);
            ViewBag.UrgencyId = new SelectList(am.Urgencies, "UrgencyId", "Name", alert.UrgencyId);
            ViewBag.AssignedUser = new SelectList(am.Users, "UserId", "Name", alert.AssignedUser);
            return View(alert);
        }

        // POST: Alerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlertId,Title,UrgencyId,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] AlertDto alert)
        {
            if (ModelState.IsValid)
            {
                alert.UpdatedBy = User.Identity.Name;
                alert.UpdatedDate = DateTime.Now;
                bool condition = am.SaveAlert(alert, true);
                if (condition == true)
                {
                    TempData["FormSubmissionMessage"] = "Alert Updated successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }

            TempData["FormSubmissionMessage"] = "Alert is not Updated.";
            TempData["FormSubmissionStatus"] = "error";

            ViewBag.StatusId = new SelectList(am.Status, "StatusId", "Name", alert.StatusId);
            ViewBag.AssignedTeam = new SelectList(am.Teams, "TeamId", "Name", alert.AssignedTeam);
            ViewBag.UrgencyId = new SelectList(am.Urgencies, "UrgencyId", "Name", alert.UrgencyId);
            ViewBag.AssignedUser = new SelectList(am.Users, "UserId", "Name", alert.AssignedUser);
            return View(alert);
        }

        // GET: Alerts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var alert = am.GetAlert(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            return View(alert);
        }

        // POST: Alerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var alert = am.GetAlert(id);

            am.SaveAlert(alert, true, true);
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
