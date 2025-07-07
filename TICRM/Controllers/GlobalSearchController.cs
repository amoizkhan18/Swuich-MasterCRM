using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TICRM.BuisnessLayer;
using TICRM.DTOs;
using TICRM.ViewModels;

namespace TICRM.Controllers
{
    public class GlobalSearchController : Controller
    {
        // GET: GlobalSearch
        private GlobalSearchManager gsm = new GlobalSearchManager();

        public ActionResult Index()
        {
            return View();
        }

        public string SubmitGlobalSearch(string Name, string URL)
        {
            bool condition = gsm.SavedGlobalSearch(Name, URL); // pass name and URL to global Search Manager and get response true and false

            if (condition == true) //condotion Check to return success and error
            {
                return "success";
            }
            return "error";
        }
        public string UpdateGlobalSearch(Guid GlobalSearchId, string Name, string URL)
        {
            bool condition = gsm.UpdateGlobalSearch(GlobalSearchId, Name, URL); // pass id, name and URL to global Search Manager and get response true and false
            if (condition == true) //condotion Check to return success and error
            {
                return "success";
            }
            return "error";
        }

        public string GetGlobalSearchList()
        {
            return gsm.GetGlobalSearchList(); // get list from Global Search Manager and return to view
        }

        public JsonResult EditGlobalSearch(Guid GlobalSearchId)
        {
            GlobalSearchDto data = new GlobalSearchDto(); // create an object of Global Search DTO
            data = gsm.GetGlobalSearchOnId(GlobalSearchId); // get global search on id and place in data object
            return Json(data, JsonRequestBehavior.AllowGet); // return data in json format
        }


        public string DeleteGlobalSearch(Guid GlobalSearchId)
        {
            bool status = gsm.DeleteGlobalSearchOnId(GlobalSearchId); // pass id to delete data global search

            if (status == true) // check condotion to return status to view
            {
                return "success";
            }
            return "error";
        }

