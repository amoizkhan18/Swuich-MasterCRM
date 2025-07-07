using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.BuisnessLayer
{
    public class AccountManager : BaseManager
    {

        public AccountManager()
        {
            AccountSizes = GetAccountSizes();
            AccountTypes = GetAccountTypes();
            Industries = GetIndustries();
        }

        #region Properties & Methods

        public List<AccountSizeDto> AccountSizes { get; set; }
        public List<AccountTypeDto> AccountTypes { get; set; }
        public List<IndustryDto> Industries { get; set; }

        /// <summary>
        /// Get Account Size Dt
        /// </summary>
        /// <returns></returns>
        public List<AccountSizeDto> GetAccountSizes()
        {
            try
            {
                InsertEventLog("GetAccountSize", EventType.Log, EventColor.yellow, "Successfully Enter in GetAccountSizes", "TICRM.BusinessLayer.AccountManager", "");

                List<AccountSizeDto> accountSizeDtos = new List<AccountSizeDto>();

                foreach (AccountSize item in dbEnt.AccountSizes)
                {
                    accountSizeDtos.Add(objMapper.GetAccountSizeDTO(item));
                }
                return accountSizeDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetAccountSize", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.AccountManager", "");
                throw;
            }
        }

        /// <summary>
        /// Get Account Types Dtos list
        /// </summary>
        /// <returns></returns>
        public List<AccountTypeDto> GetAccountTypes()
        {
            try
            {
                InsertEventLog("GetAccountTypes",  EventType.Log, EventColor.yellow, "Successfully Enter in GetAccountTypes", "TICRM.BusinessLayer.AccountManager", "");

                List<AccountTypeDto> accountTypeDtos = new List<AccountTypeDto>();

                foreach (AccountType item in dbEnt.AccountTypes)
                {
                    accountTypeDtos.Add(objMapper.GetAccountTypeDTO(item));
                }
                return accountTypeDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetAccountTypes", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.AccountManager", "");
                throw;
            }
        }

        /// <summary>
        /// Get Industries Dtos list
        /// </summary>
        /// <returns></returns>
        public List<IndustryDto> GetIndustries()
        {
            try
            {
                InsertEventLog("GetIndustries",  EventType.Log, EventColor.yellow, "Successfully Enter in GetIndustries", "TICRM.BusinessLayer.AccountManager", "");

                List<IndustryDto> industriesDtos = new List<IndustryDto>();

                foreach (Industry item in dbEnt.Industries)
                {
                    industriesDtos.Add(objMapper.GetIndustryDTO(item));
                }
                return industriesDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetIndustries", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.AccountManager", "");
                throw;
            }
        }


        #endregion

        public AccountDto GetAccount(Guid? guid)
        {
            try
            {
                InsertEventLog("GetAccount",  EventType.Log, EventColor.yellow, "Successfully Enter in GetAccount", "TICRM.BusinessLayer.AccountManager", "");
                return objMapper.GetAccountDTO(dbEnt.Accounts.Find(guid));
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetAccount", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.AccountManager", "");

                throw ex;
            }
        }






        /// <summary>
        /// Get account list 
        /// </summary>
        /// <returns></returns>
        public List<AccountDto> GetAccounts()
        {
            try
            {
                InsertEventLog("GetAccounts",  EventType.Log, EventColor.yellow, "Successfully Enter in GetAccounts", "TICRM.BusinessLayer.AccountManager", "");
                List<AccountDto> accountDtos = new List<AccountDto>();
                List<Account> accounts = dbEnt.Accounts.Include(a => a.AccountSize).Include(a => a.AccountType).Include(a => a.Address).Include(a => a.Address1).Include(a => a.Industry).Include(a => a.Status).Include(a => a.Team).Include(a => a.User).Where(a => a.IsDeleted != true).ToList();
                foreach (Account item in accounts)
                {
                    accountDtos.Add(objMapper.GetAccountDTO(item));
                }
                return accountDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetAccounts", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.AccountManager", "");
                throw;
            }


        }
        /// <summary>
        /// save and edit account 
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        public bool SaveAccount(AccountDto acc, string CurrentUserId, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SaveAccount",  EventType.Log, EventColor.yellow, "Successfully Enter in SaveAccount", "TICRM.BusinessLayer.AccountManager", CurrentUserId);
                Account account;
                if (isEditMode)
                {
                    account = objMapper.GetAccount(acc);
                    Account a = dbEnt.Accounts.FirstOrDefault(x => x.AccountId == account.AccountId);
                    if (a != null)
                    {
                        InsertEventLog("SaveAccount",  EventType.Log, EventColor.yellow, "For Edit. Successfully Enter in SaveAccount", "TICRM.BusinessLayer.AccountManager", CurrentUserId);
                        a.Name = account.Name;
                        a.ShippingAddress = account.ShippingAddress;
                        a.BillingAddress = account.BillingAddress;
                        a.AccountTypeId = account.AccountTypeId;
                        a.PhoneOffice = account.PhoneOffice;
                        a.Email = account.Email;
                        a.Fax = account.Fax;
                        a.WebSite = account.WebSite;
                        a.AccountSizeId = account.AccountSizeId;
                        a.IndustryId = account.IndustryId;
                        a.Description = account.Description;
                        a.StatusId = account.StatusId;
                        a.AssignedUser = account.AssignedUser;
                        a.AssignedTeam = account.AssignedTeam;
                        if (isDeleteMode)
                        {
                            InsertEventLog("SaveAccount",  EventType.Log, EventColor.yellow, "For Delete. Successfully Enter in SaveAccount", "TICRM.BusinessLayer.AccountManager", CurrentUserId);
                            Account accountForDelete = dbEnt.Accounts.FirstOrDefault(x=>x.AccountId == account.AccountId);
                            accountForDelete.IsDeleted = true;
                        }
                        HttpContext.Current.Session["AccountObject"] = account;
                        HttpContext.Current.Session["CurrentUserId"] = CurrentUserId;
                        if (dbEnt.SaveChanges() > 0)
                        {
                            return true;
                        }
                        //dbEnt.Entry(account).State = EntityState.Modified;
                    }
                }
                else
                {
                    InsertEventLog("SaveAccount",  EventType.Log, EventColor.yellow, "For Create. Successfully Enter in SaveAccount", "TICRM.BusinessLayer.AccountManager", CurrentUserId);

                    account = objMapper.GetAccount(acc);
                    account.AccountId = Guid.NewGuid();
                    dbEnt.Accounts.Add(account);
                }

                if (dbEnt.SaveChanges() > 0)
                {
                    HttpContext.Current.Session["AccountObject"] = account;
                    InsertEventLog("SaveAccount",  EventType.Log, EventColor.yellow, "data saved successfully in SaveAccount", "TICRM.BusinessLayer.AccountManager", CurrentUserId);
                    return true;
                }

            }
            catch (Exception ex)
            {
                InsertEventMonitor("SaveAccount", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.AccountManager", CurrentUserId);
                throw ex;
            }

            return false;
        }

        /// <summary>
        /// Get detailed object of account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public AccountViewModel GetAccountAndDetails(Guid accountId)
        {
            InsertEventLog("GetAccountAndDetails",  EventType.Log, EventColor.yellow, "get accountviewmodel on accountid. Successfully Enter in GetAccountAndDetails", "TICRM.BusinessLayer.AccountManager", "");

            AccountViewModel avm = new AccountViewModel();
            try
            {
                LocationManager lm = new LocationManager();
                OpportunityManager om = new OpportunityManager();
                DeviceManager dm = new DeviceManager();
                CustomerAssetManager cam = new CustomerAssetManager();
                ActivityManager acc = new ActivityManager();

                avm.account = GetAccount(accountId);
                avm.accountLocations = lm.GetLocations(accountId);
                avm.accountDevices = dm.GetDevices(accountId);
                avm.accountOppertunities = om.GetOpportunities(accountId);
                avm.accountAssetes = cam.GetCustomerAssets(accountId);
                avm.accountActivity = acc.GetAccountActivities(accountId);

            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetAccountAndDetails", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.AccountManager", "");
                throw;
            }
            InsertEventLog("GetAccountAndDetails",  EventType.Log, EventColor.yellow, "Successfully get information in GetAccountAndDetails", "TICRM.BusinessLayer.AccountManager", "");
            return avm;
        }








        public AccountViewModel Get_Account_Associates(Guid accountId)
        {
            InsertEventLog("Get_Account_Associates", EventType.Log, EventColor.yellow, "get Get_Account_Associates on accountid. Successfully Enter in Get_Account_Associates", "TICRM.BusinessLayer.AccountManager,Get_Account_Associates", "");

            AccountViewModel avm = new AccountViewModel();
            try
            {
                LocationManager lm = new LocationManager();
                OpportunityManager om = new OpportunityManager();
                DeviceManager dm = new DeviceManager();
                CustomerAssetManager cam = new CustomerAssetManager();
                ActivityManager acc = new ActivityManager();

                avm.account = GetAccount(accountId);
                avm.accountLocations = lm.GetLocations(accountId);
                avm.accountDevices = dm.GetDevices(accountId);
                avm.accountOppertunities = om.GetOpportunities(accountId);
                avm.accountAssetes = cam.GetCustomerAssets(accountId);
                avm.accountActivity = acc.GetAccountActivities(accountId);

            }
            catch (Exception ex)
            {
                InsertEventMonitor("Get_Account_Associates", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.AccountManager.Get_Account_Associates", "");
                throw;
            }
            InsertEventLog("Get_Account_Associates", EventType.Log, EventColor.yellow, "Successfully get information in Get_Account_Associates", "TICRM.BusinessLayer.AccountManager.Get_Account_Associates", "");
            return avm;
        }











    }
}
