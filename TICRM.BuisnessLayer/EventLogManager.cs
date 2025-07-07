using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.BuisnessLayer
{
    public class EventLogManager : BaseManager
    {
        public List<EventLogDTO> GetEventLogList()
        {
            try
            {
                InsertEventLog("GetEventLogList", EventType.Log, EventColor.yellow, "to get list of event logs ", "TICRM.BuisnessLayer.EventLogManager.GetEventLogList", "");
                List<EventLogDTO> eventLogDTOs = new List<EventLogDTO>();// create strongly type list Object of EventLog DTO

                List<EventLog> eventLogs = dbEnt.EventLogs.ToList(); // Get List Of EventLogs from DB
                // apply iteration on workFlowMappings
                foreach (EventLog item in eventLogs)
                {
                    eventLogDTOs.Add(objMapper.GetEventLogDTO(item)); // add in a list object
                }
                return eventLogDTOs; // return Collection Object in Response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetEventLogList", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.EventLogManager.GetEventLogList", "");
                throw;
            }
        }

        public bool SaveEventLog(EventLogDTO eventLogDTO, string CurrentUserId, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SaveEventLog",  EventType.Log, EventColor.yellow, "enter ", "TICRM.BuisnessLayer.EventLogManager.SaveEventLog", "");

                EventLog eventLog; // create a new object
                eventLog = objMapper.GetEventLog(eventLogDTO); // pass parameter object to eventLog object
                if (isEditMode) // check if is is edit mode is true
                {
                    EventLog dbData = dbEnt.EventLogs.FirstOrDefault(x => x.EventLogId == eventLog.EventLogId); // get data from database and pass in new EventLog class object

                    if (dbData != null) // check if data is null
                    {
                        if (isDeleteMode) // if is delete mode is true 
                        {
                            InsertEventLog("SaveEventLog", EventType.Log, EventColor.yellow, "enter in Delete mode to delete event log ", "TICRM.BuisnessLayer.EventLogManager.SaveEventLog", "");
                            dbEnt.EventLogs.Remove(dbData); // remove object in database
                        }
                        else
                        {
                            InsertEventLog("SaveEventLog", EventType.Log, EventColor.yellow, "enter in edit mode to update Data event log ", "TICRM.BuisnessLayer.EventLogManager.SaveEventLog", "");
                            dbData.Name = eventLog.Name;
                            dbData.Type = eventLog.Type;
                            dbData.Status = eventLog.Status;
                            dbData.Message = eventLog.Message;
                            dbData.Color = eventLog.Color;
                            dbData.IPAddress = eventLog.IPAddress;
                            dbData.UpdatedDate = DateTime.Now;
                            dbData.UpdatedBy = CurrentUserId;
                            dbEnt.Entry(dbData).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        InsertEventLog("SaveEventLog", EventType.Log, EventColor.yellow, "enter in edit mode data is null ", "TICRM.BuisnessLayer.EventLogManager.SaveEventLog", "");
                        return false; // return false if no any condition found for edit and delete
                    }

                    if (dbEnt.SaveChanges() > 0) // check if database save changes is done return true
                    {
                        InsertEventLog("SaveEventLog", EventType.Log, EventColor.yellow, "for edit and delete data is saved in DB ", "TICRM.BuisnessLayer.EventLogManager.SaveEventLog", "");
                        return true;
                    }

                }
                else
                {
                    InsertEventLog("SaveEventLog", EventType.Log, EventColor.yellow, "Enter In Create new record ", "TICRM.BuisnessLayer.EventLogManager.SaveEventLog", "");
                    eventLog.EventLogId = Guid.NewGuid();
                    eventLog.CreatedBy = CurrentUserId;
                    eventLog.CreatedDate = DateTime.Now;
                    dbEnt.EventLogs.Add(eventLog); // add in a database
                    if (dbEnt.SaveChanges() > 0)
                    {
                        InsertEventLog("SaveEventLog", EventType.Log, EventColor.yellow, "New Record is saved ", "TICRM.BuisnessLayer.EventLogManager.SaveEventLog", "");
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                InsertEventMonitor("SaveEventLog", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.EventLogManager.SaveEventLog", "");
                throw ex;
            }
            return false;
        }

        public EventLogDTO GetEventLogOnId(Guid? guid)
        {
            try
            {
                InsertEventLog("GetEventLogOnId", EventType.Log, EventColor.yellow, "get event log on id ", "TICRM.BuisnessLayer.EventLogManager.GetEventLogOnId", "");
                return objMapper.GetEventLogDTO(dbEnt.EventLogs.FirstOrDefault(x => x.EventLogId == guid)); // Get EventLog On Id and and convert it DTO and then return in response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetEventLogOnId", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.EventLogManager.GetEventLogOnId", "");
                throw ex;
            }
        }



    }
}