        public JsonResult GetEACSearchList()
        {
            // declare a string array with name urlSegment and pass Current URL Segments in string array
            string[] urlSegments = Request.UrlReferrer.Segments;

            List<SearchDataViewModel> global = new List<SearchDataViewModel>();     // create a SeachDataViewModel List Object
            string SegmentOne = urlSegments.Length > 1 ? urlSegments[1].Replace(@"/", "") : ""; // Get Segment One In Array if is null then it pass empty.tostring     

            GlobalSearchViewModel gsvm = new GlobalSearchViewModel();   // create an object to save in session for further used
            gsvm.URL = Request.UrlReferrer.AbsolutePath;        // save a absolute path of URL in gsvm object
            gsvm.SearchDataList = new List<SearchDataViewModel>();      // create a new List object of  SearchDataViewModel

            GlobalSearchViewModel query = Session["GlobalSearchViewSession"] as GlobalSearchViewModel;      // query in session get data

            if (query == null) { gsvm.FirstInSearch = gsm.FirstInSearchData(); } else { gsvm.FirstInSearch = query.FirstInSearch; }




            // switch statement is used for get dynamically for Global Search
            switch (SegmentOne)
            {
                case "Devices":
                    int pos = Array.IndexOf(urlSegments, "device", 2);      // check position 2 is exist with name device
                    if (pos > -1)
                    {
                        string segmentTwo = urlSegments[2].Replace("/", "");    // remove '/' from '/device' and 
                        string MACoptional = Session["MacAddress"] as string;   // Get MACAddress from session
                        //global = gsm.DeviceDataOfMAC(MACoptional);      
                        gsvm.SearchDataList = gsm.DeviceDataOfMAC(MACoptional);// Pass MacAddress to get data against  MacAddress
                    }
                    else
                    {
                        gsvm.SearchDataList = gsm.DeviceDataForSearch();    // Get Device Data for Edit,Details and View
                    }
                    break;
                case "devices":
                    int pos1 = Array.IndexOf(urlSegments, "device", 2);// check position 2 is exist with name device
                    if (pos1 > -1)
                    {
                        string segmentTwo = urlSegments[2].Replace("/", "");    // remove '/' from '/device' and 

                        string MACoptional = Session["MacAddress"] as string;   // Get MACAddress from session

                        gsvm.SearchDataList = gsm.DeviceDataOfMAC(MACoptional); // Pass MacAddress to get data against  MacAddress

                    }
                    else
                    {
                        gsvm.SearchDataList = gsm.DeviceDataForSearch();    // Get Device Data for Edit,Details and View
                    }
                    break;

                case "Leads":
                    gsvm.SearchDataList = gsm.LeadsDataForSearch(); // Get Lead Data for Edit,Details and View

                    // gsvm.SearchDataList = gsm.LeadsExtraSearch(); // Get Lead Data for Edit,Details and View
                    break;
                case "Opportunities":
                    gsvm.SearchDataList = gsm.OpportunitiesDataForSearch(); // Get Opportunities Data for Edit,Details and View
                    break;
                case "Accounts":
                    int Account_pos = Array.IndexOf(urlSegments, "AccountsDetail/", 2);
                    if (Account_pos > -1)
                    {

                        gsvm.SearchDataList.AddRange(gsm.GetAccountsDetailForSearch(new Guid(urlSegments[3])));

                    }

                    gsvm.SearchDataList.AddRange(gsm.AccountsDataForSearch());

                    //gsvm.SearchDataList = gsm.AccountsDataForSearch();// Get Accounts Data for Edit,Details and View
                    break;
                case "CustomerAssets":
                    gsvm.SearchDataList = gsm.CustomerAssetsDataForSearch();// Get CustomerAssets Data for Edit,Details and View
                    break;
                case "Readings":
                    gsvm.SearchDataList = gsm.ReadingsDataForSearch();// Get Readings Data for Edit,Details and View
                    break;
                case "ServiceCalls":
                    gsvm.SearchDataList = gsm.ServiceCallsDataForSearch();// Get ServiceCalls Data for Edit,Details and View
                    break;
                case "Resources":
                    gsvm.SearchDataList = gsm.ResourcesDataForSearch();// Get Resources Data for Edit,Details and View
                    break;
                case "WorkOrders":
                    gsvm.SearchDataList = gsm.WorkOrdersDataForSearch();// Get WorkOrders Data for Edit,Details and View
                    break;
                case "ReadingTypes":
                    gsvm.SearchDataList = gsm.ReadingTypesDataForSearch();// Get ReadingTypes Data for Edit,Details and View
                    break;
                case "ReadingUnits":
                    gsvm.SearchDataList = gsm.ReadingUnitsDataForSearch();// Get ReadingUnits Data for Edit,Details and View
                    break;

                case "Addresses":
                    gsvm.SearchDataList = gsm.AddressesDataForSearch();// Get Addresses Data for Edit,Details and View
                    break;
                case "Locations":
                    gsvm.SearchDataList = gsm.LocationsDataForSearch();// Get Locations Data for Edit,Details and View
                    break;
                case "Alerts":
                    gsvm.SearchDataList = gsm.AlertsDataForSearch();// Get Alerts Data for Edit,Details and View
                    break;
                case "WorkFlows":
                    gsvm.SearchDataList = gsm.WorkFlowsDataForSearch();// Get WorkFlows Data for Edit,Details and View
                    break;
                case "WorkFlowMappings":
                    gsvm.SearchDataList = gsm.WorkFlowMappingDataForSearch();// Get WorkFlowMapping Data for Edit,Details and View
                    break;
                case "WorkFlowReports":
                    gsvm.SearchDataList = gsm.WorkFlowReportsDataForSearch();// Get workflowreport Data for Edit,Details and View
                    break;

            }

            Session["GlobalSearchViewSession"] = gsvm;      // Pass gsvm Data in Session to fast its functionality


            return Json(gsvm);  // return a json list of gsvm in response
        }

