using Microsoft.AspNet.Identity;
using System;
using System.Net;
using System.Web.Mvc;
using TICRM.BuisnessLayer;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.Controllers
{
    public class EmailTemplatesController : Controller
    {
        private CRMEntities db = new CRMEntities();
        private EmailTemplateManager emailTemplateManager = new EmailTemplateManager();
        private EmailConfigurationManager emailConfigurationManager = new EmailConfigurationManager();
        private WorkFlowManager workFlowManager = new WorkFlowManager();

        // GET: EmailTemplates
        public ActionResult Index()
        {
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            return View(emailTemplateManager.GetEmailTemplateDTOs(CurrentUserId));
        }

        // GET: EmailTemplates/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            EmailTemplateDTO emailTemplateDTO = emailTemplateManager.GetEmailTemplateDtoOnId(id, CurrentUserId);
            if (emailTemplateDTO == null)
            {
                return HttpNotFound();
            }
            return View(emailTemplateDTO);
        }



        // GET: Readings/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            EmailTemplateDTO emailTemplateDTO = emailTemplateManager.GetEmailTemplateDtoOnId(id, CurrentUserId);
            return PartialView("_PartialEmailTemplatesDetails", emailTemplateDTO);
        }


        // GET: Readings/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            EmailTemplateDTO emailTemplateDTO = emailTemplateManager.GetEmailTemplateDtoOnId(id, CurrentUserId);
            return PartialView("_PartialEmailTemplatesDelete", emailTemplateDTO);
        }


        // GET: EmailTemplates/Create
        public ActionResult Create()
        {
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            ViewBag.EmailConfigurationId = new SelectList(emailConfigurationManager.GetEmailConfigurationDTOs(CurrentUserId), "EmailConfigurationId", "Email");
            ViewBag.WorkFlowId = new SelectList(workFlowManager.GetWorkFlows(), "WorkFlowId", "Name");
            return View();
        }

        // POST: EmailTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "EmailTemplateId,EmailConfigurationId,WorkFlowId,Subject,Body,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] EmailTemplateDTO emailTemplateDTO)
        {
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            if (ModelState.IsValid)
            {
                bool condition = emailTemplateManager.SaveEmailTemplate(emailTemplateDTO, CurrentUserId, false, false);
                if (!condition)
                {
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");

                    TempData["FormSubmissionMessage"] = "Email Template is not saved.";
                    TempData["FormSubmissionStatus"] = "error";

                }
                else
                {
                    TempData["FormSubmissionMessage"] = "Email Template is saved successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.EmailConfigurationId = new SelectList(emailConfigurationManager.GetEmailConfigurationDTOs(CurrentUserId), "EmailConfigurationId", "Email", emailTemplateDTO.EmailConfigurationId);
            ViewBag.WorkFlowId = new SelectList(workFlowManager.GetWorkFlows(), "WorkFlowId", "Name", emailTemplateDTO.WorkFlowId);
            return View(emailTemplateDTO);
        }

        // GET: EmailTemplates/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            EmailTemplateDTO emailTemplateDTO = emailTemplateManager.GetEmailTemplateDtoOnId(id, CurrentUserId);
            if (emailTemplateDTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmailConfigurationId = new SelectList(emailConfigurationManager.GetEmailConfigurationDTOs(CurrentUserId), "EmailConfigurationId", "Email", emailTemplateDTO.EmailConfigurationId);
            ViewBag.WorkFlowId = new SelectList(workFlowManager.GetWorkFlows(), "WorkFlowId", "Name", emailTemplateDTO.WorkFlowId);
            return View(emailTemplateDTO);
        }

        // POST: EmailTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmailTemplateId,EmailConfigurationId,WorkFlowId,Subject,Body,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] EmailTemplateDTO emailTemplateDTO)
        {
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            if (ModelState.IsValid)
            {
                bool condition = emailTemplateManager.SaveEmailTemplate(emailTemplateDTO, CurrentUserId, true, false);
                if (!condition)
                {
                    TempData["FormSubmissionMessage"] = "Email Template is not updated.";
                    TempData["FormSubmissionStatus"] = "error";
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    TempData["FormSubmissionMessage"] = "Email Template is Updated successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.EmailConfigurationId = new SelectList(emailConfigurationManager.GetEmailConfigurationDTOs(CurrentUserId), "EmailConfigurationId", "Email", emailTemplateDTO.EmailConfigurationId);
            ViewBag.WorkFlowId = new SelectList(workFlowManager.GetWorkFlows(), "WorkFlowId", "Name", emailTemplateDTO.WorkFlowId);
            return View(emailTemplateDTO);
        }

        // GET: EmailTemplates/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            EmailTemplateDTO emailTemplateDTO = emailTemplateManager.GetEmailTemplateDtoOnId(id, CurrentUserId);
            if (emailTemplateDTO == null)
            {
                return HttpNotFound();
            }
            return View(emailTemplateDTO);
        }

        // POST: EmailTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            EmailTemplateDTO emailTemplateDTO = emailTemplateManager.GetEmailTemplateDtoOnId(id, CurrentUserId);
            bool condition = emailTemplateManager.SaveEmailTemplate(emailTemplateDTO, CurrentUserId, true, true);
            return RedirectToAction("Index");
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

     






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
