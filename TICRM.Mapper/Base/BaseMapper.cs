using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.Mapper.Base
{
    /// <summary>
    /// Base Class having Common Domain Transfer objects 
    /// </summary>
    public class BaseMapper
    {
        protected CRMEntities dbEnt = new CRMEntities();

        /// <summary>
        /// Status DTO
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public StatusDto GetStatusDTO(Status e)
        {
            try
            {
                if (e == null) return null;

                var d = new StatusDto();
                d.StatusId = e.StatusId;
                d.Name = e.Name;
                d.Type = e.Type;

                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Status GetStatus(StatusDto e)
        {
            try
            {
                if (e == null) return null;

                var d = new Status();
                d.StatusId = e.StatusId;
                d.Name = e.Name;
                d.Type = e.Type;

                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Get Team Dto 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public TeamDto GetTeamDTO(Team e)
        {
            try
            {
                if (e == null) return null;
                var d = new TeamDto();
                d.TeamId = e.TeamId;
                d.Name = e.Name;
                d.Description = e.Description;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;

                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Team GetTeam(TeamDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new Team();
                d.TeamId = e.TeamId;
                d.Name = e.Name;
                d.Description = e.Description;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;

                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Get User DTO
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public UserDto GetUserDTO(User e)
        {
            try
            {
                if (e == null) return null;
                var d = new UserDto();
                d.UserId = e.UserId;
                d.Name = e.Name;
                d.Email = e.Email;
                d.Phone = e.Phone;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;

                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public User GetUser(UserDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new User();
                d.UserId = e.UserId;
                d.Name = e.Name;
                d.Email = e.Email;
                d.Phone = e.Phone;
                d.StatusId = e.StatusId;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;

                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        #region WorkFlow
        public WorkFlowDTO TakeWorkFlowDTO(WorkFlow e)
        {
            try
            {
                if (e == null) return null;
                var d = new WorkFlowDTO();
                d.WorkFlowId = e.WorkFlowId;
                d.Name = e.Name;
                d.TriggerCondition = e.TriggerCondition;
                d.TriggerIn = e.TriggerIn;
                d.TriggerOut = e.TriggerOut;
                d.TargetOn = e.TargetOn;
                d.Description = e.Description;
                d.WorkFlowStatus = e.WorkFlowStatus;
                d.Priority = e.Priority;
                d.AppliedTo = e.AppliedTo;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;

                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public WorkFlow TakeWorkFlows(WorkFlowDTO e)
        {
            try
            {
                if (e == null) return null;
                var d = new WorkFlow();
                d.WorkFlowId = e.WorkFlowId;
                d.Name = e.Name;
                d.TriggerCondition = e.TriggerCondition;
                d.TriggerIn = e.TriggerIn;
                d.TriggerOut = e.TriggerOut;
                d.TargetOn = e.TargetOn;
                d.Description = e.Description;
                d.WorkFlowStatus = e.WorkFlowStatus;
                d.AppliedTo = e.AppliedTo;
                d.Priority = e.Priority;
                d.CreatedBy = e.CreatedBy;
                d.CreatedDate = e.CreatedDate;
                d.UpdatedBy = e.UpdatedBy;
                d.UpdatedDate = e.UpdatedDate;

                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #endregion







        /// <summary>
        /// Get account size dto
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public AccountSizeDto GetAccountSizeDTO(AccountSize e)
        {
            try
            {
                if (e == null) return null;
                var d = new AccountSizeDto();
                d.AccountSizeId = e.AccountSizeId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AccountSize GetAccountSize(AccountSizeDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new AccountSize();
                d.AccountSizeId = e.AccountSizeId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// get account type dto 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public AccountTypeDto GetAccountTypeDTO(AccountType e)
        {
            try
            {
                if (e == null) return null;
                var d = new AccountTypeDto();
                d.AccountTypeId = e.AccountTypeId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AccountType GetAccountType(AccountTypeDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new AccountType();
                d.AccountTypeId = e.AccountTypeId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Get address dto
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public AddressDto GetAddressDTO(Address e)
        {
            try
            {
                if (e == null) return null;
                var d = new AddressDto();
                d.AddressId = e.AddressId;
                d.Street1 = e.Street1;
                d.Street2 = e.Street2;
                d.City = e.City;
                d.State = d.State;
                d.Zip = e.Zip;
                d.Country = e.Country;
                d.IsDeleted = e.IsDeleted;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Address GetAddress(AddressDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new Address();
                d.AddressId = e.AddressId;
                d.Street1 = e.Street1;
                d.Street2 = e.Street2;
                d.City = e.City;
                d.State = d.State;
                d.Zip = e.Zip;
                d.Country = e.Country;
                d.IsDeleted = e.IsDeleted;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Get portability 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public PobabilityDto GetPobabilityDTO(Pobability e)
        {
            try
            {
                if (e == null) return null;
                var d = new PobabilityDto();
                d.ProbabilityId = e.ProbabilityId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Pobability GetPobability(PobabilityDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new Pobability();
                d.ProbabilityId = e.ProbabilityId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Get opportunity stage
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public OpportunityStageDto GetOpportunityStageDTO(OpportunityStage e)
        {
            try
            {
                if (e == null) return null;
                var d = new OpportunityStageDto();
                d.OpportunityStageId = e.OpportunityStageId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public OpportunityStage GetOpportunityStage(OpportunityStageDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new OpportunityStage();
                d.OpportunityStageId = e.OpportunityStageId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Opportunity stage Dto
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public CurrencyDto GetCurrencyDTO(Currency e)
        {
            try
            {
                if (e == null) return null;
                var d = new CurrencyDto();
                d.CurrencyId = e.CurrencyId;
                d.Name = e.Name;
                d.Value = e.Value;
                d.Sign = e.Sign;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Currency GetCurrency(CurrencyDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new Currency();
                d.CurrencyId = e.CurrencyId;
                d.Name = e.Name;
                d.Value = e.Value;
                d.Sign = e.Sign;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        ///  //IndustryDto
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public IndustryDto GetIndustryDTO(Industry e)
        {
            try
            {
                if (e == null) return null;
                var d = new IndustryDto();
                d.IndustryId = e.IndustryId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Industry GetIndustry(IndustryDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new Industry();
                d.IndustryId = e.IndustryId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        ///    //LeadSourceDto
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public LeadSourceDto GetLeadSourceDTO(LeadSource e)
        {
            try
            {
                if (e == null) return null;
                var d = new LeadSourceDto();
                d.LeadSourceId = e.LeadSourceId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public LeadSource GetLeadSource(LeadSourceDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new LeadSource();
                d.LeadSourceId = e.LeadSourceId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        ///  //LeadTypeDto
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public LeadTypeDto GetLeadTypeDTO(LeadType e)
        {
            try
            {
                if (e == null) return null;
                var d = new LeadTypeDto();
                d.LeadTypeId = e.LeadTypeId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public LeadType GetLeadType(LeadTypeDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new LeadType();
                d.LeadTypeId = e.LeadTypeId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CustomerAssetTypeDto GetCustomerAssetTypeDTO(CustomerAssetType e)
        {
            try
            {
                if (e == null) return null;
                var d = new CustomerAssetTypeDto();
                d.CustomerAssetTypeId = e.CustomerAssetTypeId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CustomerAssetType GetCustomerAssetType(CustomerAssetTypeDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new CustomerAssetType();
                d.CustomerAssetTypeId = e.CustomerAssetTypeId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Urgency Dto
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public UrgencyDto GetUrgencyDTO(Urgency e)
        {
            try
            {
                if (e == null) return null;
                var d = new UrgencyDto();
                d.UrgencyId = e.UrgencyId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Urgency GetUrgency(UrgencyDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new Urgency();
                d.UrgencyId = e.UrgencyId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Location Type Dto
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public LocationTypeDto GetLocationTypeDTO(LocationType e)
        {
            try
            {
                if (e == null) return null;
                var d = new LocationTypeDto();
                d.LocationTypeId = e.LocationTypeId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public LocationType GetLocationType(LocationTypeDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new LocationType();
                d.LocationTypeId = e.LocationTypeId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Reading Type Dto
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public ReadingTypeDto GetReadingTypeDTO(ReadingType e)
        {
            try
            {
                if (e == null) return null;
                var d = new ReadingTypeDto();
                d.ReadingTypeId = e.ReadingTypeId;
                d.Name = e.Name;
                d.IsDeleted = e.IsDeleted;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ReadingType GetReadingType(ReadingTypeDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new ReadingType();
                d.ReadingTypeId = e.ReadingTypeId;
                d.Name = e.Name;
                d.IsDeleted = e.IsDeleted;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Reading Unit Dto
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public ReadingUnitDto GetReadingUnitDTO(ReadingUnit e)
        {
            try
            {
                if (e == null) return null;
                var d = new ReadingUnitDto();
                d.ReadingUnitId = e.ReadingUnitId;
                d.Name = e.Name;
                d.IsDeleted = e.IsDeleted;
                d.Type = e.Type;
                d.ReadingType = GetReadingTypeDTO(e.ReadingType);
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ReadingUnit GetReadingUnit(ReadingUnitDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new ReadingUnit();
                d.ReadingUnitId = e.ReadingUnitId;
                d.Name = e.Name;
                d.IsDeleted = e.IsDeleted;
                d.Type = e.Type;
                d.ReadingType = GetReadingType(e.ReadingType);
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        ///  get work stage DTO
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public WorkStageDto GetWorkStageDTO(WorkStage e)
        {
            try
            {
                if (e == null) return null;
                var d = new WorkStageDto();
                d.WorkStageId = e.WorkStageId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public WorkStage GetWorkStage(WorkStageDto e)
        {
            try
            {
                if (e == null) return null;
                var d = new WorkStage();
                d.WorkStageId = e.WorkStageId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        ///  //GetSkillLevelDTO
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public SkillLevelDto GetSkillLevelDTO(SkillLevel e)
        {
            try
            {
                if (e == null) return null;
                var d = new SkillLevelDto();
                d.SkillLevelId = e.SkillLevelId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// //GetSkillDTO
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public SkillDto GetSkillDTO(Skill e)
        {
            try
            {
                if (e == null) return null;
                var d = new SkillDto();
                d.SkillId = e.SkillId;
                d.Name = e.Name;
                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        public string GetAccountOnId(Guid? guid,string relatedTo)
        {
            try
            {
                if (relatedTo == "Account")
                {
                    //if (guid == null)
                    //{
                    //    return "";
                    //}
                    return dbEnt.Accounts.FirstOrDefault(x => x.AccountId == guid) == null ? "" : dbEnt.Accounts.FirstOrDefault(x => x.AccountId == guid).Name;
                }

                return "";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }








    }
}
