using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TICRM.BuisnessLayer;
using TICRM.DTOs;
using System.Linq;
using static TICRM.DTOs.WFDesignerViewModel;

namespace TICRM.Controllers
{
    public class WorkFlowsController : Controller
    {

        private WorkFlowManager wfm = new WorkFlowManager();
        private AccountManager am = new AccountManager();

        // GET: WorkFlows
        public ActionResult Index()
        {
            return View(wfm.GetWorkFlows());
        }

        public ActionResult WorkFlowDesigner(Guid? id)
        {
            ViewBag.id = "";
            if (id != null)
            {
                ViewBag.id = id;
            }
            return View();
        }

        public string EditFromDesigner(Guid? id)
        {
            if (id == null)
            {
                return "";
            }
            WorkFlowDTO workFlow = wfm.GetWorkFlowOnId(id);
            if (workFlow.WorkFlowDesign == null)
            {
                return wfm.ConvertToDesigner(workFlow);
            }
            return workFlow.WorkFlowDesign;
        }

        public string SaveWorkFlowFromDesigner(string mySavedModel, string Name, string Description, string Priority, string Frequency, string TriggerIn, string TriggerOut)
        {
            WorkFlowDTO workFlowDTO = new WorkFlowDTO();

            workflowDesigner data = Newtonsoft.Json.JsonConvert.DeserializeObject(mySavedModel) as workflowDesigner;
            workflowDesigner workflowDesigner = new JavaScriptSerializer().Deserialize<workflowDesigner>(mySavedModel);
            
            List<WorkFlowNodeDTO> list = workflowDesigner.nodeDataArray;

            foreach (WorkFlowNodeDTO item in list)
            {
                if (item.text == RelatedToEnum.Account.ToString() || item.text == RelatedToEnum.Leads.ToString() || item.text == RelatedToEnum.Oppertunities.ToString())
                {
                    workFlowDTO.TargetOn = item.text;
                    workflowDesigner.TargetOn = item.text;
                }


                if (item.text == TrigegrConditionConstant.Pre_Event || item.text == TrigegrConditionConstant.Post_Event)
                {
                    workFlowDTO.TriggerCondition = item.text;
                    workflowDesigner.TriggerCondition = item.text;
                }

                if (item.text == appliedToConstant.OnCreate || item.text == appliedToConstant.OnUpdate || item.text == appliedToConstant.CreateAndUpdate)
                {
                    workFlowDTO.AppliedTo = TrigegrConditionConstant.Pre_Event;
                    workflowDesigner.AppliedTo = TrigegrConditionConstant.Pre_Event;
                }

                if (WFActionConstant.Email == item.text || WFActionConstant.Meeting == item.text || WFActionConstant.Note == item.text || WFActionConstant.Alert == item.text)
                {
                    workFlowDTO.Action = item.text;
                    workflowDesigner.Action = item.text;
                }


            }

            workflowDesigner.Name = Name;
            workflowDesigner.Description = Description;
            workflowDesigner.Priority = Convert.ToInt32(Priority);
            workflowDesigner.Frequency = Convert.ToInt32(Frequency);
            workflowDesigner.TriggerIn = Convert.ToDateTime(TriggerIn);
            workflowDesigner.TriggerOut = Convert.ToDateTime(TriggerOut);
            workflowDesigner.WorkFlowStatus = WFStatusConstant.Paused;


            //workFlowDTO.AppliedTo = appliedToConstant.CreateAndUpdate;
            //workflowDesigner.AppliedTo = appliedToConstant.CreateAndUpdate;
            //workFlowDTO.TriggerCondition = TrigegrConditionConstant.Pre_Event;
            //workflowDesigner.TriggerCondition = TrigegrConditionConstant.Pre_Event;



            workFlowDTO.Name = Name;
            workFlowDTO.Description = Description;
            workFlowDTO.Priority = Convert.ToInt32(Priority);
            workFlowDTO.Frequency = Convert.ToInt32(Frequency);
            workFlowDTO.TriggerIn = Convert.ToDateTime(TriggerIn);
            workFlowDTO.TriggerOut = Convert.ToDateTime(TriggerOut);
            workFlowDTO.WorkFlowStatus = WFStatusConstant.Paused;
            workFlowDTO.WorkFlowDesign = new JavaScriptSerializer().Serialize(workflowDesigner);

            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            bool condition = wfm.SaveItWorkFlow(workFlowDTO, CurrentUserId, false, false);

            string workflow = new JavaScriptSerializer().Serialize(workflowDesigner);
            return workflow;
        }

        // GET: WorkFlows/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlowDTO workFlow = wfm.GetWorkFlowOnId(id);
            if (workFlow == null)
            {
                return HttpNotFound();
            }
            return View(workFlow);
        }


