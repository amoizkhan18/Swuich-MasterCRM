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
    public class EmailConfigurationsController : Controller
    {
        private CRMEntities db = new CRMEntities();
        private EmailConfigurationManager emailConfigurationManager = new EmailConfigurationManager();

        // GET: EmailConfigurations
        public ActionResult Index()
        {
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            return View(emailConfigurationManager.GetEmailConfigurationDTOs(CurrentUserId));
        }

        // GET: EmailConfigurations/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            EmailConfigurationDTO emailConfigurationDTO = emailConfigurationManager.GetEmailConfigurationDtoOnId(id, CurrentUserId);
            if (emailConfigurationDTO == null)
            {
                return HttpNotFound();
            }
            return View(emailConfigurationDTO);
        }



        // GET: Readings/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            EmailConfigurationDTO emailConfigurationDTO = emailConfigurationManager.GetEmailConfigurationDtoOnId(id, CurrentUserId);
            return PartialView("_PartialEmailConfigurationsDetails", emailConfigurationDTO);
        }


        // GET: Readings/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            EmailConfigurationDTO emailConfigurationDTO = emailConfigurationManager.GetEmailConfigurationDtoOnId(id, CurrentUserId);
            return PartialView("_PartialEmailConfigurationsDelete", emailConfigurationDTO);
        }





        // GET: EmailConfigurations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailConfigurations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmailConfigurationId,Email,UserName,Password,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] EmailConfigurationDTO emailConfigurationDTO)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid
                bool condition = emailConfigurationManager.SaveEmailConfiguration(emailConfigurationDTO, CurrentUserId, false, false);
                if (!condition)
                {
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return View(emailConfigurationDTO);
        }

        // GET: EmailConfigurations/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            EmailConfigurationDTO emailConfigurationDTO = emailConfigurationManager.GetEmailConfigurationDtoOnId(id, CurrentUserId);
            if (emailConfigurationDTO == null)
            {
                return HttpNotFound();
            }
            return View(emailConfigurationDTO);
        }

        // POST: EmailConfigurations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmailConfigurationId,Email,UserName,Password,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] EmailConfigurationDTO emailConfigurationDTO)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid
                bool condition = emailConfigurationManager.SaveEmailConfiguration(emailConfigurationDTO, CurrentUserId, true, false);
                if (!condition)
                {
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(emailConfigurationDTO);
        }

        // GET: EmailConfigurations/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            EmailConfigurationDTO emailConfigurationDTO = emailConfigurationManager.GetEmailConfigurationDtoOnId(id, CurrentUserId);
            if (emailConfigurationDTO == null)
            {
                return HttpNotFound();
            }
            return View(emailConfigurationDTO);
        }

        // POST: EmailConfigurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            EmailConfigurationDTO emailConfigurationDTO = emailConfigurationManager.GetEmailConfigurationDtoOnId(id, CurrentUserId);
            bool condition = emailConfigurationManager.SaveEmailConfiguration(emailConfigurationDTO, CurrentUserId, true, true);
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
