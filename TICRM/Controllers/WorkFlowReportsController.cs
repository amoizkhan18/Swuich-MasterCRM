using Microsoft.AspNet.Identity;
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
    public class WorkFlowReportsController : Controller
    {
        private WorkFlowReportManager workFlowReportManager = new WorkFlowReportManager();

        // GET: WorkFlowReports
        public ActionResult Index()
        {
            return View(workFlowReportManager.GetWorkFlowReports());
        }

        // GET: WorkFlowReports/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlowReportDTO workFlowReportDTO = workFlowReportManager.GetWorkFlowReportOnId(id);
            if (workFlowReportDTO == null)
            {
                return HttpNotFound();
            }
            return View(workFlowReportDTO);
        }

        // GET: WorkFlowReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkFlowReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkFlowReportId,WorkFlowId,WorkFlowStatus,Action,WorkFlowActionStatus,WorkFlowDesign,AppliedTo,Frequency,Priority,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,IsDeleted")] WorkFlowReportDTO workFlowReportDTO)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid

                bool condition = workFlowReportManager.SaveItWorkFlowReport(workFlowReportDTO, CurrentUserId, false, false);
                if (!condition)
                {
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(workFlowReportDTO);
        }

        // GET: WorkFlowReports/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlowReportDTO workFlowReportDTO = workFlowReportManager.GetWorkFlowReportOnId(id);
            if (workFlowReportDTO == null)
            {
                return HttpNotFound();
            }
            return View(workFlowReportDTO);
        }

        // POST: WorkFlowReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkFlowReportId,WorkFlowId,WorkFlowStatus,Action,WorkFlowActionStatus,WorkFlowDesign,AppliedTo,Frequency,Priority,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,IsDeleted")] WorkFlowReportDTO workFlowReportDTO)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid

                bool condition = workFlowReportManager.SaveItWorkFlowReport(workFlowReportDTO, CurrentUserId,true, false);
                if (!condition)
                {
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(workFlowReportDTO);
        }

        // GET: WorkFlowReports/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlowReportDTO workFlowReportDTO = workFlowReportManager.GetWorkFlowReportOnId(id);
            if (workFlowReportDTO == null)
            {
                return HttpNotFound();
            }
            return View(workFlowReportDTO);
        }

        // POST: WorkFlowReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            WorkFlowReportDTO workFlowReportDTO = workFlowReportManager.GetWorkFlowReportOnId(id);
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            bool condition = workFlowReportManager.SaveItWorkFlowReport(workFlowReportDTO, CurrentUserId, true, true);
            return RedirectToAction("Index");
        }
        
    }
}
