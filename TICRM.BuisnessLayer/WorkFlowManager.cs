using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Script.Serialization;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;
using static TICRM.DTOs.WFDesignerViewModel;

namespace TICRM.BuisnessLayer
{
    public class WorkFlowManager : BaseManager
    {
        // GetWorkFlows
        public List<WorkFlowDTO> GetWorkFlows()
        {
            try
            {
            InsertEventLog("GetWorkFlows", EventType.Log, EventColor.yellow, "going to Get WorkFlows list","TICRM.BusinessLayer.WorkFlowManager.GetWorkFlows", "");
                List<WorkFlowDTO> workFlowDTOs = new List<WorkFlowDTO>(); // create list Object of workflow DTO

                List<WorkFlow> workFlows = dbEnt.WorkFlows.ToList(); // declare a local variable to Get List Of workflows from DB
                // apply iteration on Workflow
                foreach (WorkFlow item in workFlows)
                {
                    workFlowDTOs.Add(objMapper.GetWorkFlowDTO(item)); // add in a list object
                }
                return workFlowDTOs; // return List Object in Response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetWorkFlows", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.WorkFlowManager.GetWorkFlows", "");
                throw;
            }
        }


        public bool SaveItWorkFlow(WorkFlowDTO workFlowDTO, string CurrentUserId, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
            InsertEventLog("SaveItWorkFlow", EventType.Log, EventColor.yellow, "enter","TICRM.BusinessLayer.WorkFlowManager.SaveItWorkFlow", CurrentUserId);
                WorkFlow workFlow; // create a new object
                workFlow = objMapper.GetWorkFlow(workFlowDTO); // pass parameter object to workflow object

                if (isEditMode) // check if is is edit mode is true
                {

                    WorkFlow dbData = dbEnt.WorkFlows.FirstOrDefault(x => x.WorkFlowId == workFlow.WorkFlowId); // get data from database and pass in new activity class object

                    if (dbData != null) // check if data is null
                    {
                        if (isDeleteMode) // if is delete mode is true 
                        {
            InsertEventLog("SaveItWorkFlow", EventType.Log, EventColor.yellow, "going to delete workflow","TICRM.BusinessLayer.WorkFlowManager.SaveItWorkFlow", CurrentUserId);
                            // here we delete hard
                            dbEnt.WorkFlows.Remove(dbData); // remove object in database 
                        }
                        else
                        {
            InsertEventLog("SaveItWorkFlow", EventType.Log, EventColor.yellow, "going to update workflow","TICRM.BusinessLayer.WorkFlowManager.SaveItWorkFlow", CurrentUserId);
                            dbData.Name = workFlow.Name;
                            dbData.TriggerCondition = workFlow.TriggerCondition;
                            dbData.TriggerIn = workFlow.TriggerIn;
                            dbData.TriggerOut = workFlow.TriggerOut;
                            dbData.TargetOn = workFlow.TargetOn;
                            dbData.Frequency = workFlow.Frequency;
                            dbData.FrequencyOut = workFlow.FrequencyOut;
                            dbData.Description = workFlow.Description;
                            dbData.WorkFlowStatus = workFlow.WorkFlowStatus;
                            dbData.AppliedTo = workFlow.AppliedTo;
                            dbData.Priority = workFlow.Priority;
                            dbData.AssignedUser = workFlow.AssignedUser;
                            dbData.AssignedTeam = workFlow.AssignedTeam;
                            dbData.UpdatedDate = DateTime.Now;
                            dbData.UpdatedBy = CurrentUserId;
                            dbData.Action = workFlow.Action;
                            dbEnt.Entry(dbData).State = EntityState.Modified;
                        }
                    }
                    else
                    {
            InsertEventLog("SaveItWorkFlow", EventType.Log, EventColor.yellow, "work flow data is null on id"+workFlowDTO.WorkFlowId+"","TICRM.BusinessLayer.WorkFlowManager.SaveItWorkFlow", CurrentUserId);
                        return false; // return false if no any condition found for edit and delete

                    }

                    if (dbEnt.SaveChanges() > 0) // check if database save changes is done return true
                    {
                        InsertEventLog("SaveItWorkFlow", EventType.Log, EventColor.yellow, "WorkFlow Is Updated Successfully","TICRM.BusinessLayer.WorkFlowManager.SaveItWorkFlow", CurrentUserId);
                        return true;
                    }

                }
                else
                {
            InsertEventLog("SaveItWorkFlow", EventType.Log, EventColor.yellow, "Going to create new Record of work flow","TICRM.BusinessLayer.WorkFlowManager.SaveItWorkFlow", CurrentUserId);
                    workFlow.WorkFlowId = Guid.NewGuid();
                    workFlow.CreatedBy = CurrentUserId;
                    workFlow.CreatedDate = DateTime.Now;
                    if (workFlow.WorkFlowDesign == null)
                    {
                        workFlow.WorkFlowDesign = ConvertToDesigner(workFlowDTO);
                    }

                    dbEnt.WorkFlows.Add(workFlow); // add in a database
                    if (dbEnt.SaveChanges() > 0)
                    {
            InsertEventLog("SaveItWorkFlow", EventType.Log, EventColor.yellow, "new WorkFlow Is Save Successfully","TICRM.BusinessLayer.WorkFlowManager.SaveItWorkFlow", CurrentUserId);
                        return true;
                    }
                }



            }
            catch (Exception ex)
            {
                InsertEventMonitor("SaveItWorkFlow", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.WorkFlowManager.SaveItWorkFlow", CurrentUserId);
                throw ex;
            }
            return false;

        }