        public JsonResult GetEAC_On_LoadEvent(string value)
        {
            // declare a string array with name urlSegment and pass Current URL Segments in string array
            string[] urlSegments = value.Split(' ', ',', '/', '>');

            List<SearchDataViewModel> global = new List<SearchDataViewModel>();     // create a SeachDataViewModel List Object
            string SegmentOne = urlSegments.Length > 1 ? urlSegments[1].Replace(@"/", "") : ""; // Get Segment One In Array if is null then it pass empty.tostring     

            GlobalSearchViewModel gsvm = new GlobalSearchViewModel();   // create an object to save in session for further used
            gsvm.URL = Request.UrlReferrer.AbsolutePath;        // save a absolute path of URL in gsvm object
            gsvm.SearchDataList = new List<SearchDataViewModel>();      // create a new List object of  SearchDataViewModel

            GlobalSearchViewModel query = Session["GlobalSearchViewSession"] as GlobalSearchViewModel;      // query in session get data

            // if (query == null) { gsvm.FirstInSearch = gsm.FirstInSearchData(); } else { gsvm.FirstInSearch = query.FirstInSearch; }




            // switch statement is used for get dynamically for Global Search
            switch (value)
            {
                case "Devices":
                    int pos = Array.IndexOf(urlSegments, "device", 2);      // check position 2 is exist with name device
                    if (pos > -1)
                    {
                        string segmentTwo = urlSegments[2].Replace("/", "");    // remove '/' from '/device' and 

                        string MACoptional = Session["MacAddress"] as string;   // Get MACAddress from session

                        //global = gsm.DeviceDataOfMAC(MACoptional);      
                        gsvm.SearchDataList = gsm.DeviceDataOfMAC(MACoptional);// Pass MacAddress to get data against  MacAddress

                    }
                    else
                    {
                        gsvm.SearchDataList = gsm.DeviceDataForSearch();    // Get Device Data for Edit,Details and View
                    }
                    break;
                case "devices":
                    int pos1 = Array.IndexOf(urlSegments, "device", 2);// check position 2 is exist with name device
                    if (pos1 > -1)
                    {
                        string segmentTwo = urlSegments[2].Replace("/", "");    // remove '/' from '/device' and 

                        string MACoptional = Session["MacAddress"] as string;   // Get MACAddress from session

                        gsvm.SearchDataList = gsm.DeviceDataOfMAC(MACoptional); // Pass MacAddress to get data against  MacAddress

                    }
                    else
                    {
                        gsvm.SearchDataList = gsm.DeviceDataForSearch();    // Get Device Data for Edit,Details and View
                    }
                    break;

                case "Leads":
                    gsvm.SearchDataList = gsm.LeadsDataForSearch(); // Get Lead Data for Edit,Details and View

                    // gsvm.SearchDataList = gsm.LeadsExtraSearch(); // Get Lead Data for Edit,Details and View
                    break;
                case "Opportunities":
                    gsvm.SearchDataList = gsm.OpportunitiesDataForSearch(); // Get Opportunities Data for Edit,Details and View
                    break;
                case "Accounts":
                    int Account_pos = Array.IndexOf(urlSegments, "AccountsDetail/", 2);
                    if (Account_pos > -1)
                    {

                        gsvm.SearchDataList.AddRange(gsm.GetAccountsDetailForSearch(new Guid(urlSegments[3])));

                    }

                    gsvm.SearchDataList.AddRange(gsm.AccountsDataForSearch());

                    //gsvm.SearchDataList = gsm.AccountsDataForSearch();// Get Accounts Data for Edit,Details and View
                    break;
                case "CustomerAssets":
                    gsvm.SearchDataList = gsm.CustomerAssetsDataForSearch();// Get CustomerAssets Data for Edit,Details and View
                    break;
                case "Readings":
                    gsvm.SearchDataList = gsm.ReadingsDataForSearch();// Get Readings Data for Edit,Details and View
                    break;
                case "ServiceCalls":
                    gsvm.SearchDataList = gsm.ServiceCallsDataForSearch();// Get ServiceCalls Data for Edit,Details and View
                    break;
                case "Resources":
                    gsvm.SearchDataList = gsm.ResourcesDataForSearch();// Get Resources Data for Edit,Details and View
                    break;
                case "WorkOrders":
                    gsvm.SearchDataList = gsm.WorkOrdersDataForSearch();// Get WorkOrders Data for Edit,Details and View
                    break;
                case "ReadingTypes":
                    gsvm.SearchDataList = gsm.ReadingTypesDataForSearch();// Get ReadingTypes Data for Edit,Details and View
                    break;
                case "ReadingUnits":
                    gsvm.SearchDataList = gsm.ReadingUnitsDataForSearch();// Get ReadingUnits Data for Edit,Details and View
                    break;

                case "Addresses":
                    gsvm.SearchDataList = gsm.AddressesDataForSearch();// Get Addresses Data for Edit,Details and View
                    break;
                case "Locations":
                    gsvm.SearchDataList = gsm.LocationsDataForSearch();// Get Locations Data for Edit,Details and View
                    break;
                case "Alerts":
                    gsvm.SearchDataList = gsm.AlertsDataForSearch();// Get Alerts Data for Edit,Details and View
                    break;
                case "WorkFlows":
                    gsvm.SearchDataList = gsm.WorkFlowsDataForSearch();// Get WorkFlows Data for Edit,Details and View
                    break;
                case "WorkFlowMappings":
                    gsvm.SearchDataList = gsm.WorkFlowMappingDataForSearch();// Get WorkFlowMapping Data for Edit,Details and View
                    break;
                case "WorkFlowReports":
                    gsvm.SearchDataList = gsm.WorkFlowReportsDataForSearch();// Get workflowreport Data for Edit,Details and View
                    break;

            }

            Session["GlobalSearchViewSession"] = gsvm;      // Pass gsvm Data in Session to fast its functionality


            return Json(gsvm);  // return a json list of gsvm in response
        }

