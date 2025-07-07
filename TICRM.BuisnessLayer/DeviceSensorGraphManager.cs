using System;
using System.Collections.Generic;
using System.Linq;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.BuisnessLayer
{
    public class DeviceSensorGraphManager : BaseManager
    {
        public DeviceSensorGraphManager()
        {

        }

        public string GetDeviceSensorGraphList()
        {
            InsertEventLog("GetDeviceSensorGraphList",  EventType.Log, EventColor.yellow, "Enter ", "TICRM.BuisnessLayer.DeviceSensorGraphManager.GetDeviceSensorGraphList", "");
            try
            {
                // query in database to get desired result
                var query = (from dsg in dbEnt.DeviceSensorGraphs
                             join d in dbEnt.Devices on dsg.DeviceId equals d.DeviceId
                             join g in dbEnt.Graphs on dsg.GraphId equals g.GraphId
                             join s in dbEnt.Sensors on dsg.SensorId equals s.SensorId
                             where dsg.IsDeleted == false
                             select new { dsg.DeviceSensorGraphId, DeviceName = d.Name, g.GraphName, s.SensorName }).ToList();
                string status = Newtonsoft.Json.JsonConvert.SerializeObject(query); // convert query in json Sting
                InsertEventLog("GetDeviceSensorGraphList", EventType.Log, EventColor.yellow, "Ready to response Json Status ", "TICRM.BuisnessLayer.DeviceSensorGraphManager.GetDeviceSensorGraphList", "");
                return status; // return in json
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetDeviceSensorGraphList", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DeviceSensorGraphManager.GetDeviceSensorGraphList", "");
                throw;
            }
        }



        public string GetDSGListOn_DeviceId(Guid deviceId)
        {
            InsertEventLog("GetDeviceSensorGraphList", EventType.Log, EventColor.yellow, "Enter ", "TICRM.BuisnessLayer.DeviceSensorGraphManager.GetDSGListOn_DeviceId", "");
            try
            {
                // query in database to get desired result
                var query = (from dsg in dbEnt.DeviceSensorGraphs
                             join d in dbEnt.Devices on dsg.DeviceId equals d.DeviceId
                             join g in dbEnt.Graphs on dsg.GraphId equals g.GraphId
                             join s in dbEnt.Sensors on dsg.SensorId equals s.SensorId
                             where dsg.IsDeleted == false && dsg.DeviceId == deviceId
                             select new { dsg.DeviceSensorGraphId, DeviceName = d.Name, g.GraphName, s.SensorName }).ToList();
                string status = Newtonsoft.Json.JsonConvert.SerializeObject(query); // convert query in json Sting
                InsertEventLog("GetDeviceSensorGraphList", EventType.Log, EventColor.yellow, "Ready to response Json Status ", "TICRM.BuisnessLayer.DeviceSensorGraphManager.GetDSGListOn_DeviceId", "");
                return status; // return in json
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetDeviceSensorGraphList", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DeviceSensorGraphManager.GetDSGListOn_DeviceId", "");
                throw;
            }
        }



        public List<SensorDto> GetSensorList()
        {
            try
            {
                InsertEventLog("GetSensorList", EventType.Log, EventColor.yellow, "to get list of sensor dto ", "TICRM.BuisnessLayer.DeviceSensorGraphManager.GetSensorList", "");
                List<SensorDto> SensorDtos = new List<SensorDto>(); // create a new object of SensorDtos list
                List<Sensor> Sensors = dbEnt.Sensors.ToList(); // create a new object of Sensors
                // apply iteration on sensor list
                foreach (Sensor item in Sensors)
                {
                    SensorDtos.Add(objMapper.GetSensorDto(item));
                }
                return SensorDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetSensorList", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DeviceSensorGraphManager.GetSensorList", "");
                throw;
            }
        }



        public bool SubmitDeviceSensorGraph(Guid DeviceId, Guid SensorId, Guid GraphId, string CurrentUserId)
        {
            try
            {
                InsertEventLog("SubmitDeviceSensorGraph",  EventType.Log, EventColor.yellow, "create new record of device sensor graph ", "TICRM.BuisnessLayer.DeviceSensorGraphManager.SubmitDeviceSensorGraph", "");
                DeviceSensorGraph dsg = new DeviceSensorGraph(); // create a new Object DeviceSensorGraph
                dsg.DeviceSensorGraphId = Guid.NewGuid(); // add new guid in object
                dsg.DeviceId = DeviceId;
                dsg.SensorId = SensorId;
                dsg.GraphId = GraphId;
                dsg.CreatedOn = DateTime.Now;
                dsg.CreatedBy = CurrentUserId;
                dsg.IsDeleted = false;
                dbEnt.DeviceSensorGraphs.Add(dsg);
                dbEnt.SaveChanges(); // save in databse
                return true;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("SubmitDeviceSensorGraph", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DeviceSensorGraphManager.SubmitDeviceSensorGraph", "");
                throw;
            }


        }


        public bool UpdateDeviceSensorGraph(Guid DeviceSensorGraphId, Guid DeviceId, Guid SensorId, Guid GraphId, string CurrentUserId)
        {
            try
            {
                InsertEventLog("UpdateDeviceSensorGraph",  EventType.Log, EventColor.yellow, "Enter ", "TICRM.BuisnessLayer.DeviceSensorGraphManager.UpdateDeviceSensorGraph", "");
                DeviceSensorGraph dsg = dbEnt.DeviceSensorGraphs.FirstOrDefault(x => x.DeviceSensorGraphId == DeviceSensorGraphId); // query in database
                if (dsg != null)
                {
                    dsg.DeviceId = DeviceId;
                    dsg.SensorId = SensorId;
                    dsg.GraphId = GraphId;
                    dsg.UpdatedOn = DateTime.Now;
                    dsg.UpdatedBy = CurrentUserId;
                    if (dbEnt.SaveChanges() > 0)
                    {
                        InsertEventLog("UpdateDeviceSensorGraph",  EventType.Log, EventColor.yellow, "update successfully ", "TICRM.BuisnessLayer.DeviceSensorGraphManager.UpdateDeviceSensorGraph", "");
                        return true;
                    }
                }

                return false;  //there is no changes is done in db against the object
            }
            catch (Exception ex)

            {
                InsertEventMonitor("UpdateDeviceSensorGraph", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DeviceSensorGraphManager.UpdateDeviceSensorGraph", "");
                throw;
            }


        }


        public string DeviceSensorGraphOfMAC(string MacAddress)
        {
            try
            {
                InsertEventLog("DeviceSensorGraphOfMAC",  EventType.Log, EventColor.yellow, "Get Device sensor graph on macaddres ", "TICRM.BuisnessLayer.DeviceSensorGraphManager.DeviceSensorGraphOfMAC", "");
                // apply query in database to get desired result
                var query = (from dsg in dbEnt.DeviceSensorGraphs
                             join d in dbEnt.Devices on dsg.DeviceId equals d.DeviceId
                             join g in dbEnt.Graphs on dsg.GraphId equals g.GraphId
                             join s in dbEnt.Sensors on dsg.SensorId equals s.SensorId
                             where d.Mac == MacAddress && dsg.IsDeleted == false
                             select new { dsg.DeviceId, d.Mac, DeviceName = d.Name, d.Latitude, d.Longitude, g.GraphName, s.SensorName }).ToList();

                string status = Newtonsoft.Json.JsonConvert.SerializeObject(query);
                InsertEventLog("DeviceSensorGraphOfMAC", EventType.Log, EventColor.yellow, "return data in json ", "TICRM.BuisnessLayer.DeviceSensorGraphManager.DeviceSensorGraphOfMAC", "");
                return status;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("DeviceSensorGraphOfMAC", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DeviceSensorGraphManager.DeviceSensorGraphOfMAC", "");
                throw;
            }
        }


        public DeviceSensorGraphDto GetDeviceSensorGraphOnId(Guid DeviceSensorGraphId)
        {
            try
            {
                InsertEventLog("GetDeviceSensorGraphOnId", EventType.Log, EventColor.yellow, "get device sensor graph on id=" + DeviceSensorGraphId + " ", "TICRM.BuisnessLayer.DeviceSensorGraphManager.GetDeviceSensorGraphOnId", "");
                // firstly Get data of device sensor graph on id and then place in DTO class
                return objMapper.GetDeviceSensorGraphDto(dbEnt.DeviceSensorGraphs.FirstOrDefault(x => x.DeviceSensorGraphId == DeviceSensorGraphId));
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetDeviceSensorGraphOnId", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DeviceSensorGraphManager.GetDeviceSensorGraphOnId", "");
                throw;
            }
        }

        public bool DeleteDeviceSensorGraph(Guid DeviceSensorGraphId)
        {
            try
            {
                InsertEventLog("DeleteDeviceSensorGraph",  EventType.Log, EventColor.yellow, "Enter", "TICRM.BuisnessLayer.DeviceSensorGraphManager.DeleteDeviceSensorGraph", "");
                DeviceSensorGraph dsg = dbEnt.DeviceSensorGraphs.FirstOrDefault(x => x.DeviceSensorGraphId == DeviceSensorGraphId); // get device sensor graph for delete
                if (dsg != null)
                {
                    InsertEventLog("DeleteDeviceSensorGraph", EventType.Log, EventColor.yellow, "delete deveice sensor graph on id is not null ", "TICRM.BuisnessLayer.DeviceSensorGraphManager.DeleteDeviceSensorGraph", "");
                    dsg.IsDeleted = true;
                    if (dbEnt.SaveChanges() > 0)
                    {
                        InsertEventLog("DeleteDeviceSensorGraph", EventType.Log, EventColor.yellow, "Successfully deleted deveice sensor graph on id ", "TICRM.BuisnessLayer.DeviceSensorGraphManager.DeleteDeviceSensorGraph", "");
                        return true;
                    }
                }
                return false; // there is no changes in devicesensorgraph 
            }
            catch (Exception ex)
            {
                InsertEventMonitor("DeleteDeviceSensorGraph", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DeviceSensorGraphManager.DeleteDeviceSensorGraph", "");
                throw;
            }
        }


    }
}
