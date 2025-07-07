using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.BuisnessLayer
{
    public class ReadingManager : BaseManager
    {
        public ReadingManager()
        {
            ReadingTypes = GetReadingTypes();
            ReadingUnits = GetReadingUnits();
        }

        public List<ReadingTypeDto> ReadingTypes { get; set; }
        public List<ReadingUnitDto> ReadingUnits { get; set; }

        /// <summary>
        /// Get Reading Types
        /// </summary>
        /// <returns></returns>
        public List<ReadingTypeDto> GetReadingTypes()
        {
            try
            {
                InsertEventLog("GetReadingTypes", EventType.Log, EventColor.yellow, "Get list of ReadingTypeDto", "TICRM.BusinessLayer.ReadingManager.GetReadingTypes", "");
                List<ReadingTypeDto> readingTypeDtos = new List<ReadingTypeDto>();

                foreach (ReadingType item in dbEnt.ReadingTypes)
                {
                    readingTypeDtos.Add(objMapper.GetReadingTypeDTO(item));
                }
                return readingTypeDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetReadingTypes", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ReadingManager.GetReadingTypes", "");
                throw;
            }
        }

        public List<ReadingUnitDto> GetReadingUnits()
        {
            try
            {
                InsertEventLog("GetReadingUnits", EventType.Log, EventColor.yellow, "Get list of GetReadingUnits", "TICRM.BusinessLayer.ReadingManager.GetReadingUnits", "");
                List<ReadingUnitDto> readingUnitDtos = new List<ReadingUnitDto>();

                foreach (ReadingUnit item in dbEnt.ReadingUnits)
                {
                    readingUnitDtos.Add(objMapper.GetReadingUnitDTO(item));
                }
                return readingUnitDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetReadingUnits", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ReadingManager.GetReadingUnits", "");

                throw;
            }
        }




        public ReadingDto GetReading(Guid? guid)
        {
            try
            {
                InsertEventLog("GetReading", EventType.Log, EventColor.yellow, "going to Get ReadingDto object", "TICRM.BusinessLayer.ReadingManager.GetReading", "");
                return objMapper.GetReadingDTO(dbEnt.Readings.Find(guid));
            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetReading", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ReadingManager.GetReading", "");
                throw ex;
            }


        }
        /// <summary>
        /// Get reading list 
        /// </summary>
        /// <returns></returns>
        public List<ReadingDto> GetReadings()
        {
            try
            {
                InsertEventLog("GetReadings", EventType.Log, EventColor.yellow, "going to Get ReadingDto list", "TICRM.BusinessLayer.ReadingManager.GetReadings", "");
                List<ReadingDto> readingDtos = new List<ReadingDto>();
                List<Reading> readings = dbEnt.Readings.Include(r => r.ReadingType).Include(r => r.ReadingUnit).Include(r => r.Status).Include(r => r.Team).Include(r => r.User).Where(a => a.IsDeleted != true).ToList();
                foreach (Reading item in readings)
                {
                    readingDtos.Add(objMapper.GetReadingDTO(item));
                }
                return readingDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetReadings", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ReadingManager.GetReadings", "");

                throw;
            }


        }


        /// <summary>
        /// save and edit ReadingDto 
        /// </summary>
        /// <param name="readng"></param>
        /// <returns></returns>
        public bool SaveReading(ReadingDto readng, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SaveReading", EventType.Log, EventColor.yellow, "Enter", "TICRM.BusinessLayer.ReadingManager.SaveReading", "");
                Reading reading;
                if (isEditMode)
                {

                    InsertEventLog("SaveReading", EventType.Log, EventColor.yellow, "going to edit Reading on id=" + readng.ReadingId + "", "TICRM.BusinessLayer.ReadingManager.SaveReading", "");
                    reading = objMapper.GetReading(readng);
                    if (isDeleteMode)
                    {
                        InsertEventLog("SaveReading", EventType.Log, EventColor.yellow, "going to Delete Reading on id=" + readng.ReadingId + "", "TICRM.BusinessLayer.ReadingManager.SaveReading", "");
                        Reading readingForDelete = dbEnt.Readings.FirstOrDefault(x => x.ReadingId == reading.ReadingId);
                        readingForDelete.IsDeleted = true;
                    }
                    else
                    {
                        dbEnt.Entry(reading).State = EntityState.Modified;
                    }
                }
                else
                {
                    InsertEventLog("SaveReading", EventType.Log, EventColor.yellow, "Create New Record Reading", "TICRM.BusinessLayer.ReadingManager.SaveReading", "");
                    reading = objMapper.GetReading(readng);
                    reading.ReadingId = Guid.NewGuid();
                    dbEnt.Readings.Add(reading);
                }

                dbEnt.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                InsertEventMonitor("SaveReading", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ReadingManager.SaveReading", "");
                throw ex;
            }


        }


    }
}
