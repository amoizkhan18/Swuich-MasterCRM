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
    public class EventMonitorsController : Controller
    {
        private EventMonitorManager eventMonitorManager = new EventMonitorManager();


        // GET: EventMonitors
        public ActionResult Index()
        {
            return View(eventMonitorManager.GetEventMonitorList());
        }

        // GET: EventMonitors/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventMonitorDTO eventMonitorDTO = eventMonitorManager.GetEventMonitorOnId(id);
            if (eventMonitorDTO == null)
            {
                return HttpNotFound();
            }
            return View(eventMonitorDTO);
        }

        // GET: EventMonitors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventMonitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventMonitorId,Name,Type,Status,Message,Color,IPAddress,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] EventMonitorDTO eventMonitorDTO)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid
                bool condition = eventMonitorManager.SaveEventMonitor(eventMonitorDTO, CurrentUserId, false, false);
                if (!condition)
                {
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return View(eventMonitorDTO);
        }

        // GET: EventMonitors/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventMonitorDTO eventMonitorDTO = eventMonitorManager.GetEventMonitorOnId(id);
            if (eventMonitorDTO == null)
            {
                return HttpNotFound();
            }
            return View(eventMonitorDTO);
        }

        // POST: EventMonitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventMonitorId,Name,Type,Status,Message,Color,IPAddress,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] EventMonitorDTO eventMonitorDTO)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid
                bool condition = eventMonitorManager.SaveEventMonitor(eventMonitorDTO, CurrentUserId, true, false);
                if (!condition)
                {
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(eventMonitorDTO);
        }

        // GET: EventMonitors/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventMonitorDTO eventMonitorDTO = eventMonitorManager.GetEventMonitorOnId(id);
            if (eventMonitorDTO == null)
            {
                return HttpNotFound();
            }
            return View(eventMonitorDTO);
        }

        // POST: EventMonitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            EventMonitorDTO eventMonitorDTO = eventMonitorManager.GetEventMonitorOnId(id);
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            bool condition = eventMonitorManager.SaveEventMonitor(eventMonitorDTO, CurrentUserId, true, true);
            return RedirectToAction("Index");
        }

    
    }
}
