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
    public class EventNotificationsController : Controller
    {
        private CRMEntities db = new CRMEntities();
        private EventNotificationManager eventNotificationManager = new EventNotificationManager();


        // GET: EventNotifications
        public ActionResult Index()
        {
            return View(eventNotificationManager.GetEventNotificationList());
        }

        // GET: EventNotifications/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventNotificationDTO eventNotificationDTO = eventNotificationManager.GetEventNotificationOnId(id);
            if (eventNotificationDTO == null)
            {
                return HttpNotFound();
            }
            return View(eventNotificationDTO);
        }

        // GET: EventNotifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventNotifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventNotificationId,Name,Type,Status,Message,Color,IPAddress,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] EventNotificationDTO eventNotificationDTO)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid
                bool condition = eventNotificationManager.SaveEventNotification(eventNotificationDTO, CurrentUserId, false, false);
                if (!condition)
                {
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return View(eventNotificationDTO);
        }

        // GET: EventNotifications/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventNotificationDTO eventNotificationDTO = eventNotificationManager.GetEventNotificationOnId(id);
            if (eventNotificationDTO == null)
            {
                return HttpNotFound();
            }
            return View(eventNotificationDTO);
        }

        // POST: EventNotifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventNotificationId,Name,Type,Status,Message,Color,IPAddress,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] EventNotificationDTO eventNotificationDTO)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid
                bool condition = eventNotificationManager.SaveEventNotification(eventNotificationDTO, CurrentUserId, true, false);
                if (!condition)
                {
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(eventNotificationDTO);
        }

        // GET: EventNotifications/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventNotificationDTO eventNotificationDTO = eventNotificationManager.GetEventNotificationOnId(id);
            if (eventNotificationDTO == null)
            {
                return HttpNotFound();
            }
            return View(eventNotificationDTO);
        }

        // POST: EventNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            EventNotificationDTO eventNotificationDTO = eventNotificationManager.GetEventNotificationOnId(id);
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            bool condition = eventNotificationManager.SaveEventNotification(eventNotificationDTO, CurrentUserId, true, true);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
