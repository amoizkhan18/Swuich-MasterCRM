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
    public class GlobalSearchManager : BaseManager
    {


        public bool SavedGlobalSearch(string Name, string URL)
        {
            try
            {
                InsertEventLog("SavedGlobalSearch", EventType.Log, EventColor.yellow, "save global search ", "TICRM.BuisnessLayer.GlobalSearchManager.SavedGlobalSearch", "");
                GlobalSearch data = new GlobalSearch(); // Decalre an object for to save a data
                data.GlobalSearchId = Guid.NewGuid();   // inset a new guid in object
                data.Name = Name;
                data.URL = URL;
                data.Type = "URL"; // set type as URL its useful in searching
                dbEnt.GlobalSearches.Add(data); // add objct to save in db
                if (dbEnt.SaveChanges() > 0)
                {
                    InsertEventLog("SavedGlobalSearch", EventType.Log, EventColor.yellow, "successfully saved global search ", "TICRM.BuisnessLayer.GlobalSearchManager.SavedGlobalSearch", "");
                    return true;    // return true if data saved.
                }
            }
            catch (Exception ex)
            {
                InsertEventMonitor("SavedGlobalSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.GlobalSearchManager.SavedGlobalSearch", "");
                throw;
            }

            return false; // return false if data not saved
        }


        public bool UpdateGlobalSearch(Guid GlobalSearchId, string Name, string URL)
        {
            try
            {
                InsertEventLog("UpdateGlobalSearch", EventType.Log, EventColor.yellow, "going to get globalsearch on id=" + GlobalSearchId + "", "TICRM.BusinessLayer.GlobalSearchManager.UpdateGlobalSearch", "");
                GlobalSearch data = dbEnt.GlobalSearches.FirstOrDefault(x => x.GlobalSearchId == GlobalSearchId);       // Get global search object by globalsearchid
                data.Name = Name;
                data.URL = URL;
                data.Type = "URL";
                if (dbEnt.SaveChanges() > 0)
                {
                    InsertEventLog("UpdateGlobalSearch", EventType.Log, EventColor.yellow, "successfully updated global search", "TICRM.BusinessLayer.GlobalSearchManager.UpdateGlobalSearch", "");
                    return true; // return true if data is saved
                }
            }
            catch (Exception ex)
            {
                InsertEventMonitor("UpdateGlobalSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.UpdateGlobalSearch", "");
                throw;
            }
            return false; // return false if data is not saved.
        }


        public string GetGlobalSearchList()
        {
            try
            {
                InsertEventLog("GetGlobalSearchList", EventType.Log, EventColor.yellow, "going to get globalsearch list", "TICRM.BusinessLayer.GlobalSearchManager.GetGlobalSearchList", "");
                List<GlobalSearch> query = dbEnt.GlobalSearches.ToList();  // Get Globalsearch List
                string status = Newtonsoft.Json.JsonConvert.SerializeObject(query); // convert in json string
                return status;  // return json string
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetGlobalSearchList", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.GetGlobalSearchList", "");
                throw;
            }
        }


        public GlobalSearchDto GetGlobalSearchOnId(Guid GlobalSearchId)
        {
            try
            {
                InsertEventLog("GetGlobalSearchOnId", EventType.Log, EventColor.yellow, "get global searchDto On Id=" + GlobalSearchId + "", "TICRM.BusinessLayer.GlobalSearchManager.GetGlobalSearchOnId", "");
                // return object GlobalSearches on GlobalSearchId
                return objMapper.GetGlobalSearchDto(dbEnt.GlobalSearches.FirstOrDefault(x => x.GlobalSearchId == GlobalSearchId));
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetGlobalSearchOnId", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.GetGlobalSearchOnId", "");
                throw;
            }
        }

        public bool DeleteGlobalSearchOnId(Guid GlobalSearchId)
        {
            try
            {
                InsertEventLog("DeleteGlobalSearchOnId", EventType.Log, EventColor.yellow, "enter in delete global search On Id=" + GlobalSearchId + "", "TICRM.BusinessLayer.GlobalSearchManager.DeleteGlobalSearchOnId", "");
                GlobalSearch dsg = dbEnt.GlobalSearches.FirstOrDefault(x => x.GlobalSearchId == GlobalSearchId); // find object in database on GlobalSearchId
                if (dsg != null)
                {
                    dbEnt.GlobalSearches.Remove(dsg); // remove object in database
                    if (dbEnt.SaveChanges() > 0)
                    {
                        InsertEventLog("DeleteGlobalSearchOnId", EventType.Log, EventColor.yellow, "successfully deleted global search On Id=" + GlobalSearchId + "", "TICRM.BusinessLayer.GlobalSearchManager.DeleteGlobalSearchOnId", "");

                        return true; // if data removed return true
                    }
                }
                else
                {
                    InsertEventLog("DeleteGlobalSearchOnId", EventType.Log, EventColor.yellow, "for delete global search On Id=" + GlobalSearchId + " data is null", "TICRM.BusinessLayer.GlobalSearchManager.DeleteGlobalSearchOnId", "");
                }
                return false; // if data not saved
            }
            catch (Exception ex)
            {
                InsertEventMonitor("DeleteGlobalSearchOnId", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.DeleteGlobalSearchOnId", "");
                throw;
            }
        }

        public List<SearchDataViewModel> FirstInSearchData()
        {
            try
            {
                InsertEventLog("FirstInSearchData", EventType.Log, EventColor.yellow, "going to getting list first time load URL", "TICRM.BusinessLayer.GlobalSearchManager.FirstInSearchData", "");
                List<SearchDataViewModel> list = new List<SearchDataViewModel>(); // declare a new list object
                List<GlobalSearch> query = dbEnt.GlobalSearches.ToList();  // query in db to get List
                // apply iteration on query and add in list object
                foreach (GlobalSearch item in query)
                {
                    // body of foreach loop
                    SearchDataViewModel SearchData = new SearchDataViewModel();
                    SearchData.Result = "<a href='" + item.URL + "'>" + item.Name + "</a>";
                    SearchData.FirstURL = item.URL;
                    SearchData.Text = item.Name;
                    SearchData.Type = item.Type;
                    SearchData.value = "List";
                    list.Add(SearchData); // add an Object in list object
                }
                return list; // return list object
            }
            catch (Exception ex)
            {
                InsertEventMonitor("FirstInSearchData", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.FirstInSearchData", "");
                throw ex;
            }

        }

        public List<SearchDataViewModel> DeviceDataForSearch()
        {
            try
            {
                InsertEventLog("DeviceDataForSearch", EventType.Log, EventColor.yellow, "Get List Of Device For Search", "TICRM.BusinessLayer.GlobalSearchManager.DeviceDataForSearch", "");

                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                var query = dbEnt.Devices.ToList();
                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.Device.ToString()).Where(x => x.DataType == "String").ToList();

                foreach (var item in query)
                {
                    // body of foreach loop
                    SearchDataViewModel deviceEdit = new SearchDataViewModel();     // declare a new object to add data for search
                    SearchDataViewModel deviceDetails = new SearchDataViewModel();  // declare a new object to add data for search
                    SearchDataViewModel MacData = new SearchDataViewModel();        // declare a new object to add data for search
                    deviceEdit.Result = "<a href='/Devices/Edit/" + item.DeviceId + "'>Device</a>";
                    deviceEdit.FirstURL = "/Devices/Edit/" + item.DeviceId;
                    deviceEdit.Text = "Devices > " + item.Name+ " > Edit";
                    deviceEdit.Type = "URL";
                    deviceEdit.value = item.Mac;

                    list.Add(deviceEdit); // add an Object in list object

                    deviceDetails.Result = "<a href='/Devices/Details/" + item.DeviceId + "'>Device</a>";
                    deviceDetails.FirstURL = item.DeviceId.ToString();
                    deviceDetails.Text = "Devices > " + item.Name + " > Details";
                    deviceDetails.Type = "Modal";
                    deviceDetails.value = item.Mac;
                    deviceDetails.JS_function = "Devices_Details_Modal('"+ item.DeviceId +"')";
                    list.Add(deviceDetails); // add an Object in list object

                    MacData.Result = item.Mac;
                    MacData.FirstURL = "/Devices/device";
                    MacData.Text = "Devices > " + item.Name + " > View";
                    MacData.value = item.Mac;
                    MacData.Type = "MACAddress";
                    list.Add(MacData); // add an Object in list object

                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/Devices/Edit/" + item.DeviceId + "'>Edit Devices</a>";
                        s2.FirstURL = "/Devices/Edit/" + item.DeviceId;
                        s2.Text = "Devices > " + item.Name + " > " + selected.ColumnName;
                        string value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.value = value;
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }

                }
                SearchDataViewModel DeviceCreate = new SearchDataViewModel();     // declare a new object to add data in list
                DeviceCreate.Result = "<a href='/Devices/Create'>Create Device</a>";
                DeviceCreate.FirstURL = "/Devices/Create";
                DeviceCreate.Text = "Devices > Create";
                DeviceCreate.Type = "URL";
                DeviceCreate.value = "Navigate to Create Device Page.";
                list.Add(DeviceCreate); // add an Object in list object


                return list; // return list object
            }
            catch (Exception ex)
            {

                InsertEventMonitor("DeviceDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.DeviceDataForSearch", "");
                throw ex;
            }

        }

        public List<SearchDataViewModel> DeviceDataOfMAC(string MAC)
        {
            try
            {
                InsertEventLog("DeviceDataOfMAC", EventType.Log, EventColor.yellow, "Get List Of MAC For Search", "TICRM.BusinessLayer.GlobalSearchManager.DeviceDataOfMAC", "");

                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                Device query = dbEnt.Devices.Include(x => x.Status).Include(d => d.Team).Include(d => d.User).FirstOrDefault(x => x.Mac == MAC);
                if (query != null)
                {
                    SearchDataViewModel deviceName = new SearchDataViewModel();                     // declare a new object to add data in a list
                    SearchDataViewModel deviceMAC = new SearchDataViewModel();                      // declare a new object to add data in a list
                    SearchDataViewModel deviceEMEI = new SearchDataViewModel();                     // declare a new object to add data in a list
                    SearchDataViewModel deviceRegistrationDate = new SearchDataViewModel();         // declare a new object to add data in a list
                    SearchDataViewModel deviceLatitude = new SearchDataViewModel();                 // declare a new object to add data in a list
                    SearchDataViewModel deviceLongitude = new SearchDataViewModel();                // declare a new object to add data in a list
                    SearchDataViewModel deviceStatus = new SearchDataViewModel();                   // declare a new object to add data in a list
                    SearchDataViewModel deviceAccountId = new SearchDataViewModel();                // declare a new object to add data in a list
                    SearchDataViewModel deviceAssignedUser = new SearchDataViewModel();             // declare a new object to add data in a list
                    SearchDataViewModel deviceAssignedTeam = new SearchDataViewModel();             // declare a new object to add data in a list




                    deviceName.FirstURL = "/Devices/device";
                    deviceName.Result = query.Mac;
                    deviceName.Text = "Device Name";
                    deviceName.value = query.Name;
                    deviceName.Type = "MacInfo";
                    list.Add(deviceName); // add an Object in list object

                    deviceMAC.FirstURL = "/Devices/device";
                    deviceMAC.Result = query.Mac;
                    deviceMAC.Text = "MAC";
                    deviceMAC.value = query.Mac;
                    deviceMAC.Type = "MacInfo";
                    list.Add(deviceMAC); // add an Object in list object


                    deviceEMEI.FirstURL = "/Devices/device";
                    deviceEMEI.Result = query.Mac;
                    deviceEMEI.Text = "EMEI";
                    deviceEMEI.value = query.EMEINumber;
                    deviceEMEI.Type = "MacInfo";
                    list.Add(deviceEMEI); // add an Object in list object


                    deviceRegistrationDate.FirstURL = "/Devices/device";
                    deviceRegistrationDate.Result = query.Mac;
                    deviceRegistrationDate.Text = "Registration Date";
                    deviceRegistrationDate.value = query.RegistrationDate == null ? "" : query.RegistrationDate.Value.ToShortDateString();
                    deviceRegistrationDate.Type = "MacInfo";
                    list.Add(deviceRegistrationDate); // add an Object in list object


                    deviceLatitude.FirstURL = "/Devices/device";
                    deviceLatitude.Result = query.Mac;
                    deviceLatitude.Text = "Latitude";
                    deviceLatitude.value = query.Latitude == null ? "" : query.Latitude.ToString();
                    deviceLatitude.Type = "MacInfo";
                    list.Add(deviceLatitude); // add an Object in list object



                    deviceLongitude.FirstURL = "/Devices/device";
                    deviceLongitude.Result = query.Mac;
                    deviceLongitude.Text = "Longitude";
                    deviceLongitude.value = query.Longitude == null ? "" : query.Longitude.ToString();
                    deviceLongitude.Type = "MacInfo";
                    list.Add(deviceLongitude); // add an Object in list object



                    deviceStatus.FirstURL = "/Devices/device";
                    deviceStatus.Result = query.Mac;
                    deviceStatus.Text = "Status";
                    deviceStatus.value = query.Status == null ? "" : query.Status.Name.ToString();
                    deviceStatus.Type = "MacInfo";
                    list.Add(deviceStatus); // add an Object in list object


                    deviceAccountId.FirstURL = "/Devices/device";
                    deviceAccountId.Result = query.Mac;
                    deviceAccountId.Text = "Account";
                    deviceAccountId.value = dbEnt.Accounts.FirstOrDefault(x => x.AccountId == query.AccountId).Name;
                    deviceAccountId.Type = "MacInfo";
                    list.Add(deviceAccountId); // add an Object in list object


                    deviceAssignedUser.FirstURL = "/Devices/device";
                    deviceAssignedUser.Result = query.Mac;
                    deviceAssignedUser.Text = "Assigned User";
                    deviceAssignedUser.value = query.User.Name;
                    deviceAssignedUser.Type = "MacInfo";
                    list.Add(deviceAssignedUser); // add an Object in list object

                    deviceAssignedTeam.FirstURL = "/Devices/device";
                    deviceAssignedTeam.Result = query.Mac;
                    deviceAssignedTeam.Text = "Assigned Team";
                    deviceAssignedTeam.value = query.Team.Name;
                    deviceAssignedTeam.Type = "MacInfo";
                    list.Add(deviceAssignedTeam); // add an Object in list object

                }

                return list; // return list object
            }
            catch (Exception ex)
            {
                InsertEventMonitor("DeviceDataOfMAC", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.DeviceDataOfMAC", "");
                throw ex;
            }

        }

        public List<SearchDataViewModel> LeadsDataForSearch()
        {
            try
            {
                InsertEventLog("LeadsDataForSearch", EventType.Log, EventColor.yellow, "Get List Of Lead For Search", "TICRM.BusinessLayer.GlobalSearchManager.LeadsDataForSearch", "");
                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object
                var query = dbEnt.Leads.ToList();
                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.Lead.ToString()).Where(x => x.DataType == "String").ToList();

                foreach (var item in query)
                {
                    SearchDataViewModel leadEdit = new SearchDataViewModel();   // declare a new object to add data in a list
                    SearchDataViewModel leadDetail = new SearchDataViewModel(); // declare a new object to add data in a list
                    leadEdit.Result = "<a href='/Leads/Edit/" + item.LeadId + "'>Edit Lead</a>";
                    leadEdit.FirstURL = "/Leads/Edit/" + item.LeadId;
                    leadEdit.Text = "Leads > " + item.Name+ " > Edit";
                    leadEdit.Type = "URL";
                    leadEdit.value = "Navigate to Edit Page.";
                    list.Add(leadEdit); // add an Object in list object

                    leadDetail.Result = "<a href='/Leads/Details/" + item.LeadId + "'>Detail Lead</a>";
                    leadDetail.FirstURL = item.LeadId.ToString();
                    leadDetail.Text = "Leads > " + item.Name + " > Details";
                    leadDetail.Type = "Modal";
                    leadDetail.JS_function = "Leads_Details_Modal('"+ item.LeadId + "')";
                    list.Add(leadDetail); // add an Object in list object


                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/Leads/Edit/" + item.LeadId + "'>Edit Leads</a>";
                        s2.FirstURL = "/Leads/Edit/" + item.LeadId;
                        s2.Text = "Leads > " + item.Name + " > " + selected.ColumnName;
                        string value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.value = value;
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }


                }
                SearchDataViewModel LeadCreate = new SearchDataViewModel();// declare a new object to add data in a list
                LeadCreate.Result = "<a href='/Leads/Create'>Create Lead</a>";
                LeadCreate.FirstURL = "/Leads/Create";
                LeadCreate.Text = "Leads > Create";
                LeadCreate.Type = "URL";
                LeadCreate.value = "Navigate to Lead Create Page.";
                list.Add(LeadCreate); // add an Object in list object

                return list; // return list object
            }
            catch (Exception ex)
            {

                InsertEventMonitor("LeadsDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.LeadsDataForSearch", "");
                throw ex;
            }
        }

        public List<SearchDataViewModel> OpportunitiesDataForSearch()
        {
            try
            {

                InsertEventLog("OpportunitiesDataForSearch", EventType.Log, EventColor.yellow, "Get List Of Opportunity For Search", "TICRM.BusinessLayer.GlobalSearchManager.OpportunitiesDataForSearch", "");
                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                var query = dbEnt.Opportunities.ToList();
                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.Oppertunity.ToString()).Where(x => x.DataType == "String").ToList();

                foreach (var item in query)
                {
                    SearchDataViewModel OpportunitiesEdit = new SearchDataViewModel();  // declare a new object to add data in a list
                    SearchDataViewModel OpportunitiesDetail = new SearchDataViewModel();// declare a new object to add data in a list
                    OpportunitiesEdit.Result = "<a href='/Opportunities/Edit/" + item.OpportunityId + "'>Edit Opportunities</a>";
                    OpportunitiesEdit.FirstURL = "/Opportunities/Edit/" + item.OpportunityId;
                    OpportunitiesEdit.Text = "Opportunities > " + item.Title + " > Edit";
                    OpportunitiesEdit.Type = "URL";
                    OpportunitiesEdit.value = "Navigate to Oppertunity Edit Page.";
                    list.Add(OpportunitiesEdit); // add an Object in list object

                    OpportunitiesDetail.Result = "<a href='/Opportunities/Details/" + item.OpportunityId + "'>Detail Opportunities</a>";
                    OpportunitiesDetail.FirstURL = item.OpportunityId.ToString();
                    OpportunitiesDetail.Text = "Opportunities > " + item.Title+ " > Details";
                    OpportunitiesDetail.Type = "Modal";
                    OpportunitiesDetail.JS_function = "Opportunities_Details_Modal('" + item.OpportunityId + "')";
                    list.Add(OpportunitiesDetail); // add an Object in list object

                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/Opportunities/Edit/" + item.OpportunityId + "'>Edit Opportunities</a>";
                        s2.FirstURL = "/Opportunities/Edit/" + item.OpportunityId;
                        s2.Text = "Opportunities > " + item.Title + " > " + selected.ColumnName;
                        string value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.value = value;
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }

                }
                SearchDataViewModel OpportunitiesCreate = new SearchDataViewModel();// declare a new object to add data in a list
                OpportunitiesCreate.Result = "<a href='/Opportunities/Create'>Create Opportunity</a>";
                OpportunitiesCreate.FirstURL = "/Opportunities/Create";
                OpportunitiesCreate.Text = "Opportunities > Create";
                OpportunitiesCreate.Type = "URL";
                OpportunitiesCreate.value = "Navigate to Oppertunity Create Page.";
                list.Add(OpportunitiesCreate); // add an Object in list object

                return list; // return list object
            }
            catch (Exception ex)
            {
                InsertEventMonitor("OpportunitiesDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.OpportunitiesDataForSearch", "");

                throw ex;
            }
        }

        public List<SearchDataViewModel> AccountsDataForSearch()
        {
            try
            {
                InsertEventLog("AccountsDataForSearch", EventType.Log, EventColor.yellow, "Get List Of account For Search", "TICRM.BusinessLayer.GlobalSearchManager.AccountsDataForSearch", "");
                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                List<Account> query = dbEnt.Accounts.ToList();

                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.Account.ToString()).Where(x => x.DataType == "String").ToList();


                foreach (Account item in query)
                {
                    SearchDataViewModel AccountsEdit = new SearchDataViewModel();
                    AccountsEdit.Result = "<a href='/Accounts/Edit/" + item.AccountId + "'>Edit Customer</a>";
                    AccountsEdit.FirstURL = "/Accounts/Edit/" + item.AccountId;
                    AccountsEdit.Text = "Accounts > " + item.Name + " > Edit";
                    AccountsEdit.Type = "URL";
                    AccountsEdit.value = "Navigate to Edit Account Page.";
                    list.Add(AccountsEdit); // add an Object in list object

                    SearchDataViewModel AccountsDetail_Page = new SearchDataViewModel();
                    AccountsDetail_Page.Result = "<a href='/Accounts/AccountsDetail/" + item.AccountId + "'>Edit Customer</a>";
                    AccountsDetail_Page.FirstURL = "/Accounts/AccountsDetail/" + item.AccountId;
                    AccountsDetail_Page.Text = "Accounts > " + item.Name + " > Details";
                    AccountsDetail_Page.Type = "URL";
                    list.Add(AccountsDetail_Page); // add an Object in list object

                    //SearchDataViewModel AccountsDetail = new SearchDataViewModel();
                    //AccountsDetail.Result = "<a href='/Accounts/Details/" + item.AccountId + "'>Detail Customer</a>";
                    //AccountsDetail.FirstURL = item.AccountId.ToString();
                    //AccountsDetail.Text = "Accounts > " + item.Name + " > Details";
                    //AccountsDetail.Type = "Modal";
                    //AccountsDetail.JS_function = "Accounts_Details_Modal('"+ item.AccountId +"')";
                    //list.Add(AccountsDetail); // add an Object in list object
                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/Accounts/Edit/" + item.AccountId + "'>Edit Accounts</a>";
                        s2.FirstURL = "/Accounts/Edit/" + item.AccountId;
                        s2.Text = "Accounts > " + item.Name + " > " + selected.ColumnName;
                        string value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.value = value;
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }

                }
                SearchDataViewModel AccountsCreate = new SearchDataViewModel();
                AccountsCreate.Result = "<a href='/Accounts/Create'>Create Accoount</a>";
                AccountsCreate.FirstURL = "/Accounts/Create";
                AccountsCreate.Text = "Accounts > Create";
                AccountsCreate.Type = "URL";
                AccountsCreate.value = "Navigate to Account Create Page.";
                list.Add(AccountsCreate); // add an Object in list object

                return list; // return list object
            }
            catch (Exception ex)
            {

                InsertEventMonitor("AccountsDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.AccountsDataForSearch", "");
                throw ex;
            }
        }

        public List<SearchDataViewModel> GetAccountsDetailForSearch(Guid accountId)
        {
            try
            {

                InsertEventLog("AccountsDataForSearch", EventType.Log, EventColor.yellow, "Get List Of account For Search", "TICRM.BusinessLayer.GlobalSearchManager.AccountsDataForSearch", "");
                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                AccountManager account = new AccountManager();
                var account_associate = account.Get_Account_Associates(accountId);

                List<workflowDataTypeDTO> Device_attr = GetAttributesOfEntity(EntityTypes.Device.ToString()).Where(x => x.DataType == "String").ToList();
                List<workflowDataTypeDTO> CustomerAssets_attr = GetAttributesOfEntity(EntityTypes.CustomerAsset.ToString()).Where(x => x.DataType == "String").ToList();
                List<workflowDataTypeDTO> Location_attr = GetAttributesOfEntity(EntityTypes.Location.ToString()).Where(x => x.DataType == "String").ToList();
                List<workflowDataTypeDTO> Opportunity_attr = GetAttributesOfEntity(EntityTypes.Oppertunity.ToString()).Where(x => x.DataType == "String").ToList();

                foreach (var item in account_associate.accountDevices)
                {
                    foreach (workflowDataTypeDTO selected in Device_attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/Devices/Edit/" + item.DeviceId + "'>Edit Devices</a>";
                        s2.FirstURL = "/Devices/Edit/" + item.DeviceId;
                        s2.Text = "Accounts > " + account_associate.account.Name + " > Device > " + item.Name + " > " + selected.ColumnName;
                        string value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.value = value;
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }
                }

                foreach (var item in account_associate.accountLocations)
                {
                    foreach (workflowDataTypeDTO selected in Location_attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/Locations/Edit/" + item.LocationId + "'>Edit Locations</a>";
                        s2.FirstURL = "/Locations/Edit/" + item.LocationId;
                        s2.Text = "Accounts > " + account_associate.account.Name + " > Location > " + item.Name + " > " + selected.ColumnName;
                        s2.value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }

                }

                foreach (var item in account_associate.accountAssetes)
                {
                    foreach (workflowDataTypeDTO selected in CustomerAssets_attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/CustomerAssets/Edit/" + item.AccountId + "'>Edit CustomerAssets</a>";
                        s2.FirstURL = "/CustomerAssets/Edit/" + item.AccountId;
                        s2.Text = "Accounts > " + account_associate.account.Name + " > CustomerAssets > " + item.Title + " > " + selected.ColumnName;
                        string value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.value = value;
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }

                }

                foreach (var item in account_associate.accountOppertunities)
                {
                    foreach (workflowDataTypeDTO selected in Opportunity_attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/Opportunities/Edit/" + item.OpportunityId + "'>Edit Opportunities</a>";
                        s2.FirstURL = "/Opportunities/Edit/" + item.OpportunityId;
                        s2.Text = "Accounts > " + account_associate.account.Name + " > Opportunities > " + item.Title + " > " + selected.ColumnName;
                        string value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.value = value;
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }

                }



                return list; // return list object
            }
            catch (Exception ex)
            {

                InsertEventMonitor("AccountsDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.AccountsDataForSearch", "");
                throw ex;
            }
        }

        public List<SearchDataViewModel> CustomerAssetsDataForSearch()
        {
            try
            {
                InsertEventLog("CustomerAssetsDataForSearch", EventType.Log, EventColor.yellow, "Get List Of customer Assets For Search", "TICRM.BusinessLayer.GlobalSearchManager.CustomerAssetsDataForSearch", "");

                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                List<CustomerAsset> query = dbEnt.CustomerAssets.ToList();
                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.CustomerAsset.ToString()).Where(x => x.DataType == "String").ToList();


                foreach (CustomerAsset item in query)
                {
                    SearchDataViewModel CustomerAssetsEdit = new SearchDataViewModel();     // declare a new object to add data in a list
                    SearchDataViewModel CustomerAssetsDetail = new SearchDataViewModel();   // declare a new object to add data in a list
                    CustomerAssetsEdit.Result = "<a href='/CustomerAssets/Edit/" + item.CustomerAssetId + "'>Edit Customer Assets</a>";
                    CustomerAssetsEdit.FirstURL = "/CustomerAssets/Edit/" + item.CustomerAssetId;
                    CustomerAssetsEdit.Text = "CustomerAssets > " + item.Title+ " > Edit";
                    CustomerAssetsEdit.Type = "URL";
                    CustomerAssetsEdit.value = "Navigate to Edit Page.";
                    list.Add(CustomerAssetsEdit); // add an Object in list object

                    CustomerAssetsDetail.Result = "<a href='/CustomerAssets/Details/" + item.CustomerAssetId + "'>Detail Customer Assets</a>";
                    CustomerAssetsDetail.FirstURL = item.CustomerAssetId.ToString();
                    CustomerAssetsDetail.Text = "CustomerAssets > " + item.Title+ " > Details";
                    CustomerAssetsDetail.Type = "Modal";
                    CustomerAssetsDetail.JS_function = "CustomerAssets_Details_Modal('" + item.CustomerAssetId + "')";
                    list.Add(CustomerAssetsDetail); // add an Object in list object

                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/CustomerAssets/Edit/" + item.AccountId + "'>Edit CustomerAssets</a>";
                        s2.FirstURL = "/CustomerAssets/Edit/" + item.AccountId;
                        s2.Text = "CustomerAssets > " + item.Title + " > " + selected.ColumnName;
                        string value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.value = value;
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }

                }
                SearchDataViewModel CustomerAssetsCreate = new SearchDataViewModel();// declare a new object to add data in a list
                CustomerAssetsCreate.Result = "<a href='/CustomerAssets/Create'>Create Customer Assets</a>";
                CustomerAssetsCreate.FirstURL = "/CustomerAssets/Create";
                CustomerAssetsCreate.Text = "CustomerAssets > Create";
                CustomerAssetsCreate.Type = "URL";
                CustomerAssetsCreate.value = "Navigate to Create Page.";
                list.Add(CustomerAssetsCreate); // add an Object in list object

                return list; // return list object
            }
            catch (Exception ex)
            {

                InsertEventMonitor("CustomerAssetsDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.CustomerAssetsDataForSearch", "");
                throw ex;
            }
        }

        public List<SearchDataViewModel> ReadingsDataForSearch()
        {
            try
            {
                InsertEventLog("ReadingsDataForSearch", EventType.Log, EventColor.yellow, "Get List Of Reading For Search", "TICRM.BusinessLayer.GlobalSearchManager.ReadingsDataForSearch", "");

                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                List<Reading> query = dbEnt.Readings.ToList();

                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.Reading.ToString()).Where(x => x.DataType == "String").ToList();

                foreach (var item in query)
                {
                    SearchDataViewModel ReadingsEdit = new SearchDataViewModel();       // declare a new object to add data in a list
                    SearchDataViewModel ReadingsDetail = new SearchDataViewModel();     // declare a new object to add data in a list
                    ReadingsEdit.Result = "<a href='/Readings/Edit/" + item.ReadingId + "'>Edit Reading Name</a>";
                    ReadingsEdit.FirstURL = "/Readings/Edit/" + item.ReadingId;
                    ReadingsEdit.Text = "Readings > " + item.Value+ " > Edit";
                    ReadingsEdit.Type = "URL";
                    ReadingsEdit.value = "Navigate to Edit Page.";
                    list.Add(ReadingsEdit); // add an Object in list object

                    ReadingsDetail.Result = "<a href='/Readings/Details/" + item.ReadingId + "'>Detail Reading Name</a>";
                    ReadingsDetail.FirstURL = item.ReadingId.ToString();
                    ReadingsDetail.Text = "Readings > " + item.Value+ " > Details";
                    ReadingsDetail.Type = "Modal";
                    ReadingsDetail.JS_function = "Readings_Details_Modal('" + item.ReadingId + "')";
                    list.Add(ReadingsDetail); // add an Object in list object
                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/Readings/Edit/" + item.ReadingId + "'>Edit Readings</a>";
                        s2.FirstURL = "/Readings/Edit/" + item.ReadingId;
                        s2.Text = "Readings > " + item.Value + " > " + selected.ColumnName;
                        string value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.value = value;
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }
                }
                SearchDataViewModel ReadingsCreate = new SearchDataViewModel();// declare a new object to add data in a list
                ReadingsCreate.Result = "<a href='/Readings/Create'>Create Reading Name</a>";
                ReadingsCreate.FirstURL = "/Readings/Create";
                ReadingsCreate.Text = "Readings > Create";
                ReadingsCreate.Type = "URL";
                ReadingsCreate.value = "Navigate to Create Page.";
                list.Add(ReadingsCreate); // add an Object in list object

                return list; // return list object
            }
            catch (Exception ex)
            {

                InsertEventMonitor("ReadingsDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.ReadingsDataForSearch", "");
                throw ex;
            }
        }

        public List<SearchDataViewModel> ServiceCallsDataForSearch()
        {
            try
            {
                InsertEventLog("ServiceCallsDataForSearch", EventType.Log, EventColor.yellow, "Get List Of Service call For Search", "TICRM.BusinessLayer.GlobalSearchManager.ServiceCallsDataForSearch", "");

                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                var query = dbEnt.ServiceCalls.ToList();

                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.ServiceCall.ToString()).Where(x => x.DataType == "String").ToList();

                foreach (var item in query)
                {
                    SearchDataViewModel ServiceCallsEdit = new SearchDataViewModel();   // declare a new object to add data in a list
                    SearchDataViewModel ServiceCallsDetail = new SearchDataViewModel(); // declare a new object to add data in a list
                    ServiceCallsEdit.Result = "<a href='/ServiceCalls/Edit/" + item.ServiceCallId + "'>Edit Reading Name</a>";
                    ServiceCallsEdit.FirstURL = "/ServiceCalls/Edit/" + item.ServiceCallId;
                    ServiceCallsEdit.Text = "ServiceCalls > " + item.Title+" > Edit";
                    ServiceCallsEdit.Type = "URL";
                    ServiceCallsEdit.value = "Navigate to Edit Page.";
                    list.Add(ServiceCallsEdit); // add an Object in list object

                    ServiceCallsDetail.Result = "<a href='/ServiceCalls/Details/" + item.ServiceCallId + "'>Detail Reading Name</a>";
                    ServiceCallsDetail.FirstURL = item.ServiceCallId.ToString();
                    ServiceCallsDetail.Text = "ServiceCalls > " + item.Title+ " > Details";
                    ServiceCallsDetail.Type = "Modal";
                    ServiceCallsDetail.JS_function = "ServiceCalls_Details_Modal('" + item.ServiceCallId + "')";
                    list.Add(ServiceCallsDetail); // add an Object in list object



                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/ServiceCalls/Edit/" + item.ServiceCallId + "'>Edit ServiceCalls</a>";
                        s2.FirstURL = "/ServiceCalls/Edit/" + item.ServiceCallId;
                        s2.Text = "ServiceCalls > " + item.Title + " > " + selected.ColumnName;
                        s2.value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }



                }
                SearchDataViewModel ServiceCallsCreate = new SearchDataViewModel();// declare a new object to add data in a list
                ServiceCallsCreate.Result = "<a href='/ServiceCalls/Create'>Create Reading Name</a>";
                ServiceCallsCreate.FirstURL = "/ServiceCalls/Create";
                ServiceCallsCreate.Text = "ServiceCalls > Create";
                ServiceCallsCreate.Type = "URL";
                ServiceCallsCreate.value = "Navigate to Create Page.";
                list.Add(ServiceCallsCreate); // add an Object in list object

                return list; // return list object
            }
            catch (Exception ex)
            {

                InsertEventMonitor("ServiceCallsDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.ServiceCallsDataForSearch", "");
                throw ex;
            }
        }

        public List<SearchDataViewModel> ResourcesDataForSearch()
        {
            try
            {
                InsertEventLog("ResourcesDataForSearch", EventType.Log, EventColor.yellow, "Get List Of Resources For Search", "TICRM.BusinessLayer.GlobalSearchManager.ResourcesDataForSearch", "");

                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                var query = dbEnt.Resources.ToList();

                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.Resource.ToString()).Where(x => x.DataType == "String").ToList();

                foreach (var item in query)
                {
                    SearchDataViewModel ResourcesEdit = new SearchDataViewModel();  // declare a new object to add data in a list
                    SearchDataViewModel ResourcesDetail = new SearchDataViewModel();// declare a new object to add data in a list
                    ResourcesEdit.Result = "<a href='/Resources/Edit/" + item.ResourceId + "'>Edit Resources Name</a>";
                    ResourcesEdit.FirstURL = "/Resources/Edit/" + item.ResourceId;
                    ResourcesEdit.Text = "Resources > " + item.Name+ " > Edit";
                    ResourcesEdit.Type = "URL";
                    ResourcesEdit.value = "Navigate to Edit Page.";
                    list.Add(ResourcesEdit); // add an Object in list object

                    ResourcesDetail.Result = "<a href='/Resources/Details/" + item.ResourceId + "'>Detail Resources Name</a>";
                    ResourcesDetail.FirstURL = item.ResourceId.ToString();
                    ResourcesDetail.Text = "Resources > " + item.Name+ " > Details";
                    ResourcesDetail.Type = "Modal";
                    ResourcesDetail.JS_function = "Resources_Details_Modal('" + item.ResourceId + "')";
                    list.Add(ResourcesDetail);

                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/Resources/Edit/" + item.ResourceId + "'>Edit Resources</a>";
                        s2.FirstURL = "/Resources/Edit/" + item.ResourceId;
                        s2.Text = "Resources > " + item.Name + " > " + selected.ColumnName;
                        s2.value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }

                }
                SearchDataViewModel ResourcesCreate = new SearchDataViewModel();// declare a new object to add data in a list
                ResourcesCreate.Result = "<a href='/Resources/Create'>Create Resources Name</a>";
                ResourcesCreate.FirstURL = "/Resources/Create";
                ResourcesCreate.Text = "Resources > Create";
                ResourcesCreate.Type = "URL";
                ResourcesCreate.value = "Navigate to Create Page.";
                list.Add(ResourcesCreate); // add an Object in list object

                return list; // return list object
            }
            catch (Exception ex)
            {

                InsertEventMonitor("ResourcesDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.ResourcesDataForSearch", "");
                throw ex;
            }
        }

        public List<SearchDataViewModel> WorkOrdersDataForSearch()
        {
            try
            {
                InsertEventLog("WorkOrdersDataForSearch", EventType.Log, EventColor.yellow, "Get List Of work Order For Search", "TICRM.BusinessLayer.GlobalSearchManager.WorkOrdersDataForSearch", "");

                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                var query = dbEnt.WorkOrders.ToList();
                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.WorkOrder.ToString()).Where(x => x.DataType == "String").ToList();

                foreach (var item in query)
                {
                    SearchDataViewModel WorkOrdersEdit = new SearchDataViewModel();     // declare a new object to add data in a list
                    SearchDataViewModel WorkOrdersDetail = new SearchDataViewModel();   // declare a new object to add data in a list
                    WorkOrdersEdit.Result = "<a href='/WorkOrders/Edit/" + item.WorkOrderId + "'>Edit WorkOrders</a>";
                    WorkOrdersEdit.FirstURL = "/WorkOrders/Edit/" + item.WorkOrderId;
                    WorkOrdersEdit.Text = "WorkOrders > " + item.Title+ " > Edit";
                    WorkOrdersEdit.Type = "URL";
                    WorkOrdersEdit.value = "Navigate to Edit Page.";
                    list.Add(WorkOrdersEdit); // add an Object in list object

                    WorkOrdersDetail.Result = "<a href='/WorkOrders/Details/" + item.WorkOrderId + "'>Detail WorkOrders</a>";
                    WorkOrdersDetail.FirstURL = item.WorkOrderId.ToString();
                    WorkOrdersDetail.Text = "WorkOrders > " + item.Title+ " > Details";
                    WorkOrdersDetail.Type = "Modal";
                    WorkOrdersDetail.JS_function = "WorkOrders_Details_Modal('" + item.WorkOrderId + "')";
                    list.Add(WorkOrdersDetail); // add an Object in list object
                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/WorkOrders/Edit/" + item.WorkOrderId + "'>Edit WorkOrders</a>";
                        s2.FirstURL = "/WorkOrders/Edit/" + item.WorkOrderId;
                        s2.Text = "WorkOrders > " + item.Title + " > " + selected.ColumnName;
                        s2.value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }
                }
                SearchDataViewModel WorkOrdersCreate = new SearchDataViewModel();// declare a new object to add data in a list
                WorkOrdersCreate.Result = "<a href='/WorkOrders/Create'>Create WorkOrders</a>";
                WorkOrdersCreate.FirstURL = "/WorkOrders/Create";
                WorkOrdersCreate.Text = "WorkOrders > Create";
                WorkOrdersCreate.Type = "URL";
                WorkOrdersCreate.value = "Navigate to Create Page.";
                list.Add(WorkOrdersCreate); // add an Object in list object

                return list; // return list object
            }
            catch (Exception ex)
            {

                InsertEventMonitor("WorkOrdersDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.WorkOrdersDataForSearch", "");
                throw ex;
            }
        }

        public List<SearchDataViewModel> ReadingTypesDataForSearch()
        {
            try
            {
                InsertEventLog("ReadingTypesDataForSearch", EventType.Log, EventColor.yellow, "Get List Of Reading Type For Search", "TICRM.BusinessLayer.GlobalSearchManager.ReadingTypesDataForSearch", "");
                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                var query = dbEnt.ReadingTypes.ToList();
                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.ReadingType.ToString()).Where(x => x.DataType == "String").ToList();

                foreach (var item in query)
                {
                    SearchDataViewModel ReadingTypesEdit = new SearchDataViewModel();       // declare a new object to add data in a list
                    SearchDataViewModel ReadingTypesDetail = new SearchDataViewModel();     // declare a new object to add data in a list
                    ReadingTypesEdit.Result = "<a href='/ReadingTypes/Edit/" + item.ReadingTypeId + "'>Edit ReadingTypes</a>";
                    ReadingTypesEdit.FirstURL = "/ReadingTypes/Edit/" + item.ReadingTypeId;
                    ReadingTypesEdit.Text = "ReadingTypes > " + item.Name+ " > Edit";
                    ReadingTypesEdit.Type = "URL";
                    ReadingTypesEdit.value = "Navigate to Edit Page.";
                    list.Add(ReadingTypesEdit); // add an Object in list object

                    ReadingTypesDetail.Result = "<a href='/ReadingTypes/Details/" + item.ReadingTypeId + "'>Detail ReadingTypes</a>";
                    ReadingTypesDetail.FirstURL = item.ReadingTypeId.ToString();
                    ReadingTypesDetail.Text = "ReadingTypes > " + item.Name+ " > Details";
                    ReadingTypesDetail.Type = "Modal";
                    ReadingTypesDetail.JS_function = "ReadingTypes_Details_Modal('" + item.ReadingTypeId + "')";
                    list.Add(ReadingTypesDetail); // add an Object in list object
                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/ReadingTypes/Edit/" + item.ReadingTypeId + "'>Edit ReadingTypes</a>";
                        s2.FirstURL = "/ReadingTypes/Edit/" + item.ReadingTypeId;
                        s2.Text = "ReadingTypes > " + item.Name + " > " + selected.ColumnName;
                        s2.value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }
                }
                SearchDataViewModel ReadingTypesCreate = new SearchDataViewModel();// declare a new object to add data in a list
                ReadingTypesCreate.Result = "<a href='/ReadingTypes/Create'>Create ReadingTypes</a>";
                ReadingTypesCreate.FirstURL = "/ReadingTypes/Create";
                ReadingTypesCreate.Text = "ReadingTypes > Create";
                ReadingTypesCreate.Type = "URL";
                ReadingTypesCreate.value = "Navigate to Create Page.";
                list.Add(ReadingTypesCreate); // add an Object in list object

                return list; // return list object
            }
            catch (Exception ex)
            {

                InsertEventMonitor("ReadingTypesDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.ReadingTypesDataForSearch", "");
                throw ex;
            }
        }

        public List<SearchDataViewModel> ReadingUnitsDataForSearch()
        {
            try
            {
                InsertEventLog("ReadingUnitsDataForSearch", EventType.Log, EventColor.yellow, "Get List Of Reading Units For Search", "TICRM.BusinessLayer.GlobalSearchManager.ReadingUnitsDataForSearch", "");
                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                var query = dbEnt.ReadingUnits.ToList();
                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.ReadingUnit.ToString()).Where(x => x.DataType == "String").ToList();

                foreach (var item in query)
                {
                    SearchDataViewModel ReadingUnitsEdit = new SearchDataViewModel();       // declare a new object to add data in a list
                    SearchDataViewModel ReadingUnitsDetail = new SearchDataViewModel();     // declare a new object to add data in a list
                    ReadingUnitsEdit.Result = "<a href='/ReadingUnits/Edit/" + item.ReadingUnitId + "'>Edit ReadingUnits</a>";
                    ReadingUnitsEdit.FirstURL = "/ReadingUnits/Edit/" + item.ReadingUnitId;
                    ReadingUnitsEdit.Text = "ReadingUnits > " + item.Name+" > Edit";
                    ReadingUnitsEdit.Type = "URL";
                    ReadingUnitsEdit.value = "Navigate to Edit Page.";
                    list.Add(ReadingUnitsEdit); // add an Object in list object

                    ReadingUnitsDetail.Result = "<a href='/ReadingUnits/Details/" + item.ReadingUnitId + "'>Detail ReadingUnits</a>";
                    ReadingUnitsDetail.FirstURL = item.ReadingUnitId.ToString();
                    ReadingUnitsDetail.Text = "ReadingUnits > " + item.Name+ " > Details";
                    ReadingUnitsDetail.Type = "Modal";
                    ReadingUnitsDetail.JS_function = "ReadingUnits_Details_Modal('" + item.ReadingUnitId + "')";
                    list.Add(ReadingUnitsDetail); // add an Object in list object
                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/ReadingUnits/Edit/" + item.ReadingUnitId + "'>Edit ReadingUnits</a>";
                        s2.FirstURL = "/ReadingUnits/Edit/" + item.ReadingUnitId;
                        s2.Text = "ReadingUnits > " + item.Name + " > " + selected.ColumnName;
                        s2.value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }
                }
                SearchDataViewModel ReadingUnitsCreate = new SearchDataViewModel();// declare a new object to add data in a list
                ReadingUnitsCreate.Result = "<a href='/ReadingUnits/Create'>Create ReadingUnits</a>";
                ReadingUnitsCreate.FirstURL = "/ReadingUnits/Create";
                ReadingUnitsCreate.Text = "ReadingUnits > Create";
                ReadingUnitsCreate.Type = "URL";
                ReadingUnitsCreate.value = "Navigate to Create Page.";
                list.Add(ReadingUnitsCreate); // add an Object in list object

                return list; // return list object
            }
            catch (Exception ex)
            {

                InsertEventMonitor("ReadingUnitsDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.ReadingUnitsDataForSearch", "");
                throw ex;
            }
        }

        public List<SearchDataViewModel> AddressesDataForSearch()
        {
            try
            {
                InsertEventLog("AddressesDataForSearch", EventType.Log, EventColor.yellow, "Get List Of Address Data For Search", "TICRM.BusinessLayer.GlobalSearchManager.AddressesDataForSearch", "");
                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                var query = dbEnt.Addresses.ToList();
                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.Address.ToString()).Where(x => x.DataType == "String").ToList();

                foreach (var item in query)
                {
                    SearchDataViewModel AddressesEdit = new SearchDataViewModel();  // declare a new object to add data in a list
                    SearchDataViewModel AddressesDetail = new SearchDataViewModel();// declare a new object to add data in a list
                    AddressesEdit.Result = "<a href='/Addresses/Edit/" + item.AddressId + "'>Edit Addresses</a>";
                    AddressesEdit.FirstURL = "/Addresses/Edit/" + item.AddressId;
                    AddressesEdit.Text = "Addresses > " + item.Street2+ " > Edit";
                    AddressesEdit.Type = "URL";
                    AddressesEdit.value = "Navigate to Edit Page.";
                    list.Add(AddressesEdit); // add an Object in list object

                    AddressesEdit.Result = "<a href='/Addresses/Edit/" + item.AddressId + "'>Edit Addresses</a>";
                    AddressesEdit.FirstURL = "/Addresses/Edit/" + item.AddressId;
                    AddressesEdit.Text = "Addresses > " + item.Street1+ " > Edit";
                    AddressesEdit.Type = "URL";
                    AddressesEdit.value = "Navigate to Edit Page.";
                    list.Add(AddressesEdit); // add an Object in list object




                    AddressesDetail.Result = "<a href='/Addresses/Details/" + item.AddressId + "'>Detail Addresses</a>";
                    AddressesDetail.FirstURL = item.AddressId.ToString();
                    AddressesDetail.Text = "Addresses > " + item.Street2+ " > Details";
                    AddressesDetail.Type = "Modal";
                    AddressesDetail.JS_function = "Addresses_Details_Modal('" + item.AddressId + "')";
                    list.Add(AddressesDetail); // add an Object in list object


                    AddressesDetail.Result = "<a href='/Addresses/Details/" + item.AddressId + "'>Detail Addresses</a>";
                    AddressesDetail.FirstURL = item.AddressId.ToString();
                    AddressesDetail.Text = "Addresses > " + item.Street1+ " > Details";
                    AddressesDetail.Type = "Modal";
                    AddressesDetail.JS_function = "Addresses_Details_Modal('" + item.AddressId + "')";
                    list.Add(AddressesDetail); // add an Object in list object


                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/Addresses/Edit/" + item.AddressId + "'>Edit Addresses</a>";
                        s2.FirstURL = "/Addresses/Edit/" + item.AddressId;
                        s2.Text = "Addresses > " + item.Street1 + " > " + selected.ColumnName;
                        s2.value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object


                        SearchDataViewModel s3 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s3.Result = "<a href='/Addresses/Edit/" + item.AddressId + "'>Edit Addresses</a>";
                        s3.FirstURL = "/Addresses/Edit/" + item.AddressId;
                        s3.Text = "Addresses > " + item.Street2 + " > " + selected.ColumnName;
                        s3.value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s3.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }

                }
                SearchDataViewModel AddressesCreate = new SearchDataViewModel();    // declare a new object to add data in a list
                AddressesCreate.Result = "<a href='/Addresses/Create'>Create Addresses</a>";
                AddressesCreate.FirstURL = "/Addresses/Create";
                AddressesCreate.Text = "Addresses > Create";
                AddressesCreate.Type = "URL";
                AddressesCreate.value = "Navigate to Create Page.";
                list.Add(AddressesCreate); // add an Object in list object

                return list; // return list object
            }
            catch (Exception ex)
            {
                InsertEventMonitor("AddressesDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.AddressesDataForSearch", "");

                throw ex;
            }
        }

        public List<SearchDataViewModel> LocationsDataForSearch()
        {
            try
            {
                InsertEventLog("LocationsDataForSearch", EventType.Log, EventColor.yellow, "Get List Of Location Data For Search", "TICRM.BusinessLayer.GlobalSearchManager.LocationsDataForSearch", "");
                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                var query = dbEnt.Locations.ToList();
                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.Location.ToString()).Where(x => x.DataType == "String").ToList();

                foreach (var item in query)
                {
                    SearchDataViewModel LocationsEdit = new SearchDataViewModel();  // declare a new object to add data in a list
                    SearchDataViewModel LocationsDetail = new SearchDataViewModel();// declare a new object to add data in a list
                    LocationsEdit.Result = "<a href='/Locations/Edit/" + item.LocationId + "'>Edit Locations</a>";
                    LocationsEdit.FirstURL = "/Locations/Edit/" + item.LocationId;
                    LocationsEdit.Text = "Locations > " + item.Name+ " > Edit";
                    LocationsEdit.Type = "URL";
                    list.Add(LocationsEdit); // add an Object in list object

                    LocationsDetail.Result = "<a href='/Locations/Details/" + item.LocationId + "'>Detail Locations</a>";
                    LocationsDetail.FirstURL = item.LocationId.ToString();
                    LocationsDetail.Text = "Locations > " + item.Name+ " > Details";
                    LocationsDetail.Type = "Modal";
                    LocationsDetail.JS_function = "Locations_Details_Modal('" + item.LocationId + "')";
                    list.Add(LocationsDetail); // add an Object in list object
                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/Locations/Edit/" + item.LocationId + "'>Edit Locations</a>";
                        s2.FirstURL = "/Locations/Edit/" + item.LocationId;
                        s2.Text = "Locations > " + item.Name + " > " + selected.ColumnName;
                        s2.value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }
                }
                SearchDataViewModel LocationsCreate = new SearchDataViewModel();// declare a new object to add data in a list
                LocationsCreate.Result = "<a href='/Locations/Create'>Create Locations</a>";
                LocationsCreate.FirstURL = "/Locations/Create";
                LocationsCreate.Text = "Locations > Create";
                LocationsCreate.Type = "URL";
                list.Add(LocationsCreate); // add an Object in list object

                return list; // return list object
            }
            catch (Exception ex)
            {

                InsertEventMonitor("LocationsDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.LocationsDataForSearch", "");
                throw ex;
            }
        }

        public List<SearchDataViewModel> AlertsDataForSearch()
        {
            try
            {

                InsertEventLog("AlertsDataForSearch", EventType.Log, EventColor.yellow, "Get List Of Alerts Data For Search", "TICRM.BusinessLayer.GlobalSearchManager.AlertsDataForSearch", "");
                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                var query = dbEnt.Alerts.ToList();
                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.Alert.ToString()).Where(x => x.DataType == "String").ToList();

                foreach (var item in query)
                {
                    SearchDataViewModel AlertsEdit = new SearchDataViewModel();     // declare a new object to add data in a list
                    SearchDataViewModel AlertsDetail = new SearchDataViewModel();   // declare a new object to add data in a list
                    AlertsEdit.Result = "<a href='/Alerts/Edit/" + item.AlertId + "'>Edit Alerts</a>";
                    AlertsEdit.FirstURL = "/Alerts/Edit/" + item.AlertId;
                    AlertsEdit.Text = "Alerts > " + item.Title+" > Edit";
                    AlertsEdit.Type = "URL";
                    AlertsEdit.value = "Navigate to Edit Page.";
                    list.Add(AlertsEdit); // add an Object in list object

                    AlertsDetail.Result = "<a href='/Alerts/Details/" + item.AlertId + "'>Detail Alerts</a>";
                    AlertsDetail.FirstURL = item.AlertId.ToString();
                    AlertsDetail.Text = "Alerts > " + item.Title + " > Details";
                    AlertsDetail.Type = "Modal";
                    AlertsDetail.JS_function = "Alerts_Details_Modal('" + item.AlertId + "')";
                    list.Add(AlertsDetail); // add an Object in list object
                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/Alerts/Edit/" + item.AlertId + "'>Edit Alerts</a>";
                        s2.FirstURL = "/Alerts/Edit/" + item.Title;
                        s2.Text = "Alerts > " + item.Title + " > " + selected.ColumnName;
                        s2.value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }
                }
                SearchDataViewModel AlertsCreate = new SearchDataViewModel();// declare a new object to add data in a list
                AlertsCreate.Result = "<a href='/Alerts/Create'>Create Alerts</a>";
                AlertsCreate.FirstURL = "/Alerts/Create";
                AlertsCreate.Text = "Alerts > Create";
                AlertsCreate.Type = "URL";
                AlertsCreate.value = "Navigate to Create Page.";
                list.Add(AlertsCreate);

                return list; // return list object
            }
            catch (Exception ex)
            {
                InsertEventMonitor("AlertsDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.AlertsDataForSearch", "");
                throw ex;
            }
        }

        public List<SearchDataViewModel> WorkFlowsDataForSearch()
        {
            try
            {

                InsertEventLog("WorkFlowsDataForSearch", EventType.Log, EventColor.yellow, "Get List Of WorkFlows Data For Search", "TICRM.BusinessLayer.GlobalSearchManager.WorkFlowsDataForSearch", "");
                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                var query = dbEnt.WorkFlows.ToList();
                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.WorkFlow.ToString()).Where(x => x.DataType == "String").ToList();

                foreach (var item in query)
                {
                    SearchDataViewModel WorkFlowsEdit = new SearchDataViewModel();     // declare a new object to add data in a list
                    SearchDataViewModel WorkFlowsDetail = new SearchDataViewModel();   // declare a new object to add data in a list
                    WorkFlowsEdit.Result = "<a href='/WorkFlows/Edit/" + item.WorkFlowId + "'>Edit WorkFlows</a>";
                    WorkFlowsEdit.FirstURL = "/WorkFlows/Edit/" + item.WorkFlowId;
                    WorkFlowsEdit.Text = "WorkFlows > " + item.Name + " > Edit";
                    WorkFlowsEdit.Type = "URL";
                    WorkFlowsEdit.value = "Navigate to Edit Page.";
                    list.Add(WorkFlowsEdit); // add an Object in list object

                    WorkFlowsDetail.Result = "<a href='/WorkFlows/Details/" + item.WorkFlowId + "'>Detail WorkFlows</a>";
                    WorkFlowsDetail.FirstURL = item.WorkFlowId.ToString();
                    WorkFlowsDetail.Text = "WorkFlows > " + item.Name + " > Details";
                    WorkFlowsDetail.Type = "Modal";
                    WorkFlowsDetail.JS_function = "WorkFlows_Details_Modal('" + item.WorkFlowId + "')";
                    list.Add(WorkFlowsDetail); // add an Object in list object
                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/WorkFlows/Edit/" + item.WorkFlowId + "'>Edit WorkFlows</a>";
                        s2.FirstURL = "/WorkFlows/Edit/" + item.WorkFlowId;
                        s2.Text = "WorkFlows > " + item.Name + " > " + selected.ColumnName;
                        s2.value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }
                }
                SearchDataViewModel AlertsCreate = new SearchDataViewModel();// declare a new object to add data in a list
                AlertsCreate.Result = "<a href='/WorkFlows/Create'>Create WorkFlows</a>";
                AlertsCreate.FirstURL = "/WorkFlows/Create";
                AlertsCreate.Text = "WorkFlows > Create";
                AlertsCreate.Type = "URL";
                AlertsCreate.value = "Navigate to Create Page.";
                list.Add(AlertsCreate);

                return list; // return list object
            }
            catch (Exception ex)
            {
                InsertEventMonitor("WorkFlowsDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.WorkFlowsDataForSearch", "");
                throw ex;
            }
        }

        public List<SearchDataViewModel> WorkFlowMappingDataForSearch()
        {
            try
            {

                InsertEventLog("WorkFlowMappingDataForSearch", EventType.Log, EventColor.yellow, "Get List Of WorkFlows Data For Search", "TICRM.BusinessLayer.GlobalSearchManager.WorkFlowMappingDataForSearch", "");
                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                var query = dbEnt.WorkFlowMappings.ToList();
                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.WorkFlowMapping.ToString()).Where(x => x.DataType == "String").ToList();

                foreach (var item in query)
                {
                    SearchDataViewModel WorkFlowEdit = new SearchDataViewModel();     // declare a new object to add data in a list
                    SearchDataViewModel WorkFlowDetail = new SearchDataViewModel();   // declare a new object to add data in a list
                    WorkFlowEdit.Result = "<a href='/WorkFlowMappings/Edit/" + item.WorkFlowMappingId + "'>Edit WorkFlowMappings</a>";
                    WorkFlowEdit.FirstURL = "/WorkFlowMappings/Edit/" + item.WorkFlowMappingId;
                    WorkFlowEdit.Text = "WorkFlowMappings > " + item.SourceType + " > Edit";
                    WorkFlowEdit.Type = "URL";
                    WorkFlowEdit.value = "Navigate to Eidt Page.";
                    list.Add(WorkFlowEdit); // add an Object in list object

                    WorkFlowDetail.Result = "<a href='/WorkFlowMappings/Details/" + item.WorkFlowMappingId + "'>Detail WorkFlowMappings</a>";
                    WorkFlowDetail.FirstURL = item.WorkFlowMappingId.ToString();
                    WorkFlowDetail.Text = "WorkFlowMappings > " + item.SourceType + " > Details";
                    WorkFlowDetail.Type = "Modal";
                    WorkFlowDetail.JS_function = "WorkFlowMappings_Details_Modal('" + item.WorkFlowMappingId + "')";
                    list.Add(WorkFlowDetail); // add an Object in list object
                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/WorkFlowMappings/Edit/" + item.WorkFlowMappingId + "'>Edit WorkFlowMappings</a>";
                        s2.FirstURL = "/WorkFlowMappings/Edit/" + item.WorkFlowMappingId;
                        s2.Text = "WorkFlowMappings > " + item.SourceType + " > " + selected.ColumnName;
                        s2.value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }
                }
                SearchDataViewModel AlertsCreate = new SearchDataViewModel();// declare a new object to add data in a list
                AlertsCreate.Result = "<a href='/WorkFlowMappings/Create'>Create WorkFlowMappings</a>";
                AlertsCreate.FirstURL = "/WorkFlowMappings/Create";
                AlertsCreate.Text = "WorkFlowMappings > Create";
                AlertsCreate.Type = "URL";
                AlertsCreate.value = "Navigate to Create Page.";
                list.Add(AlertsCreate);

                return list; // return list object
            }
            catch (Exception ex)
            {
                InsertEventMonitor("WorkFlowMappingDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.WorkFlowMappingDataForSearch", "");
                throw ex;
            }
        }

        public List<SearchDataViewModel> WorkFlowReportsDataForSearch()
        {
            try
            {
                InsertEventLog("WorkFlowReportsDataForSearch", EventType.Log, EventColor.yellow, "Get List Of WorkFlows Data For Search", "TICRM.BusinessLayer.GlobalSearchManager.WorkFlowReportsDataForSearch", "");
                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                //var query = dbEnt.WorkFlowReports.ToList();
                //List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.WorkFlowReport.ToString()).Where(x => x.DataType == "String").ToList();
                //foreach (var item in query)
                //{
                //    SearchDataViewModel WorkFlowReportsEdit = new SearchDataViewModel();     // declare a new object to add data in a list
                //    //SearchDataViewModel WorkFlowReportsDetail = new SearchDataViewModel();   // declare a new object to add data in a list
                //    //WorkFlowReportsEdit.Result = "<a href='/WorkFlowReports/Details/" + item.WorkFlowReportId + "'>Detail WorkFlowReports</a>";
                //    //WorkFlowReportsEdit.FirstURL = item.WorkFlowReportId.ToString();
                //    //WorkFlowReportsEdit.Text = "WorkFlowReports > " + item.CreatedDate + " > Details";
                //    //WorkFlowReportsEdit.Type = "Modal";
                //    //WorkFlowReportsEdit.JS_function = "WorkFlowReports_Details_Modal('" + item.WorkFlowReportId + "')";
                //    //list.Add(WorkFlowReportsEdit); // add an Object in list object
                //    //foreach (workflowDataTypeDTO selected in attr)
                //    //{
                //    //    SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                //    //    s2.Result = "<a href='/WorkFlowReports/Details/" + item.WorkFlowReportId + "'>Details WorkFlowReports</a>";
                //    //    s2.FirstURL = "/WorkFlowReports/Details/" + item.WorkFlowReportId;
                //    //    s2.Text = "WorkFlowReports > " + item.CreatedDate + " > " + selected.ColumnName;
                //    //    s2.value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                //    //    s2.Type = "Modal";
                //    //    s2.JS_function = "WorkFlowReports_Details_Modal('" + item.WorkFlowReportId + "')";
                //    //    list.Add(s2); // add an Object in list object
                //    //}
                //}

                return list; // return list object
            }
            catch (Exception ex)
            {
                InsertEventMonitor("WorkFlowReportsDataForSearch", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.WorkFlowReportsDataForSearch", "");
                throw ex;
            }
        }

        public List<SearchDataViewModel> LeadsExtraSearch()
        {
            try
            {
                List<SearchDataViewModel> list = new List<SearchDataViewModel>();// declare a new list object

                List<Lead> query = dbEnt.Leads.ToList();

                List<workflowDataTypeDTO> attr = GetAttributesOfEntity(EntityTypes.Lead.ToString()).Where(x => x.DataType == "String").ToList();

                foreach (Lead item in query)
                {
                    SearchDataViewModel leadEdit = new SearchDataViewModel();   // declare a new object to add data in a list
                    leadEdit.Result = "<a href='/Leads/Edit/" + item.LeadId + "'>Edit Lead</a>";
                    leadEdit.FirstURL = "/Leads/Edit/" + item.LeadId;
                    leadEdit.Text = "Lead/" + item.Name + "/Edit";
                    leadEdit.Type = "URL";
                    leadEdit.Type = "Navigate to Edit Page.";
                    list.Add(leadEdit); // add an Object in list object

                    SearchDataViewModel leadDetail = new SearchDataViewModel(); // declare a new object to add data in a list
                    leadDetail.Result = "<a href='/Leads/Details/" + item.LeadId + "'>Detail Lead</a>";
                    leadDetail.FirstURL = item.LeadId.ToString();
                    leadDetail.Text = "Lead/" + item.Name + "/Details";
                    leadDetail.Type = "Modal";
                    leadDetail.JS_function = "Leads_Details_Modal('" + item.LeadId + "')";
                    list.Add(leadDetail); // add an Object in list object

                    foreach (workflowDataTypeDTO selected in attr)
                    {
                        SearchDataViewModel s2 = new SearchDataViewModel(); // declare a new object to add data in a list
                        s2.Result = "<a href='/Leads/Edit/" + item.LeadId + "'>Edit Lead</a>";
                        s2.FirstURL = "/Leads/Edit/" + item.LeadId;
                        s2.Text = "Lead/" + item.Name + "/" + selected.ColumnName;
                        string value = (String)item.GetType().GetProperty(selected.ColumnName).GetValue(item);
                        s2.value = value;
                        s2.Type = "URL";
                        list.Add(s2); // add an Object in list object
                    }
                }
                SearchDataViewModel LeadCreate = new SearchDataViewModel();// declare a new object to add data in a list
                LeadCreate.Result = "<a href='/Leads/Create'>Create Lead</a>";
                LeadCreate.FirstURL = "/Leads/Create";
                LeadCreate.Text = "Create Lead";
                LeadCreate.Type = "URL";
                LeadCreate.value = "Navigate to Create Page.";
                list.Add(LeadCreate); // add an Object in list object

                return list; // return list object
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<workflowDataTypeDTO> GetAttributesOfEntity(string type)
        {
            try
            {
                InsertEventLog("GetAttributesOfEntity", EventType.Log, EventColor.yellow, "going to get attribute of " + type, "TICRM.BusinessLayer.GlobalSearchManager.GetAttributesOfEntity", "");
                List<workflowDataTypeDTO> vs = new List<workflowDataTypeDTO>();


                PropertyInfo[] Query = type == EntityTypes.Account.ToString() ? typeof(Account).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : type == EntityTypes.Lead.ToString() ? typeof(Lead).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : type == EntityTypes.Device.ToString() ? typeof(Device).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : type == EntityTypes.WorkOrder.ToString() ? typeof(WorkOrder).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : type == EntityTypes.Oppertunity.ToString() ? typeof(Opportunity).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : type == EntityTypes.CustomerAsset.ToString() ? typeof(CustomerAsset).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : type == EntityTypes.Reading.ToString() ? typeof(Reading).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : type == EntityTypes.ServiceCall.ToString() ? typeof(ServiceCall).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : type == EntityTypes.Resource.ToString() ? typeof(Resource).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : type == EntityTypes.ReadingType.ToString() ? typeof(ReadingType).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : type == EntityTypes.ReadingUnit.ToString() ? typeof(ReadingUnit).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : type == EntityTypes.Address.ToString() ? typeof(Address).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : type == EntityTypes.Location.ToString() ? typeof(Location).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : type == EntityTypes.Alert.ToString() ? typeof(Alert).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : type == EntityTypes.WorkFlow.ToString() ? typeof(WorkFlow).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : type == EntityTypes.WorkFlowMapping.ToString() ? typeof(WorkFlowMapping).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : type == EntityTypes.WorkFlowReport.ToString() ? typeof(WorkFlowReport).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        : null;

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

                return vs;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetAttributesOfEntity", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GlobalSearchManager.GetAttributesOfEntity", "");
                throw;
            }
        }

    }




}
