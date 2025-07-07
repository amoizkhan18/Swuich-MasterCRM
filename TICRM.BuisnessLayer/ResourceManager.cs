using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.BuisnessLayer
{
    public class ResourceManager : BaseManager
    {
        public ResourceManager()
        {

        }




        public ResourceDto GetResource(Guid? guid)
        {
            try
            {
                InsertEventLog("GetResource", EventType.Log, EventColor.yellow, "Get ResourceDto on resource id","TICRM.BusinessLayer.ResourceManager.GetResource", "");
                return objMapper.GetResourceDTO(dbEnt.Resources.Find(guid));
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetResource", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ResourceManager.GetResource", "");
                throw ex;
            }


        }
        /// <summary>
        /// Get resource list 
        /// </summary>
        /// <returns></returns>
        public List<ResourceDto> GetResources()
        {
            try
            {
                InsertEventLog("GetResources", EventType.Log, EventColor.yellow, "Get list of ResourceDto on resource id","TICRM.BusinessLayer.ResourceManager.GetResources", "");
                List<ResourceDto> resourceDtos = new List<ResourceDto>();
                List<Resource> resources = dbEnt.Resources.Include(r => r.Status).Include(r => r.Team).Include(r => r.User).Include(r => r.Address1).Include(r => r.Address2).Where(a => a.IsDeleted != true).ToList();
                foreach (Resource item in resources)
                {
                    resourceDtos.Add(objMapper.GetResourceDTO(item));
                }
                return resourceDtos;
            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetResources", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ResourceManager.GetResources", "");
                throw;
            }


        }
        /// <summary>
        /// save and edit ResourceDto 
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public bool SaveResource(ResourceDto res, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SaveResource", EventType.Log, EventColor.yellow, "Enter","TICRM.BusinessLayer.ResourceManager.SaveResource", "");
                Resource resource;
                if (isEditMode)
                {
                    InsertEventLog("SaveResource", EventType.Log, EventColor.yellow, "going to edit Resource","TICRM.BusinessLayer.ResourceManager.SaveResource", "");
                    resource = objMapper.GetResource(res);
                    if (isDeleteMode)
                    {
                        InsertEventLog("SaveResource", EventType.Log, EventColor.yellow, "going to delete on id","TICRM.BusinessLayer.ResourceManager.SaveResource", "");
                        Resource resourceForDelete = dbEnt.Resources.FirstOrDefault(x => x.ResourceId == resource.ResourceId);
                        resourceForDelete.IsDeleted = true;
                    }
                    else
                    {
                        dbEnt.Entry(resource).State = EntityState.Modified;
                    }
                }
                else
                {
                    InsertEventLog("SaveResource", EventType.Log, EventColor.yellow, "Create new Record of Resource","TICRM.BusinessLayer.ResourceManager.SaveResource", "");
                    resource = objMapper.GetResource(res);
                    resource.ResourceId = Guid.NewGuid();
                    dbEnt.Resources.Add(resource);
                }

                dbEnt.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                InsertEventMonitor("SaveResource", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ResourceManager.SaveResource", "");
                throw ex;
            }


        }





    }
}
