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
    public class ReadingTypesController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: ReadingTypes
        public ActionResult Index()
        {
            return View(db.ReadingTypes.Where(a => a.IsDeleted != true).ToList());
        }

        // GET: ReadingTypes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReadingType readingType = db.ReadingTypes.Find(id);
            if (readingType == null)
            {
                return HttpNotFound();
            }
            return View(readingType);
        }

        // GET: ReadingTypes/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            ReadingType readingType = db.ReadingTypes.Find(id);
            return PartialView("_PartialReadingTypesDetails", readingType);
        }


        // GET: ReadingTypes/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            ReadingType readingType = db.ReadingTypes.Find(id);
            return PartialView("_PartialReadingTypesDelete", readingType);
        }


        // GET: ReadingTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReadingTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReadingTypeId,Name")] ReadingType readingType)
        {
            if (ModelState.IsValid)
            {
                readingType.ReadingTypeId = Guid.NewGuid();
                db.ReadingTypes.Add(readingType);
                if (db.SaveChanges()>0)
                {
                    TempData["FormSubmissionMessage"] = "Reading Type is Created successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            TempData["FormSubmissionMessage"] = "Reading Type is not Created.";
            TempData["FormSubmissionStatus"] = "error";
            return View(readingType);
        }

        // GET: ReadingTypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReadingType readingType = db.ReadingTypes.Find(id);
            if (readingType == null)
            {
                return HttpNotFound();
            }
            return View(readingType);
        }

        // POST: ReadingTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReadingTypeId,Name")] ReadingType readingType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(readingType).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    TempData["FormSubmissionMessage"] = "Reading Type is updated successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            TempData["FormSubmissionMessage"] = "Reading Type is not updated.";
            TempData["FormSubmissionStatus"] = "error";
            return View(readingType);
        }

        // GET: ReadingTypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReadingType readingType = db.ReadingTypes.Find(id);
            if (readingType == null)
            {
                return HttpNotFound();
            }
            return View(readingType);
        }

        // POST: ReadingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ReadingType readingType = db.ReadingTypes.Find(id);
            db.Entry(readingType).State = EntityState.Modified;
            readingType.IsDeleted = true;
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