        // GET: Devices/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            WorkFlowDTO workFlow = wfm.GetWorkFlowOnId(id);
            return PartialView("_PartialWorkFlowsDetails", workFlow);
        }

        // GET: Devices/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            WorkFlowDTO workFlow = wfm.GetWorkFlowOnId(id);
            return PartialView("_PartialWorkFlowsDelete", workFlow);
        }

        // GET: WorkFlows/Create
        public ActionResult Create()
        {
            ViewBag.AssignedTeam = new SelectList(am.Teams, "TeamId", "Name");
            ViewBag.AssignedUser = new SelectList(am.Users, "UserId", "Name");


            ViewBag.TriggerCondition = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = TrigegrConditionConstant.Post_Event, Value = TrigegrConditionConstant.Post_Event},
        new SelectListItem { Text = TrigegrConditionConstant.Pre_Event, Value = TrigegrConditionConstant.Pre_Event}}, "Value", "Text");

            

                 ViewBag.AppliedTo = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = appliedToConstant.OnCreate, Value = appliedToConstant.OnCreate},
        new SelectListItem { Text = appliedToConstant.OnUpdate, Value = appliedToConstant.OnUpdate},
        new SelectListItem { Text = appliedToConstant.CreateAndUpdate, Value = appliedToConstant.CreateAndUpdate}}, "Value", "Text");



            ViewBag.Action = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = WFActionConstant.Email, Value = WFActionConstant.Email},
        new SelectListItem { Text = WFActionConstant.Alert, Value = WFActionConstant.Alert},
        new SelectListItem { Text = WFActionConstant.Meeting, Value = WFActionConstant.Meeting},
        new SelectListItem { Text = WFActionConstant.Note, Value = WFActionConstant.Note},
        new SelectListItem { Text = WFActionConstant.Account, Value = WFActionConstant.Account}},
        "Value", "Text");

            ViewBag.TargetOn = new SelectList(from EntityTypes e in Enum.GetValues(typeof(EntityTypes)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkFlowId,Name,TriggerCondition,TriggerIn,TriggerOut,TargetOn,Description,WorkFlowStatus,AppliedTo,Frequency,FrequencyOut,Priority,Action,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] WorkFlowDTO workFlow)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid
                bool condition = wfm.SaveItWorkFlow(workFlow, CurrentUserId, false, false);
                if (!condition)
                {
                    TempData["FormSubmissionMessage"] = workFlow.Name + " WorkFlow not Created.";
                    TempData["FormSubmissionStatus"] = "error";
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    TempData["FormSubmissionMessage"] = workFlow.Name + " WorkFlow Created successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.AssignedTeam = new SelectList(am.Teams, "TeamId", "Name", workFlow.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(am.Users, "UserId", "Name", workFlow.AssignedUser);

            ViewBag.TriggerCondition = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = TrigegrConditionConstant.Post_Event, Value = TrigegrConditionConstant.Post_Event},
        new SelectListItem { Text = TrigegrConditionConstant.Pre_Event, Value = TrigegrConditionConstant.Pre_Event}}, "Value", "Text", workFlow.TriggerCondition);

            ViewBag.AppliedTo = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = appliedToConstant.OnCreate, Value = appliedToConstant.OnCreate},
        new SelectListItem { Text = appliedToConstant.OnUpdate, Value = appliedToConstant.OnUpdate},
        new SelectListItem { Text = appliedToConstant.CreateAndUpdate, Value = appliedToConstant.CreateAndUpdate}}, "Value", "Text", workFlow.AppliedTo);


            ViewBag.Action = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = WFActionConstant.Email, Value = WFActionConstant.Email},
        new SelectListItem { Text = WFActionConstant.Alert, Value = WFActionConstant.Alert},
        new SelectListItem { Text = WFActionConstant.Meeting, Value = WFActionConstant.Meeting},
        new SelectListItem { Text = WFActionConstant.Note, Value = WFActionConstant.Note},
            new SelectListItem { Text = WFActionConstant.Account, Value = WFActionConstant.Account}},
        "Value", "Text", workFlow.Action);

            ViewBag.TargetOn = new SelectList(from EntityTypes e in Enum.GetValues(typeof(EntityTypes)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name", workFlow.TargetOn);

            return View(workFlow);
        }

        // GET: WorkFlows/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlowDTO workFlow = wfm.GetWorkFlowOnId(id);
            if (workFlow == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignedTeam = new SelectList(am.Teams, "TeamId", "Name", workFlow.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(am.Users, "UserId", "Name", workFlow.AssignedUser);
            ViewBag.TriggerCondition = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = TrigegrConditionConstant.Post_Event, Value = TrigegrConditionConstant.Post_Event},
        new SelectListItem { Text = TrigegrConditionConstant.Pre_Event, Value = TrigegrConditionConstant.Pre_Event}}, "Value", "Text", workFlow.TriggerCondition);

            ViewBag.AppliedTo = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = appliedToConstant.OnCreate, Value = appliedToConstant.OnCreate},
        new SelectListItem { Text = appliedToConstant.OnUpdate, Value = appliedToConstant.OnUpdate},
        new SelectListItem { Text = appliedToConstant.CreateAndUpdate, Value = appliedToConstant.CreateAndUpdate}}, "Value", "Text",workFlow.AppliedTo);

            ViewBag.Action = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = WFActionConstant.Email, Value = WFActionConstant.Email},
        new SelectListItem { Text = WFActionConstant.Alert, Value = WFActionConstant.Alert},
        new SelectListItem { Text = WFActionConstant.Meeting, Value = WFActionConstant.Meeting},
        new SelectListItem { Text = WFActionConstant.Note, Value = WFActionConstant.Note},
            new SelectListItem { Text = WFActionConstant.Account, Value = WFActionConstant.Account}},
        "Value", "Text", workFlow.Action);

            ViewBag.TargetOn = new SelectList(from EntityTypes e in Enum.GetValues(typeof(EntityTypes)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name", workFlow.TargetOn);
            return View(workFlow);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkFlowId,Name,TriggerCondition,TriggerIn,TriggerOut,TargetOn,Description,WorkFlowStatus,AppliedTo,Frequency,FrequencyOut,Priority,Action,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] WorkFlowDTO workFlow)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId(); // get current userid

                bool condition = wfm.SaveItWorkFlow(workFlow, CurrentUserId, true, false);
                if (!condition)
                {
                    TempData["FormSubmissionMessage"] = workFlow.Name + " WorkFlow not updated.";
                    TempData["FormSubmissionStatus"] = "error";
                    ModelState.AddModelError("", "Data Is Not Saved Please Refresh the page.");
                }
                else
                {
                    TempData["FormSubmissionMessage"] = workFlow.Name + " WorkFlow updated successfully.";
                    TempData["FormSubmissionStatus"] = "success";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.AssignedTeam = new SelectList(am.Teams, "TeamId", "Name", workFlow.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(am.Users, "UserId", "Name", workFlow.AssignedUser);

            ViewBag.TriggerCondition = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = TrigegrConditionConstant.Post_Event, Value = TrigegrConditionConstant.Post_Event},
        new SelectListItem { Text = TrigegrConditionConstant.Pre_Event, Value = TrigegrConditionConstant.Pre_Event}}, "Value", "Text", workFlow.TriggerCondition);

            ViewBag.AppliedTo = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = appliedToConstant.OnCreate, Value = appliedToConstant.OnCreate},
        new SelectListItem { Text = appliedToConstant.OnUpdate, Value = appliedToConstant.OnUpdate},
        new SelectListItem { Text = appliedToConstant.CreateAndUpdate, Value = appliedToConstant.CreateAndUpdate}}, "Value", "Text", workFlow.AppliedTo);

            ViewBag.Action = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = WFActionConstant.Email, Value = WFActionConstant.Email},
        new SelectListItem { Text = WFActionConstant.Alert, Value = WFActionConstant.Alert},
        new SelectListItem { Text = WFActionConstant.Meeting, Value = WFActionConstant.Meeting},
        new SelectListItem { Text = WFActionConstant.Note, Value = WFActionConstant.Note},
            new SelectListItem { Text = WFActionConstant.Account, Value = WFActionConstant.Account}},
        "Value", "Text", workFlow.Action);

            ViewBag.TargetOn = new SelectList(from EntityTypes e in Enum.GetValues(typeof(EntityTypes)) select new { ID = e.ToString(), Name = e.ToString() }, "Name", "Name", workFlow.TargetOn);
            return View(workFlow);
        }

        // GET: WorkFlows/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlowDTO workFlow = wfm.GetWorkFlowOnId(id);
            if (workFlow == null)
            {
                return HttpNotFound();
            }
            return View(workFlow);
        }

        // POST: WorkFlows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            WorkFlowDTO workFlow = wfm.GetWorkFlowOnId(id);
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            bool condition = wfm.SaveItWorkFlow(workFlow, CurrentUserId, true, true);
            return RedirectToAction("Index");
        }









        public JsonResult GetQueueList()
        {
            return Json(wfm.GetWorkFlows(), JsonRequestBehavior.AllowGet);
        }










    }
}






