using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using TICRM.BuisnessLayer.Base;
using TICRM.DTOs;
using TICRM.DAL;

namespace TICRM.BuisnessLayer
{
    public class AlertManager : BaseManager
    {
        public AlertManager()
        {
            Urgencies = GetUrgencies();
        }

        public List<UrgencyDto> Urgencies { get; set; }

        /// <summary>
        /// Get Urgencies Domain Transfer Objects
        /// </summary>
        /// <returns></returns>
        public List<UrgencyDto> GetUrgencies()
        {
            try
            {
                InsertEventLog("GetUrgencies", EventType.Log, EventColor.yellow, "Get activity Template on id", "TICRM.BuisnessLayer.AlertManager.GetUrgencies", "");
                var urgencyDtos = new List<UrgencyDto>();

                foreach (var item in dbEnt.Urgencies)
                {
                    urgencyDtos.Add(objMapper.GetUrgencyDTO(item));
                }
                return urgencyDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetUrgencies", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.AlertManager.GetUrgencies", "");
                throw;
            }
        }




        public AlertDto GetAlert(Guid? guid)
        {
            try
            {
                InsertEventLog("GetAlert", EventType.Log, EventColor.yellow, "Get Alert on id=" + guid + " ", "TICRM.BuisnessLayer.AlertManager.GetAlert", "");
                return objMapper.GetAlertDTO(dbEnt.Alerts.Find(guid));
            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetAlert", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.AlertManager.GetAlert", "");
                throw ex;
            }


        }
        /// <summary>
        /// Get alert list 
        /// </summary>
        /// <returns></returns>
        public List<AlertDto> GetAlerts()
        {
            try
            {
                InsertEventLog("GetAlerts", EventType.Log, EventColor.yellow, "Get Alert dto list", "TICRM.BuisnessLayer.AlertManager.GetAlerts", "");
                var alertDtos = new List<AlertDto>();
                var alerts = dbEnt.Alerts.Include(a => a.Status).Include(a => a.Team).Include(a => a.Urgency).Include(a => a.User).Where(a => a.IsDeleted != true).ToList();
                foreach (var item in alerts)
                {
                    alertDtos.Add(objMapper.GetAlertDTO(item));
                }
                return alertDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetAlerts", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.AlertManager.GetAlerts", "");

                throw;
            }


        }
        /// <summary>
        /// save and edit AlertDto 
        /// </summary>
        /// <param name="alr"></param>
        /// <returns></returns>
        public bool SaveAlert(AlertDto alr, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SaveAlert",  EventType.Log, EventColor.yellow, "Enter", "TICRM.BuisnessLayer.AlertManager.SaveAlert", "");

                Alert alert;
                if (isEditMode)
                {
                    InsertEventLog("SaveAlert",  EventType.Log, EventColor.yellow, "Entrer In Edit Mode", "TICRM.BuisnessLayer.AlertManager.SaveAlert", "");
                    alert = objMapper.GetAlert(alr);
                    if (isDeleteMode)
                    {
                        InsertEventLog("SaveAlert",  EventType.Log, EventColor.yellow, "Entrer In Delete Mode", "TICRM.BuisnessLayer.AlertManager.SaveAlert", "");
                        alert.IsDeleted = true;
                    }
                    dbEnt.Entry(alert).State = EntityState.Modified;
                }
                else
                {
                    InsertEventLog("SaveAlert",  EventType.Log, EventColor.yellow, "Enter In Create New Record Alert","TICRM.BuisnessLayer.AlertManager.SaveAlert", "");
                    alert = objMapper.GetAlert(alr);
                    alert.AlertId = Guid.NewGuid();
                    dbEnt.Alerts.Add(alert);
                }

                dbEnt.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("SaveAlert", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.AlertManager.SaveAlert", "");
                throw ex;
            }


        }

    }
}
