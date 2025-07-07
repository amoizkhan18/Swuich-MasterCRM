using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace TICRM.MQTT.Listner
{
    internal class Program
    {
        public static TechImplementCRMEntities db = new TechImplementCRMEntities();

        private static int cnt = 0;
        private static List<SensorData> lstsensor = new List<SensorData>();
        /// <summary>
        /// Main Execution of the Listner
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            try
            {
                MqttClient client = Connect("broker.hivemq.com");
                Console.WriteLine("Connectd Successfully to broker.hivemq.com ");
                Subscribe(client, "GatewayServer");
                Console.WriteLine("Subscribed Successfully to the Topic TiTempSensor");
                Console.WriteLine("Now Waiting for connected devices data");
            }
            catch (Exception ex)
            {
                // will log this exception
                Console.WriteLine("Main :" + ex.Message);
            }
        }

        /// <summary>
        /// Connect with Broker
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static MqttClient Connect(string host)
        {
            MqttClient client = new MqttClient(host);
            try
            {
                string clientId = Guid.NewGuid().ToString();
                client.Connect(clientId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connect :" + ex.Message);
            }
            return client;
        }

        /// <summary>
        /// Subscribe
        /// </summary>
        /// <param name="client"></param>
        /// <param name="topic"></param>
        public static void Subscribe(MqttClient client, string topic)
        {
            try
            {
                // register to message received
                client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
                string clientId = Guid.NewGuid().ToString();
                client.Connect(clientId);
                client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            }
            catch (Exception ex)
            {

                Console.WriteLine("Subscribe :" + ex.Message);
            }
        }
        
        /// <summary>
        /// Message received 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                cnt++;
                Console.WriteLine(e.Topic + " : " + Encoding.UTF8.GetString(e.Message));
                string[] msg = Encoding.UTF8.GetString(e.Message).Split(',');
                string mac = msg[1];
                Tracking tracking = new Tracking();
                tracking.Id = Guid.NewGuid();
                tracking.Devicemac = msg[1];
                tracking.Devicelocation = msg[2] + "," + msg[3];
                tracking.Datatime= DateTime.Now;
                db.Trackings.Add(tracking);
                db.SaveChanges();

                lstsensor.Add(GetDeviceData(msg[1], msg[0]));
                if (cnt >= 15)
                {
                    cnt = 0;
                    InsertDeviceData(lstsensor);
                    lstsensor.Clear();
                    lstsensor = new List<SensorData>();
                    Console.Clear();
                    Console.WriteLine("Clear Screen \n  Sensor Data Bulk inserted into DataBase Successfully!");
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Publish Received: " + ex.Message);
            }

        }

        /// <summary>
        /// Publis Send Value to the Client - Devices
        /// </summary>
        /// <param name="client"></param>
        /// <param name="title"></param>
        /// <param name="value"></param>
        public static void Publish(MqttClient client, string title, string value)
        {
            try
            {
                string strValue = Convert.ToString(value);
                client.Publish(title, Encoding.UTF8.GetBytes(strValue));
            }
            catch (Exception ex)
            {

                Console.WriteLine("Publish :" + ex.Message);
            }

        }

        /// <summary>
        /// Add sensor data to the device 
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="data"></param>
        public static SensorData GetDeviceData(string mac, string data)
        {
            try
            {
                IQueryable<Device> device = db.Devices.Where(a => a.Mac == mac);
                if (device != null)
                {
                    IQueryable<DeviceSensor> sensor = db.DeviceSensors.Where(a => a.DeviceId == device.FirstOrDefault().DeviceId);
                    if (device.FirstOrDefault().ServiceDate != null)
                    {
                        DateTime deviceserviceDate = Convert.ToDateTime(device.FirstOrDefault().ServiceDate);
                        int result = DateTime.Compare(deviceserviceDate.Date, DateTime.Now.Date);
                        if (device.FirstOrDefault().ServiceDateFlag == false && result == 0)
                        {
                            Device createWorkOrder = device.FirstOrDefault();
                            string MacAddress = device.FirstOrDefault().Mac;
                            Guid? AssignedUser = device.FirstOrDefault().AssignedUser;
                            Guid? AssignedTeam = device.FirstOrDefault().AssignedTeam;

                            bool status = GenerateWorkOrder(MacAddress, AssignedUser, AssignedTeam);
                            if (status == true)
                            {
                                device.FirstOrDefault().ServiceDateFlag = true;
                            }
                        }
                    }

                    if (sensor != null)
                    {
                        SensorData sensorData = new SensorData();
                        
                        // sensorData.DeviceSensor = sensor;
                        sensorData.DeviceSensorId = sensor.FirstOrDefault().DeviceSensorId;
                        sensorData.SensorValue = Double.Parse(data);
                        sensorData.RecordDate = DateTime.Now;
                        using (System.IO.StreamWriter file =
          new System.IO.StreamWriter(@"C:\Users\akhtar.zaman\Desktop\Blink\New.txt", true))
                        {
                            file.WriteLine(data);
                        }
                        return sensorData;

                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Insert Device Data :" + ex.Message);
            }
            return null;
        }

        public static void InsertDeviceData(List<SensorData> lst)
        {
            try
            {
                db.SensorDatas.AddRange(lst);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Insert Device Data :" + ex.Message);
            }
        }

        public static bool GenerateWorkOrder(string MacAddress,Guid? AssignedUser, Guid? AssignedTeam)
        {                                    
            WorkOrder wo = new WorkOrder();
            try
            {
                Device devicecheck = db.Devices.Where(a => a.Mac == MacAddress).FirstOrDefault();
                if (devicecheck.ServiceDateFlag == false)
                {
                    wo.WorkOrderId = Guid.NewGuid();
                    wo.Title = MacAddress;
                    wo.NTE = Convert.ToDecimal(1542.23);
                    wo.WorkOrderStageId = new Guid("fd9313c9-65c9-44b7-b34f-f67fd7a8f90c"); // to insert new WorkOrder stage
                    wo.Description = "Device Need To be Serviced.";
                    wo.IsDeleted = false;
                    wo.StatusId = new Guid("192f959f-2dfa-4d41-8464-dd482325dc6c"); // insert a active entry in database that have a value "192f959f-2dfa-4d41-8464-dd482325dc6c"
                    wo.AssignedUser = AssignedUser;
                    wo.AssignedTeam = AssignedTeam;
                    wo.CreatedDate = DateTime.Now;
                    db.WorkOrders.Add(wo);
                    if (db.SaveChanges() > 0)
                    {
                        devicecheck.ServiceDateFlag = true;
                        if (db.SaveChanges()>0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return false;
        }

    }
}
