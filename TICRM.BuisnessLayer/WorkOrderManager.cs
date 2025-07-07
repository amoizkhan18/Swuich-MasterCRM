using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.BuisnessLayer
{
    public class WorkOrderManager : BaseManager
    {
        public WorkOrderManager()
        {
            WorkStages = GetWorkStages();
        }
        public List<WorkStageDto> WorkStages { get; set; }

        /// <summary>
        /// Get Work Stage DTOs
        /// </summary>
        /// <returns></returns>
        public List<WorkStageDto> GetWorkStages()
        {
            try
            {
                InsertEventLog("GetWorkStages", EventType.Log, EventColor.yellow, "getting List of WorkStageDto", "TICRM.BusinessLayer.WorkOrderManager.GetWorkStages", "");
                List<WorkStageDto> workStageDtos = new List<WorkStageDto>();

                foreach (WorkStage item in dbEnt.WorkStages)
                {
                    workStageDtos.Add(objMapper.GetWorkStageDTO(item));
                }
                return workStageDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetWorkStages", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.WorkOrderManager.GetWorkStages", "");
                throw;
            }
        }

        public WorkOrderDto GetWorkOrder(Guid? guid)
        {
            try
            {
                InsertEventLog("GetWorkOrder", EventType.Log, EventColor.yellow, "getting WorkOrder on id='" + guid + "'", "TICRM.BusinessLayer.WorkOrderManager.GetWorkOrder", "");
                return objMapper.GetWorkOrderDTO(dbEnt.WorkOrders.Find(guid));
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetWorkStages", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.WorkOrderManager.GetWorkOrder", "");
                throw ex;
            }


        }
        /// <summary>
        /// Get WorkOrder list 
        /// </summary>
        /// <returns></returns>
        public List<WorkOrderDto> GetWorkOrders()
        {
            try
            {
                InsertEventLog("GetWorkOrders", EventType.Log, EventColor.yellow, "getting list of WorkOrderDto", "TICRM.BusinessLayer.WorkOrderManager.GetWorkOrders", "");
                List<WorkOrderDto> WorkOrderDtos = new List<WorkOrderDto>();
                List<WorkOrder> workOrders = dbEnt.WorkOrders.Include(w => w.Status).Include(w => w.Team).Include(w => w.User).Include(w => w.WorkStage).Where(a => a.IsDeleted != true).ToList();
                foreach (WorkOrder item in dbEnt.WorkOrders)
                {
                    WorkOrderDtos.Add(objMapper.GetWorkOrderDTO(item));
                }
                return WorkOrderDtos;
            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetWorkOrder", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.WorkOrderManager.GetWorkOrders", "");
                throw;
            }


        }
        /// <summary>
        /// save and edit WorkOrder 
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        public bool SaveWorkOrder(WorkOrderDto acc, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SaveWorkOrder", EventType.Log, EventColor.yellow, "Enter", "TICRM.BusinessLayer.WorkOrderManager.SaveWorkOrder", "");
                WorkOrder WorkOrder;
                if (isEditMode)
                {

                    WorkOrder = objMapper.GetWorkOrder(acc);
                    if (isDeleteMode)
                    {
                        InsertEventLog("SaveWorkOrder", EventType.Log, EventColor.yellow, "going to delete workorder on id='" + acc.WorkOrderId + "'", "TICRM.BusinessLayer.WorkOrderManager.SaveWorkOrder", "");
                        WorkOrder order = dbEnt.WorkOrders.FirstOrDefault(x => x.WorkOrderId == WorkOrder.WorkOrderId);
                        order.IsDeleted = true;
                        dbEnt.WorkOrders.Remove(order);
                        dbEnt.SaveChanges();
                    }
                    else
                    {
                        dbEnt.Entry(WorkOrder).State = EntityState.Modified;
                    }
                    InsertEventLog("SaveWorkOrder", EventType.Log, EventColor.yellow, "going to Update workorder on id='" + acc.WorkOrderId + "'", "TICRM.BusinessLayer.WorkOrderManager.SaveWorkOrder", "");
                    if (dbEnt.SaveChanges() > 0)
                    {
                        InsertEventLog("SaveWorkOrder", EventType.Log, EventColor.yellow, "workorder Updated Successfully", "TICRM.BusinessLayer.WorkOrderManager.SaveWorkOrder", "");
                        return true;
                    }
                }
                else
                {
                    InsertEventLog("SaveWorkOrder", EventType.Log, EventColor.yellow, "going to create new record of workorder", "TICRM.BusinessLayer.WorkOrderManager.SaveWorkOrder", "");
                    WorkOrder = objMapper.GetWorkOrder(acc);
                    WorkOrder.WorkOrderId = Guid.NewGuid();
                    dbEnt.WorkOrders.Add(WorkOrder);
                }

                if (dbEnt.SaveChanges() > 0)
                {
                    InsertEventLog("SaveWorkOrder", EventType.Log, EventColor.yellow, "workorder Saved Successfully", "TICRM.BusinessLayer.WorkOrderManager.SaveWorkOrder", "");
                    return true;
                }

            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetWorkOrders", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.WorkOrderManager.SaveWorkOrder", "");
                throw ex;
            }

            return false;
        }



    }
}
