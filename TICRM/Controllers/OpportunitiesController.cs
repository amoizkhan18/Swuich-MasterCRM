using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TICRM;
using TICRM.BuisnessLayer;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.Controllers
{
    [Authorize]
    public class OpportunitiesController : Controller
    {
        OpportunityManager om = new OpportunityManager();

        // GET: Opportunities
        public ActionResult Index()
        {
            return View(om.GetOpportunities());
        }

        // GET: Opportunities/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var opportunity = om.GetOpportunity(id);
           
            if (opportunity == null)
            {
                return HttpNotFound();
            }
            return View(opportunity);
        }

        // GET: Opportunities/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            var opportunity = om.GetOpportunity(id);
            return PartialView("_PartialRightSideDetail", opportunity);
        }

        // GET: Opportunities/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            var opportunity = om.GetOpportunity(id);
            return PartialView("_PartialOpportunityDelete", opportunity);
        }

        // GET: Opportunities/Create
        public ActionResult Create()
        {
            ViewBag.CurrencyId = new SelectList(om.Currencies, "CurrencyId", "Name");
            ViewBag.AssignedTeam = new SelectList(om.Teams, "TeamId", "Name");
            ViewBag.AssignedUser = new SelectList(om.Users, "UserId", "Name");
            ViewBag.OpportunityStageId = new SelectList(om.OpportunityStages, "OpportunityStageId", "Name");
            ViewBag.ProbabilityId = new SelectList(om.Probabilities, "ProbabilityId", "Name");
            ViewBag.StatusId = new SelectList(om.Status, "StatusId", "Name");
            return View();
        }

        // POST: Opportunities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OpportunityId,Amount,ProbabilityId,OpportunityStageId,Title,CurrencyId,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] OpportunityDto opportunity)
        {
            if (ModelState.IsValid)
            {
                // pass current userid
                string CurrentUserId = User.Identity.GetUserId();
                bool condition = om.SaveOpportunity(opportunity, CurrentUserId);
                if (condition == true)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.CurrencyId = new SelectList(om.Currencies, "CurrencyId", "Name", opportunity.CurrencyId);
            ViewBag.AssignedTeam = new SelectList(om.Teams, "TeamId", "Name", opportunity.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(om.Users, "UserId", "Name", opportunity.AssignedUser);
            ViewBag.OpportunityStageId = new SelectList(om.OpportunityStages, "OpportunityStageId", "Name", opportunity.OpportunityStageId);
            ViewBag.ProbabilityId = new SelectList(om.Probabilities, "ProbabilityId", "Name", opportunity.ProbabilityId);
            ViewBag.StatusId = new SelectList(om.Status, "StatusId", "Name", opportunity.StatusId);
            return View(opportunity);
        }

        // GET: Opportunities/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var opportunity = om.GetOpportunity(id);
            if (opportunity == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrencyId = new SelectList(om.Currencies, "CurrencyId", "Name", opportunity.CurrencyId);
            ViewBag.AssignedTeam = new SelectList(om.Teams, "TeamId", "Name", opportunity.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(om.Users, "UserId", "Name", opportunity.AssignedUser);
            ViewBag.OpportunityStageId = new SelectList(om.OpportunityStages, "OpportunityStageId", "Name", opportunity.OpportunityStageId);
            ViewBag.ProbabilityId = new SelectList(om.Probabilities, "ProbabilityId", "Name", opportunity.ProbabilityId);
            ViewBag.StatusId = new SelectList(om.Status, "StatusId", "Name", opportunity.StatusId);
            return View(opportunity);
        }

        // POST: Opportunities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OpportunityId,Amount,ProbabilityId,OpportunityStageId,Title,CurrencyId,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] OpportunityDto opportunity)
        {
            if (ModelState.IsValid)
            {
                // pass current userid
                string CurrentUserId = User.Identity.GetUserId();
                bool condition = om.SaveOpportunity(opportunity, CurrentUserId,true);
                if (condition == true)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CurrencyId = new SelectList(om.Currencies, "CurrencyId", "Name", opportunity.CurrencyId);
            ViewBag.AssignedTeam = new SelectList(om.Teams, "TeamId", "Name", opportunity.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(om.Users, "UserId", "Name", opportunity.AssignedUser);
            ViewBag.OpportunityStageId = new SelectList(om.OpportunityStages, "OpportunityStageId", "Name", opportunity.OpportunityStageId);
            ViewBag.ProbabilityId = new SelectList(om.Probabilities, "ProbabilityId", "Name", opportunity.ProbabilityId);
            ViewBag.StatusId = new SelectList(om.Status, "StatusId", "Name", opportunity.StatusId);
            return View(opportunity);
        }

        // GET: Opportunities/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var opportunity = om.GetOpportunity(id);
            if (opportunity == null)
            {
                return HttpNotFound();
            }
            return View(opportunity);
        }

        // POST: Opportunities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var opportunity = om.GetOpportunity(id);
            // pass current userid
            string CurrentUserId = User.Identity.GetUserId();
            om.SaveOpportunity(opportunity, CurrentUserId, true, true);
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
