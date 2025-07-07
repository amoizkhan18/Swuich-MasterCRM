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
    public class EmailConfigurationManager : BaseManager
    {
        // EmailConfigurationManager

        public List<EmailConfigurationDTO> GetEmailConfigurationDTOs(string CurrentUserId)
        {
            try
            {
                InsertEventLog("GetEmailConfigurationDTOs", EventType.Log, EventColor.yellow, "going to get list of Email Configuration DTO", "TICRM.BuisnessLayer.EmailConfigurationManager.GetEmailConfigurationDTOs", CurrentUserId);
                List<EmailConfigurationDTO> emailConfigurationDTOs = new List<EmailConfigurationDTO>();// create strongly type list Object of EmailConfiguration DTO

                List<EmailConfiguration> emailConfigurations = dbEnt.EmailConfigurations.ToList(); // Get List Of EmailConfiguration from DB
                // apply iteration on email configration
                foreach (EmailConfiguration item in emailConfigurations)
                {
                    emailConfigurationDTOs.Add(objMapper.GetEmailConfigurationDTO(item)); // add in a list DTO object
                }
                return emailConfigurationDTOs; // return Collection Object in Response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetEmailConfigurationDTOs", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.EmailConfigurationManager.GetEmailConfigurationDTOs", CurrentUserId);
                throw;
            }
        }

        public bool SaveEmailConfiguration(EmailConfigurationDTO emailConfigurationDTO, string CurrentUserId, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SaveEmailConfiguration", EventType.Log, EventColor.yellow, "Successfully Enter in function", "TICRM.BuisnessLayer.EmailConfigurationManager.SaveEmailConfiguration", CurrentUserId);

                EmailConfiguration emailConfiguration; // create a new object
                emailConfiguration = objMapper.GetEmailConfiguration(emailConfigurationDTO); // pass parameter object to Email Configuration object
                if (isEditMode) // check if is is edit mode is true
                {
                    EmailConfiguration dbData = dbEnt.EmailConfigurations.FirstOrDefault(x => x.EmailConfigurationId == emailConfiguration.EmailConfigurationId); // get data from database and pass in new EmailConfiguration class object

                    if (dbData != null) // check if data is null
                    {
                        if (isDeleteMode) // if is delete mode is true 
                        {
                            InsertEventLog("SaveEmailConfiguration", EventType.Log, EventColor.yellow, "to delete Email Configuration on id=" + emailConfiguration.EmailConfigurationId + " ", "TICRM.BuisnessLayer.EmailConfigurationManager.SaveEmailConfiguration", CurrentUserId);
                            dbEnt.EmailConfigurations.Remove(dbData); // remove object in database
                        }
                        else
                        {
                            InsertEventLog("SaveEmailConfiguration", EventType.Log, EventColor.yellow, "to update Email Configuration on id=" + emailConfiguration.EmailConfigurationId + " ", "TICRM.BuisnessLayer.EmailConfigurationManager.SaveEmailConfiguration", CurrentUserId);
                            dbData.EmailConfigurationId = emailConfiguration.EmailConfigurationId;
                            dbData.Email = emailConfiguration.Email;
                            dbData.UserName = emailConfiguration.UserName;
                            dbData.Password = emailConfiguration.Password;
                            dbData.UpdatedDate = DateTime.Now;
                            dbData.UpdatedBy = CurrentUserId;
                            dbEnt.Entry(dbData).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        InsertEventLog("SaveEmailConfiguration", EventType.Log, EventColor.yellow, "to Email Configuration on id=" + emailConfiguration.EmailConfigurationId + " return null data ", "TICRM.BuisnessLayer.EmailConfigurationManager.SaveEmailConfiguration", CurrentUserId);
                        return false; // return false if no any condition found for edit and delete
                    }

                    if (dbEnt.SaveChanges() > 0) // check if database save changes is done return true
                    {
                        return true;
                    }

                }
                else
                {
                    InsertEventLog("SaveEmailConfiguration", EventType.Log, EventColor.yellow, "create new record of Email Configuration", "TICRM.BuisnessLayer.EmailConfigurationManager.SaveEmailConfiguration", CurrentUserId);
                    emailConfiguration.EmailConfigurationId = Guid.NewGuid();
                    emailConfiguration.CreatedBy = CurrentUserId;
                    emailConfiguration.CreatedDate = DateTime.Now;
                    dbEnt.EmailConfigurations.Add(emailConfiguration); // add in a database
                    if (dbEnt.SaveChanges() > 0)
                    {
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                InsertEventMonitor("SaveEmailConfiguration", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.EmailConfigurationManager.SaveEmailConfiguration", CurrentUserId);
                throw ex;
            }
            return false;
        }

        public EmailConfigurationDTO GetEmailConfigurationDtoOnId(Guid? guid,string CurrentUserId)
        {
            try
            {
                InsertEventLog("GetEmailConfigurationDtoOnId", EventType.Log, EventColor.yellow, "to get Email Configuration on id=" + guid + " ", "TICRM.BuisnessLayer.EmailConfigurationManager.GetEmailConfigurationDtoOnId", "");
                return objMapper.GetEmailConfigurationDTO(dbEnt.EmailConfigurations.FirstOrDefault(x => x.EmailConfigurationId == guid)); // Get EmailConfiguration On Id and and convert it DTO and then return in response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetEmailConfigurationDtoOnId", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.EmailConfigurationManager.GetEmailConfigurationDtoOnId", "");
                throw ex;
            }
        }

    }
}
