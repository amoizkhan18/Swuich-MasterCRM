using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TICRM.DAL;
using TICRM.DTOs;
using TICRM.Mapper.Base;

namespace TICRM.Mapper
{
    public class TIMapper : BaseMapper
    {
        /// <summary>
        /// Account Domain transfer object
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public AccountDto GetAccountDTO(Account e)
        {
            try
            {
                if (e == null) return null;
                var d = new AccountDto();

                d.AccountId = e.AccountId;
                d.Name = e.Name;
                d.ShippingAddress = e.ShippingAddress;
                d.BillingAddress = e.BillingAddress;
                d.AccountTypeId = e.AccountTypeId;
                d.PhoneOffice = e.PhoneOffice;
                d.Email = e.Email;
                d.Fax = e.Fax;
                d.WebSite = e.WebSite;
                d.AccountSizeId = e.AccountSizeId;
                d.IndustryId = e.IndustryId;
                d.Description = e.Description;
                d.AccountSize = GetAccountSizeDTO(e.AccountSize);
                d.AccountType = GetAccountTypeDTO(e.AccountType);
                d.Address = GetAddressDTO(e.Address);
                d.Address = GetAddressDTO(e.Address1);

                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);

                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Account GetAccount(AccountDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new Account();
                d.AccountId = e.AccountId;
                d.Name = e.Name;
                d.ShippingAddress = e.ShippingAddress;
                d.BillingAddress = e.BillingAddress;
                d.AccountTypeId = e.AccountTypeId;
                d.PhoneOffice = e.PhoneOffice;
                d.Email = e.Email;
                d.Fax = e.Fax;
                d.WebSite = e.WebSite;
                d.AccountSizeId = e.AccountSizeId;
                d.IndustryId = e.IndustryId;
                d.Description = e.Description;
                d.AccountSize = GetAccountSize(e.AccountSize);
                d.AccountType = GetAccountType(e.AccountType);
                d.Address = GetAddress(e.Address);
                d.Address = GetAddress(e.Address1);

                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatus(e.Status);
                d.Team = GetTeam(e.Team);
                d.User = GetUser(e.User);

                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Get opportunity Dtos
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public OpportunityDto GetOpportunityDTO(Opportunity e)
        {
            try
            {
                if (e == null) return null;
                var d = new OpportunityDto();

                d.OpportunityId = e.OpportunityId;
                d.Amount = e.Amount;
                d.ProbabilityId = e.ProbabilityId;
                d.OpportunityStageId = e.OpportunityStageId;
                d.Title = e.Title;
                d.CurrencyId = e.CurrencyId;
                d.Description = e.Description;
                d.Currency = GetCurrencyDTO(e.Currency);
                d.OpportunityStage = GetOpportunityStageDTO(e.OpportunityStage);
                d.Pobability = GetPobabilityDTO(e.Pobability);
                d.AccountId = e.AccountId;
                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);

                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Opportunity GetOpportunity(OpportunityDto dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new Opportunity();

                en.OpportunityId = dt.OpportunityId;
                en.Amount = dt.Amount;
                en.ProbabilityId = dt.ProbabilityId;
                en.OpportunityStageId = dt.OpportunityStageId;
                en.Title = dt.Title;
                en.CurrencyId = dt.CurrencyId;
                en.Description = dt.Description;
                en.Currency = GetCurrency(dt.Currency);
                en.OpportunityStage = GetOpportunityStage(dt.OpportunityStage);
                en.Pobability = GetPobability(dt.Pobability);
                en.AccountId = dt.AccountId;
                en.IsDeleted = dt.IsDeleted;
                en.StatusId = dt.StatusId;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                en.AssignedTeam = dt.AssignedTeam;
                en.AssignedUser = dt.AssignedUser;
                en.Status = GetStatus(dt.Status);
                en.Team = GetTeam(dt.Team);
                en.User = GetUser(dt.User);

                return en;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        /// <summary>
        /// Get Lead Dtos 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public LeadDto GetLeadDTO(Lead e)
        {
            try
            {
                if (e == null) return null;
                var d = new LeadDto();


                d.LeadId = e.LeadId;
                d.Name = e.Name;
                d.LeadTypeId = e.LeadTypeId;
                d.LeadSourceId = e.LeadSourceId;
                d.PhoneNumber = e.PhoneNumber;
                d.Email = e.Email;
                d.AddressId = e.AddressId;
                d.IndustryId = e.IndustryId;
                d.Description = e.Description;
                d.Address = GetAddressDTO(e.Address);
                d.Industry = GetIndustryDTO(e.Industry);
                d.LeadSource = GetLeadSourceDTO(e.LeadSource);
                d.LeadType = GetLeadTypeDTO(e.LeadType);

                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);

                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Lead GetLead(LeadDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new Lead();


                d.LeadId = e.LeadId;
                d.Name = e.Name;
                d.LeadTypeId = e.LeadTypeId;
                d.LeadSourceId = e.LeadSourceId;
                d.PhoneNumber = e.PhoneNumber;
                d.Email = e.Email;
                d.AddressId = e.AddressId;
                d.IndustryId = e.IndustryId;
                d.Description = e.Description;
                d.Address = GetAddress(e.Address);
                d.Industry = GetIndustry(e.Industry);
                d.LeadSource = GetLeadSource(e.LeadSource);
                d.LeadType = GetLeadType(e.LeadType);

                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatus(e.Status);
                d.Team = GetTeam(e.Team);
                d.User = GetUser(e.User);

                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Add by zaman khan contact module
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public ContactDto GetContactDto(Contact e)
        {
            try
            {
                if (e == null)
                    return null;
                var d = new ContactDto();
                d.ContactId = e.Id;
                d.Name = e.Name;
                d.Phone = e.Phone;
                d.Email = e.Email;
                d.Address = e.Address;
                d.AccountId = e.AccountId;
                d.Account = GetAccountDTO(e.Account);


                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);
                return d;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public Contact GetContact(ContactDto e)
        {
            try
            {
                if (e == null)
                    return null;
                var d = new Contact();
                d.Id  = e.ContactId;
                d.Name = e.Name;
                d.Phone = e.Phone;
                d.Email = e.Email;
                d.Address = e.Address;
                d.AccountId = e.AccountId;
                d.Account = GetAccount(e.Account);

                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatus(e.Status);
                d.Team = GetTeam(e.Team);
                d.User = GetUser(e.User);

                return d;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        /// <summary>
        /// Get customer assets Dto
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public CustomerAssetDto GetCustomerAssetDTO(CustomerAsset e)
        {
            try
            {
                if (e == null) return null;
                var d = new CustomerAssetDto();

                d.CustomerAssetId = e.CustomerAssetId;
                d.Title = e.Title;
                d.CustomerAssetTypeId = e.CustomerAssetTypeId;
                d.Manufacture = e.Manufacture;
                d.Model = e.Model;
                d.YearOfManufacture = e.YearOfManufacture;
                d.Value = e.Value;
                d.DepriciatedValue = e.DepriciatedValue;
                d.SKU = e.SKU;
                d.Description = e.Description;
                d.CustomerAssetType = GetCustomerAssetTypeDTO(e.CustomerAssetType);
                d.AccountId = e.AccountId;
                d.LocationId = e.LocationId;
                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);

                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public CustomerAsset GetCustomerAsset(CustomerAssetDto dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new CustomerAsset();

                en.CustomerAssetId = dt.CustomerAssetId;
                en.Title = dt.Title;
                en.CustomerAssetTypeId = dt.CustomerAssetTypeId;
                en.Manufacture = dt.Manufacture;
                en.Model = dt.Model;
                en.YearOfManufacture = dt.YearOfManufacture;
                en.Value = dt.Value;
                en.DepriciatedValue = dt.DepriciatedValue;
                en.SKU = dt.SKU;
                en.LocationId = dt.LocationId;
                en.Description = dt.Description;
                en.CustomerAssetType = GetCustomerAssetType(dt.CustomerAssetType);
                en.AccountId = dt.AccountId;
                en.IsDeleted = dt.IsDeleted;
                en.StatusId = dt.StatusId;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                en.AssignedTeam = dt.AssignedTeam;
                en.AssignedUser = dt.AssignedUser;
                en.Status = GetStatus(dt.Status);
                en.Team = GetTeam(dt.Team);
                en.User = GetUser(dt.User);

                return en;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Get Alert Domain Transfer Object 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public AlertDto GetAlertDTO(Alert e)
        {
            try
            {
                if (e == null) return null;
                var d = new AlertDto();

                d.AlertId = e.AlertId;
                d.UrgencyId = e.UrgencyId;
                d.Urgency = GetUrgencyDTO(e.Urgency);
                d.Title = e.Title;
                d.Description = e.Description;

                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);

                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Alert GetAlert(AlertDto dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new Alert();

                en.AlertId = dt.AlertId;
                en.UrgencyId = dt.UrgencyId;
                en.Urgency = GetUrgency(dt.Urgency);
                en.Title = dt.Title;
                en.Description = dt.Description;

                en.IsDeleted = dt.IsDeleted;
                en.StatusId = dt.StatusId;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                en.AssignedTeam = dt.AssignedTeam;
                en.AssignedUser = dt.AssignedUser;
                en.Status = GetStatus(dt.Status);
                en.Team = GetTeam(dt.Team);
                en.User = GetUser(dt.User);

                return en;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Get Location Doamin Transfer Object
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public LocationDto GetLocationDTO(Location e)
        {
            try
            {
                if (e == null) return null;
                var d = new LocationDto();

                d.LocationId = e.LocationId;
                d.Name = e.Name;
                d.AddressId = e.AddressId;
                d.LocationTypeId = e.LocationTypeId;
                d.Description = e.Description;
                d.Latitude = e.Latitude;
                d.Longitude = e.Longitude;
                d.LocationType = GetLocationTypeDTO(e.LocationType);
                d.Address = GetAddressDTO(e.Address);
                d.AccountId = e.AccountId;
                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);

                return d;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Location GetLocation(LocationDto dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new Location();

                en.LocationId = dt.LocationId;
                en.Name = dt.Name;
                en.AddressId = dt.AddressId;
                en.LocationTypeId = dt.LocationTypeId;
                en.Description = dt.Description;
                en.Latitude = dt.Latitude;
                en.Longitude = dt.Longitude;
                en.LocationType = GetLocationType(dt.LocationType);
                en.Address = GetAddress(dt.Address);
                en.AccountId = dt.AccountId;
                en.IsDeleted = dt.IsDeleted;
                en.StatusId = dt.StatusId;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                en.AssignedTeam = dt.AssignedTeam;
                en.AssignedUser = dt.AssignedUser;
                en.Status = GetStatus(dt.Status);
                en.Team = GetTeam(dt.Team);
                en.User = GetUser(dt.User);

                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Reading DTO
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public ReadingDto GetReadingDTO(Reading e)
        {
            try
            {
                if (e == null) return null;
                var d = new ReadingDto();

                d.ReadingId = e.ReadingId;
                d.Value = e.Value;
                d.ReadingTypeId = e.ReadingTypeId;
                d.ReadingUnitId = e.ReadingUnitId;
                d.MarginOfErrorInPercent = e.MarginOfErrorInPercent;
                d.Description = e.Description;

                d.ReadingType = GetReadingTypeDTO(e.ReadingType);
                d.ReadingUnit = GetReadingUnitDTO(e.ReadingUnit);


                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);

                return d;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Reading GetReading(ReadingDto dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new Reading();

                en.ReadingId = dt.ReadingId;
                en.Value = dt.Value;
                en.ReadingTypeId = dt.ReadingTypeId;
                en.ReadingUnitId = dt.ReadingUnitId;
                en.MarginOfErrorInPercent = dt.MarginOfErrorInPercent;
                en.Description = dt.Description;

                en.ReadingType = GetReadingType(dt.ReadingType);
                en.ReadingUnit = GetReadingUnit(dt.ReadingUnit);


                en.IsDeleted = dt.IsDeleted;
                en.StatusId = dt.StatusId;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                en.AssignedTeam = dt.AssignedTeam;
                en.AssignedUser = dt.AssignedUser;
                en.Status = GetStatus(dt.Status);
                en.Team = GetTeam(dt.Team);
                en.User = GetUser(dt.User);

                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get service call DTO
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public ServiceCallDto GetServiceCallDTO(ServiceCall e)
        {
            try
            {
                if (e == null) return null;
                var d = new ServiceCallDto();

                d.ServiceCallId = e.ServiceCallId;
                d.Title = e.Title;
                d.Detail = e.Detail;
                d.UrgencyId = e.UrgencyId;
                d.ServiceCallStageId = e.ServiceCallStageId;
                d.Description = e.Description;
                d.Urgency = GetUrgencyDTO(e.Urgency);
                d.WorkStage = GetWorkStageDTO(e.WorkStage);
                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);

                return d;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ServiceCall GetServiceCall(ServiceCallDto dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new ServiceCall();

                en.ServiceCallId = dt.ServiceCallId;
                en.Title = dt.Title;
                en.Detail = dt.Detail;
                en.UrgencyId = dt.UrgencyId;
                en.ServiceCallStageId = dt.ServiceCallStageId;
                en.Description = dt.Description;
                en.Urgency = GetUrgency(dt.Urgency);
                en.WorkStage = GetWorkStage(dt.WorkStage);
                en.IsDeleted = dt.IsDeleted;
                en.StatusId = dt.StatusId;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                en.AssignedTeam = dt.AssignedTeam;
                en.AssignedUser = dt.AssignedUser;
                en.Status = GetStatus(dt.Status);
                en.Team = GetTeam(dt.Team);
                en.User = GetUser(dt.User);

                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Resource DTO
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public ResourceDto GetResourceDTO(Resource e)
        {
            try
            {
                if (e == null) return null;
                var d = new ResourceDto();

                d.ResourceId = e.ResourceId;
                d.Name = e.Name;
                d.Address = e.Address;
                d.CurrentAddress = e.CurrentAddress;
                d.PhoneHome = e.PhoneHome;
                d.Email = e.Email;
                d.Website = e.Website;
                d.PhoneOffice = e.PhoneOffice;
                d.Description = e.Description;
                d.Address1 = GetAddressDTO(e.Address1);
                d.Address2 = GetAddressDTO(e.Address2);

                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);

                return d;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Resource GetResource(ResourceDto dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new Resource();

                en.ResourceId = dt.ResourceId;
                en.Name = dt.Name;
                en.Address = dt.Address;
                en.CurrentAddress = dt.CurrentAddress;
                en.PhoneHome = dt.PhoneHome;
                en.Email = dt.Email;
                en.Website = dt.Website;
                en.PhoneOffice = dt.PhoneOffice;
                en.Description = dt.Description;
                en.Address1 = GetAddress(dt.Address1);
                en.Address2 = GetAddress(dt.Address2);

                en.IsDeleted = dt.IsDeleted;
                en.StatusId = dt.StatusId;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                en.AssignedTeam = dt.AssignedTeam;
                en.AssignedUser = dt.AssignedUser;
                en.Status = GetStatus(dt.Status);
                en.Team = GetTeam(dt.Team);
                en.User = GetUser(dt.User);

                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Resource a
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public ResourceAvailabilityDto GetResourceAvailabilityDTO(ResourceAvailability e)
        {
            try
            {
                if (e == null) return null;
                var d = new ResourceAvailabilityDto();

                d.ResourceAvailabilityId = e.ResourceAvailabilityId;
                d.Date = e.Date;
                d.Hour = e.Hour;
                d.StartTime = e.StartTime;
                d.EndTime = e.EndTime;
                d.Description = e.Description;
                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);

                return d;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// resource skill 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public ResourceSkillDto GetResourceSkillDTO(ResourceSkill e)
        {
            try
            {
                if (e == null) return null;
                var d = new ResourceSkillDto();

                d.ResourceSkillId = e.ResourceSkillId;
                d.SkillId = e.SkillId;
                d.SkillLevelId = e.SkillLevelId;
                d.Description = e.Description;
                d.SkillLevel = GetSkillLevelDTO(e.SkillLevel);
                d.Skill = GetSkillDTO(e.Skill);

                d.Description = e.Description;
                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);

                return d;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get categories dto
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public CategoryDto GetCategoryDTO(Category e)
        {
            try
            {
                if (e == null) return null;
                var d = new CategoryDto();

                d.CategoryId = e.CategoryId;
                d.Name = e.Name;
                d.Description = e.Description;
                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);

                return d;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public Category GetDtoToCategory(CategoryDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new Category();

                d.CategoryId = e.CategoryId;
                d.Name = e.Name;
                d.Description = e.Description;
                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;

                return d;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }










        /// <summary>
        /// get ProductCatelog dto
        /// </summary>
        /// <param name="obj">it is a db object that convert it into DTO</param>
        /// <returns></returns>
        public ProductCatelogDTO GetProductCatelogDTO(ProductCatelog obj)
        {
            try
            {
                if (obj == null) return null;
                var data = new ProductCatelogDTO();

                data.ProductId = obj.ProductId;
                data.SerialNumber = obj.SerialNumber;
                data.ProductName = obj.ProductName;
                data.CategoryId = obj.CategoryId;
                data.ValidFrom = obj.ValidFrom;
                data.ValidTo = obj.ValidTo;
                data.ProductNote = obj.ProductNote;
                data.Description = obj.Description;
                data.StatusId = obj.StatusId;
                data.CreatedBy = obj.CreatedBy;
                data.CreatedDate = obj.CreatedDate;
                data.UpdatedBy = obj.UpdatedBy;
                data.UpdatedDate = obj.UpdatedDate;
                data.AssignedTeam = obj.AssignedTeam;
                data.AssignedUser = obj.AssignedUser;
                data.Status = GetStatusDTO(obj.Status);
                data.Team = GetTeamDTO(obj.Team);
                data.User = GetUserDTO(obj.User);

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get ProductCatelog
        /// </summary>
        /// <param name="obj">it is a dto object that convert it into db Object</param>
        /// <returns></returns>
        public ProductCatelog GetProductCatelog(ProductCatelogDTO obj)
        {
            try
            {
                if (obj == null) return null;
                var data = new ProductCatelog();

                data.ProductId = obj.ProductId;
                data.SerialNumber = obj.SerialNumber;
                data.ProductName = obj.ProductName;
                data.CategoryId = obj.CategoryId;
                data.ValidFrom = obj.ValidFrom;
                data.ValidTo = obj.ValidTo;
                data.ProductNote = obj.ProductNote;
                data.Description = obj.Description;
                data.StatusId = obj.StatusId;
                data.CreatedBy = obj.CreatedBy;
                data.CreatedDate = obj.CreatedDate;
                data.UpdatedBy = obj.UpdatedBy;
                data.UpdatedDate = obj.UpdatedDate;
                data.AssignedTeam = obj.AssignedTeam;
                data.AssignedUser = obj.AssignedUser;

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// get ProductPriceList dto
        /// </summary>
        /// <param name="obj">it is a db object that convert it into DTO</param>
        /// <returns></returns>
        public ProductPriceListDTO ProductPriceListDTO(ProductPriceList obj)
        {
            try
            {
                if (obj == null) return null;
                var data = new ProductPriceListDTO();

                data.ProductPriceId = obj.ProductPriceId;
                data.CurrencyId = obj.CurrencyId;
                data.Amount = obj.Amount;
                data.ProductId = obj.ProductId;
                data.Description = obj.Description;
                data.StatusId = obj.StatusId;
                data.CreatedBy = obj.CreatedBy;
                data.CreatedDate = obj.CreatedDate;
                data.UpdatedBy = obj.UpdatedBy;
                data.UpdatedDate = obj.UpdatedDate;
                data.ProductCatelog = GetProductCatelogDTO(obj.ProductCatelog);
                data.Currency = GetCurrencyDTO(obj.Currency);
                data.Status = GetStatusDTO(obj.Status);

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get ProductCatelog
        /// </summary>
        /// <param name="obj">it is a dto object that convert it into db Object</param>
        /// <returns></returns>
        public ProductPriceList GetProductPriceList(ProductPriceListDTO obj)
        {
            try
            {
                if (obj == null) return null;
                var data = new ProductPriceList();

                data.ProductPriceId = obj.ProductPriceId;
                data.CurrencyId = obj.CurrencyId;
                data.Amount = obj.Amount;
                data.ProductId = obj.ProductId;
                data.Description = obj.Description;
                data.StatusId = obj.StatusId;
                data.CreatedBy = obj.CreatedBy;
                data.CreatedDate = obj.CreatedDate;
                data.UpdatedBy = obj.UpdatedBy;
                data.UpdatedDate = obj.UpdatedDate;

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /// <summary>
        /// get Discount dto
        /// </summary>
        /// <param name="obj">it is a db object that convert it into DTO</param>
        /// <returns></returns>
        public DiscountDTO GetDiscountDTO(Discount obj)
        {
            try
            {
                if (obj == null) return null;
                var data = new DiscountDTO();

                data.DiscountId = obj.DiscountId;
                data.Name = obj.Name;
                data.MinQuantity = obj.MinQuantity;
                data.MaxQuantity = obj.MaxQuantity;
                data.ProductPriceId = obj.ProductPriceId;
                data.DiscountAmount = obj.DiscountAmount;
                data.DiscountPercentage = obj.DiscountPercentage;
                data.Description = obj.Description;
                data.StatusId = obj.StatusId;
                data.CreatedBy = obj.CreatedBy;
                data.CreatedDate = obj.CreatedDate;
                data.UpdatedBy = obj.UpdatedBy;
                data.UpdatedDate = obj.UpdatedDate;

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Discount
        /// </summary>
        /// <param name="obj">it is a dto object that convert it into db Object</param>
        /// <returns></returns>
        public Discount GetDiscount(DiscountDTO obj)
        {
            try
            {
                if (obj == null) return null;
                var data = new Discount();

                data.DiscountId = obj.DiscountId;
                data.Name = obj.Name;
                data.MinQuantity = obj.MinQuantity;
                data.MaxQuantity = obj.MaxQuantity;
                data.ProductPriceId = obj.ProductPriceId;
                data.DiscountAmount = obj.DiscountAmount;
                data.DiscountPercentage = obj.DiscountPercentage;
                data.Description = obj.Description;
                data.StatusId = obj.StatusId;
                data.CreatedBy = obj.CreatedBy;
                data.CreatedDate = obj.CreatedDate;
                data.UpdatedBy = obj.UpdatedBy;
                data.UpdatedDate = obj.UpdatedDate;

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        /// <summary>
        /// Get work order 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public WorkOrderDto GetWorkOrderDTO(WorkOrder e)
        {
            try
            {
                if (e == null) return null;
                var d = new WorkOrderDto();

                d.WorkOrderId = e.WorkOrderId;
                d.Title = e.Title;
                d.NTE = e.NTE;
                d.WorkOrderStageId = e.WorkOrderStageId;
                d.Description = e.Description;
                d.WorkStage = GetWorkStageDTO(e.WorkStage);

                d.Description = e.Description;
                d.IsDeleted = e.IsDeleted;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);

                return d;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public WorkOrder GetWorkOrder(WorkOrderDto dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new WorkOrder();

                en.WorkOrderId = dt.WorkOrderId;
                en.Title = dt.Title;
                en.NTE = dt.NTE;
                en.WorkOrderStageId = dt.WorkOrderStageId;
                en.Description = dt.Description;
                en.WorkStage = GetWorkStage(dt.WorkStage);

                en.Description = dt.Description;
                en.IsDeleted = dt.IsDeleted;
                en.StatusId = dt.StatusId;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                en.AssignedTeam = dt.AssignedTeam;
                en.AssignedUser = dt.AssignedUser;
                en.Status = GetStatus(dt.Status);
                en.Team = GetTeam(dt.Team);
                en.User = GetUser(dt.User);

                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get converted Dto from Entity
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public DeviceDto GetDeviceDTO(Device e)
        {
            try
            {
                if (e == null) return null;
                var d = new DeviceDto();

                d.DeviceId = e.DeviceId;
                d.Name = e.Name;
                d.Mac = e.Mac;
                d.EMEINumber = e.EMEINumber;
                d.RegistrationDate = e.RegistrationDate;
                d.ServiceDate = e.ServiceDate;
                d.Latitude = e.Latitude;
                d.Longitude = e.Longitude;
                d.IsDeleted = e.IsDeleted;
                d.AccountId = e.AccountId;
                d.CustomerAssetId = e.CustomerAssetId;
                d.StatusId = e.StatusId;
                d.Maintenance = e.Maintenance;
                d.CloudServices = e.CloudServices;
                d.CloudData = e.CloudData;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);

                return d;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Device GetDevice(DeviceDto dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new Device();

                en.DeviceId = dt.DeviceId;
                en.Name = dt.Name;
                en.Mac = dt.Mac;
                en.EMEINumber = dt.EMEINumber;
                en.RegistrationDate = dt.RegistrationDate;
                en.ServiceDate = dt.ServiceDate;
                en.Latitude = dt.Latitude;
                en.Longitude = dt.Longitude;
                en.IsDeleted = dt.IsDeleted;
                en.AccountId = dt.AccountId;
                en.CustomerAssetId = dt.CustomerAssetId;
                en.StatusId = dt.StatusId;
                en.Maintenance = dt.Maintenance;
                en.CloudServices = dt.CloudServices;
                en.CloudData = dt.CloudData;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                en.AssignedTeam = dt.AssignedTeam;
                en.AssignedUser = dt.AssignedUser;
                en.Status = GetStatus(dt.Status);
                en.Team = GetTeam(dt.Team);
                en.User = GetUser(dt.User);

                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // Get: GetGraphDto
        public GraphDto GetGraphDto(Graph graph)
        {
            try
            {
                if (graph == null) return null;
                var g = new GraphDto();
                g.GraphId = graph.GraphId;
                g.GraphName = graph.GraphName;
                return g;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        // Get: GetSensorDto
        public SensorDto GetSensorDto(Sensor sensor)
        {
            try
            {
                if (sensor == null) return null;
                var s = new SensorDto();
                s.SensorId = sensor.SensorId;
                s.SensorName = sensor.SensorName;
                return s;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        // Get: GetDeviceSensorDto
        public DeviceSensorDto GetDeviceSensorDto(DeviceSensor ds)
        {
            try
            {
                if (ds == null) return null;
                var en = new DeviceSensorDto();

                en.DeviceId = ds.DeviceId;
                en.SensorName = ds.SensorName;
                en.DeviceSensorId = ds.DeviceSensorId;
                en.SensorType = ds.SensorType;
                en.StatusId = ds.StatusId;
                en.CreatedBy = ds.CreatedBy;
                en.CreatedDate = ds.CreatedDate;
                en.UpdatedBy = ds.UpdatedBy;
                en.UpdatedDate = ds.UpdatedDate;

                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        // Get: GetDeviceSensorGraphDto
        public DeviceSensorGraphDto GetDeviceSensorGraphDto(DeviceSensorGraph sensor)
        {
            try
            {
                if (sensor == null) return null;
                var s = new DeviceSensorGraphDto();
                s.DeviceSensorGraphId = sensor.DeviceSensorGraphId;
                s.DeviceId = sensor.DeviceId;
                s.GraphId = sensor.GraphId;
                s.CreatedOn = sensor.CreatedOn;
                s.CreatedBy = sensor.CreatedBy;
                s.SensorId = sensor.SensorId;
                return s;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public GlobalSearchDto GetGlobalSearchDto(GlobalSearch search)
        {
            try
            {
                if (search == null) return null;
                var s = new GlobalSearchDto();
                s.GlobalSearchId = search.GlobalSearchId;
                s.Name = search.Name;
                s.URL = search.URL;
                s.Type = search.Type;
                return s;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #region Activity

        public ActivityDTO GetActivityDTO(Activity e)
        {
            try
            {
                if (e == null) return null; // if e is null then return it back null

                var d = new ActivityDTO(); // create activity Object

                d.ActivityId = e.ActivityId;
                d.Name = e.Name;
                d.Description = e.Description;
                d.Type = e.Type;
                d.RelatedTo = e.RelatedTo;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.RelatedToID = e.RelatedToID;
                d.RelatedToName = GetAccountOnId(e.RelatedToID,e.RelatedTo);
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);

                return d; // reurn object
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Activity GetActivity(ActivityDTO dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new Activity();

                en.ActivityId = dt.ActivityId;
                en.Name = dt.Name;
                en.Description = dt.Description;
                en.Type = dt.Type;
                en.RelatedTo = dt.RelatedTo;
                en.RelatedToID = dt.RelatedToID;
                en.StatusId = dt.StatusId;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                en.AssignedTeam = dt.AssignedTeam;
                en.AssignedUser = dt.AssignedUser;
                en.Status = GetStatus(dt.Status);
                en.Team = GetTeam(dt.Team);
                en.User = GetUser(dt.User);

                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ActivityTemplate

        public ActivityTemplateDTO GetActivityTemplateDTO(ActivityTemplate e)
        {
            try
            {
                if (e == null) return null; // if e is null then return it back null

                var d = new ActivityTemplateDTO(); // create activity Object

                d.ActivityTemplateId = e.ActivityTemplateId;
                d.ActivityTemplateType = e.ActivityTemplateType;
                d.PropertyName = e.PropertyName;
                d.PropertyType = e.PropertyType;
                d.PropertyValue = e.PropertyValue;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;

                return d; // reurn object
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActivityTemplate GetActivityTemplate(ActivityTemplateDTO dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new ActivityTemplate();

                en.ActivityTemplateId = dt.ActivityTemplateId;
                en.ActivityTemplateType = dt.ActivityTemplateType;
                en.PropertyName = dt.PropertyName;
                en.PropertyType = dt.PropertyType;
                en.PropertyValue = dt.PropertyValue;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;

                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region Calendar

        public CalendarEventDTO GetCalendarEventDTO(Activity e)
        {
            try
            {
                if (e == null) return null; // if e is null then return it back null
                var d = new CalendarEventDTO(); // create activity Object
                d.Summary = e.Name;
                d.Description = e.Description;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Status = GetStatusDTO(e.Status);
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);
                return d; // reurn object
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region WorkFlow

        public WorkFlowDTO GetWorkFlowDTO(WorkFlow e)
        {
            try
            {
                if (e == null) return null; // if e is null then return it back null

                var d = new WorkFlowDTO(); // create workflow Object

                d.WorkFlowId = e.WorkFlowId;
                d.Name = e.Name;
                d.TriggerCondition = e.TriggerCondition;
                d.TriggerIn = e.TriggerIn;
                d.TriggerOut = e.TriggerOut;
                d.TargetOn = e.TargetOn;
                d.Frequency = e.Frequency;
                d.FrequencyOut = e.FrequencyOut;
                d.Description = e.Description;
                d.WorkFlowStatus = e.WorkFlowStatus;
                d.AppliedTo = e.AppliedTo;
                d.Priority = e.Priority;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.AssignedTeam = e.AssignedTeam;
                d.AssignedUser = e.AssignedUser;
                d.Team = GetTeamDTO(e.Team);
                d.User = GetUserDTO(e.User);
                d.WorkFlowDesign = e.WorkFlowDesign;
                d.Action = e.Action;
                return d; // reurn object
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WorkFlow GetWorkFlow(WorkFlowDTO dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new WorkFlow();

                en.WorkFlowId = dt.WorkFlowId;
                en.Name = dt.Name;
                en.TriggerCondition = dt.TriggerCondition;
                en.TriggerIn = dt.TriggerIn;
                en.TriggerOut = dt.TriggerOut;
                en.TargetOn = dt.TargetOn;
                en.Description = dt.Description;
                en.WorkFlowStatus = dt.WorkFlowStatus;
                en.WorkFlowStatus = dt.WorkFlowStatus;
                en.AppliedTo = dt.AppliedTo;
                en.Frequency = dt.Frequency;
                en.FrequencyOut = dt.FrequencyOut;
                en.Priority = dt.Priority;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                en.AssignedTeam = dt.AssignedTeam;
                en.AssignedUser = dt.AssignedUser;
                en.Team = GetTeam(dt.Team);
                en.User = GetUser(dt.User);
                en.WorkFlowDesign = dt.WorkFlowDesign;
                en.Action = dt.Action;
                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region WorkFlowReport

        

        public WorkFlowReportDTO GetWorkFlowReportDTO(WorkFlowReport e)
        {
            try
            {
                if (e == null) return null; // if e is null then return it back null

                var d = new WorkFlowReportDTO(); // create workflow Object

                d.WorkFlowReportId = e.WorkFlowReportId;
                d.WorkFlowId = e.WorkFlowId;
                d.WorkFlowStatus = e.WorkFlowStatus;
                d.Action = e.Action;
                d.WorkFlowActionStatus = e.WorkFlowActionStatus;
                d.WorkFlowDesign = e.WorkFlowDesign;
                d.AppliedTo = e.AppliedTo;
                d.Frequency = e.Frequency;
                d.Priority = e.Priority;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.IsDeleted = e.IsDeleted;
                d.WorkFlow = TakeWorkFlowDTO(e.WorkFlow);
                return d; // reurn object
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WorkFlowReport GetWorkFlowReport(WorkFlowReportDTO dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new WorkFlowReport();
                en.WorkFlowReportId = dt.WorkFlowReportId;
                en.WorkFlowId = dt.WorkFlowId;
                en.WorkFlowStatus = dt.WorkFlowStatus;
                en.WorkFlowDesign = dt.WorkFlowDesign;
                en.WorkFlowActionStatus = dt.WorkFlowActionStatus;
                en.Action = dt.Action;
                en.AppliedTo = dt.AppliedTo;
                en.Frequency = dt.Frequency;
                en.Priority = dt.Priority;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                en.IsDeleted = dt.IsDeleted;
                //en.WorkFlow = TakeWorkFlows(dt.WorkFlow);
                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region WorkFlowMapping



        public WorkFlowMappingDTO GetWorkFlowMappingDTO(WorkFlowMapping e)
        {
            try
            {
                if (e == null) return null; // if e is null then return it back null

                var d = new WorkFlowMappingDTO(); // create WorkFlowMappingDTO Object

                d.WorkFlowMappingId = e.WorkFlowMappingId;
                d.WorkFlowId = e.WorkFlowId;
                d.Action = e.Action;
                d.SourceType = e.SourceType;
                d.SourceColumn = e.SourceColumn;
                d.SourceValue = e.SourceValue;
                d.SourceData = e.SourceData;
                d.IsDone = e.IsDone;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.WorkFlow = TakeWorkFlowDTO(e.WorkFlow);
                return d; // reurn object
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WorkFlowMapping GetWorkFlowMapping(WorkFlowMappingDTO dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new WorkFlowMapping();
                en.WorkFlowMappingId = dt.WorkFlowMappingId;
                en.WorkFlowId = dt.WorkFlowId;
                en.Action = dt.Action;
                en.SourceType = dt.SourceType;
                en.SourceColumn = dt.SourceColumn;
                en.SourceValue = dt.SourceValue;
                en.SourceData = dt.SourceData;
                en.IsDone = dt.IsDone;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



        #region EventMonitor

        public EventMonitorDTO GetEventMonitorDTO(EventMonitor e)
        {
            try
            {
                if (e == null) return null; // if e is null then return it back null

                var d = new EventMonitorDTO(); // create new EventMonitorDTO Object
                d.EventMonitorId = e.EventMonitorId;
                d.Name = e.Name;
                d.Type = e.Type;
                d.Status = e.Status;
                d.Message = e.Message;
                d.Color = e.Color;
                d.IPAddress = e.IPAddress;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                return d; // reurn object
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EventMonitor GetEventMonitor(EventMonitorDTO dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new EventMonitor();
                en.EventMonitorId = dt.EventMonitorId;
                en.Name = dt.Name;
                en.Type = dt.Type;
                en.Status = dt.Status;
                en.Message = dt.Message;
                en.Color = dt.Color;
                en.IPAddress = dt.IPAddress;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region EventLog

        public EventLogDTO GetEventLogDTO(EventLog e)
        {
            try
            {
                if (e == null) return null; // if e is null then return it back null

                var d = new EventLogDTO(); // create new EventLogDTO Object
                d.EventLogId = e.EventLogId;
                d.Name = e.Name;
                d.Type = e.Type;
                d.Status = e.Status;
                d.Message = e.Message;
                d.Color = e.Color;
                d.IPAddress = e.IPAddress;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                return d; // reurn object
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EventLog GetEventLog(EventLogDTO dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new EventLog();
                en.EventLogId = dt.EventLogId;
                en.Name = dt.Name;
                en.Type = dt.Type;
                en.Status = dt.Status;
                en.Message = dt.Message;
                en.Color = dt.Color;
                en.IPAddress = dt.IPAddress;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



        #region EventNotification

        public EventNotificationDTO GetEventNotificationDTO(EventNotification e)
        {
            try
            {
                if (e == null) return null; // if e is null then return it back null

                var d = new EventNotificationDTO(); // create new EventNotificationDTO Object
                d.EventNotificationId = e.EventNotificationId;
                d.Name = e.Name;
                d.Type = e.Type;
                d.Status = e.Status;
                d.Message = e.Message;
                d.Color = e.Color;
                d.IPAddress = e.IPAddress;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                return d; // reurn object
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EventNotification GetEventNotification(EventNotificationDTO dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new EventNotification();
                en.EventNotificationId = dt.EventNotificationId;
                en.Name = dt.Name;
                en.Type = dt.Type;
                en.Status = dt.Status;
                en.Message = dt.Message;
                en.Color = dt.Color;
                en.IPAddress = dt.IPAddress;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region EmailConfiguration

        public EmailConfigurationDTO GetEmailConfigurationDTO(EmailConfiguration e)
        {
            try
            {
                if (e == null) return null; // if e is null then return it back null

                var d = new EmailConfigurationDTO(); // create new EventNotificationDTO Object
                d.EmailConfigurationId = e.EmailConfigurationId;
                d.Email = e.Email;
                d.UserName = e.UserName;
                d.Password = e.Password;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                return d; // reurn object
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmailConfiguration GetEmailConfiguration(EmailConfigurationDTO dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new EmailConfiguration();
                en.EmailConfigurationId = dt.EmailConfigurationId;
                en.Email = dt.Email;
                en.UserName = dt.UserName;
                en.Password = dt.Password;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



        #region EmailTemplate

        public EmailTemplateDTO GetEmailTemplateDTO(EmailTemplate e)
        {
            try
            {
                if (e == null) return null; // if e is null then return it back null

                var d = new EmailTemplateDTO(); // create new EmailTemplateDTO Object
                d.EmailTemplateId = e.EmailTemplateId;
                d.EmailConfigurationId = e.EmailConfigurationId;
                d.WorkFlowId = e.WorkFlowId;
                d.Subject = e.Subject;
                d.Body = e.Body;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;
                d.WorkFlow = TakeWorkFlowDTO(e.WorkFlow);
                d.EmailConfiguration = GetEmailConfigurationDTO(e.EmailConfiguration);
                return d; // reurn object
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmailTemplate GetEmailTemplate(EmailTemplateDTO dt)
        {
            try
            {
                if (dt == null) return null;
                var en = new EmailTemplate();
                en.EmailTemplateId = dt.EmailTemplateId;
                en.EmailConfigurationId = dt.EmailConfigurationId;
                en.WorkFlowId = dt.WorkFlowId;
                en.Subject = dt.Subject;
                en.Body = dt.Body;
                en.CreatedBy = dt.CreatedBy;
                en.CreatedDate = dt.CreatedDate;
                en.UpdatedBy = dt.UpdatedBy;
                en.UpdatedDate = dt.UpdatedDate;
                return en;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion




    }




}
