using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TICRM.BuisnessLayer;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.Controllers
{
    public class ActivitiesController : Controller
    {
        private CRMEntities db = new CRMEntities();
        private DeviceManager dm = new DeviceManager();
        private ActivityManager am = new ActivityManager();
        private AccountManager accountmangr = new AccountManager();
        


        // GET: Activities
        public ActionResult Index()
        {
            return View(am.GetActivities().ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityDTO activity = am.GetActivity(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }




        // GET: Readings/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            ActivityDTO activity = am.GetActivity(id);
            return PartialView("_PartialActivitiesDetails", activity);
        }


        // GET: Readings/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            ActivityDTO activity = am.GetActivity(id);
            return PartialView("_PartialActivityDelete", activity);
        }




        // GET: Activities/Create
        public ActionResult Create()
        {
            ViewBag.Type = new SelectList(from ActivityType e in Enum.GetValues(typeof(ActivityType)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name");
            ViewBag.RelatedTo = new SelectList(from RelatedToEnum e in Enum.GetValues(typeof(RelatedToEnum)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name");

            ViewBag.RelatedToID = new SelectList("");


            // Use collection initializer.
            ViewBag.StatusId = new SelectList(dm.Status, "StatusId", "Name");
            ViewBag.AssignedTeam = new SelectList(dm.Teams, "TeamId", "Name");
            ViewBag.AssignedUser = new SelectList(dm.Users, "UserId", "Name");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityId,Name,ActivityPartyId,ActivityPointerId,Description,StatusId,AssignedUser,AssignedTeam,Type,RelatedTo,RelatedToID,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] ActivityDTO activity)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid
                bool condition = am.SaveActivity(activity, CurrentUserId, false,false);
                if (!condition)
                {
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Type = new SelectList(from ActivityType e in Enum.GetValues(typeof(ActivityType)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name", activity.Type);
            ViewBag.RelatedTo = new SelectList(from RelatedToEnum e in Enum.GetValues(typeof(RelatedToEnum)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name", activity.RelatedTo);
            ViewBag.RelatedToID = new SelectList("");
            ViewBag.StatusId = new SelectList(dm.Status, "StatusId", "Name", activity.StatusId);
            ViewBag.AssignedTeam = new SelectList(dm.Teams, "TeamId", "Name", activity.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(dm.Users, "UserId", "Name", activity.AssignedUser);
            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityDTO activity = am.GetActivity(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type = new SelectList(from ActivityType e in Enum.GetValues(typeof(ActivityType)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name", activity.Type);
            ViewBag.RelatedTo = new SelectList(from RelatedToEnum e in Enum.GetValues(typeof(RelatedToEnum)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name",activity.RelatedTo);
            ViewBag.RelatedToID = new SelectList("");

            ViewBag.StatusId = new SelectList(dm.Status, "StatusId", "Name", activity.StatusId);
            ViewBag.AssignedTeam = new SelectList(dm.Teams, "TeamId", "Name", activity.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(dm.Users, "UserId", "Name", activity.AssignedUser);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityId,Name,ActivityPartyId,ActivityPointerId,Description,StatusId,AssignedUser,RelatedTo,RelatedToID,Type,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] ActivityDTO activity)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid

                bool condition = am.SaveActivity(activity, CurrentUserId, true,false);
                if (!condition)
                {
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Type = new SelectList(from ActivityType e in Enum.GetValues(typeof(ActivityType)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name", activity.Type);
            ViewBag.RelatedTo = new SelectList(from RelatedToEnum e in Enum.GetValues(typeof(RelatedToEnum)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name", activity.RelatedTo);
            ViewBag.RelatedToID = new SelectList("");
            ViewBag.StatusId = new SelectList(dm.Status, "StatusId", "Name", activity.StatusId);
            ViewBag.AssignedTeam = new SelectList(dm.Teams, "TeamId", "Name", activity.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(dm.Users, "UserId", "Name", activity.AssignedUser);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityDTO activity = am.GetActivity(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ActivityDTO activity = am.GetActivity(id);
            string CurrentUserId = User.Identity.GetUserId(); // get current userid

            bool condition = am.SaveActivity(activity, CurrentUserId, true, true);
            return RedirectToAction("Index");
        }

        //function overloading
        public JsonResult GetRelatedToData(string value)
        {

            if (RelatedToEnum.Account.ToString() == value)
            {

                var data = new SelectList(accountmangr.GetAccounts(), "AccountId", "Name");

               return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRelatedToValue(string value,Guid selectedvalue)
        {

            if (RelatedToEnum.Account.ToString() == value)
            {

                var data = new SelectList(accountmangr.GetAccounts(), "AccountId", "Name", selectedvalue);

                return Json(data, JsonRequestBehavior.AllowGet);
                //return Json(accountmangr.GetAccounts(), JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateActivity(ActivityDTO activity)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid
                bool condition = am.SaveActivity(activity, CurrentUserId, false, false);
                if (!condition)
                {
                    return Json("error", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStatusDropdownList()
        {
            var data = new SelectList(dm.Status, "StatusId", "Name");
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAssignUserDropdownList()
        {
            var data = new SelectList(dm.Users, "UserId", "Name");
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAssignTeamDropdownList()
        {
            var data = new SelectList(dm.Teams, "TeamId", "Name");
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetActivityTypeDropdownList()
        {
            var data = new SelectList(from ActivityType e in Enum.GetValues(typeof(ActivityType)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name");
            return Json(data, JsonRequestBehavior.AllowGet);
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
