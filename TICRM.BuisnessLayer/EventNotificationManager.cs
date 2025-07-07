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
    public class EventNotificationManager : BaseManager
    {
        public List<EventNotificationDTO> GetEventNotificationList()
        {
            try
            {
                InsertEventLog("GetEventNotificationList", EventType.Log, EventColor.yellow, "to get list of event Notification ", "TICRM.BuisnessLayer.EventNotificationManager.GetEventNotificationList", "");
                List<EventNotificationDTO> eventNotificationDTO = new List<EventNotificationDTO>();// create strongly type list Object of EventNotification DTO

                List<EventNotification> eventNotifications = dbEnt.EventNotifications.ToList(); // Get List Of EventNotifications from DB
                // apply iteration on eventNotifications
                foreach (EventNotification item in eventNotifications)
                {
                    eventNotificationDTO.Add(objMapper.GetEventNotificationDTO(item)); // add in a list object
                }
                return eventNotificationDTO; // return Collection Object in Response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetEventNotificationList", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.EventNotificationManager.GetEventNotificationList", "");
                throw;
            }
        }

        public bool SaveEventNotification(EventNotificationDTO eventNotificationDTO, string CurrentUserId, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SaveEventNotification", EventType.Log, EventColor.yellow, "Enter ", "TICRM.BuisnessLayer.EventNotificationManager.SaveEventNotification", CurrentUserId);

                EventNotification eventNotification; // create a new object
                eventNotification = objMapper.GetEventNotification(eventNotificationDTO); // pass parameter object to Event Notification object
                if (isEditMode) // check if is is edit mode is true
                {
                    EventNotification dbData = dbEnt.EventNotifications.FirstOrDefault(x => x.EventNotificationId == eventNotification.EventNotificationId); // get data from database and pass in new EventNotifications class object

                    if (dbData != null) // check if data is null
                    {

                        if (isDeleteMode) // if is delete mode is true 
                        {
                            InsertEventLog("SaveEventNotification", EventType.Log, EventColor.yellow, "to delete event monitor on id=" + eventNotification.EventNotificationId + " ", "TICRM.BuisnessLayer.EventNotificationManager.SaveEventNotification", CurrentUserId);

                            dbEnt.EventNotifications.Remove(dbData); // remove object in database
                        }
                        else
                        {
                            InsertEventLog("SaveEventMonitor", EventType.Log, EventColor.yellow, "to update event monitor on id=" + eventNotification.EventNotificationId + " ", "TICRM.BuisnessLayer.EventNotificationManager.SaveEventNotification", CurrentUserId);
                            dbData.Name = eventNotification.Name;
                            dbData.Type = eventNotification.Type;
                            dbData.Status = eventNotification.Status;
                            dbData.Message = eventNotification.Message;
                            dbData.Color = eventNotification.Color;
                            dbData.IPAddress = eventNotification.IPAddress;
                            dbData.UpdatedDate = DateTime.Now;
                            dbData.UpdatedBy = CurrentUserId;
                            dbEnt.Entry(dbData).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        InsertEventLog("SaveEventNotification", EventType.Log, EventColor.yellow, "to event monitor on id=" + eventNotification.EventNotificationId + " return null data ", "TICRM.BuisnessLayer.EventNotificationManager.SaveEventNotification", CurrentUserId);
                        return false; // return false if no any condition found for edit and delete
                    }

                    if (dbEnt.SaveChanges() > 0) // check if database save changes is done return true
                    {
                        return true;
                    }

                }
                else
                {
                    InsertEventLog("SaveEventNotification", EventType.Log, EventColor.yellow, "create new record of Event Nootification ", "TICRM.BuisnessLayer.EventNotificationManager.SaveEventNotification", CurrentUserId);
                    eventNotification.EventNotificationId = Guid.NewGuid();
                    eventNotification.CreatedBy = CurrentUserId;
                    eventNotification.CreatedDate = DateTime.Now;
                    dbEnt.EventNotifications.Add(eventNotification); // add in a database
                    if (dbEnt.SaveChanges() > 0)
                    {
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                InsertEventMonitor("SaveEventNotification", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.EventNotificationManager.SaveEventNotification", CurrentUserId);
                throw ex;
            }
            return false;
        }

        public EventNotificationDTO GetEventNotificationOnId(Guid? guid)
        {
            try
            {
                InsertEventLog("GetEventNotificationOnId", EventType.Log, EventColor.yellow, "to get event monitor on id=" + guid + " ", "TICRM.BuisnessLayer.EventNotificationManager.GetEventNotificationOnId", "");
                return objMapper.GetEventNotificationDTO(dbEnt.EventNotifications.FirstOrDefault(x => x.EventNotificationId == guid)); // Get EventNotification On Id and and convert it DTO and then return in response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetEventNotificationOnId", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.EventNotificationManager.GetEventNotificationOnId", "");
                throw ex;
            }
        }
    }
}
