using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;
using static TICRM.Cloud.Adapter.Adaptee.IBM;

namespace TICRM.BuisnessLayer
{
    public class DeviceManager : BaseManager
    {
        public DeviceManager()
        {

        }

        public bool UpdateDeviceServiceDate(string mac, string date)
        {
            try
            {
                InsertEventLog("UpdateDeviceServiceDate", EventType.Log, EventColor.yellow, "Successfully Enter", "TICRM.BuisnessLayer.DeviceManager.UpdateDeviceServiceDate", "");

                Device device = dbEnt.Devices.Where(a => a.Mac == mac).FirstOrDefault();
                if (device != null)
                {
                    InsertEventLog("UpdateDeviceServiceDate", EventType.Log, EventColor.yellow, "device is not null", "TICRM.BuisnessLayer.DeviceManager.UpdateDeviceServiceDate", "");

                    dbEnt.Entry(device).State = EntityState.Modified;
                    //var dt = DateTime.Parse(date);
                    DateTime dt = DateTime.ParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    //update service date 
                    device.ServiceDate = dt;
                    device.ServiceDateFlag = false;
                    dbEnt.SaveChanges();
                }
                else
                {
                    InsertEventLog("UpdateDeviceServiceDate", EventType.Log, EventColor.yellow, "device is null on mac and date", "TICRM.BuisnessLayer.DeviceManager.UpdateDeviceServiceDate", "");
                }
            }
            catch (Exception ex)
            {
                InsertEventMonitor("UpdateDeviceServiceDate", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DeviceManager.UpdateDeviceServiceDate", "");

                throw ex;
            }
            return true;
        }

        public DeviceDto GetDevice(Guid? guid)
        {
            try
            {
                InsertEventLog("UpdateDeviceServiceDate", EventType.Log, EventColor.yellow, "To get Device Dto on deveice id=" + guid + " ", "TICRM.BuisnessLayer.DeviceManager.UpdateDeviceServiceDate", "");

                return objMapper.GetDeviceDTO(dbEnt.Devices.Find(guid));
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetDevice on deviceid", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DeviceManager.UpdateDeviceServiceDate", "");

                throw ex;
            }
        }


        public string GetIBMCloudList(string CloudId, Guid AccountId, Guid CustomerAssetId, Guid AssignedTeam, Guid AssignedUser)
        {
            string status = "";
            List<IBMDevicesInfo> registerDevicesInfos = new List<IBMDevicesInfo>();
            List<IBMCloudViewModel> ibm_List = new List<IBMCloudViewModel>();
            try
            {
                List<Device> devices = dbEnt.Devices.Include(d => d.Status).Include(d => d.Team).Include(d => d.User).Where(a => a.IsDeleted != true).ToList();
                TICRM.Cloud.Adapter.CloudMain cloudMain = new Cloud.Adapter.CloudMain();

                foreach (Device item in devices)
                {
                    if (item.CloudData != null && item.CloudData != "")
                    {
                        IBMCloudViewModel IBMCloudView = new IBMCloudViewModel();

                        IBMCloudViewModel qIBMCloudView = Newtonsoft.Json.JsonConvert.DeserializeObject<IBMCloudViewModel>(item.CloudData) as IBMCloudViewModel;
                        ibm_List.Add(qIBMCloudView);
                    }
                }

                registerDevicesInfos = cloudMain.GetAllIBMCloudDevice(ibm_List);


                foreach (IBMDevicesInfo item in registerDevicesInfos)
                {
                    //if (item.APIKey == null && item.AuthToken == null){continue;}

                    IBMCloudViewModel condition = ibm_List.FirstOrDefault(x => x.DeviceId == item.deviceId && x.DeviceType == item.typeId);

                    Device get_Device = devices.FirstOrDefault(x => x.Name == item.deviceId);


                    if (get_Device == null && condition == null)
                    {

                        Device d = new Device();
                        d.AccountId = AccountId;
                        d.CustomerAssetId = CustomerAssetId;
                        d.AssignedTeam = AssignedTeam;
                        d.AssignedUser = AssignedUser;
                        d.CloudData = "";
                        d.CloudServices = "";
                        d.CreatedDate = DateTime.Now;
                        //d.DeciveConfigurations = "";
                        d.DeviceId = Guid.NewGuid();
                        //d.DeviceSensors = "";
                        d.EMEINumber = item.deviceInfo.serialNumber;
                        d.IsDeleted = false;
                        d.Latitude = (decimal)item.location.latitude;
                        d.Longitude = (decimal)item.location.longitude;
                        d.Mac = item.deviceInfo.model;
                        d.Maintenance = DeviceMaintenance.None;
                        d.Name = item.deviceId;
                        d.RegistrationDate = Convert.ToDateTime(item.registration.date);
                        d.StatusId = new Guid("192f959f-2dfa-4d41-8464-dd482325dc6c");

                        //IBMCloudViewModel ibm = new IBMCloudViewModel();
                        ////ibm.OrganizationId = item.
                        ////ibm.APIKey = item.
                        ////ibm.AppId =item.
                        //ibm.AuthToken = item.authToken;
                        //ibm.DeviceId = item.deviceId;
                        ////ibm.DeviceTokken = item
                        //ibm.DeviceType = item.typeId;

                        dbEnt.Devices.Add(d);
                        dbEnt.SaveChanges();

                    }
                }


            }
            catch (Exception ex)
            {

            }
            return status;
        }



