using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using TICRM.DAL;
using TICRM.DTOs;
using TICRM.Mapper;

namespace TICRM.BuisnessLayer.Base
{
    public class BaseManager 
    {


        protected CRMEntities dbEnt = new CRMEntities();
        protected TIMapper objMapper = new TIMapper();

        #region Properties
        public List<StatusDto> Status { get; set; }
        public List<TeamDto> Teams { get; set; }
        public List<UserDto> Users { get; set; }
        public List<AddressDto> Addresses { get; set; }

        public List<AccountDto> Accounts { get; set; }

        #endregion

        public BaseManager()
        {
            Status = GetStatuses();
            Teams = GetTeams();
            Users = GetUsers();
            Addresses = GetAddresses();
            Accounts = GetAccounts();
        }

        #region Properties's methods

        /// <summary>
        /// Get All Statuses Domain Transfer Objects
        /// </summary>
        /// <returns></returns>
        public List<StatusDto> GetStatuses()
        {
            try
            {
                var statusDtos = new List<StatusDto>();

                foreach (var item in dbEnt.Status)
                {
                    statusDtos.Add(objMapper.GetStatusDTO(item));
                }
                return statusDtos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
       
        /// <summary>
        /// Get Team Dto
        /// </summary>
        /// <returns></returns>
        public List<TeamDto> GetTeams()
        {
            try
            {
                var teamDtos = new List<TeamDto>();

                foreach (var item in dbEnt.Teams)
                {
                    teamDtos.Add(objMapper.GetTeamDTO(item));
                }
                return teamDtos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Get Users Dtos
        /// </summary>
        /// <returns></returns>
        public List<UserDto> GetUsers()
        {
            try
            {
                var userDtos = new List<UserDto>();

                foreach (var item in dbEnt.Users)
                {
                    userDtos.Add(objMapper.GetUserDTO(item));
                }
                return userDtos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// get addresses Dtos
        /// </summary>
        /// <returns></returns>
        public List<AddressDto> GetAddresses()
        {
            try
            {
                var addressDtos = new List<AddressDto>();

                foreach (var item in dbEnt.Addresses)
                {
                    addressDtos.Add(objMapper.GetAddressDTO(item));
                }
                return addressDtos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<AccountDto> GetAccounts()
        {
            try
            {
                var accountDtos = new List<AccountDto>();

                foreach (var item in dbEnt.Accounts)
                {
                    accountDtos.Add(objMapper.GetAccountDTO(item));
                }
                return accountDtos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public List<CustomerAssetDto> CustomerAssetsOnAccountId(Guid accountId)
        {
            try
            {
                var customerAssetsDTO = new List<CustomerAssetDto>();
                List<CustomerAsset> customerAssets = dbEnt.CustomerAssets.Where(x => x.AccountId == accountId).ToList();
                foreach (CustomerAsset item in customerAssets)
                {
                    customerAssetsDTO.Add(objMapper.GetCustomerAssetDTO(item));
                }
                return customerAssetsDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CustomerAssetDto> GetAllCustomerAssets()
        {
            try
            {
                var customerAssetsDTO = new List<CustomerAssetDto>();
                List<CustomerAsset> customerAssets = dbEnt.CustomerAssets.ToList();
                foreach (CustomerAsset item in customerAssets)
                {
                    customerAssetsDTO.Add(objMapper.GetCustomerAssetDTO(item));
                }
                return customerAssetsDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        #endregion



        #region Event Monitor and Event Log
        public void InsertEventMonitor(string Name, string Type, string color, string Message, string Path, string CurrentUserId)
        {
            try
            {
                EventMonitor eventMonitor = new EventMonitor();
                eventMonitor.EventMonitorId = Guid.NewGuid();
                eventMonitor.Name = Name;
                eventMonitor.Type = Type;
                eventMonitor.Path = Path;
                eventMonitor.Message = Message;
                eventMonitor.Status = true;
                eventMonitor.Color = color;
                eventMonitor.CreatedDate = DateTime.Now;
                eventMonitor.CreatedBy = CurrentUserId;
                dbEnt.EventMonitors.Add(eventMonitor);


                dbEnt.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void InsertEventLog(string Name,string Type, string color, string Message,string Path, string CurrentUserId)
        {
            try
            {
                EventLog eventLog = new EventLog();
                eventLog.EventLogId = Guid.NewGuid();
                eventLog.Name = Name;
                eventLog.Type = Type;
                eventLog.Path = Path;
                eventLog.Message = Message;
                eventLog.Status = true;
                eventLog.Color = color;
                eventLog.CreatedDate = DateTime.Now;
                eventLog.CreatedBy = CurrentUserId;
                dbEnt.EventLogs.Add(eventLog);
                dbEnt.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void InsertEventNotification(string Name,string Type, string color, string Message,string Path,string CurrentUserId)
        {
            try
            {
                EventNotification eventNotification = new EventNotification();
                eventNotification.EventNotificationId = Guid.NewGuid();
                eventNotification.Name = Name;
                eventNotification.Type = Type;
                eventNotification.Message = Message;
                eventNotification.Status = true;
                eventNotification.Color = color;
                eventNotification.CreatedDate = DateTime.Now;
                eventNotification.CreatedBy = CurrentUserId;
                dbEnt.EventNotifications.Add(eventNotification);
                dbEnt.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        #endregion


        //public void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        dbEnt.Dispose();
        //    }
        //    //base.Dispose(disposing);
        //}
    }
}