        public string ConvertToDesigner(WorkFlowDTO workFlowDTO)
        {
            InsertEventLog("ConvertToDesigner", EventType.Log, EventColor.yellow, "enter in to convert workflow to design","TICRM.BusinessLayer.WorkFlowManager.ConvertToDesigner", "");
            workflowDesigner designer = new workflowDesigner();
            designer.@class = "go.GraphLinksModel";
            designer.nodeKeyProperty = "text";
            designer.linkKeyProperty = "iterate";


            List<WorkFlowNodeDTO> nodeList = new List<WorkFlowNodeDTO>();

            WorkFlowNodeDTO nodeDataStart = new WorkFlowNodeDTO();
            //nodeDataStart.NodeDataId = Guid.NewGuid();
            nodeDataStart.text = "Start";
            nodeDataStart.key = "Start";
            nodeDataStart.figure = "Circle";
            nodeDataStart.fill = "#00AD5F";
            nodeDataStart.loc = "175 0";
            nodeList.Add(nodeDataStart);



            WorkFlowNodeDTO nodeDataTrigger = new WorkFlowNodeDTO();
            //nodeDataTrigger.NodeDataId = Guid.NewGuid();
            nodeDataTrigger.text = workFlowDTO.TriggerCondition == null ? "" : workFlowDTO.TriggerCondition;
            nodeDataTrigger.key = workFlowDTO.TriggerCondition == null ? "" : workFlowDTO.TriggerCondition;
            nodeDataTrigger.figure = "";
            nodeDataTrigger.fill = "white";
            nodeDataTrigger.loc = "175 100";
            nodeList.Add(nodeDataTrigger);




            WorkFlowNodeDTO nodeDataAppliedTo = new WorkFlowNodeDTO();
            //nodeDataAppliedTo.NodeDataId = Guid.NewGuid();
            nodeDataAppliedTo.text = workFlowDTO.AppliedTo == null ? "" : workFlowDTO.AppliedTo;
            nodeDataAppliedTo.key = workFlowDTO.AppliedTo == null ? "" : workFlowDTO.AppliedTo;
            nodeDataAppliedTo.figure = "";
            nodeDataAppliedTo.fill = "white";
            nodeDataAppliedTo.loc = "175 300";
            nodeList.Add(nodeDataAppliedTo);


            WorkFlowNodeDTO nodeDataTargetOn = new WorkFlowNodeDTO();
            //nodeDataTargetOn.NodeDataId = Guid.NewGuid();
            nodeDataTargetOn.text = workFlowDTO.TargetOn == null ? "" : workFlowDTO.TargetOn;
            nodeDataTargetOn.key = workFlowDTO.TargetOn == null ? "" : workFlowDTO.TargetOn;
            nodeDataTargetOn.figure = "Database";
            nodeDataTargetOn.fill = "lightgray";
            nodeDataTargetOn.loc = "375 300";
            nodeList.Add(nodeDataTargetOn);


            WorkFlowNodeDTO nodeDataAction = new WorkFlowNodeDTO();
            //nodeDataAppliedTo.NodeDataId = Guid.NewGuid();
            nodeDataAction.text = workFlowDTO.Action == null ? "" : workFlowDTO.Action;
            nodeDataAction.key = workFlowDTO.Action == null ? "" : workFlowDTO.Action;
            nodeDataAction.figure = "Diamond";
            nodeDataAction.fill = "white";
            nodeDataAction.loc = "575 300";
            nodeList.Add(nodeDataAction);


            designer.nodeDataArray = nodeList;

            List<LinkDataArray> link = new List<LinkDataArray>();

            int count = designer.nodeDataArray.Count;
            int loop = 0;
            

            for (int i = 1; i < designer.nodeDataArray.Count;i++)
            {
                LinkDataArray obj = new LinkDataArray();
                obj.from = designer.nodeDataArray[i-1].text;
                obj.to = designer.nodeDataArray[i].text;
                obj.iterate = -i;
                link.Add(obj);
            }
            designer.linkDataArray = link;

            designer.Name = workFlowDTO.Name;
            designer.Description = workFlowDTO.Description;
            designer.Frequency = workFlowDTO.Frequency;
            designer.Priority = workFlowDTO.Priority;
            designer.WorkFlowStatus = workFlowDTO.WorkFlowStatus;




            string workflow = new JavaScriptSerializer().Serialize(designer);
            return workflow;
        }