        public List<IBMDevicesInfo> IBMCloudList()
        {
            string status = "";
            List<IBMDevicesInfo> registerDevicesInfos = new List<IBMDevicesInfo>();
            List<IBMCloudViewModel> ibm_List = new List<IBMCloudViewModel>();
            List<IBMDevicesInfo> return_list = new List<IBMDevicesInfo>();

            try
            {
                List<Device> devices = dbEnt.Devices.Include(d => d.Status).Include(d => d.Team).Include(d => d.User).Where(a => a.IsDeleted != true).ToList();
                TICRM.Cloud.Adapter.CloudMain cloudMain = new Cloud.Adapter.CloudMain();

                foreach (Device item in devices)
                {
                    if (item.CloudData != null && item.CloudData != "")
                    {
                        IBMCloudViewModel IBMCloudView = new IBMCloudViewModel();

                        IBMCloudViewModel qIBMCloudView = Newtonsoft.Json.JsonConvert.DeserializeObject<IBMCloudViewModel>(item.CloudData) as IBMCloudViewModel;
                        ibm_List.Add(qIBMCloudView);
                    }
                }

                registerDevicesInfos = cloudMain.GetAllIBMCloudDevice(ibm_List);




                foreach (IBMDevicesInfo item in registerDevicesInfos)
                {
                    //if (item.APIKey == null && item.AuthToken == null){continue;}

                    IBMCloudViewModel condition = ibm_List.FirstOrDefault(x => x.DeviceId == item.deviceId && x.DeviceType == item.typeId);

                    Device get_Device = devices.FirstOrDefault(x => x.Name == item.deviceId); // here device id is not a int its is string and it come from ibm

                    if (get_Device == null && condition == null)
                    {
                        return_list.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return return_list;
        }






        /// <summary>
        /// Get device list 
        /// </summary>
        /// <returns></returns>
        /// 

        public List<DeviceDto> GetDevices()
        {
            try
            {
                InsertEventLog("GetDevices", EventType.Log, EventColor.yellow, "To get Device Dto list ", "TICRM.BuisnessLayer.DeviceManager.GetDevices", "");

                List<DeviceDto> deviceDtos = new List<DeviceDto>();
                List<IBMCloudViewModel> ibmList = new List<IBMCloudViewModel>();

                List<Device> devices = dbEnt.Devices.Include(d => d.Status).Include(d => d.Team).Include(d => d.User).Where(a => a.IsDeleted != true).ToList();
                foreach (Device item in devices)
                {
                    deviceDtos.Add(objMapper.GetDeviceDTO(item));
                }

                return deviceDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetDevices", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DeviceManager.GetDevices", "");

                throw;
            }


        }

        public List<DeviceDto> GetDevices(Guid accountId)
        {
            try
            {
                InsertEventLog("GetDevices", EventType.Log, EventColor.yellow, "To get Device Dto list on accountid=" + accountId + " ", "TICRM.BuisnessLayer.DeviceManager.GetDevices", "");
                List<DeviceDto> deviceDtos = new List<DeviceDto>();
                List<Device> devices = dbEnt.Devices.Include(d => d.Status).Include(d => d.Team).Include(d => d.User).Where(a => a.IsDeleted != true && a.AccountId == accountId).ToList();
                foreach (Device item in devices)
                {
                    deviceDtos.Add(objMapper.GetDeviceDTO(item));
                }
                return deviceDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetDevices on Accountid", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DeviceManager.GetDevices", "");
                throw;
            }

        }

        public List<DeviceDto> GetDevicesOnAssetsId(Guid assetId)
        {
            try
            {
                InsertEventLog("GetDevices", EventType.Log, EventColor.yellow, "To get Device Dto list on assetId=" + assetId + " ", "TICRM.BuisnessLayer.DeviceManager.GetDevicesOnAssetsId", "");
                List<DeviceDto> deviceDtos = new List<DeviceDto>();
                List<Device> devices = dbEnt.Devices.Include(d => d.Status).Include(d => d.Team).Include(d => d.User).Where(a => a.IsDeleted != true && a.CustomerAssetId == assetId).ToList();
                foreach (Device item in devices)
                {
                    deviceDtos.Add(objMapper.GetDeviceDTO(item));
                }
                return deviceDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetDevices on Accountid", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DeviceManager.GetDevicesOnAssetsId", "");
                throw;
            }

        }

