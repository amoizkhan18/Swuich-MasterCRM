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
    public class EventMonitorManager : BaseManager
    {

        public List<EventMonitorDTO> GetEventMonitorList()
        {
            try
            {
                InsertEventLog("GetEventMonitorList", EventType.Log, EventColor.yellow, "to get list of event Monitor ", "TICRM.BuisnessLayer.EventMonitorManager.GetEventMonitorList", "");
                List<EventMonitorDTO> eventMonitorDTOs = new List<EventMonitorDTO>();// create strongly type list Object of EventMonitor DTO

                List<EventMonitor> eventMonitors = dbEnt.EventMonitors.ToList(); // Get List Of EventMonitors from DB
                // apply iteration on workFlowMappings
                foreach (EventMonitor item in eventMonitors)
                {
                    eventMonitorDTOs.Add(objMapper.GetEventMonitorDTO(item)); // add in a list object
                }
                return eventMonitorDTOs; // return Collection Object in Response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetEventMonitorList", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.EventMonitorManager.GetEventMonitorList", "");
                throw;
            }
        }

        public bool SaveEventMonitor(EventMonitorDTO eventMonitorDTO, string CurrentUserId, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SaveEventMonitor", EventType.Log, EventColor.yellow, "Enter ", "TICRM.BuisnessLayer.EventMonitorManager.SaveEventMonitor", CurrentUserId);

                EventMonitor eventMonitor; // create a new object
                eventMonitor = objMapper.GetEventMonitor(eventMonitorDTO); // pass parameter object to eventMonitor object
                if (isEditMode) // check if is is edit mode is true
                {
                    EventMonitor dbData = dbEnt.EventMonitors.FirstOrDefault(x => x.EventMonitorId == eventMonitor.EventMonitorId); // get data from database and pass in new EventMonitor class object

                    if (dbData != null) // check if data is null
                    {

                        if (isDeleteMode) // if is delete mode is true 
                        {
                            InsertEventLog("SaveEventMonitor", EventType.Log, EventColor.yellow, "to delete event monitor on id="+eventMonitor.EventMonitorId+ " ", "TICRM.BuisnessLayer.EventMonitorManager.SaveEventMonitor", CurrentUserId);

                            dbEnt.EventMonitors.Remove(dbData); // remove object in database
                        }
                        else
                        {
                            InsertEventLog("SaveEventMonitor", EventType.Log, EventColor.yellow, "to update event monitor on id="+eventMonitor.EventMonitorId+ " ", "TICRM.BuisnessLayer.EventMonitorManager.SaveEventMonitor", CurrentUserId);
                            dbData.Name = eventMonitor.Name;
                            dbData.Type = eventMonitor.Type;
                            dbData.Status = eventMonitor.Status;
                            dbData.Message = eventMonitor.Message;
                            dbData.Color = eventMonitor.Color;
                            dbData.IPAddress = eventMonitor.IPAddress;
                            dbData.UpdatedDate = DateTime.Now;
                            dbData.UpdatedBy = CurrentUserId;
                            dbEnt.Entry(dbData).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        InsertEventLog("SaveEventMonitor", EventType.Log, EventColor.yellow, "to event monitor on id=" + eventMonitor.EventMonitorId + " return null data ", "TICRM.BuisnessLayer.EventMonitorManager.SaveEventMonitor", CurrentUserId);
                        return false; // return false if no any condition found for edit and delete
                    }

                    if (dbEnt.SaveChanges() > 0) // check if database save changes is done return true
                    {
                        return true;
                    }

                }
                else
                {
                    InsertEventLog("SaveEventMonitor", EventType.Log, EventColor.yellow, "create new record of activity monitor event ", "TICRM.BuisnessLayer.EventMonitorManager.SaveEventMonitor", CurrentUserId);
                    eventMonitor.EventMonitorId = Guid.NewGuid();
                    eventMonitor.CreatedBy = CurrentUserId;
                    eventMonitor.CreatedDate = DateTime.Now;
                    dbEnt.EventMonitors.Add(eventMonitor); // add in a database
                    if (dbEnt.SaveChanges() > 0)
                    {
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                InsertEventMonitor("SaveEventMonitor", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.EventMonitorManager.SaveEventMonitor", CurrentUserId);
                throw ex;
            }
            return false;
        }

        public EventMonitorDTO GetEventMonitorOnId(Guid? guid)
        {
            try
            {
                InsertEventLog("GetEventMonitorOnId", EventType.Log, EventColor.yellow, "to get event monitor on id=" + guid + " ", "TICRM.BuisnessLayer.EventMonitorManager.GetEventMonitorOnId", "");
                return objMapper.GetEventMonitorDTO(dbEnt.EventMonitors.FirstOrDefault(x => x.EventMonitorId == guid)); // Get EventMonitor On Id and and convert it DTO and then return in response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetEventMonitorOnId", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.EventMonitorManager.GetEventMonitorOnId", "");
                throw ex;
            }
        }


    }
}
