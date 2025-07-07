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
    public class ServiceCallsController : Controller
    {
        ServiceCallManager scm = new ServiceCallManager();

        // GET: ServiceCalls
        public ActionResult Index()
        {
           
            return View(scm.GetServiceCalls());
        }

        // GET: ServiceCalls/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var serviceCall = scm.GetServiceCall(id);
            if (serviceCall == null)
            {
                return HttpNotFound();
            }
            return View(serviceCall);
        }

        
        // GET: Devices/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            var serviceCall = scm.GetServiceCall(id);
            return PartialView("_PartialServiceCallsDetails", serviceCall);
        }


        // GET: Devices/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            var serviceCall = scm.GetServiceCall(id);
            return PartialView("_PartialServiceCallsDelete", serviceCall);
        }

        // GET: ServiceCalls/Create
        public ActionResult Create()
        {
            ViewBag.StatusId = new SelectList(scm.Status, "StatusId", "Name");
            ViewBag.AssignedTeam = new SelectList(scm.Teams, "TeamId", "Name");
            ViewBag.UrgencyId = new SelectList(scm.Urgencies, "UrgencyId", "Name");
            ViewBag.AssignedUser = new SelectList(scm.Users, "UserId", "Name");
            ViewBag.ServiceCallStageId = new SelectList(scm.WorkStages, "WorkStageId", "Name");
            return View();
        }

        // POST: ServiceCalls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceCallId,Title,Detail,UrgencyId,ServiceCallStageId,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] ServiceCallDto serviceCall)
        {
            if (ModelState.IsValid)
            {
                bool condition = scm.SaveServiceCall(serviceCall);
                if (condition == true)
                {
                    TempData["FormSubmissionMessage"] = serviceCall.Title + " Service Call Created successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            TempData["FormSubmissionMessage"] = serviceCall.Title + " Service Call not Created.";
            TempData["FormSubmissionStatus"] = "error";
            ViewBag.StatusId = new SelectList(scm.Status, "StatusId", "Name", serviceCall.StatusId);
            ViewBag.AssignedTeam = new SelectList(scm.Teams, "TeamId", "Name", serviceCall.AssignedTeam);
            ViewBag.UrgencyId = new SelectList(scm.Urgencies, "UrgencyId", "Name", serviceCall.UrgencyId);
            ViewBag.AssignedUser = new SelectList(scm.Users, "UserId", "Name", serviceCall.AssignedUser);
            ViewBag.ServiceCallStageId = new SelectList(scm.WorkStages, "WorkStageId", "Name", serviceCall.ServiceCallStageId);
            return View(serviceCall);
        }

        // GET: ServiceCalls/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var serviceCall = scm.GetServiceCall(id);
            if (serviceCall == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusId = new SelectList(scm.Status, "StatusId", "Name", serviceCall.StatusId);
            ViewBag.AssignedTeam = new SelectList(scm.Teams, "TeamId", "Name", serviceCall.AssignedTeam);
            ViewBag.UrgencyId = new SelectList(scm.Urgencies, "UrgencyId", "Name", serviceCall.UrgencyId);
            ViewBag.AssignedUser = new SelectList(scm.Users, "UserId", "Name", serviceCall.AssignedUser);
            ViewBag.ServiceCallStageId = new SelectList(scm.WorkStages, "WorkStageId", "Name", serviceCall.ServiceCallStageId);
            return View(serviceCall);
        }

        // POST: ServiceCalls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceCallId,Title,Detail,UrgencyId,ServiceCallStageId,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] ServiceCallDto serviceCall)
        {
            if (ModelState.IsValid)
            {
                bool condition = scm.SaveServiceCall(serviceCall, true);
                if (condition == true)
                {
                    TempData["FormSubmissionMessage"] = serviceCall.Title + " Service Call Updated successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            TempData["FormSubmissionMessage"] = "Service Call not Updated.";
            TempData["FormSubmissionStatus"] = "error";
            ViewBag.StatusId = new SelectList(scm.Status, "StatusId", "Name", serviceCall.StatusId);
            ViewBag.AssignedTeam = new SelectList(scm.Teams, "TeamId", "Name", serviceCall.AssignedTeam);
            ViewBag.UrgencyId = new SelectList(scm.Urgencies, "UrgencyId", "Name", serviceCall.UrgencyId);
            ViewBag.AssignedUser = new SelectList(scm.Users, "UserId", "Name", serviceCall.AssignedUser);
            ViewBag.ServiceCallStageId = new SelectList(scm.WorkStages, "WorkStageId", "Name", serviceCall.ServiceCallStageId);
            return View(serviceCall);
        }

        // GET: ServiceCalls/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var serviceCall = scm.GetServiceCall(id);
            if (serviceCall == null)
            {
                return HttpNotFound();
            }
            return View(serviceCall);
        }

        // POST: ServiceCalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var serviceCall = scm.GetServiceCall(id);
            scm.SaveServiceCall(serviceCall, true, true);
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
