using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.BuisnessLayer
{
    public class ActivityManager : BaseManager
    {

        // Get Activities
        public List<ActivityDTO> GetActivities()
        {
            try
            {
                InsertEventLog("GetActivities", EventType.Log, EventColor.yellow, "Successfully Enter in GetActivities","TICRM.BusinessLayer.ActivityManager", "");

                List<ActivityDTO> ActivityDtos = new List<ActivityDTO>(); // create list Object of Activity DTO

                List<DAL.Activity> activities = dbEnt.Activities.ToList(); // Get List Of Activities from DB
                // apply iteration on getting activities
                foreach (DAL.Activity item in activities)
                {
                    ActivityDtos.Add(objMapper.GetActivityDTO(item)); // add in a list object
                }
                return ActivityDtos; // return List Object in Response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetActivities", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ActivityManager", "");
                throw;
            }
        }

        public bool SaveActivity(ActivityDTO dvc, string CurrentUserId, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SaveActivity", EventType.Log, EventColor.yellow, "Successfully Enter in SaveActivity","TICRM.BusinessLayer.ActivityManager", "");

                Activity activity; // create a new object
                activity = objMapper.GetActivity(dvc); // pass parameter object to activity object
                if (isEditMode) // check if is is edit mode is true
                {

                    
                    Activity dbData = dbEnt.Activities.FirstOrDefault(x => x.ActivityId == activity.ActivityId); // get data from database and pass in new activity class object

                    if (dbData != null) // check if data is null
                    {

                        if (isDeleteMode) // if is delete mode is true 
                        {
                            InsertEventLog("SaveActivity", EventType.Log, EventColor.yellow, "For Delete Successfully Enter in SaveActivity","TICRM.BusinessLayer.ActivityManager", "");
                            //activity.IsDeleted = true;
                            dbEnt.Activities.Remove(dbData); // remove object in database
                        }
                        else
                        {
                        InsertEventLog("SaveActivity", EventType.Log, EventColor.yellow, "For Edit Successfully Enter", "TICRMTICRM.BuisnessLayer.ActivityManager.SaveActivity", "");
                            dbData.Name = activity.Name;
                            dbData.Description = activity.Description;
                            dbData.Type = activity.Type;
                            dbData.RelatedTo = activity.RelatedTo;
                            dbData.RelatedToID = activity.RelatedToID;
                            dbData.StatusId = activity.StatusId;
                            dbData.AssignedUser = activity.AssignedUser;
                            dbData.AssignedTeam = activity.AssignedTeam;
                            dbData.UpdatedDate = DateTime.Now;
                            dbData.UpdatedBy = CurrentUserId;
                            dbEnt.Entry(dbData).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        InsertEventLog("SaveActivity", EventType.Log, EventColor.yellow, "For Edit and Delete: Data is null on id "+ dvc.ActivityId ,"TICRM.BuisnessLayer.ActivityTemplateManager.SaveActivity", "");
                        return false; // return false if no any condition found for edit and delete
                    }

                    if (dbEnt.SaveChanges() > 0) // check if database save changes is done return true
                    {

                        return true;
                    }

                }
                else
                {
                    InsertEventLog("SaveActivity", EventType.Log, EventColor.yellow, "For Create Successfully Enter", "TICRMTICRM.BuisnessLayer.ActivityManager.SaveActivity", "");

                    activity = objMapper.GetActivity(dvc);  // pass parameter activity object to activity object
                    activity.ActivityId = Guid.NewGuid();
                    activity.CreatedBy = CurrentUserId;
                    activity.CreatedDate = DateTime.Now;
                    dbEnt.Activities.Add(activity); // add in a database
                    if (dbEnt.SaveChanges() > 0)
                    {
                        return true;
                    }
                }



            }
            catch (Exception ex)
            {
                InsertEventMonitor("SaveActivity", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRMTICRM.BuisnessLayer.ActivityManager.SaveActivity", "");
                throw ex;
            }
            return false;

        }


        public ActivityDTO GetActivity(Guid? guid)
        {
            try
            {
                        InsertEventLog("GetActivity", EventType.Log, EventColor.yellow, "Successfully Enter in GetActivity to Get Data on id","TICRM.BusinessLayer.ActivityManager", "");
                return objMapper.GetActivityDTO(dbEnt.Activities.Find(guid)); // get activity on id and and convert it DTO and then return in response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetAccountSize", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ActivityManager", "");
                throw ex;
            }
        }
        

        public List<ActivityDTO> GetAccountActivities(Guid? guid)
        {
            try
            {
                InsertEventLog("GetAccountActivities", EventType.Log, EventColor.yellow, "Successfully Enter to Get Data on Id", "TICRM.BusinessLayer.ActivityManager.GetAccountActivities", "");
                List<ActivityDTO> ActivityDtos = new List<ActivityDTO>(); // create list Object of Activity DTo

                List<DAL.Activity> activities = dbEnt.Activities.Where(x => x.RelatedTo == RelatedToEnum.Account.ToString() && x.RelatedToID == guid).ToList(); // Get List Of Activities from DB
                // apply iteration on activities
                foreach (DAL.Activity item in activities)
                {
                    ActivityDtos.Add(objMapper.GetActivityDTO(item)); // add in a list object
                }
                return ActivityDtos; // return List Object in Response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetAccountActivities", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ActivityManager.GetAccountActivities", "");
                throw;
            }
        }





    








    }
}
