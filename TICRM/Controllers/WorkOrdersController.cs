using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TICRM.BuisnessLayer;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.Controllers
{
    public class WorkOrdersController : Controller
    {
        WorkOrderManager wom = new WorkOrderManager();

        // GET: WorkOrders
        public ActionResult Index()
        {
            return View(wom.GetWorkOrders());
        }

        // GET: WorkOrders/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var workOrder = wom.GetWorkOrder(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            return View(workOrder);
        }






        // GET: Devices/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            var workOrder = wom.GetWorkOrder(id);
            return PartialView("_PartialWorkOrderDetails", workOrder);
        }

        // GET: Devices/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            var workOrder = wom.GetWorkOrder(id);
            return PartialView("_PartialWorkOrderDelete", workOrder);
        }




        // GET: WorkOrders/Create
        public ActionResult Create()
        {
            ViewBag.StatusId = new SelectList(wom.Status, "StatusId", "Name");
            ViewBag.AssignedTeam = new SelectList(wom.Teams, "TeamId", "Name");
            ViewBag.AssignedUser = new SelectList(wom.Users, "UserId", "Name");
            ViewBag.WorkOrderStageId = new SelectList(wom.WorkStages, "WorkStageId", "Name");
            return View();
        }

        // POST: WorkOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkOrderId,Title,NTE,WorkOrderStageId,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] WorkOrderDto workOrder)
        {
            if (ModelState.IsValid)
            {
                bool condition = wom.SaveWorkOrder(workOrder);
                if (condition == true)
                {
                    TempData["FormSubmissionMessage"] = "WorkOrder is Created successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            TempData["FormSubmissionMessage"] = "WorkOrder is not Created.";
            TempData["FormSubmissionStatus"] = "error";
            ViewBag.StatusId = new SelectList(wom.Status, "StatusId", "Name", workOrder.StatusId);
            ViewBag.AssignedTeam = new SelectList(wom.Teams, "TeamId", "Name", workOrder.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(wom.Users, "UserId", "Name", workOrder.AssignedUser);
            ViewBag.WorkOrderStageId = new SelectList(wom.WorkStages, "WorkStageId", "Name", workOrder.WorkOrderStageId);
            return View(workOrder);
        }

        // GET: WorkOrders/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var workOrder = wom.GetWorkOrder(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusId = new SelectList(wom.Status, "StatusId", "Name", workOrder.StatusId);
            ViewBag.AssignedTeam = new SelectList(wom.Teams, "TeamId", "Name", workOrder.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(wom.Users, "UserId", "Name", workOrder.AssignedUser);
            ViewBag.WorkOrderStageId = new SelectList(wom.WorkStages, "WorkStageId", "Name", workOrder.WorkOrderStageId);
            return View(workOrder);
        }

        // POST: WorkOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkOrderId,Title,NTE,WorkOrderStageId,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] WorkOrderDto workOrder)
        {
            if (ModelState.IsValid)
            {
                bool condition = wom.SaveWorkOrder(workOrder, true);
                if (condition == true)
                {
                    TempData["FormSubmissionMessage"] = "WorkOrder is updated successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            TempData["FormSubmissionMessage"] = "WorkOrder is not updated.";
            TempData["FormSubmissionStatus"] = "error";
            ViewBag.StatusId = new SelectList(wom.Status, "StatusId", "Name", workOrder.StatusId);
            ViewBag.AssignedTeam = new SelectList(wom.Teams, "TeamId", "Name", workOrder.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(wom.Users, "UserId", "Name", workOrder.AssignedUser);
            ViewBag.WorkOrderStageId = new SelectList(wom.WorkStages, "WorkStageId", "Name", workOrder.WorkOrderStageId);
            return View(workOrder);
        }

        // GET: WorkOrders/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var workOrder = wom.GetWorkOrder(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            return View(workOrder);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var workOrder = wom.GetWorkOrder(id);
            wom.SaveWorkOrder(workOrder, true, true);
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
