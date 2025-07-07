using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TICRM.DAL;

namespace TICRM.Controllers
{
    [Authorize]
    public class AddressesController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: Addresses
        public ActionResult Index()
        {
            return View(db.Addresses.Where(a => a.IsDeleted != true).ToList());
        }

        // GET: Addresses/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }



        // GET: Addresses/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            Address address = db.Addresses.Find(id);
            return PartialView("_PartialAddressesDetails", address);
        }


        // GET: Readings/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            Address address = db.Addresses.Find(id);
            return PartialView("_PartialAddressesDelete", address);
        }


        // GET: Addresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AddressId,Street1,Street2,City,State,Zip,Country")] Address address)
        {
            if (ModelState.IsValid)
            {
                address.AddressId = Guid.NewGuid();
                db.Addresses.Add(address);
                if (db.SaveChanges() > 0)
                {
                    TempData["FormSubmissionMessage"] = "Address is created successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            TempData["FormSubmissionMessage"] = "Address is not created.";
            TempData["FormSubmissionStatus"] = "error";
            return View(address);
        }

        // GET: Addresses/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AddressId,Street1,Street2,City,State,Zip,Country")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    TempData["FormSubmissionMessage"] = "Address is Updated successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            TempData["FormSubmissionMessage"] = "Address is not Updated.";
            TempData["FormSubmissionStatus"] = "error";
            return View(address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Address address = db.Addresses.Find(id);

            //soft delete for account
            db.Entry(address).State = EntityState.Modified;
            address.IsDeleted = true;
            db.SaveChanges();

            db.Addresses.Remove(address);
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
