using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TICRM.BuisnessLayer.Base;
using TICRM.DTOs;
using TICRM.DAL;

namespace TICRM.BuisnessLayer
{
    public class CustomerAssetManager : BaseManager
    {

        public CustomerAssetManager()
        {
            CustomerAssetTypes = GetCustomerAssetTypes();
        }
        public List<CustomerAssetTypeDto> CustomerAssetTypes { get; set; }

        //public List<Account> Accounts { get; set; }

        /// <summary>
        /// Get Customer Asset type 
        /// </summary>
        /// <returns></returns>
        public List<CustomerAssetTypeDto> GetCustomerAssetTypes()
        {
            try
            {
                InsertEventLog("GetCustomerAssetTypes",  EventType.Log, EventColor.yellow, "Successfully Enter", "TICRM.BuisnessLayer.CustomerAssetManager.GetCustomerAssetTypes", "");
                InsertEventLog("GetCustomerAssetTypes", EventType.Log, EventColor.yellow, "Enter in to get list Of customer assets types", "TICRM.BuisnessLayer.CustomerAssetManager.GetCustomerAssetTypes", "");

                var customerAssetTypeDtos = new List<CustomerAssetTypeDto>();

                foreach (var item in dbEnt.CustomerAssetTypes)
                {
                    customerAssetTypeDtos.Add(objMapper.GetCustomerAssetTypeDTO(item));
                }
                return customerAssetTypeDtos;
            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetCustomerAssetTypes", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.CustomerAssetManager.GetCustomerAssetTypes", "");
                throw;
            }
        }
        public CustomerAssetDto GetCustomerAsset(Guid? guid)
        {
            try
            {
                InsertEventLog("GetCustomerAsset", EventType.Log, EventColor.yellow, "Enter in to get customer assets on id="+guid+ " ", "TICRM.BuisnessLayer.CustomerAssetManager.GetCustomerAsset", "");
                return objMapper.GetCustomerAssetDTO(dbEnt.CustomerAssets.Find(guid));
            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetCustomerAsset", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.CustomerAssetManager.GetCustomerAsset", "");
                throw ex;
            }


        }
        /// <summary>
        /// Get account list 
        /// </summary>
        /// <returns></returns>
        public List<CustomerAssetDto> GetCustomerAssets()
        {
            try
            {
                InsertEventLog("GetCustomerAssets", EventType.Log, EventColor.yellow, "Enter in to get list of customer assets", "TICRM.BuisnessLayer.CustomerAssetManager.GetCustomerAssets", "");
                var customerAssetDtos = new List<CustomerAssetDto>();
                var customerAssets = dbEnt.CustomerAssets.Include(c => c.CustomerAssetType).Include(c => c.Status).Include(c => c.Team).Include(c => c.User).Where(a => a.IsDeleted != true).ToList();

                foreach (var item in customerAssets)
                {
                    customerAssetDtos.Add(objMapper.GetCustomerAssetDTO(item));
                }
                return customerAssetDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetCustomerAssets", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.CustomerAssetManager.GetCustomerAssets", "");

                throw;
            }


        }
        public List<CustomerAssetDto> GetCustomerAssets(Guid accountId)
        {
            try
            {
                InsertEventLog("GetCustomerAssets", EventType.Log, EventColor.yellow, "Enter in to get list of customer assets on accountid="+ accountId + " ", "TICRM.BuisnessLayer.CustomerAssetManager.GetCustomerAssets", "");
                var customerAssetDtos = new List<CustomerAssetDto>();
                var customerAssets = dbEnt.CustomerAssets.Include(c => c.CustomerAssetType).Include(c => c.Status).Include(c => c.Team).Include(c => c.User).Where(a => a.IsDeleted != true && a.AccountId == accountId).ToList();

                foreach (var item in customerAssets)
                {
                    customerAssetDtos.Add(objMapper.GetCustomerAssetDTO(item));
                }
                return customerAssetDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetCustomerAssets", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.CustomerAssetManager.GetCustomerAssets", "");
                throw;
            }


        }



        public List<CustomerAssetDto> GetLocationAssets(Guid locationId)
        {
            try
            {
                InsertEventLog("GetLocationAssets", EventType.Log, EventColor.yellow, "Enter in to get list of customer assets on locationId=" + locationId + " ", "TICRM.BuisnessLayer.CustomerAssetManager.GetLocationAssets", "");
                var customerAssetDtos = new List<CustomerAssetDto>();
                var customerAssets = dbEnt.CustomerAssets.Include(c => c.CustomerAssetType).Include(c => c.Status).Include(c => c.Team).Include(c => c.User).Where(a => a.IsDeleted != true && a.LocationId == locationId).ToList();

                foreach (var item in customerAssets)
                {
                    customerAssetDtos.Add(objMapper.GetCustomerAssetDTO(item));
                }
                return customerAssetDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetLocationAssets", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.CustomerAssetManager.GetLocationAssets", "");
                throw;
            }


        }



        /// <summary>
        /// save and edit account 
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        public bool SaveCustomerAsset(CustomerAssetDto acc, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SaveCustomerAsset",  EventType.Log, EventColor.yellow, "Successfully Enter", "TICRM.BuisnessLayer.CustomerAssetManager.SaveCustomerAsset", "");

                CustomerAsset customerAsset;
                if (isEditMode)
                {
                    InsertEventLog("SaveCustomerAsset", EventType.Log, EventColor.yellow, "Enter in edit Mode to edit on id="+ acc.CustomerAssetId, "TICRM.BuisnessLayer.CustomerAssetManager.SaveCustomerAsset", "");

                    customerAsset = objMapper.GetCustomerAsset(acc);
                    if (isDeleteMode)
                    {
                        InsertEventLog("SaveCustomerAsset", EventType.Log, EventColor.yellow, "Enter in delete Mode to delete on id=" + acc.CustomerAssetId + " ", "TICRM.BuisnessLayer.CustomerAssetManager.SaveCustomerAsset", "");

                        CustomerAsset customer = dbEnt.CustomerAssets.FirstOrDefault(x => x.CustomerAssetId == customerAsset.CustomerAssetId);
                        customer.IsDeleted = true;
                    }
                    else
                    {
                        dbEnt.Entry(customerAsset).State = EntityState.Modified;
                    }
                }
                else
                {

                    InsertEventLog("SaveCustomerAsset", EventType.Log, EventColor.yellow, "Enter in Create New Record of Customer Asset", "TICRM.BuisnessLayer.CustomerAssetManager.SaveCustomerAsset", "");
                    customerAsset = objMapper.GetCustomerAsset(acc);
                    dbEnt.CustomerAssets.Add(customerAsset);
                }
                // apply condition to check if db changes is done then  return true in response 
                if (dbEnt.SaveChanges()>0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                InsertEventMonitor("SaveCustomerAsset", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.CustomerAssetManager.SaveCustomerAsset", "");
                throw ex;
            }
            return false; //there is on changes in db against the object
        }


    }
}
