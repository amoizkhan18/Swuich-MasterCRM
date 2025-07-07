using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.BuisnessLayer
{
    public class DiscountManager : BaseManager
    {

        public DiscountManager() { }


        public List<DiscountDTO> GetDiscountDTOs()
        {
            try
            {
                InsertEventLog("GetDiscountDTOs", EventType.Log, EventColor.yellow, "to get list of Category ", "TICRM.BuisnessLayer.DiscountManager.GetDiscountDTOs", "");

                List<DiscountDTO> discountDTOs = new List<DiscountDTO>(); // initilize the list object

                List<Discount> discounts = dbEnt.Discounts.ToList(); // Get List Of Discounts from DB
                                                                     // apply iteration on Discounts
                foreach (Discount item in discounts)
                {
                    discountDTOs.Add(objMapper.GetDiscountDTO(item)); // add in a list object
                }

                return discountDTOs; // return Collection Object in Response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetDiscountDTOs", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DiscountManager.GetDiscountDTOs", "");
                throw;
            }
        }

        public bool SubmitDiscount(CategoryDto categoryDto, string CurrentUserId, bool isEditMode = false, bool isDeleteMode = false)
        {
            try
            {
                InsertEventLog("SubmitCategory", EventType.Log, EventColor.yellow, "enter ", "TICRM.BuisnessLayer.DiscountManager.SubmitCategory", "");

                Category category; // create a new object
                category = objMapper.GetDtoToCategory(categoryDto); // pass parameter object to categoryDto object
                if (isEditMode) // check if is is edit mode is true
                {
                    Category dbData = dbEnt.Categories.FirstOrDefault(x => x.CategoryId == category.CategoryId); // get data from database and pass in new Category class object

                    if (dbData != null) // check if data is null
                    {
                        if (isDeleteMode) // if is delete mode is true 
                        {
                            InsertEventLog("SubmitCategory", EventType.Log, EventColor.yellow, "enter in Delete mode to delete event log ", "TICRM.BuisnessLayer.DiscountManager.SubmitCategory", "");
                            dbEnt.Categories.Remove(dbData); // remove object in database
                        }
                        else
                        {
                            InsertEventLog("SubmitCategory", EventType.Log, EventColor.yellow, "enter in edit mode to update Data event log ", "TICRM.BuisnessLayer.DiscountManager.SubmitCategory", "");
                            dbData.Name = category.Name;
                            dbData.Description = category.Description;
                            dbData.AssignedUser = category.AssignedUser;
                            dbData.AssignedTeam = category.AssignedTeam;
                            dbData.StatusId = category.StatusId;
                            dbData.UpdatedDate = DateTime.Now;
                            dbData.UpdatedBy = CurrentUserId;
                            dbEnt.Entry(dbData).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        InsertEventLog("SubmitCategory", EventType.Log, EventColor.yellow, "enter in edit mode data is null ", "TICRM.BuisnessLayer.DiscountManager.SubmitCategory", "");
                        return false; // return false if no any condition found for edit and delete
                    }

                    if (dbEnt.SaveChanges() > 0) // check if database save changes is done return true
                    {
                        InsertEventLog("SubmitCategory", EventType.Log, EventColor.yellow, "for edit and delete data is saved in DB ", "TICRM.BuisnessLayer.DiscountManager.SubmitCategory", "");
                        return true;
                    }

                }
                else
                {
                    InsertEventLog("SubmitCategory", EventType.Log, EventColor.yellow, "Enter In Create new record ", "TICRM.BuisnessLayer.DiscountManager.SubmitCategory", "");
                    category.CategoryId = Guid.NewGuid();
                    category.CreatedBy = CurrentUserId;
                    category.CreatedDate = DateTime.Now;
                    category.IsDeleted = false;
                    dbEnt.Categories.Add(category); // add in a database
                    if (dbEnt.SaveChanges() > 0)
                    {
                        InsertEventLog("SubmitCategory", EventType.Log, EventColor.yellow, "New Record is saved ", "TICRM.BuisnessLayer.DiscountManager.SubmitCategory", "");
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                InsertEventMonitor("SubmitCategory", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DiscountManager.SubmitCategory", "");
                throw ex;
            }
            return false;
        }

        public CategoryDto GetCategoryOnId(Guid? guid)
        {
            try
            {
                InsertEventLog("GetCategoryOnId", EventType.Log, EventColor.yellow, "get event log on id ", "TICRM.BuisnessLayer.DiscountManager.GetCategoryOnId", "");
                return objMapper.GetCategoryDTO(dbEnt.Categories.FirstOrDefault(x => x.CategoryId == guid)); // Get Category On Id and and convert it DTO and then return in response
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetCategoryOnId", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BuisnessLayer.DiscountManager.GetCategoryOnId", "");
                throw ex;
            }
        }


    }
}