        public JsonResult Get_All_EACSearchList()
        {
            string[] urlSegments = Request.UrlReferrer.Segments;
            GlobalSearchViewModel gsvm = new GlobalSearchViewModel();   // create an object to save in session for further used

            gsvm.FirstInSearch = gsm.FirstInSearchData();
            string MACoptional = Session["MacAddress"] as string;
            if (!string.IsNullOrEmpty(MACoptional))
            {
                gsvm.FirstInSearch.AddRange(gsm.DeviceDataOfMAC(MACoptional));
            }
            else
            {
                gsvm.FirstInSearch.AddRange(gsm.DeviceDataForSearch());
            }
            gsvm.FirstInSearch.AddRange(gsm.LeadsDataForSearch());
            gsvm.FirstInSearch.AddRange(gsm.OpportunitiesDataForSearch());
            gsvm.FirstInSearch.AddRange(gsm.CustomerAssetsDataForSearch());

            if (urlSegments.Length > 2)
            {
                int Account_pos = Array.IndexOf(urlSegments, "AccountsDetail/", 2);
                if (Account_pos > -1)
                {
                    gsvm.FirstInSearch.AddRange(gsm.GetAccountsDetailForSearch(new Guid(urlSegments[3])));

                }
            }
            gsvm.FirstInSearch.AddRange(gsm.AccountsDataForSearch());
            gsvm.FirstInSearch.AddRange(gsm.ReadingsDataForSearch());
            gsvm.FirstInSearch.AddRange(gsm.ServiceCallsDataForSearch());
            gsvm.FirstInSearch.AddRange(gsm.ResourcesDataForSearch());
            gsvm.FirstInSearch.AddRange(gsm.WorkOrdersDataForSearch());
            gsvm.FirstInSearch.AddRange(gsm.ReadingTypesDataForSearch());
            gsvm.FirstInSearch.AddRange(gsm.ReadingUnitsDataForSearch());
            gsvm.FirstInSearch.AddRange(gsm.AddressesDataForSearch());
            gsvm.FirstInSearch.AddRange(gsm.LocationsDataForSearch());
            gsvm.FirstInSearch.AddRange(gsm.AlertsDataForSearch());
            gsvm.FirstInSearch.AddRange(gsm.WorkFlowsDataForSearch());
            gsvm.FirstInSearch.AddRange(gsm.WorkFlowMappingDataForSearch());
            gsvm.FirstInSearch.AddRange(gsm.WorkFlowReportsDataForSearch());

            return Json(gsvm);  // return a json list of gsvm in response
        }


        //public string ClearGlobalSearchSession()
        //{
        //    Session["GlobalSearchViewSession"] = null; // clear session
        //    if (Session["GlobalSearchViewSession"] == null) // check session is null
        //    {
        //        return "success"; // Return succes on session Null
        //    }
        //    return "error"; // return Error Status
        //}


        public string SaveMac(string MacAddress)
        {
            Session["MacAddress"] = null; // first declare a seession null;

            Session["MacAddress"] = MacAddress; // add a MACAddress in a sesssion
            if (Session["MacAddress"] != null)
            {
                return "success"; // Return Success on save MACAddress In Session
            }
            return "error"; // return Error Status
        }


    }
}