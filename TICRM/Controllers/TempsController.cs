using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
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
    public class TempsController : Controller
    {
        private CRMEntities db = new CRMEntities();
        Temp temp = new Temp();
        MqttClient clients;
        string clientIds;
        public string[] split;
        public string Temperature;
        public string message;
        public string Humadity;
        int count = 1;
        // GET: Temps
        public ActionResult Index()
        {
            do
            {
                try
                {
                    
                    clients = new MqttClient("broker.hivemq.com", 1883, false, MqttSslProtocols.None, null, null);
                    clientIds = Guid.NewGuid().ToString();
                    clients.Connect(clientIds);
                    if (clients.IsConnected)
                    {
                        System.Diagnostics.Debug.WriteLine("Connected");
                    }

                    ushort msgId = clients.Subscribe(new string[] { "TiTempSensors" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                    Debug.WriteLine(msgId);
                    clients.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;

                }
                catch (Exception ex)
                {
                    // will log this exception
                    Console.WriteLine("Main :" + ex.Message);
                }
                count = count + 1;
                System.Diagnostics.Debug.WriteLine(count);
                return View(db.Temps.ToList());
            } while (true);


        }


        //[HttpPost]
        public void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            message = Encoding.UTF8.GetString(e.Message);
            Debug.WriteLine("Olamba");

            Debug.WriteLine("message recieved:" + message);
            Debug.WriteLine("");

            if (message != null)
            {
                ////Splitting message
                split = message.Split(',');
                Temperature = split[0];
                Humadity = split[1];
                Debug.WriteLine("Message Recieved on: " + DateTime.Now);
                Debug.WriteLine("Temprature Recieved: " + Temperature);
                Debug.WriteLine("Temprature Recieved: " + Humadity);
                SaveDataInFirmware(Temperature, Humadity);

            }
        }


        public void SaveDataInFirmware(string Tempr, String Humadity)
        {
            temp.Date = DateTime.Now;
            temp.Temeprature = Tempr;
            temp.Humadity = Humadity;
            db.Temps.Add(temp);
            db.SaveChanges();
        }



    }
}
