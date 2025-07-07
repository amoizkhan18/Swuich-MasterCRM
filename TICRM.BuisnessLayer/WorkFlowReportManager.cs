using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.BuisnessLayer
{
    public class WorkFlowReportManager : BaseManager
    {

        // GetWorkFlows
        public List<WorkFlowReportDTO> GetWorkFlowReports()
        {
            try
            {

            InsertEventLog("GetWorkFlowReports", EventType.Log, EventColor.yellow, "getting List of WorkFlowReportDTO","TICRM.BusinessLayer.WorkFlowReportManager.GetWorkFlowReports", "");
                List<WorkFlowReportDTO> workflowreportDTO = new List<WorkFlowReportDTO>(); // create list Object of WorkFlowReports DTO

                List<WorkFlowReport> workFlowReports = dbEnt.WorkFlowReports.Include(x=>x.WorkFlow).ToList(); // declare a local variable to Get List Of WorkFlowReports from DB
                // apply iteration on WorkFlowReport
                foreach (WorkFlowReport item in workFlowReports)
                {
                    workflowreportDTO.Add(objMapper.GetWorkFlowReportDTO(item)); // add in a list object
                }
                return workflowreportDTO; // return List Object in Response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetWorkFlowReports", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.WorkFlowReportManager.GetWorkFlowReports", "");
                throw;
            }
        }


        public bool SaveItWorkFlowReport(WorkFlowReportDTO workFlowReportDTO, string CurrentUserId, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
            InsertEventLog("SaveItWorkFlowReport", EventType.Log, EventColor.yellow, "Enter","TICRM.BusinessLayer.WorkFlowReportManager.SaveItWorkFlowReport", CurrentUserId);
                WorkFlowReport workFlowReport; // create a new object
                workFlowReport = objMapper.GetWorkFlowReport(workFlowReportDTO); // pass parameter object to workflow object

                if (isEditMode) // check if is is edit mode is true
                {

                    WorkFlowReport dbData = dbEnt.WorkFlowReports.FirstOrDefault(x => x.WorkFlowReportId == workFlowReportDTO.WorkFlowReportId); // get data from database and pass in new activity class object

                    if (dbData != null) // check if data is null
                    {
                        if (isDeleteMode) // if is delete mode is true 
                        {
            InsertEventLog("SaveItWorkFlowReport", EventType.Log, EventColor.yellow, "going to delete workflow report report on id='"+dbData.WorkFlowReportId+"'","TICRM.BusinessLayer.WorkFlowReportManager.SaveItWorkFlowReport", CurrentUserId);
                            // here we delete hard
                            dbEnt.WorkFlowReports.Remove(dbData); // remove object in database 
                        }
                        else
                        {
            InsertEventLog("SaveItWorkFlowReport", EventType.Log, EventColor.yellow, "going to update workflow report report on id='" + dbData.WorkFlowReportId + "'","TICRM.BusinessLayer.WorkFlowReportManager.SaveItWorkFlowReport", CurrentUserId);
                            dbData.WorkFlowId = workFlowReport.WorkFlowId;
                            dbData.Frequency = workFlowReport.Frequency;
                            dbData.WorkFlowStatus = workFlowReport.WorkFlowStatus;
                            dbData.WorkFlowActionStatus = workFlowReport.WorkFlowActionStatus;
                            dbData.AppliedTo = workFlowReport.AppliedTo;
                            dbData.Action = workFlowReport.Action;
                            dbData.Priority = workFlowReport.Priority;
                            dbData.WorkFlowDesign = workFlowReport.WorkFlowDesign;
                            dbData.UpdatedDate = DateTime.Now;
                            dbData.UpdatedBy = CurrentUserId;
                            dbEnt.Entry(dbData).State = EntityState.Modified;
                        }
                    }
                    else
                    {
            InsertEventLog("SaveItWorkFlowReport", EventType.Log, EventColor.yellow, "Data Is null of workflow report report on id='" + dbData.WorkFlowReportId + "'","TICRM.BusinessLayer.WorkFlowReportManager.SaveItWorkFlowReport", CurrentUserId);
                        return false; // return false if no any condition found for edit and delete
                    }

                    if (dbEnt.SaveChanges() > 0) // check if database save changes is done return true
                    {
            InsertEventLog("SaveItWorkFlowReport", EventType.Log, EventColor.yellow, "workflow report Updated Successfully","TICRM.BusinessLayer.WorkFlowReportManager.SaveItWorkFlowReport", CurrentUserId);
                        return true;
                    }

                }
                else
                {
            InsertEventLog("SaveItWorkFlowReport", EventType.Log, EventColor.yellow, "going to create new record of","TICRM.BusinessLayer.WorkFlowReportManager.SaveItWorkFlowReport", CurrentUserId);
                    workFlowReport.WorkFlowReportId = Guid.NewGuid();
                    workFlowReport.CreatedBy = CurrentUserId;
                    workFlowReport.CreatedDate = DateTime.Now;
                    dbEnt.WorkFlowReports.Add(workFlowReport); // add in a database
                    if (dbEnt.SaveChanges() > 0)
                    {
            InsertEventLog("SaveItWorkFlowReport", EventType.Log, EventColor.yellow, "new work flow report created Successfully","TICRM.BusinessLayer.WorkFlowReportManager.SaveItWorkFlowReport", CurrentUserId);
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {

                InsertEventMonitor("SaveItWorkFlowReport", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.WorkFlowReportManager.SaveItWorkFlowReport", CurrentUserId);
                throw ex;
            }
            return false;

        }

        public WorkFlowReportDTO GetWorkFlowReportOnId(Guid? guid)
        {
            try
            {
            InsertEventLog("GetWorkFlowReportOnId", EventType.Log, EventColor.yellow, "get WorkFlowReportDTO on id'"+guid+"'","TICRM.BusinessLayer.WorkFlowReportManager.GetWorkFlowReportOnId", "");
                return objMapper.GetWorkFlowReportDTO(dbEnt.WorkFlowReports.Find(guid)); // get workFlowReport on id and and convert it DTO and then return in response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetWorkFlowReportOnId", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.WorkFlowReportManager.GetWorkFlowReportOnId", "");
                throw ex;
            }


        }

    }
}
