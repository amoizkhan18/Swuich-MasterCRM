using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TICRM.BuisnessLayer;
using TICRM.DTOs;

namespace TICRM.Controllers
{
    public class ResourcesController : Controller
    {
        ResourceManager rm = new ResourceManager();

        // GET: Resources
        public ActionResult Index()
        {
            return View(rm.GetResources());
        }

        // GET: Resources/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resource = rm.GetResource(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }


        // GET: Readings/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            var resource = rm.GetResource(id);
            return PartialView("_PartialResourcesDetails", resource);
        }
        // GET: Readings/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            var resource = rm.GetResource(id);
            return PartialView("_PartialResourcesDelete", resource);
        }
        
        // GET: Resources/Create
        public ActionResult Create()
        {
            ViewBag.StatusId = new SelectList(rm.Status, "StatusId", "Name");
            ViewBag.AssignedTeam = new SelectList(rm.Teams, "TeamId", "Name");
            ViewBag.AssignedUser = new SelectList(rm.Users, "UserId", "Name");
            ViewBag.Address = new SelectList(rm.Addresses, "AddressId", "Street1");
            ViewBag.CurrentAddress = new SelectList(rm.Addresses, "AddressId", "Street1");
            return View();
        }

        // POST: Resources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResourceId,Name,Address,CurrentAddress,PhoneHome,Email,Website,PhoneOffice,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] ResourceDto resource)
        {
            if (ModelState.IsValid)
            {

                bool condition = rm.SaveResource(resource);
                if (condition == true)
                {
                    TempData["FormSubmissionMessage"] = "Resource is created successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            TempData["FormSubmissionMessage"] = "Resource is not created.";
            TempData["FormSubmissionStatus"] = "error";
            ViewBag.StatusId = new SelectList(rm.Status, "StatusId", "Name", resource.StatusId);
            ViewBag.AssignedTeam = new SelectList(rm.Teams, "TeamId", "Name", resource.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(rm.Users, "UserId", "Name", resource.AssignedUser);
            ViewBag.Address = new SelectList(rm.Addresses, "AddressId", "Street1", resource.Address);
            ViewBag.CurrentAddress = new SelectList(rm.Addresses, "AddressId", "Street1", resource.CurrentAddress);
            return View(resource);
        }

        // GET: Resources/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resource = rm.GetResource(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusId = new SelectList(rm.Status, "StatusId", "Name", resource.StatusId);
            ViewBag.AssignedTeam = new SelectList(rm.Teams, "TeamId", "Name", resource.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(rm.Users, "UserId", "Name", resource.AssignedUser);
            ViewBag.Address = new SelectList(rm.Addresses, "AddressId", "Street1", resource.Address);
            ViewBag.CurrentAddress = new SelectList(rm.Addresses, "AddressId", "Street1", resource.CurrentAddress);
            return View(resource);
        }

        // POST: Resources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResourceId,Name,Address,CurrentAddress,PhoneHome,Email,Website,PhoneOffice,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] ResourceDto resource)
        {
            if (ModelState.IsValid)
            {
                bool condition = rm.SaveResource(resource, true);
                if (condition == true)
                {
                    TempData["FormSubmissionMessage"] = "Resource is Updated successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            TempData["FormSubmissionMessage"] = "Resource is not Updated.";
            TempData["FormSubmissionStatus"] = "error";
            ViewBag.StatusId = new SelectList(rm.Status, "StatusId", "Name", resource.StatusId);
            ViewBag.AssignedTeam = new SelectList(rm.Teams, "TeamId", "Name", resource.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(rm.Users, "UserId", "Name", resource.AssignedUser);
            ViewBag.Address = new SelectList(rm.Addresses, "AddressId", "Street1", resource.Address);
            ViewBag.CurrentAddress = new SelectList(rm.Addresses, "AddressId", "Street1", resource.CurrentAddress);
            return View(resource);
        }

        // GET: Resources/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resource = rm.GetResource(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var resource = rm.GetResource(id);
            rm.SaveResource(resource, true, true);
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
