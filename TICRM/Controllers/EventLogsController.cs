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
    public class EventLogsController : Controller
    {
        private EventLogManager eventLogManager = new EventLogManager();



        public ActionResult Login()
        {
            return View();
        }



        // GET: EventLogs
        public ActionResult Index()
        {
            return View(eventLogManager.GetEventLogList());
        }

        // GET: EventLogs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventLogDTO eventLogDTO = eventLogManager.GetEventLogOnId(id);
            if (eventLogDTO == null)
            {
                return HttpNotFound();
            }
            return View(eventLogDTO);
        }

        // GET: EventLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventLogId,Name,Type,Status,Message,Color,IPAddress,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] EventLogDTO eventLogDTO)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid
                bool condition = eventLogManager.SaveEventLog(eventLogDTO, CurrentUserId, false, false);
                if (!condition)
                {
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return View(eventLogDTO);
        }

        // GET: EventLogs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventLogDTO eventLogDTO = eventLogManager.GetEventLogOnId(id);
            if (eventLogDTO == null)
            {
                return HttpNotFound();
            }
            return View(eventLogDTO);
        }

        // POST: EventLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventLogId,Name,Type,Status,Message,Color,IPAddress,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] EventLogDTO eventLogDTO)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid
                bool condition = eventLogManager.SaveEventLog(eventLogDTO, CurrentUserId, true, false);
                if (!condition)
                {
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(eventLogDTO);
        }

        // GET: EventLogs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventLogDTO eventLogDTO = eventLogManager.GetEventLogOnId(id);
            if (eventLogDTO == null)
            {
                return HttpNotFound();
            }
            return View(eventLogDTO);
        }

        // POST: EventLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            EventLogDTO eventLogDTO = eventLogManager.GetEventLogOnId(id);
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            bool condition = eventLogManager.SaveEventLog(eventLogDTO, CurrentUserId, true, true);
            return RedirectToAction("Index");
        }

    }
}
