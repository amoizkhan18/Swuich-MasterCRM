using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TICRM.BuisnessLayer;
using TICRM.DTOs;

namespace TICRM.Controllers
{
    public class ContactController : Controller
    {
        private ContactManager cm = new ContactManager();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // executes before the action method execution 

        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
        // GET: Contact/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactDto contact = cm.GetContact(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: contact/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(long? id)
        {
            ContactDto contact = cm.GetContact(id);
            return PartialView("_PartialContactDetails", contact);
        }
        // GET: Contact
        public ActionResult Index()
        {
            return View(cm.GetContacts());
        }
        public ActionResult Create()
        {
            // ViewBag.AddressId = new SelectList(lm.Addresses, "AddressId", "Street1");
            // ViewBag.IndustryId = new SelectList(lm.Industries, "IndustryId", "Name");
            // ViewBag.LeadSourceId = new SelectList(lm.LeadSources, "LeadSourceId", "Name");
            //ViewBag.LeadTypeId = new SelectList(lm.LeadTypes, "LeadTypeId", "Name");
            ViewBag.AccountId = new SelectList(cm.Accounts, "AccountId", "Name");
            ViewBag.StatusId = new SelectList(cm.Status, "StatusId", "Name");
            ViewBag.AssignedTeam = new SelectList(cm.Teams, "TeamId", "Name");
            ViewBag.AssignedUser = new SelectList(cm.Users, "UserId", "Name");
            return View();
        }
        // [ContactActionFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Phone,Address,AccountId,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] ContactDto contact)
        {
            if (ModelState.IsValid)
            {
                // pass current userid
                string CurrentUserId = User.Identity.GetUserId();

                cm.SaveContact(contact, false, false, CurrentUserId);
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(cm.Accounts, "AccountId", "Name", contact.AccountId);
            ViewBag.StatusId = new SelectList(cm.Status, "StatusId", "Name", contact.StatusId);
            ViewBag.AssignedTeam = new SelectList(cm.Teams, "TeamId", "Name", contact.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(cm.Users, "UserId", "Name", contact.AssignedUser);
            return View(contact);
            //ViewBag.AddressId = new SelectList(lm.Addresses, "AddressId", "Street1");
            //ViewBag.IndustryId = new SelectList(lm.Industries, "IndustryId", "Name");
            //ViewBag.LeadSourceId = new SelectList(lm.LeadSources, "LeadSourceId", "Name");
            //ViewBag.LeadTypeId = new SelectList(lm.LeadTypes, "LeadTypeId", "Name");
            //ViewBag.StatusId = new SelectList(lm.Status, "StatusId", "Name");
            //ViewBag.AssignedTeam = new SelectList(lm.Teams, "TeamId", "Name");
            //ViewBag.AssignedUser = new SelectList(lm.Users, "UserId", "Name");
            //return View();
        }
    }
}