        /// <summary>
        /// save and edit DeviceDto 
        /// </summary>
        /// <param name="dvc"></param>
        /// <returns></returns>
        public bool SaveDevice(DeviceDto dvc, IBMCloudViewModel ibm = null, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SaveDevice", EventType.Log, EventColor.yellow, "Successfully Enter ", "TICRM.BuisnessLayer.DeviceManager.SaveDevice", "");
                Device device;
                if (isEditMode)
                {
                    InsertEventLog("SaveDevice", EventType.Log, EventColor.yellow, "Enter in Edit Mode To Save Edit Device on id ", "TICRM.BuisnessLayer.DeviceManager.SaveDevice", "");

                    device = objMapper.GetDevice(dvc);
                    if (isDeleteMode)
                    {
                        InsertEventLog("SaveDevice", EventType.Log, EventColor.yellow, "Enter in Delete Mode To Delete Device ", "TICRM.BuisnessLayer.DeviceManager.SaveDevice", "");
                        Device devicefordelete = dbEnt.Devices.FirstOrDefault(x => x.DeviceId == device.DeviceId);
                        devicefordelete.IsDeleted = true;
                    }
                    else
                    {
                        Device d = dbEnt.Devices.FirstOrDefault(x => x.DeviceId == dvc.DeviceId);
                        if (d != null)
                        {
                            if (dvc.CloudServices == CloudServiceForDD.IBM)
                            {
                                TICRM.Cloud.Adapter.CloudMain cloudMain = new Cloud.Adapter.CloudMain();
                                string result = cloudMain.IBMCloudCoverage(dvc, ibm, "update");
                                if (result == "error") { return false; }
                            }


                            d.Name = dvc.Name;
                            d.Mac = dvc.Mac;
                            d.EMEINumber = dvc.EMEINumber;
                            d.RegistrationDate = dvc.RegistrationDate;
                            d.Latitude = dvc.Latitude;
                            d.Longitude = dvc.Longitude;
                            d.StatusId = dvc.StatusId;
                            d.AccountId = dvc.AccountId;
                            d.CustomerAssetId = dvc.CustomerAssetId;
                            d.AssignedUser = dvc.AssignedUser;
                            d.AssignedTeam = dvc.AssignedTeam;
                            d.Maintenance = dvc.Maintenance;
                            d.ServiceDate = dvc.ServiceDate;
                            d.Maintenance = dvc.Maintenance;
                            d.CloudServices = dvc.CloudServices;
                            d.CloudData = Newtonsoft.Json.JsonConvert.SerializeObject(ibm);
                        }
                    }
                    dbEnt.SaveChanges();
                    return true;
                }
                else
                {
                    InsertEventLog("SaveDevice", EventType.Log, EventColor.yellow, "Create new device ", "TICRM.BuisnessLayer.DeviceManager.SaveDevice", "");
                    if (dvc.CloudServices == CloudServiceForDD.IBM)
                    {
                        TICRM.Cloud.Adapter.CloudMain cloudMain = new Cloud.Adapter.CloudMain();
                        string result = cloudMain.IBMCloudCoverage(dvc, ibm, "create");
                        if (result == "error") { return false; }
                    }

                    dvc.CloudData = Newtonsoft.Json.JsonConvert.SerializeObject(ibm);
                    device = objMapper.GetDevice(dvc);
                    device.DeviceId = Guid.NewGuid();
                    dbEnt.Devices.Add(device);
                    if (dbEnt.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                InsertEventMonitor("SaveDevice", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DeviceManager.SaveDevice", "");
                throw ex;
            }
            return false;
        }




        public DeviceDto CreateNewCloudDevice(DeviceDto dvc,IBMCloudViewModel item = null)
        {
            try
            {
                InsertEventLog("CreateNewCloudDevice", EventType.Log, EventColor.yellow, "Successfully Enter ", "TICRM.BuisnessLayer.DeviceManager.CreateNewCloudDevice", "");

                Device d = new Device();
                d.DeviceId = Guid.NewGuid();
                d.AccountId = dvc.AccountId;
                d.CustomerAssetId = dvc.CustomerAssetId;
                d.AssignedTeam = dvc.AssignedTeam;
                d.AssignedUser = dvc.AssignedUser;
                d.CloudData = "";
                d.CloudServices = "";
                d.CreatedDate = DateTime.Now;
                //d.DeciveConfigurations = "";
                //d.DeviceSensors = "";
                d.EMEINumber = dvc.EMEINumber;
                d.IsDeleted = false;
                d.Latitude = dvc.Latitude;
                d.Longitude = dvc.Longitude;
                d.Mac = dvc.Mac;
                d.Maintenance = DeviceMaintenance.None;
                d.Name = dvc.Name;
                d.RegistrationDate = dvc.RegistrationDate;
                d.StatusId = new Guid("192f959f-2dfa-4d41-8464-dd482325dc6c");
                d.CloudServices = "IBM";
                d.CloudData = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                dbEnt.Devices.Add(d);
                if (dbEnt.SaveChanges() > 0)
                {
                    return objMapper.GetDeviceDTO(d);
                }
            }
            catch (Exception ex)
            {
                InsertEventMonitor("CreateNewCloudDevice", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DeviceManager.CreateNewCloudDevice", "");
                throw ex;
            }
            return null;
        }









    }
}
