using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.BuisnessLayer
{
    public class OpportunityManager : BaseManager
    {
        public OpportunityManager()
        {
            Currencies = GetCurrencies();
            OpportunityStages = GetOpportunityStages();
            Probabilities = GetProbabilities();
        }


        #region Properties 

        public List<CurrencyDto> Currencies { get; set; }
        public List<OpportunityStageDto> OpportunityStages { get; set; }
        public List<PobabilityDto> Probabilities { get; set; }

        #endregion

        /// <summary>
        /// Get Currencies 
        /// </summary>
        /// <returns></returns>
        public List<CurrencyDto> GetCurrencies()
        {
            try
            {
                InsertEventLog("GetCurrencies", EventType.Log, EventColor.yellow, "Get List Of CurrencyDto", "TICRM.BusinessLayer.OpportunityManager.GetCurrencies", "");
                List<CurrencyDto> currenciesDtos = new List<CurrencyDto>();

                foreach (Currency item in dbEnt.Currencies)
                {
                    currenciesDtos.Add(objMapper.GetCurrencyDTO(item));
                }
                return currenciesDtos;
            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetCurrencies", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.OpportunityManager.GetCurrencies", "");
                throw;
            }
        }

        /// <summary>
        /// Opportunity Stages Dto
        /// </summary>
        /// <returns></returns>
        public List<OpportunityStageDto> GetOpportunityStages()
        {
            try
            {
                InsertEventLog("GetOpportunityStages", EventType.Log, EventColor.yellow, "Get List Of OpportunityStageDto", "TICRM.BusinessLayer.OpportunityManager.GetOpportunityStages", "");
                List<OpportunityStageDto> opportunityStageDtos = new List<OpportunityStageDto>();

                foreach (OpportunityStage item in dbEnt.OpportunityStages)
                {
                    opportunityStageDtos.Add(objMapper.GetOpportunityStageDTO(item));
                }
                return opportunityStageDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetOpportunityStages", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.OpportunityManager.GetOpportunityStages", "");

                throw;
            }
        }

        /// <summary>
        /// Get Probability Dtos
        /// </summary>
        /// <returns></returns>
        public List<PobabilityDto> GetProbabilities()
        {
            try
            {
                InsertEventLog("GetProbabilities", EventType.Log, EventColor.yellow, "Get List Of PobabilityDto", "TICRM.BusinessLayer.OpportunityManager.GetProbabilities", "");
                List<PobabilityDto> pobabilityDtos = new List<PobabilityDto>();

                foreach (Pobability item in dbEnt.Pobabilities)
                {
                    pobabilityDtos.Add(objMapper.GetPobabilityDTO(item));
                }
                return pobabilityDtos;
            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetProbabilities", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.OpportunityManager.GetProbabilities", "");
                throw;
            }
        }



        public OpportunityDto GetOpportunity(Guid? guid)
        {
            try
            {
                InsertEventLog("GetOpportunity", EventType.Log, EventColor.yellow, "going to Get OpportunityDto on id" + guid + "", "TICRM.BusinessLayer.OpportunityManager.GetOpportunity", "");
                return objMapper.GetOpportunityDTO(dbEnt.Opportunities.Find(guid));
            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetOpportunity", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.OpportunityManager.GetOpportunity", "");
                throw ex;
            }


        }
        /// <summary>
        /// Get opportunity list 
        /// </summary>
        /// <returns></returns>
        public List<OpportunityDto> GetOpportunities()
        {
            try
            {
                InsertEventLog("GetOpportunities", EventType.Log, EventColor.yellow, "going to Get List of OpportunityDto", "TICRM.BusinessLayer.OpportunityManager.GetOpportunities", "");
                List<OpportunityDto> opportunityDtos = new List<OpportunityDto>();
                List<Opportunity> opportunities = dbEnt.Opportunities.Include(o => o.Currency).Include(o => o.Team).Include(o => o.User).Include(o => o.OpportunityStage).Include(o => o.Pobability).Include(o => o.Status).Where(a => a.IsDeleted != true).ToList();

                foreach (Opportunity item in opportunities)
                {
                    opportunityDtos.Add(objMapper.GetOpportunityDTO(item));
                }
                return opportunityDtos;
            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetOpportunities", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.OpportunityManager.GetOpportunities", "");
                throw;
            }


        }


        public List<OpportunityDto> GetOpportunities(Guid accountId)
        {
            try
            {
                InsertEventLog("GetOpportunities", EventType.Log, EventColor.yellow, "going to Get List of OpportunityDto on account id = " + accountId + "", "TICRM.BusinessLayer.OpportunityManager.GetOpportunities", "");
                List<OpportunityDto> opportunityDtos = new List<OpportunityDto>();
                List<Opportunity> opportunities = dbEnt.Opportunities.Include(o => o.Currency).Include(o => o.Team).Include(o => o.User).Include(o => o.OpportunityStage).Include(o => o.Pobability).Include(o => o.Status).Where(a => a.IsDeleted != true && a.AccountId == accountId).ToList();

                foreach (Opportunity item in opportunities)
                {
                    opportunityDtos.Add(objMapper.GetOpportunityDTO(item));
                }
                return opportunityDtos;
            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetOpportunities", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.OpportunityManager.GetOpportunities", "");
                throw;
            }


        }



        /// <summary>
        /// save and edit opportunityDtos 
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        public bool SaveOpportunity(OpportunityDto acc, string CurrentUserId, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SaveOpportunity", EventType.Log, EventColor.yellow, "Enter", "TICRM.BusinessLayer.OpportunityManager.SaveOpportunity", CurrentUserId);
                Opportunity opportunity;
                if (isEditMode)
                {

                    InsertEventLog("SaveOpportunity", EventType.Log, EventColor.yellow, "going to update Opportunity on id=" + acc.OpportunityId + "", "TICRM.BusinessLayer.OpportunityManager.SaveOpportunity", CurrentUserId);
                    opportunity = objMapper.GetOpportunity(acc);
                    if (isDeleteMode)
                    {
                        InsertEventLog("SaveOpportunity", EventType.Log, EventColor.yellow, "going to update Opportunity on id=" + acc.OpportunityId + "", "TICRM.BusinessLayer.OpportunityManager.SaveOpportunity", CurrentUserId);
                        Opportunity opportunityForDelete = dbEnt.Opportunities.FirstOrDefault(x => x.OpportunityId == opportunity.OpportunityId);
                        opportunityForDelete.IsDeleted = true;
                    }
                    else
                    {
                        dbEnt.Entry(opportunity).State = EntityState.Modified;
                    }
                }
                else
                {
                    opportunity = objMapper.GetOpportunity(acc);
                    opportunity.OpportunityId = Guid.NewGuid();
                    dbEnt.Opportunities.Add(opportunity);
                }
                HttpContext.Current.Session["OpportunityObject"] = opportunity;
                HttpContext.Current.Session["CurrentUserId"] = CurrentUserId;

                if (dbEnt.SaveChanges() > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

                InsertEventMonitor("SaveOpportunity", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.OpportunityManager.SaveOpportunity", CurrentUserId);
                throw ex;
            }
            return false;
        }


    }
}
