using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.BuisnessLayer
{
    public class ServiceCallManager : BaseManager
    {
        public ServiceCallManager()
        {
            Urgencies = GetUrgencies();
            WorkStages = GetWorkStages();
        }


        public List<UrgencyDto> Urgencies { get; set; }
        public List<WorkStageDto> WorkStages { get; set; }

        /// <summary>
        /// Get Urgencies 
        /// </summary>
        /// <returns></returns>
        public List<UrgencyDto> GetUrgencies()
        {
            try
            {
                InsertEventLog("GetUrgencies", EventType.Log, EventColor.yellow, "going to getting list of UrgencyDto","TICRM.BusinessLayer.ServiceCallManager.GetUrgencies", "");
                List<UrgencyDto> urgencyDtos = new List<UrgencyDto>();

                // apply iteration on dbEnt.Urgencies
                foreach (Urgency item in dbEnt.Urgencies)
                {
                    urgencyDtos.Add(objMapper.GetUrgencyDTO(item));
                }
                return urgencyDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetUrgencies", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ServiceCallManager.GetUrgencies", "");
                throw;
            }
        }

        /// <summary>
        /// Get Work Stages DTOs
        /// </summary>
        /// <returns></returns>
        public List<WorkStageDto> GetWorkStages()
        {
            try
            {
                InsertEventLog("GetWorkStages", EventType.Log, EventColor.yellow, "going to getting list of WorkStageDto","TICRM.BusinessLayer.ServiceCallManager.GetWorkStages", "");
                List<WorkStageDto> workStageDtos = new List<WorkStageDto>();

                // apply iteration on dbEnt.WorkStages
                foreach (WorkStage item in dbEnt.WorkStages)
                {
                    workStageDtos.Add(objMapper.GetWorkStageDTO(item));
                }
                return workStageDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetWorkStages", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ServiceCallManager.GetWorkStages", "");
                throw;
            }
        }




        // get service call from id
        public ServiceCallDto GetServiceCall(Guid? guid)
        {
            try
            {
                InsertEventLog("GetServiceCall", EventType.Log, EventColor.yellow, "going to ServiceCall on id=" + guid + "","TICRM.BusinessLayer.ServiceCallManager.GetServiceCall", "");
                return objMapper.GetServiceCallDTO(dbEnt.ServiceCalls.Find(guid));
            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetServiceCall", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ServiceCallManager.GetWorkStages", "");
                throw ex;
            }


        }
        /// <summary>
        /// Get serviceCall list 
        /// </summary>
        /// <returns></returns>
        public List<ServiceCallDto> GetServiceCalls()
        {
            try
            {
                InsertEventLog("GetServiceCalls", EventType.Log, EventColor.yellow, "going to getting list of ServiceCallDto","TICRM.BusinessLayer.ServiceCallManager.GetServiceCalls", "");
                List<ServiceCallDto> serviceCallDtos = new List<ServiceCallDto>();
                List<ServiceCall> serviceCalls = dbEnt.ServiceCalls.Include(s => s.Status).Include(s => s.Team).Include(s => s.Urgency).Include(s => s.User).Include(s => s.WorkStage).Where(a => a.IsDeleted != true).ToList();
                foreach (ServiceCall item in serviceCalls)
                {
                    serviceCallDtos.Add(objMapper.GetServiceCallDTO(item));
                }
                return serviceCallDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetServiceCalls", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ServiceCallManager.GetServiceCalls", "");
                throw;
            }


        }


        /// <summary>
        /// save and edit ServiceCallDto 
        /// </summary>
        /// <param name="readng"></param>
        /// <returns></returns>
        public bool SaveServiceCall(ServiceCallDto readng, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SaveServiceCall", EventType.Log, EventColor.yellow, "Enter","TICRM.BusinessLayer.ServiceCallManager.SaveServiceCall", "");
                ServiceCall serviceCall;
                if (isEditMode)
                {

                    InsertEventLog("SaveServiceCall", EventType.Log, EventColor.yellow, "going to update service call on id=" + readng.ServiceCallId + "","TICRM.BusinessLayer.ServiceCallManager.SaveServiceCall", "");
                    serviceCall = objMapper.GetServiceCall(readng);
                    if (isDeleteMode)
                    {
                        InsertEventLog("SaveServiceCall", EventType.Log, EventColor.yellow, "going to delete service call on id","TICRM.BusinessLayer.ServiceCallManager.SaveServiceCall", "");

                        ServiceCall service = dbEnt.ServiceCalls.FirstOrDefault(x => x.ServiceCallId == serviceCall.ServiceCallId);
                        service.IsDeleted = true;

                    }
                    else
                    {
                        dbEnt.Entry(serviceCall).State = EntityState.Modified;
                    }
                }
                else
                {
                    InsertEventLog("SaveServiceCall", EventType.Log, EventColor.yellow, "going to Create new record in DB","TICRM.BusinessLayer.ServiceCallManager.SaveServiceCall", "");
                    serviceCall = objMapper.GetServiceCall(readng);
                    serviceCall.ServiceCallId = Guid.NewGuid();
                    dbEnt.ServiceCalls.Add(serviceCall);
                }

                dbEnt.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                InsertEventMonitor("SaveServiceCall", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ServiceCallManager.SaveServiceCall", "");
                throw ex;
            }


        }


    }
}