        //{ "class": "go.GraphLinksModel",
        //  "nodeKeyProperty": "text",
        //  "linkKeyProperty": "iterate",
        //  "modelData": {"position":"-5 28.098752931106922"},
        //  "nodeDataArray": [
        //{"text":"Start", "key":"Start", "figure":"Circle", "fill":"#00AD5F", "loc":"340 70"},
        //{"text":"OnCreate2", "key":"OnCreate", "fill":"white", "loc":"420 220"},
        //{"text":"Pre Event", "key":"name = aqil", "figure":"Diamond", "fill":"white", "loc":"560 360"},
        //{"text":"Create And Update", "key":"Create And Update", "fill":"white", "loc":"540 510"},
        //{"text":"Leads", "key":"Leads", "figure":"Database", "fill":"lightgray", "loc":"320 350"},
        //{"text":"Account", "key":"Account", "figure":"Database", "fill":"lightgray", "loc":"950 510"}
        // ],
        //  "linkDataArray": [
        //{"from":"Start", "to":"OnCreate2", "iterate":-2, "points":[340,106.90124706889308,340,116.90124706889308,340,154.98176138234695,420,154.98176138234695,420,193.0622756958008,420,203.0622756958008]},
        //{"from":"Pre Event", "to":"Create And Update", "iterate":-5, "points":[560,392.8754486083985,560,402.8754486083985,560,442.96886215209963,540,442.96886215209963,540,483.0622756958008,540,493.0622756958008]},
        //{"from":"OnCreate2", "to":"Leads", "iterate":-3, "points":[420,236.93772430419924,420,246.93772430419924,420,277.0311378479004,320,277.0311378479004,320,307.1245513916016,320,317.1245513916016]},
        //{"from":"Leads", "to":"Pre Event", "iterate":-4, "points":[350.18468475341797,350,360.18468475341797,350,406.4634895324707,350,406.4634895324707,360,452.7422943115235,360,462.7422943115235,360]},
        //{"from":"Create And Update", "to":"Account", "iterate":-6, "points":[614.9806747436523,510,624.9806747436523,510,763.3303489685059,510,763.3303489685059,509.9999999999999,901.6800231933594,509.9999999999999,911.6800231933594,509.9999999999999]}
        // ]}








        // get work flow on id basis
        public WorkFlowDTO GetWorkFlowOnId(Guid? guid)
        {
            try
            {
            InsertEventLog("GetWorkFlowOnId", EventType.Log, EventColor.yellow, "to get WorkFlowDTO on id","TICRM.BusinessLayer.WorkFlowManager.GetWorkFlowOnId", "");
                return objMapper.GetWorkFlowDTO(dbEnt.WorkFlows.Find(guid)); // get workflow on id and and convert it DTO and then return in response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetWorkFlowOnId", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.WorkFlowManager.GetWorkFlowOnId", "");
                throw ex;
            }


        }


    

    }
}
