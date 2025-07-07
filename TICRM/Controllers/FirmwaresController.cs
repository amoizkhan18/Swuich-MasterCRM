using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TICRM.DAL;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace TICRM.Controllers
{
    public class FirmwaresController : Controller
    {
        string targetPath = @"C:\inetpub\wwwroot";
        MqttClient client;
        string clientId;
        private CRMEntities db = new CRMEntities();
        // GET: Firmwares

        public ActionResult Index()
        {
            /// MQTT Listner
            //try
            //{
            //    clients = new MqttClient("broker.hivemq.com", 1883, false, MqttSslProtocols.None, null, null);
            //    clientIds = Guid.NewGuid().ToString();
            //    clients.Connect(clientIds);
            //    if (clients.IsConnected)
            //    {
            //        System.Diagnostics.Debug.WriteLine("Connected");
            //    }

            //    ushort msgId = clients.Subscribe(new string[] { "TiTempSensor" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            //    Debug.WriteLine(msgId);
            //    clients.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;

            //}
            //catch (Exception ex)
            //{
            //    // will log this exception
            //    Console.WriteLine("Main :" + ex.Message);
            //}
            return View(db.Firmwares.ToList());
        }

        // GET: Firmwares/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Firmware firmware = db.Firmwares.Find(id);
        //    if (firmware == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(firmware);
        //}

        // GET: Firmwares/Create
        public ActionResult Create(string MacAddress = null)
        {
            return View();
        }

        // POST: Firmwares/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Version,Description, server")] Firmware firmware, HttpPostedFileBase file, string mac)
        {

            if (ModelState.IsValid)
            {

                //id and date
                byte[] gb = Guid.NewGuid().ToByteArray();
                int i = BitConverter.ToInt32(gb, 0);
                firmware.Id = i;
                firmware.Date = DateTime.Now;
                firmware.File = file.FileName;
                db.Firmwares.Add(firmware);
                db.SaveChanges();
                //MQTT
                // string server = Server;
                string BrokerAddress = "broker.hivemq.com";
                client = new MqttClient(BrokerAddress, 1883, false, MqttSslProtocols.None, null, null);
                clientId = Guid.NewGuid().ToString();
                client.Connect(clientId);
                System.Diagnostics.Debug.WriteLine("Connected");
                String Topic = "ServerGateway";
                System.Diagnostics.Debug.WriteLine(firmware.version);
                
                //File saving
                try
                {
                    string path = Path.Combine(targetPath,
                                       Path.GetFileName(file.FileName));
                    System.Diagnostics.Debug.WriteLine(file.FileName);
                    file.SaveAs(path);
                    System.Diagnostics.Debug.WriteLine("Uploaded");
                    ViewBag.Message = "File uploaded successfully";
                }

                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception");
                    System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
                //MQTT Publish
                if (mac == "all")
                {
                    client.Publish(Topic, Encoding.UTF8.GetBytes("F-"+"A-" + firmware.version), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                }
                else
                    client.Publish(Topic, Encoding.UTF8.GetBytes("F-" + mac + "-" + firmware.version), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

                System.Diagnostics.Debug.WriteLine("Published");
                
                mac = null;
                return RedirectToAction("Index");
            }
            client.Disconnect();
            return View("Index");
        }

    }
}
