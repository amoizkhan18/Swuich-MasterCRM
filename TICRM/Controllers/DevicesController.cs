using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using TICRM.BuisnessLayer;
using TICRM.DTOs;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using static TICRM.Cloud.Adapter.Adaptee.IBM;

namespace TICRM.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class DevicesController : Controller
    {
        //Mqtt 
        MqttClient client;
        string clientId;
        MqttClient clients;
        string clientIds;

        private DeviceManager dm = new DeviceManager();
        private DeviceSensorGraphManager dsgm = new DeviceSensorGraphManager();
        private GraphManager gm = new GraphManager(); 


        // GET: Devices
        public ActionResult Index()
        {
            ViewBag.AccountId = dm.Accounts;
            ViewBag.CustomerAssetId = dm.GetAllCustomerAssets();
            ViewBag.StatusId = new SelectList(dm.Status, "StatusId", "Name");
            ViewBag.AssignedTeam = new SelectList(dm.Teams, "TeamId", "Name");
            ViewBag.AssignedUser = new SelectList(dm.Users, "UserId", "Name");
            return View(dm.GetDevices());
        }


        public ActionResult DeviceSensorGraph()
        {
            ViewBag.AccountId = dm.Accounts;
            return View();
        }

        public JsonResult GetListOfDevice()
        {
            ViewBag.AccountId = dm.Accounts;
            return Json(dm.GetDevices(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IBMCloudSynchronized(string CloudId,Guid AccountId, Guid CustomerAssetId, Guid AssignedTeam, Guid AssignedUser)
        {
            var status = dm.GetIBMCloudList(CloudId, AccountId, CustomerAssetId, AssignedTeam, AssignedUser);
            return Json("", JsonRequestBehavior.AllowGet);
        }


        public ActionResult SynchronizedWithIBM()
        {
            List<IBMDevicesInfo> status = dm.IBMCloudList();
            ViewBag.AccountId = new SelectList(dm.Accounts, "AccountId", "Name");
            ViewBag.CustomerAssetId = new SelectList(dm.GetAllCustomerAssets(), "CustomerAssetId", "Title");
            ViewBag.AssignedTeam = new SelectList(dm.Teams, "TeamId", "Name");
            ViewBag.AssignedUser = new SelectList(dm.Users, "UserId", "Name");
            return PartialView("_PartialIBMCloud", status);
        }


        //public JsonResult CreateDevice_in_IBM_INCA(DeviceDto dvc, IBMCloudViewModel ibmdata,Guid AccoountId,string DeviceId,Guid AssignedTeam,Guid AssignedUser,string APIKey,string AppId
        //    ,string authToken,string description,string hwVersion,string manufacturer,string model,string serialNumber,string fwVersion,string DeviceTokken
        //    ,string latitude,string longitude,string OrganizationId,string date,string typeId
        //    )
        public JsonResult CreateDevice_in_IBM_INCA(DeviceDto dvc, IBMCloudViewModel ibm)
        {
            var data = dm.CreateNewCloudDevice(dvc,ibm);
            var accountDto = dm.Accounts.FirstOrDefault(x => x.AccountId == data.AccountId);
            var customerAsset = dm.GetAllCustomerAssets().FirstOrDefault(x => x.CustomerAssetId == data.CustomerAssetId);
            var team = dm.Teams.FirstOrDefault(x => x.TeamId == data.AssignedTeam);
            var user = dm.Users.FirstOrDefault(x => x.UserId == data.AssignedUser);
            var status = dm.Status.FirstOrDefault(x => x.StatusId == data.StatusId);

            var obj = new
            {
                assignUser = user != null ? user.Name : "",
                assignTeam = team != null ? team.Name : "",
                CustomerAsset = customerAsset != null ? customerAsset.Title : "",
                Status = status != null ? status.Name : "",
                AccountName = accountDto != null ? accountDto.Name : "",
                DeviceId = data.DeviceId,
                Name = data.Name,
                EMEINumber = data.EMEINumber,
                RegistrationDate = data.RegistrationDate.Value.ToString(),
                Latitude = data.Latitude,
                Longitude = data.Longitude,
                AccountId = data.AccountId,
                Mac = data.Mac
            };

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // GET: Devices/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceDto device = dm.GetDevice(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // GET: Devices/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            DeviceDto device = dm.GetDevice(id);
            return PartialView("_PartialDeviceDetails", device);
        }
        // GET: Devices/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            DeviceDto device = dm.GetDevice(id);
            return PartialView("_PartialDeviceDelete", device);
        }

        // GET: Devices/Create
        public ActionResult Create()
        {
            ViewBag.StatusId = new SelectList(dm.Status, "StatusId", "Name");
            ViewBag.AssignedTeam = new SelectList(dm.Teams, "TeamId", "Name");
            ViewBag.AssignedUser = new SelectList(dm.Users, "UserId", "Name");
            ViewBag.AccountId = new SelectList(dm.Accounts, "AccountId", "Name");
            ViewBag.CustomerAssetId = new SelectList(new List<CustomerAssetDto>(), "CustomerAssetId", "Title");

            ViewBag.CloudServices = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = CloudServiceForDD.INCA, Value = CloudServiceForDD.INCA},
        new SelectListItem { Text = CloudServiceForDD.IBM, Value = CloudServiceForDD.IBM},
        new SelectListItem { Text = CloudServiceForDD.Google, Value = CloudServiceForDD.Google},
        new SelectListItem { Text = CloudServiceForDD.Amazon, Value = CloudServiceForDD.Amazon},
        new SelectListItem { Text = CloudServiceForDD.Microsoft, Value = CloudServiceForDD.Microsoft,}}, "Value", "Text");

            ViewBag.Maintenance = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = DeviceMaintenance.None, Value = DeviceMaintenance.None},
        new SelectListItem { Text = DeviceMaintenance.IsRepaired, Value = DeviceMaintenance.IsRepaired},
        new SelectListItem { Text = DeviceMaintenance.IsServiced, Value = DeviceMaintenance.IsServiced,}}, "Value", "Text");
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IBMCloudViewModel IBMCloud,DeviceDto deviceDto)
        {
            if (ModelState.IsValid)
            {
                IBMCloud.DeviceId = deviceDto.Name;
                dm.SaveDevice(deviceDto, IBMCloud);
                return RedirectToAction("Index");
            }
            ViewBag.StatusId = new SelectList(dm.Status, "StatusId", "Name", deviceDto.StatusId);
            ViewBag.AssignedTeam = new SelectList(dm.Teams, "TeamId", "Name", deviceDto.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(dm.Users, "UserId", "Name", deviceDto.AssignedUser);
            ViewBag.AccountId = new SelectList(dm.Accounts, "AccountId", "Name", deviceDto.AccountId);
            ViewBag.CustomerAssetId = new SelectList(dm.CustomerAssetsOnAccountId((Guid)deviceDto.AccountId), "CustomerAssetId", "Title", deviceDto.CustomerAssetId);


            ViewBag.CloudServices = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = CloudServiceForDD.INCA, Value = CloudServiceForDD.INCA},
        new SelectListItem { Text = CloudServiceForDD.IBM, Value = CloudServiceForDD.IBM},
        new SelectListItem { Text = CloudServiceForDD.Google, Value = CloudServiceForDD.Google},
        new SelectListItem { Text = CloudServiceForDD.Amazon, Value = CloudServiceForDD.Amazon},
        new SelectListItem { Text = CloudServiceForDD.Microsoft, Value = CloudServiceForDD.Microsoft,}}, "Value", "Text", deviceDto.CloudServices);

            ViewBag.Maintenance = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = DeviceMaintenance.None, Value = DeviceMaintenance.None},
        new SelectListItem { Text = DeviceMaintenance.IsRepaired, Value = DeviceMaintenance.IsRepaired},
        new SelectListItem { Text = DeviceMaintenance.IsServiced, Value = DeviceMaintenance.IsServiced,}}, "Value", "Text", deviceDto.Maintenance);

            DeviceViewModel deviceViewModel = new DeviceViewModel();
            deviceViewModel.Name = deviceDto.Name;
            deviceViewModel.Mac = deviceDto.Mac;
            deviceViewModel.EMEINumber = deviceDto.EMEINumber;
            deviceViewModel.RegistrationDate = deviceDto.RegistrationDate;
            deviceViewModel.Latitude = deviceDto.Latitude;
            deviceViewModel.Longitude = deviceDto.Longitude;
            deviceViewModel.IBMCloud = IBMCloud;
            return View(deviceViewModel);
        }

        // GET: Devices/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceDto device = dm.GetDevice(id);
            if (device == null)
            {
                return HttpNotFound();
            }

            DeviceViewModel deviceViewModel = new DeviceViewModel();
            deviceViewModel.DeviceId = device.DeviceId;
            deviceViewModel.Name = device.Name;
            deviceViewModel.Mac = device.Mac;
            deviceViewModel.EMEINumber = device.EMEINumber;
            deviceViewModel.RegistrationDate = device.RegistrationDate;
            deviceViewModel.Latitude = device.Latitude;
            deviceViewModel.Longitude = device.Longitude;
            if (device.CloudData == null)
            {
                deviceViewModel.IBMCloud = new IBMCloudViewModel();
            }
            else
            {
                deviceViewModel.IBMCloud = Newtonsoft.Json.JsonConvert.DeserializeObject<IBMCloudViewModel>(device.CloudData);
            }

            ViewBag.StatusId = new SelectList(dm.Status, "StatusId", "Name", device.StatusId);
            ViewBag.AssignedTeam = new SelectList(dm.Teams, "TeamId", "Name", device.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(dm.Users, "UserId", "Name", device.AssignedUser);
            ViewBag.AccountId = new SelectList(dm.Accounts, "AccountId", "Name", device.AccountId);
            ViewBag.CustomerAssetId = new SelectList(dm.CustomerAssetsOnAccountId((Guid)device.AccountId), "CustomerAssetId", "Title", device.CustomerAssetId);


            ViewBag.CloudServices = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = CloudServiceForDD.INCA, Value = CloudServiceForDD.INCA},
        new SelectListItem { Text = CloudServiceForDD.IBM, Value = CloudServiceForDD.IBM},
        new SelectListItem { Text = CloudServiceForDD.Google, Value = CloudServiceForDD.Google},
        new SelectListItem { Text = CloudServiceForDD.Amazon, Value = CloudServiceForDD.Amazon},
        new SelectListItem { Text = CloudServiceForDD.Microsoft, Value = CloudServiceForDD.Microsoft,}}, "Value", "Text", device.CloudServices);

            ViewBag.Maintenance = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = DeviceMaintenance.None, Value = DeviceMaintenance.None},
        new SelectListItem { Text = DeviceMaintenance.IsRepaired, Value = DeviceMaintenance.IsRepaired},
        new SelectListItem { Text = DeviceMaintenance.IsServiced, Value = DeviceMaintenance.IsServiced,}}, "Value", "Text", device.Maintenance);


            return View(deviceViewModel);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DeviceDto device,IBMCloudViewModel IBMCloud)
        {
            if (ModelState.IsValid)
            {
                dm.SaveDevice(device, IBMCloud, true);
                return RedirectToAction("Index");
            }
            ViewBag.StatusId = new SelectList(dm.Status, "StatusId", "Name", device.StatusId);
            ViewBag.AssignedTeam = new SelectList(dm.Teams, "TeamId", "Name", device.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(dm.Users, "UserId", "Name", device.AssignedUser);
            ViewBag.AccountId = new SelectList(dm.Accounts, "AccountId", "Name", device.AccountId);
            ViewBag.CustomerAssetId = new SelectList(dm.CustomerAssetsOnAccountId((Guid)device.AccountId), "CustomerAssetId", "Title", device.CustomerAssetId);


            ViewBag.CloudServices = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = CloudServiceForDD.INCA, Value = CloudServiceForDD.INCA},
        new SelectListItem { Text = CloudServiceForDD.IBM, Value = CloudServiceForDD.IBM},
        new SelectListItem { Text = CloudServiceForDD.Google, Value = CloudServiceForDD.Google},
        new SelectListItem { Text = CloudServiceForDD.Amazon, Value = CloudServiceForDD.Amazon},
        new SelectListItem { Text = CloudServiceForDD.Microsoft, Value = CloudServiceForDD.Microsoft,}}, "Value", "Text", device.CloudServices);

            ViewBag.Maintenance = new SelectList(new List<SelectListItem>    {
        new SelectListItem { Text = DeviceMaintenance.None, Value = DeviceMaintenance.None},
        new SelectListItem { Text = DeviceMaintenance.IsRepaired, Value = DeviceMaintenance.IsRepaired},
        new SelectListItem { Text = DeviceMaintenance.IsServiced, Value = DeviceMaintenance.IsServiced,}}, "Value", "Text", device.Maintenance);

            DeviceViewModel deviceViewModel = new DeviceViewModel();
            deviceViewModel.Name = device.Name;
            deviceViewModel.Mac = device.Mac;
            deviceViewModel.EMEINumber = device.EMEINumber;
            deviceViewModel.RegistrationDate = device.RegistrationDate;
            deviceViewModel.Latitude = device.Latitude;
            deviceViewModel.Longitude = device.Longitude;
            deviceViewModel.IBMCloud = IBMCloud;
            return View(deviceViewModel);


        }

        // GET: Devices/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceDto device = dm.GetDevice(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DeviceDto device = dm.GetDevice(id);
            dm.SaveDevice(device,null, true, true);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Device()
        {
            return View();
        }

        [HttpGet]
        public void UpdateDeviceSerivceDate(string mac, string date)
        {
            dm.UpdateDeviceServiceDate(mac, date); // pass mac and date to device manager
        }


        // here is the development part of AQIL


        // Get: GetDevicesDropdownList
        public JsonResult GetDevicesDropdownList()
        {
            List<DeviceDto> list = dm.GetDevices(); // get list of Devices from Device Manager
            return Json(list, JsonRequestBehavior.AllowGet); // return json List
        }


        // Get: GetSensorDropDownList
        public JsonResult GetSensorDropDownList()
        {
            List<SensorDto> list = dsgm.GetSensorList(); // get sensor list from device sensor graph manager
            return Json(list, JsonRequestBehavior.AllowGet); // return json list
        }


        // Get: GetGraphDropDownList
        public JsonResult GetGraphDropDownList()
        {
            List<GraphDto> list = gm.GetGraphList(); // get graph List from graph manager
            return Json(list, JsonRequestBehavior.AllowGet); // return json list
        }


        public JsonResult GetCustomerAssetsForDD(Guid accountId)
        {
            SelectList data = new SelectList(dm.CustomerAssetsOnAccountId(accountId), "CustomerAssetId", "Title");
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //POST: SubmitDeviceSensorGraph
        public string SubmitDeviceSensorGraph(Guid DeviceId, Guid SensorId, Guid GraphId)
        {
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            bool condition = dsgm.SubmitDeviceSensorGraph(DeviceId, SensorId, GraphId, CurrentUserId); // save data in device sensor graph manager

            if (condition == true) //check condition if is true return success otherwise error
            {
                return "success";
            }
            return "error";
        }


        public string GetDeviceSensorGraphList()
        {
            return dsgm.GetDeviceSensorGraphList(); // get list device sensor graph and return in response
        }

        public string GetGraphOfMACData(string MacAddress)
        {
            return dsgm.DeviceSensorGraphOfMAC(MacAddress); // get device sensor graph on mac address and return in response
        }

        public string DSGonDeviceId(Guid deviceId)
        {
            return dsgm.GetDSGListOn_DeviceId(deviceId); // get device sensor graph on deviceId and return json in response
        }


        public string UpdateDeviceSensorGraph(Guid DeviceSensorGraphId, Guid DeviceId, Guid SensorId, Guid GraphId)
        {
            string CurrentUserId = User.Identity.GetUserId(); // get current userid
            bool condition = dsgm.UpdateDeviceSensorGraph(DeviceSensorGraphId, DeviceId, SensorId, GraphId, CurrentUserId); // update device sensor graph in dsgManager
            if (condition == true) //check condition if is true return success otherwise return error
            {
                return "success";
            }
            return "error";
        }

        public JsonResult EditDeviceSensorGraph(Guid DeviceSensorGraphId)
        {
            DeviceSensorGraphDto data = new DeviceSensorGraphDto(); // create an object of a class
            data = dsgm.GetDeviceSensorGraphOnId(DeviceSensorGraphId); // get data from device sensor graph from DeviceSensorGraphManager
            return Json(data, JsonRequestBehavior.AllowGet); // return Json in Response
        }


        public ActionResult SendMessageToMqtt(bool deviceStatus)
        {
            System.Diagnostics.Debug.WriteLine(deviceStatus);
            string status = "error";

            //MQTT
            string BrokerAddress = "broker.hivemq.com";
            client = new MqttClient(BrokerAddress, 1883, false, MqttSslProtocols.None, null, null);
            clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
            System.Diagnostics.Debug.WriteLine("Connected");
            String Topic = "ServerGateway";
            String state = deviceStatus.ToString();
            state = "R-A-" + state;
            client.Publish(Topic, Encoding.UTF8.GetBytes(state), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

            try
            {

            }
            catch (Exception ex)
            {

            }
            return Content(status);
        }


        public ActionResult SendMessageToMqtts(int silderValue)
        {
            string status = "error";

            try
            {
                System.Diagnostics.Debug.WriteLine(silderValue);


                //MQTT
                string BrokerAddresss = "broker.hivemq.com";
                clients = new MqttClient(BrokerAddresss, 1883, false, MqttSslProtocols.None, null, null);
                clientIds = Guid.NewGuid().ToString();
                clients.Connect(clientIds);
                System.Diagnostics.Debug.WriteLine("Connected");
                String Topic = "ServerGateway";
                String value = silderValue.ToString();
                value = "D-A-" + value;
                clients.Publish(Topic, Encoding.UTF8.GetBytes(value), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);


            }
            catch (Exception ex)
            {

            }
            return Content(status);
        }



        public string DeleteDeviceSensorGraph(Guid DeviceSensorGraphId)
        {
            string CurrentUserId = User.Identity.GetUserId();       // get current userid
            bool status = dsgm.DeleteDeviceSensorGraph(DeviceSensorGraphId); // delete DeviceSensorGraph in DeviceSensorGraphManager and get true if successfully deleted otherwise it return false

            if (status == true)//check condition if is true return success otherwise error
            {
                return "success";
            }
            return "error";
        }

    }

}
