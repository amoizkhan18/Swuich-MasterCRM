using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TICRM.DAL;

namespace TICRM.Controllers
{
    public class TrackingsController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: Trackings
        public ActionResult Index()
        {
            return View(db.Trackings.ToList());
        }

        // GET: Trackings/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tracking tracking = db.Trackings.Find(id);
            if (tracking == null)
            {
                return HttpNotFound();
            }
            return View(tracking);
        }

        // GET: Trackings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trackings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Devicemac,Devicelocation,Datatime")] Tracking tracking)
        {
            if (ModelState.IsValid)
            {
                tracking.Id = Guid.NewGuid();
                db.Trackings.Add(tracking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tracking);
        }

        // GET: Trackings/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tracking tracking = db.Trackings.Find(id);
            if (tracking == null)
            {
                return HttpNotFound();
            }
            return View(tracking);
        }

        // POST: Trackings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Devicemac,Devicelocation,Datatime")] Tracking tracking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tracking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tracking);
        }

        // GET: Trackings/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tracking tracking = db.Trackings.Find(id);
            if (tracking == null)
            {
                return HttpNotFound();
            }
            return View(tracking);
        }

        // POST: Trackings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Tracking tracking = db.Trackings.Find(id);
            db.Trackings.Remove(tracking);
            db.SaveChanges();
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
