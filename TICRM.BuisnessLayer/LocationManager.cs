using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.BuisnessLayer
{
    public class LocationManager : BaseManager
    {
        public LocationManager()
        {
            LocationTypes = GetLocationTypes();
        }

        public List<LocationTypeDto> LocationTypes { get; set; }
        /// <summary>
        /// Get Location Types
        /// </summary>
        /// <returns></returns>
        public List<LocationTypeDto> GetLocationTypes()
        {
            try
            {
                InsertEventLog("GetLocationTypes", EventType.Log, EventColor.yellow, "Get List Of LocationTypeDto", "TICRM.BusinessLayer.LocationManager.GetLocationTypes", "");
                List<LocationTypeDto> urgencyDtos = new List<LocationTypeDto>();

                foreach (LocationType item in dbEnt.LocationTypes)
                {
                    urgencyDtos.Add(objMapper.GetLocationTypeDTO(item));
                }
                return urgencyDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("LocationTypes", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.LocationManager.GetLocationTypes", "");

                throw;
            }
        }





        public LocationDto GetLocation(Guid? guid)
        {
            try
            {
                InsertEventLog("GetLocation", EventType.Log, EventColor.yellow, "Get LocationDto", "TICRM.BusinessLayer.LocationManager.GetLocation", "");
                return objMapper.GetLocationDTO(dbEnt.Locations.Find(guid));
            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetLocation", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.LocationManager.GetLocation", "");
                throw ex;
            }


        }
        /// <summary>
        /// Get location list 
        /// </summary>
        /// <returns></returns>
        public List<LocationDto> GetLocations()
        {
            try
            {
                InsertEventLog("GetLocations", EventType.Log, EventColor.yellow, "Get list of LocationDto", "TICRM.BusinessLayer.LocationManager.GetLocations", "");
                List<LocationDto> locationDtos = new List<LocationDto>();
                List<Location> locations = dbEnt.Locations.Include(l => l.Address).Include(l => l.LocationType).Include(l => l.Status).Include(l => l.Team).Include(l => l.User).Where(a => a.IsDeleted != true).ToList();



                foreach (Location item in locations)
                {
                    locationDtos.Add(objMapper.GetLocationDTO(item));
                }
                return locationDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetLocations", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.LocationManager.GetLocations", "");

                throw;
            }


        }

        //

        public List<LocationDto> GetLocations(Guid accountId)
        {
            try
            {
                InsertEventLog("GetLocations", EventType.Log, EventColor.yellow, "Get list of LocationDto on accouont Id=" + accountId + "", "TICRM.BusinessLayer.LocationManager.GetLocations", "");
                List<LocationDto> locationDtos = new List<LocationDto>();
                List<Location> locations = dbEnt.Locations.Include(l => l.Address).Include(l => l.LocationType).Include(l => l.Status).Include(l => l.Team).Include(l => l.User).Where(a => a.IsDeleted != true && a.AccountId == accountId).ToList();

                foreach (Location item in locations)
                {
                    locationDtos.Add(objMapper.GetLocationDTO(item));
                }
                return locationDtos;
            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetLocations", EventType.Exception, EventType.Exception, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.LocationManager.GetLocations", "");
                throw;
            }


        }

        /// <summary>
        /// save and edit LocationDto 
        /// </summary>
        /// <param name="loca"></param>
        /// <returns></returns>
        public bool SaveLocation(LocationDto loca, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SaveLocation", EventType.Log, EventColor.yellow, "Enter", "TICRM.BusinessLayer.LocationManager.SaveLocation", "");
                Location location;
                if (isEditMode)
                {
                    InsertEventLog("SaveLocation", EventType.Log, EventColor.yellow, "Enter in Edit Mode", "TICRM.BusinessLayer.LocationManager.SaveLocation", "");

                    location = objMapper.GetLocation(loca);
                    if (isDeleteMode)
                    {
                        InsertEventLog("SaveLocation", EventType.Log, EventColor.yellow, "Enter in delete mode", "TICRM.BusinessLayer.LocationManager.SaveLocation", "");
                        Location locationforDelete = dbEnt.Locations.FirstOrDefault(x => x.LocationId == location.LocationId);
                        locationforDelete.IsDeleted = true;
                    }
                    else
                    {
                        dbEnt.Entry(location).State = EntityState.Modified;
                    }
                }
                else
                {
                    InsertEventLog("SaveLocation", EventType.Log, EventColor.yellow, "Create a new Record", "TICRM.BusinessLayer.LocationManager.SaveLocation", "");
                    location = objMapper.GetLocation(loca);
                    location.LocationId = Guid.NewGuid();
                    dbEnt.Locations.Add(location);
                }

                dbEnt.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("SaveLocation", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.LocationManager.SaveLocation", "");
                throw ex;
            }


        }

    }
}
