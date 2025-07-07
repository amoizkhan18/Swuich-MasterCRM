using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TICRM.BuisnessLayer;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.Controllers
{
    public class WorkFlowMappingsController : Controller
    {
        private CRMEntities db = new CRMEntities();
        private WorkFlowMappingManager workFlowMappingManager = new WorkFlowMappingManager();
        private WorkFlowManager workFlowManager = new WorkFlowManager();

        // GET: WorkFlowMappings
        public ActionResult Index()
        {
            return View(workFlowMappingManager.GetWorkFlowMappingList());
        }

        // GET: WorkFlowMappings/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlowMappingDTO workFlowMappingDTO = workFlowMappingManager.GetWorkFlowMappingOnId(id);
            if (workFlowMappingDTO == null)
            {
                return HttpNotFound();
            }
            return View(workFlowMappingDTO);
        }


        // GET: Devices/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            WorkFlowMappingDTO workFlowMappingDTO = workFlowMappingManager.GetWorkFlowMappingOnId(id);
            return PartialView("_PartialWorkFlowMappingsDetails", workFlowMappingDTO);
        }


        // GET: Devices/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            WorkFlowMappingDTO workFlowMappingDTO = workFlowMappingManager.GetWorkFlowMappingOnId(id);
            return PartialView("_PartialWorkFlowMappingsDelete", workFlowMappingDTO);
        }


        // GET: WorkFlowMappings/Create
        public ActionResult Create()
        {
            ViewBag.WorkFlowId = new SelectList(workFlowManager.GetWorkFlows(), "WorkFlowId", "Name");
            ViewBag.SourceType = new SelectList(from EntityTypes e in Enum.GetValues(typeof(EntityTypes)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name");
            ViewBag.DestinationType = new SelectList(from EntityTypes e in Enum.GetValues(typeof(EntityTypes)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name");
            ViewBag.SourceColumn = new SelectList("");
            ViewBag.DestinationColumn = new SelectList("");
            //ViewBag.Action = new SelectList("");

            ViewBag.Action = new SelectList(new List<SelectListItem>    {
                 new SelectListItem { Text = "Create", Value = "Create"},
                 new SelectListItem { Text = "Update", Value = "Update"} }, "Value", "Text");
            return View();
        }

        // POST: WorkFlowMappings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkFlowMappingId,WorkFlowId,SourceType,SourceColumn,SourceValue,SourceData,Action,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] WorkFlowMappingDTO workFlowMappingDTO)
        {

            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid
                bool condition = workFlowMappingManager.SaveWorkFlowMapping(workFlowMappingDTO, CurrentUserId, false, false);
                if (!condition)
                {
                    TempData["FormSubmissionMessage"] = "Workflow is not created.";
                    TempData["FormSubmissionStatus"] = "error";
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    TempData["FormSubmissionMessage"] = "Workflow is created Successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.WorkFlowId = new SelectList(workFlowManager.GetWorkFlows(), "WorkFlowId", "Name", workFlowMappingDTO.WorkFlowId);
            ViewBag.SourceColumn = new SelectList("");

            ViewBag.Action = new SelectList(new List<SelectListItem>    {
                 new SelectListItem { Text = "Create", Value = "Create"},
                 new SelectListItem { Text = "Update", Value = "Update"} }, "Value", "Text", workFlowMappingDTO.Action);

            return View(workFlowMappingDTO);
        }

        // GET: WorkFlowMappings/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlowMappingDTO workFlowMappingDTO = workFlowMappingManager.GetWorkFlowMappingOnId(id);
            if (workFlowMappingDTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.WorkFlowId = new SelectList(workFlowManager.GetWorkFlows(), "WorkFlowId", "Name", workFlowMappingDTO.WorkFlowId);
            ViewBag.SourceType = new SelectList(from EntityTypes e in Enum.GetValues(typeof(EntityTypes)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name", workFlowMappingDTO.SourceType);
            ViewBag.Action = new SelectList(new List<SelectListItem>    {
                 new SelectListItem { Text = "Create", Value = "Create"},
                 new SelectListItem { Text = "Update", Value = "Update"} }, "Value", "Text", workFlowMappingDTO.Action);


            return View(workFlowMappingDTO);
        }

        // POST: WorkFlowMappings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkFlowMappingId,WorkFlowId,SourceType,SourceColumn,SourceValue,SourceData,Action,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] WorkFlowMappingDTO workFlowMappingDTO)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid
                bool condition = workFlowMappingManager.SaveWorkFlowMapping(workFlowMappingDTO, CurrentUserId, true, false);
                if (!condition)
                {
                    TempData["FormSubmissionMessage"] = "Workflow is not updated.";
                    TempData["FormSubmissionStatus"] = "error";
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    TempData["FormSubmissionMessage"] = "Workflow is Updated Successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.WorkFlowId = new SelectList(workFlowManager.GetWorkFlows(), "WorkFlowId", "Name", workFlowMappingDTO.WorkFlowId);
            ViewBag.SourceType = new SelectList(from EntityTypes e in Enum.GetValues(typeof(EntityTypes)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name", workFlowMappingDTO.SourceType);
            ViewBag.Action = new SelectList(new List<SelectListItem>    {
                 new SelectListItem { Text = "Create", Value = "Create"},
                 new SelectListItem { Text = "Update", Value = "Update"} }, "Value", "Text", workFlowMappingDTO.Action);

            return View(workFlowMappingDTO);
        }

        // GET: WorkFlowMappings/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlowMappingDTO workFlowMappingDTO = workFlowMappingManager.GetWorkFlowMappingOnId(id);
            if (workFlowMappingDTO == null)
            {
                return HttpNotFound();
            }
            return View(workFlowMappingDTO);
        }

        // POST: WorkFlowMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            WorkFlowMappingDTO workFlowMappingDTO = workFlowMappingManager.GetWorkFlowMappingOnId(id);
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            bool condition = workFlowMappingManager.SaveWorkFlowMapping(workFlowMappingDTO, CurrentUserId, true, true);
            return RedirectToAction("Index");
        }

        public JsonResult GetWorkTypeValue(string type)
        {
            WorkFlowTypeViewModel data = new WorkFlowTypeViewModel();

            data.DataTypes = workFlowMappingManager.GetWorkFlowTypeList(type);
            data.Columns = new SelectList(data.DataTypes, "ColumnName", "ColumnName");

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDropDownOfSourceValue(string type, string column)
        {
            SelectList data = new SelectList(workFlowMappingManager.GetWorkFlowTypeDDList(type, column), "Id", "Name");

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetObjectOnId(string type,string Selected)
        {
            if (type == EntityTypes.Account.ToString())
            {
                AccountManager am = new AccountManager();

                Guid id = new Guid(Selected);

                var data = am.GetAccount(id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else if (type == EntityTypes.Lead.ToString())
            {
                LeadManager lm = new LeadManager();

                Guid id = new Guid(Selected);
                var data = lm.GetLead(id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
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







    public class WorkFlowTypeViewModel
    {
        public List<workflowDataTypeDTO> DataTypes { get; set; }
        public SelectList Columns { get; set; }
    }


}
