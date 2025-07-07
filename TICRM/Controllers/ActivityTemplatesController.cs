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
    public class ActivityTemplatesController : Controller
    {
        private CRMEntities db = new CRMEntities();
        private ActivityTemplateManager activityTemplateManager = new ActivityTemplateManager();


        // GET: ActivityTemplates
        public ActionResult Index()
        {
            return View(activityTemplateManager.GetActivityTemplateDTOs());
        }

        // GET: ActivityTemplates/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityTemplateDTO activityTemplateDTO = activityTemplateManager.GetActivityTemplateOnId(id);
            if (activityTemplateDTO == null)
            {
                return HttpNotFound();
            }
            return View(activityTemplateDTO);
        }





        // GET: Readings/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            ActivityTemplateDTO activityTemplateDTO = activityTemplateManager.GetActivityTemplateOnId(id);
            return PartialView("_PartialActivityTemplatesDetails", activityTemplateDTO);
        }



        // GET: Readings/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            ActivityTemplateDTO activityTemplateDTO = activityTemplateManager.GetActivityTemplateOnId(id);
            return PartialView("_PartialActivityTemplateDelete", activityTemplateDTO);
        }




        // GET: ActivityTemplates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivityTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityTemplateId,ActivityTemplateType,PropertyName,PropertyValue,PropertyType,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] ActivityTemplateDTO activityTemplateDTO)
        {
            if (ModelState.IsValid)
            {
               string CurrentUserId = User.Identity.GetUserId(); // get current userid
                bool condition = activityTemplateManager.SaveitActivityTemplate(activityTemplateDTO, CurrentUserId, false,false);
                if (!condition)
                {
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(activityTemplateDTO);
        }

        // GET: ActivityTemplates/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityTemplateDTO activityTemplateDTO = activityTemplateManager.GetActivityTemplateOnId(id);
            if (activityTemplateDTO == null)
            {
                return HttpNotFound();
            }
            return View(activityTemplateDTO);
        }

        // POST: ActivityTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityTemplateId,ActivityTemplateType,PropertyName,PropertyValue,PropertyType,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] ActivityTemplateDTO activityTemplateDTO)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid
                bool condition = activityTemplateManager.SaveitActivityTemplate(activityTemplateDTO, CurrentUserId, true, false);
                if (!condition)
                {
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(activityTemplateDTO);
        }

        // GET: ActivityTemplates/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityTemplateDTO activityTemplateDTO = activityTemplateManager.GetActivityTemplateOnId(id);
            if (activityTemplateDTO == null)
            {
                return HttpNotFound();
            }
            return View(activityTemplateDTO);
        }

        // POST: ActivityTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ActivityTemplateDTO activityTemplateDTO = activityTemplateManager.GetActivityTemplateOnId(id);
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            bool condition = activityTemplateManager.SaveitActivityTemplate(activityTemplateDTO, CurrentUserId, true, true);
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
