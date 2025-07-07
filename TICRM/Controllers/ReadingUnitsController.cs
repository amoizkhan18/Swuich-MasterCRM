using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TICRM;
using TICRM.DAL;

namespace TICRM.Controllers
{
    [Authorize]
    public class ReadingUnitsController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: ReadingUnits
        public ActionResult Index()
        {
            var readingUnits = db.ReadingUnits.Include(r => r.ReadingType);
            return View(readingUnits.Where(a => a.IsDeleted != true).ToList());
        }

        // GET: ReadingUnits/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReadingUnit readingUnit = db.ReadingUnits.Find(id);
            if (readingUnit == null)
            {
                return HttpNotFound();
            }
            return View(readingUnit);
        }


        // GET: Readings/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            ReadingUnit readingUnit = db.ReadingUnits.Find(id);
            return PartialView("_PartialReadingUnitsDetails", readingUnit);
        }

        // GET: Readings/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            ReadingUnit readingUnit = db.ReadingUnits.Find(id);
            return PartialView("_PartialReadingUnitsDelete", readingUnit);
        }

        

        // GET: ReadingUnits/Create
        public ActionResult Create()
        {
            ViewBag.Type = new SelectList(db.ReadingTypes, "ReadingTypeId", "Name");
            return View();
        }

        // POST: ReadingUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReadingUnitId,Name,Type")] ReadingUnit readingUnit)
        {
            if (ModelState.IsValid)
            {
                readingUnit.ReadingUnitId = Guid.NewGuid();
                db.ReadingUnits.Add(readingUnit);
                if (db.SaveChanges() > 0)
                {
                    TempData["FormSubmissionMessage"] = "Reading Unit is created successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            TempData["FormSubmissionMessage"] = "Reading Unit is not created.";
            TempData["FormSubmissionStatus"] = "success";
            ViewBag.Type = new SelectList(db.ReadingTypes, "ReadingTypeId", "Name", readingUnit.Type);
            return View(readingUnit);
        }

        // GET: ReadingUnits/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReadingUnit readingUnit = db.ReadingUnits.Find(id);
            if (readingUnit == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type = new SelectList(db.ReadingTypes, "ReadingTypeId", "Name", readingUnit.Type);
            return View(readingUnit);
        }

        // POST: ReadingUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReadingUnitId,Name,Type")] ReadingUnit readingUnit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(readingUnit).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    TempData["FormSubmissionMessage"] = "Reading Unit is updated successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            TempData["FormSubmissionMessage"] = "Reading Unit is not updated.";
            TempData["FormSubmissionStatus"] = "error";
            ViewBag.Type = new SelectList(db.ReadingTypes, "ReadingTypeId", "Name", readingUnit.Type);
            return View(readingUnit);
        }

        // GET: ReadingUnits/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReadingUnit readingUnit = db.ReadingUnits.Find(id);
            if (readingUnit == null)
            {
                return HttpNotFound();
            }
            return View(readingUnit);
        }

        // POST: ReadingUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ReadingUnit readingUnit = db.ReadingUnits.Find(id);
            db.Entry(readingUnit).State = EntityState.Modified;
            readingUnit.IsDeleted = true;
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
