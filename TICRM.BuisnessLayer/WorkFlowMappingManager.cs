using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.BuisnessLayer
{
    public class WorkFlowMappingManager : BaseManager
    {

        public List<WorkFlowMappingDTO> GetWorkFlowMappingList()
        {
            try
            {
                InsertEventLog("GetWorkFlowMappingList", EventType.Log, EventColor.yellow, "getting List of WorkFlowMappingDTO", "TICRM.BusinessLayer.WorkFlowMappingManager.GetWorkFlowMappingList", "");
                List<WorkFlowMappingDTO> workFlowMappingDTOs = new List<WorkFlowMappingDTO>(); // create strongly type list Object of WorkFlowMapping DTO

                List<WorkFlowMapping> workFlowMappings = dbEnt.WorkFlowMappings.Include(w => w.WorkFlow).ToList(); // Get List Of Activities from DB
                // apply iteration on workFlowMappings
                foreach (WorkFlowMapping item in workFlowMappings)
                {
                    workFlowMappingDTOs.Add(objMapper.GetWorkFlowMappingDTO(item)); // add in a list object
                }
                return workFlowMappingDTOs; // return Collection Object in Response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetWorkFlowMappingList", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.WorkFlowMappingManager.GetWorkFlowMappingList", "");
                throw;
            }
        }

        public bool SaveWorkFlowMapping(WorkFlowMappingDTO workFlowMappingDTO, string CurrentUserId, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SaveWorkFlowMapping", EventType.Log, EventColor.yellow, "Enter", "TICRM.BusinessLayer.WorkFlowMappingManager.SaveWorkFlowMapping", CurrentUserId);
                WorkFlowMapping workFlowMapping; // create a new object
                workFlowMapping = objMapper.GetWorkFlowMapping(workFlowMappingDTO); // pass parameter object to activity object
                if (isEditMode) // check if is is edit mode is true
                {
                    WorkFlowMapping dbData = dbEnt.WorkFlowMappings.FirstOrDefault(x => x.WorkFlowMappingId == workFlowMapping.WorkFlowMappingId); // get data from database and pass in new WorkFlowMappings class object

                    if (dbData != null) // check if data is null
                    {

                        InsertEventLog("SaveWorkFlowMapping", EventType.Log, EventColor.yellow, "going to delete work flow mapping on id=" + workFlowMappingDTO.WorkFlowMappingId + "", "TICRM.BusinessLayer.WorkFlowMappingManager.SaveWorkFlowMapping", CurrentUserId);
                        if (isDeleteMode) // if is delete mode is true 
                        {
                            dbEnt.WorkFlowMappings.Remove(dbData); // remove object in database
                        }
                        else
                        {
                            InsertEventLog("SaveWorkFlowMapping", EventType.Log, EventColor.yellow, "going to update work flow mapping on id=" + workFlowMappingDTO.WorkFlowMappingId + "", "TICRM.BusinessLayer.WorkFlowMappingManager.SaveWorkFlowMapping", CurrentUserId);
                            dbData.WorkFlowId = workFlowMapping.WorkFlowId;
                            dbData.SourceType = workFlowMapping.SourceType;
                            dbData.SourceColumn = workFlowMapping.SourceColumn;
                            dbData.SourceValue = workFlowMapping.SourceValue;
                            dbData.SourceData = workFlowMapping.SourceData;
                            dbData.Action = workFlowMapping.Action;
                            dbData.UpdatedDate = DateTime.Now;
                            dbData.UpdatedBy = CurrentUserId;
                            dbEnt.Entry(dbData).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        InsertEventLog("SaveWorkFlowMapping", EventType.Log, EventColor.yellow, "data is null for work flow mapping on id=" + workFlowMappingDTO.WorkFlowMappingId + "", "TICRM.BusinessLayer.WorkFlowMappingManager.SaveWorkFlowMapping", CurrentUserId);
                        return false; // return false if no any condition found for edit and delete
                    }

                    if (dbEnt.SaveChanges() > 0) // check if database save changes is done return true
                    {
                        InsertEventLog("SaveWorkFlowMapping", EventType.Log, EventColor.yellow, "work flow mapping saved Successfully", "TICRM.BusinessLayer.WorkFlowMappingManager.SaveWorkFlowMapping", CurrentUserId);
                        return true;
                    }

                }
                else
                {
                    InsertEventLog("SaveWorkFlowMapping", EventType.Log, EventColor.yellow, "Create New Record of WorkFlow Mapping", "TICRM.BusinessLayer.WorkFlowMappingManager.SaveWorkFlowMapping", CurrentUserId);
                    workFlowMapping.WorkFlowMappingId = Guid.NewGuid();
                    workFlowMapping.CreatedBy = CurrentUserId;
                    workFlowMapping.IsDone = false;
                    workFlowMapping.CreatedDate = DateTime.Now;
                    dbEnt.WorkFlowMappings.Add(workFlowMapping); // add in a database
                    if (dbEnt.SaveChanges() > 0)
                    {
                        InsertEventLog("SaveWorkFlowMapping", EventType.Log, EventColor.yellow, "new workflow mapping saved Successfully", "TICRM.BusinessLayer.WorkFlowMappingManager.SaveWorkFlowMapping", CurrentUserId);
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                InsertEventMonitor("SaveWorkFlowMapping", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.WorkFlowMappingManager.SaveWorkFlowMapping", CurrentUserId);
                throw ex;
            }
            return false;
        }

        public WorkFlowMappingDTO GetWorkFlowMappingOnId(Guid? guid)
        {
            try
            {
                InsertEventLog("GetWorkFlowMappingOnId", EventType.Log, EventColor.yellow, "going to get workflow mapping on Id='" + guid + "'", "TICRM.BusinessLayer.WorkFlowMappingManager.GetWorkFlowMappingOnId", "");
                return objMapper.GetWorkFlowMappingDTO(dbEnt.WorkFlowMappings.Include(w => w.WorkFlow).FirstOrDefault(x => x.WorkFlowMappingId == guid)); // get WorkFlowMapping on id and and convert it DTO and then return in response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetWorkFlowMappingOnId", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.WorkFlowMappingManager.GetWorkFlowMappingOnId", "");
                throw ex;
            }
        }





        public List<workflowDataTypeDTO> GetWorkFlowTypeList(string type)
        {
            try
            {
                InsertEventLog("GetWorkFlowTypeList", EventType.Log, EventColor.yellow, "going to get list of workflowDataTypeDTO", "TICRM.BusinessLayer.WorkFlowMappingManager.GetWorkFlowTypeList", "");
                List<workflowDataTypeDTO> vs = new List<workflowDataTypeDTO>();

                //var Query1 = typeof(WorkFlow).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                if (type == EntityTypes.Account.ToString())
                {
                    InsertEventLog("GetWorkFlowTypeList", EventType.Log, EventColor.yellow, "going to get list of workflowDataTypeDTO of account", "TICRM.BusinessLayer.WorkFlowMappingManager.GetWorkFlowTypeList", "");
                    PropertyInfo[] Query = typeof(Account).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    foreach (PropertyInfo item in Query)
                    {
                        if ((item.PropertyType.Name == "String" || item.PropertyType.FullName.Contains("System.Int32") || item.PropertyType.FullName.Contains("System.Guid"))
                            && ExcludedColumns.CreatedDate != item.Name
                            && ExcludedColumns.CreatedBy != item.Name
                            && ExcludedColumns.UpdatedDate != item.Name
                            && ExcludedColumns.UpdatedBy != item.Name
                            && ExcludedColumns.AccountId != item.Name)
                        {
                            workflowDataTypeDTO obj = new workflowDataTypeDTO();
                            obj.ColumnName = item.Name.ToString();
                            // apply tennary operator and another name of it is misc operator
                            obj.DataType = item.PropertyType.Name.ToString() == "String" ? item.PropertyType.Name.ToString()
                                            : item.PropertyType.FullName.Contains("System.Int32") ? "int"
                                            : item.PropertyType.FullName.Contains("System.Guid") ? "Guid" : "";
                            vs.Add(obj);
                        }
                    }

                }
                else if (type == EntityTypes.Lead.ToString())
                {
                    InsertEventLog("GetWorkFlowTypeList", EventType.Log, EventColor.yellow, "going to get list of workflowDataTypeDTO of Lead", "TICRM.BusinessLayer.WorkFlowMappingManager.GetWorkFlowTypeList", "");
                    PropertyInfo[] Query = typeof(Lead).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    foreach (PropertyInfo item in Query)
                    {
                        if ((item.PropertyType.Name == "String" || item.PropertyType.FullName.Contains("System.Int32") || item.PropertyType.FullName.Contains("System.Guid"))
                            && ExcludedColumns.CreatedDate != item.Name
                            && ExcludedColumns.CreatedBy != item.Name
                            && ExcludedColumns.UpdatedDate != item.Name
                            && ExcludedColumns.UpdatedBy != item.Name
                            && ExcludedColumns.LeadId != item.Name)
                        {
                            workflowDataTypeDTO obj = new workflowDataTypeDTO();
                            obj.ColumnName = item.Name.ToString();
                            obj.DataType = item.PropertyType.Name.ToString() == "String" ? item.PropertyType.Name.ToString()
                                            : item.PropertyType.FullName.Contains("System.Int32") ? "int"
                                            : item.PropertyType.FullName.Contains("System.Guid") ? "Guid" : "";
                            vs.Add(obj);
                        }
                    }

                }
                return vs;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetWorkFlowTypeList", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.WorkFlowMappingManager.GetWorkFlowTypeList", "");
                throw;
            }
        }

        public List<WorkFlowTypeDDdto> GetWorkFlowTypeDDList(string type, string column)
        {
            try
            {
                InsertEventLog("GetWorkFlowTypeDDList", EventType.Log, EventColor.yellow, "get list of workflow type to load dropdown", "TICRM.BusinessLayer.WorkFlowMappingManager.GetWorkFlowTypeDDList", "");
                List<WorkFlowTypeDDdto> vs = new List<WorkFlowTypeDDdto>();

                if (type == EntityTypes.Account.ToString())
                {

                    InsertEventLog("GetWorkFlowTypeDDList", EventType.Log, EventColor.yellow, "get list of workflow type to load dropdown of Account", "TICRM.BusinessLayer.WorkFlowMappingManager.GetWorkFlowTypeDDList", "");
                    if (column == "AccountTypeId")
                    {
                        List<AccountType> query = dbEnt.AccountTypes.ToList();
                        foreach (AccountType item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Name;
                            obj.Id = item.AccountTypeId.ToString();
                            vs.Add(obj);
                        }

                    }
                    else if (column == "Name")
                    {
                        List<Account> query = dbEnt.Accounts.ToList();
                        foreach (Account item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Name;
                            obj.Id = item.AccountId.ToString();
                            vs.Add(obj);
                        }
                    }
                    else if (column == "AccountSizeId")
                    {
                        List<AccountSize> query = dbEnt.AccountSizes.ToList();
                        foreach (AccountSize item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Name;
                            obj.Id = item.AccountSizeId.ToString();
                            vs.Add(obj);
                        }
                    }
                    else if (column == "IndustryId")
                    {
                        List<Industry> query = dbEnt.Industries.ToList();
                        foreach (Industry item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Name;
                            obj.Id = item.IndustryId.ToString();
                            vs.Add(obj);
                        }
                    }
                    else if (column == "StatusId")
                    {
                        List<Status> query = dbEnt.Status.ToList();
                        foreach (Status item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Name;
                            obj.Id = item.StatusId.ToString();
                            vs.Add(obj);
                        }
                    }
                    else if (column == "AssignedUser")
                    {
                        List<User> query = dbEnt.Users.ToList();
                        foreach (User item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Name;
                            obj.Id = item.UserId.ToString();
                            vs.Add(obj);
                        }
                    }
                    else if (column == "AssignedTeam")
                    {
                        List<Team> query = dbEnt.Teams.ToList();
                        foreach (Team item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Name;
                            obj.Id = item.TeamId.ToString();
                            vs.Add(obj);
                        }
                    }
                    else if (column == "ShippingAddress")
                    {
                        List<Address> query = dbEnt.Addresses.ToList();
                        foreach (Address item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Street1;
                            obj.Id = item.AddressId.ToString();
                            vs.Add(obj);
                        }
                    }
                    else if (column == "BillingAddress")
                    {
                        List<Address> query = dbEnt.Addresses.ToList();
                        foreach (Address item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Street1;
                            obj.Id = item.AddressId.ToString();
                            vs.Add(obj);
                        }
                    }


                }
                else if (type == EntityTypes.Lead.ToString())
                {
                    InsertEventLog("GetWorkFlowTypeDDList", EventType.Log, EventColor.yellow, "get list of workflow type to load dropdown of Lead", "TICRM.BusinessLayer.WorkFlowMappingManager.GetWorkFlowTypeDDList", "");
                    if (column == "Name")
                    {
                        List<Lead> query = dbEnt.Leads.ToList();
                        foreach (Lead item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Name;
                            obj.Id = item.LeadId.ToString();
                            vs.Add(obj);
                        }

                    }
                    else if (column == "LeadTypeId")
                    {
                        List<LeadType> query = dbEnt.LeadTypes.ToList();
                        foreach (LeadType item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Name;
                            obj.Id = item.LeadTypeId.ToString();
                            vs.Add(obj);
                        }
                    }
                    else if (column == "LeadSourceId")
                    {
                        List<LeadSource> query = dbEnt.LeadSources.ToList();
                        foreach (LeadSource item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Name;
                            obj.Id = item.LeadSourceId.ToString();
                            vs.Add(obj);
                        }
                    }
                    else if (column == "AddressId")
                    {
                        List<Address> query = dbEnt.Addresses.ToList();
                        foreach (Address item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Street1;
                            obj.Id = item.AddressId.ToString();
                            vs.Add(obj);
                        }
                    }
                    else if (column == "IndustryId")
                    {
                        List<Industry> query = dbEnt.Industries.ToList();
                        foreach (Industry item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Name;
                            obj.Id = item.IndustryId.ToString();
                            vs.Add(obj);
                        }
                    }
                    else if (column == "StatusId")
                    {
                        List<Status> query = dbEnt.Status.ToList();
                        foreach (Status item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Name;
                            obj.Id = item.StatusId.ToString();
                            vs.Add(obj);
                        }
                    }
                    else if (column == "AssignedUser")
                    {
                        List<User> query = dbEnt.Users.ToList();
                        foreach (User item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Name;
                            obj.Id = item.UserId.ToString();
                            vs.Add(obj);
                        }
                    }
                    else if (column == "AssignedTeam")
                    {
                        List<Team> query = dbEnt.Teams.ToList();
                        foreach (Team item in query)
                        {
                            WorkFlowTypeDDdto obj = new WorkFlowTypeDDdto();
                            obj.Name = item.Name;
                            obj.Id = item.TeamId.ToString();
                            vs.Add(obj);
                        }
                    }
                }

                //var Query1 = typeof(WorkFlow).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                return vs;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetWorkFlowTypeList", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.WorkFlowMappingManager.GetWorkFlowTypeDDList", "");
                throw;
            }
        }








    }
}
