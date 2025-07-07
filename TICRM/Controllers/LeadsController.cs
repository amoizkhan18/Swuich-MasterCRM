using Microsoft.AspNet.Identity;
using System;
using System.Net;
using System.Web.Mvc;
using TICRM.BuisnessLayer;
using TICRM.CRMFilters;
using TICRM.DTOs;

namespace TICRM.Controllers
{
    [Authorize]
    public class LeadsController : Controller
    {
        private LeadManager lm = new LeadManager();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // executes before the action method execution 


        }




        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {


        }





        // GET: Leads
        public ActionResult Index()
        {
            return View(lm.GetLeads());
        }

        // GET: Leads/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeadDto lead = lm.GetLead(id);
            if (lead == null)
            {
                return HttpNotFound();
            }
            return View(lead);
        }


        // GET: Leads/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            LeadDto lead = lm.GetLead(id);
            return PartialView("_PartialLeadDetails", lead);
        }





        // GET: Leads/Create
        public ActionResult Create()
        {
            ViewBag.AddressId = new SelectList(lm.Addresses, "AddressId", "Street1");
            ViewBag.IndustryId = new SelectList(lm.Industries, "IndustryId", "Name");
            ViewBag.LeadSourceId = new SelectList(lm.LeadSources, "LeadSourceId", "Name");
            ViewBag.LeadTypeId = new SelectList(lm.LeadTypes, "LeadTypeId", "Name");
            ViewBag.StatusId = new SelectList(lm.Status, "StatusId", "Name");
            ViewBag.AssignedTeam = new SelectList(lm.Teams, "TeamId", "Name");
            ViewBag.AssignedUser = new SelectList(lm.Users, "UserId", "Name");
            return View();
        }

        // POST: Leads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [LeadActionFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeadId,Name,LeadTypeId,LeadSourceId,PhoneNumber,Email,AddressId,IndustryId,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] LeadDto lead)
        {
            if (ModelState.IsValid)
            {
                // pass current userid
                string CurrentUserId = User.Identity.GetUserId();

                lm.SaveLead(lead,false,false, CurrentUserId);
                return RedirectToAction("Index");
            }

            ViewBag.AddressId = new SelectList(lm.Addresses, "AddressId", "Street1", lead.AddressId);
            ViewBag.IndustryId = new SelectList(lm.Industries, "IndustryId", "Name", lead.IndustryId);
            ViewBag.LeadSourceId = new SelectList(lm.LeadSources, "LeadSourceId", "Name", lead.LeadSourceId);
            ViewBag.LeadTypeId = new SelectList(lm.LeadTypes, "LeadTypeId", "Name", lead.LeadTypeId);
            ViewBag.StatusId = new SelectList(lm.Status, "StatusId", "Name", lead.StatusId);
            ViewBag.AssignedTeam = new SelectList(lm.Teams, "TeamId", "Name", lead.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(lm.Users, "UserId", "Name", lead.AssignedUser);
            return View(lead);
        }

        // GET: Leads/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            LeadDto lead = lm.GetLead(id);
            if (lead == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressId = new SelectList(lm.Addresses, "AddressId", "Street1", lead.AddressId);
            ViewBag.IndustryId = new SelectList(lm.Industries, "IndustryId", "Name", lead.IndustryId);
            ViewBag.LeadSourceId = new SelectList(lm.LeadSources, "LeadSourceId", "Name", lead.LeadSourceId);
            ViewBag.LeadTypeId = new SelectList(lm.LeadTypes, "LeadTypeId", "Name", lead.LeadTypeId);
            ViewBag.StatusId = new SelectList(lm.Status, "StatusId", "Name", lead.StatusId);
            ViewBag.AssignedTeam = new SelectList(lm.Teams, "TeamId", "Name", lead.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(lm.Users, "UserId", "Name", lead.AssignedUser);
            return View(lead);
        }

        // POST: Leads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [LeadActionFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeadId,Name,LeadTypeId,LeadSourceId,PhoneNumber,Email,AddressId,IndustryId,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] LeadDto lead)
        {
            if (ModelState.IsValid)
            {
                // pass current userid
                string CurrentUserId = User.Identity.GetUserId();

                lm.SaveLead(lead, true,false, CurrentUserId);
                TempData["FormSubmissionMessage"] = "Form submitted successfully";
                TempData["FormSubmissionStatus"] = "success";

              



                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(lm.Addresses, "AddressId", "Street1", lead.AddressId);
            ViewBag.IndustryId = new SelectList(lm.Industries, "IndustryId", "Name", lead.IndustryId);
            ViewBag.LeadSourceId = new SelectList(lm.LeadSources, "LeadSourceId", "Name", lead.LeadSourceId);
            ViewBag.LeadTypeId = new SelectList(lm.LeadTypes, "LeadTypeId", "Name", lead.LeadTypeId);
            ViewBag.StatusId = new SelectList(lm.Status, "StatusId", "Name", lead.StatusId);
            ViewBag.AssignedTeam = new SelectList(lm.Teams, "TeamId", "Name", lead.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(lm.Users, "UserId", "Name", lead.AssignedUser);
            return View(lead);
        }



        // GET: Leads/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            LeadDto lead = lm.GetLead(id);
            return PartialView("_PartialLeadDelete", lead);
        }

        // GET: Leads/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeadDto lead = lm.GetLead(id);
            if (lead == null)
            {
                return HttpNotFound();
            }
            return View(lead);
        }

        // POST: Leads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {

            if (ModelState.IsValid)
            {
                LeadDto lead = lm.GetLead(id);
                // pass current userid
                string CurrentUserId = User.Identity.GetUserId();
                //soft delete for lead
                lm.SaveLead(lead, true, true, CurrentUserId);

            }

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
