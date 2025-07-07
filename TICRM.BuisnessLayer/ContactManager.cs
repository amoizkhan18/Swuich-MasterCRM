using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TICRM.DTOs;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using System.Web;
using System.Data.Entity;

namespace TICRM.BuisnessLayer
{
    public class ContactManager : BaseManager
    {
        public ContactManager()
        {
        }

        public ContactDto GetContact(long? guid)
        {
            try
            {
                InsertEventLog("GetContact", EventType.Log, EventColor.yellow, "Get ContactDto", "TICRM.BusinessLayer.ContactManager.GetContact", "");
                return objMapper.GetContactDto(dbEnt.Contacts.Find(guid));
            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetContact", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ContactManager.GetContact", "");
                throw ex;
            }


        }
        /// <summary>
        /// get list of contacts from database
        /// </summary>
        /// <returns></returns>
        public List<ContactDto> GetContacts()
        {
            try
            {
                InsertEventLog("GetContacts", EventType.Log, EventColor.yellow, "Get list of ContactDto", "TICRM.BusinessLayer.ContactManager.GetContacts", "");
                List<ContactDto> ContactDtos = new List<ContactDto>();
                List<Contact> contacts = dbEnt.Contacts.Include("Account").ToList(); //Include(l => l.LeadSource).Include(l => l.LeadType).Include(l => l.Status).Include(l => l.Team).Include(l => l.User).Where(a => a.IsDeleted != true).ToList();
                foreach (Contact item in dbEnt.Contacts)
                {
                    ContactDtos.Add(objMapper.GetContactDto(item));
                }
                return ContactDtos;
            }
            catch (Exception ex)
            {

                InsertEventMonitor("GetContacts", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ContactManager.GetContacts", "");
                throw;
            }


        }
        /// <summary>
        /// Save and edit the contacts
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="isEditMode"></param>
        /// <param name="isDeleteMode"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public bool SaveContact(ContactDto contactDto, bool isEditMode, bool isDeleteMode, string CurrentUserId)
        {
            try
            {
                InsertEventLog("SaveContact", EventType.Log, EventColor.yellow, "Enter", "TICRM.BusinessLayer.ContactManager.SaveContact", "");
                Contact contact;

                if (isEditMode)
                {
                    InsertEventLog("SaveContact", EventType.Log, EventColor.yellow, "going to edit Contact of id =" + contactDto.ContactId + "", "TICRM.BusinessLayer.ContactManager.SaveContact", "");

                    contact = objMapper.GetContact(contactDto);
                    if (isDeleteMode)
                    {
                        InsertEventLog("SaveContact", EventType.Log, EventColor.yellow, "going to delete Contact of id =" + contact.Id + "", "TICRM.BusinessLayer.ContactManager.SaveContact", "");

                        Contact contactDelete = dbEnt.Contacts.FirstOrDefault(x => x.Id == contact.Id);
                        contactDelete.IsDeleted = true;
                    }
                    else
                    {
                        dbEnt.Entry(contact).State = EntityState.Modified;
                    }
                }
                else
                {
                    InsertEventLog("SaveContact", EventType.Log, EventColor.yellow, "going to create new Record", "TICRM.BusinessLayer.ContactManager.SaveContact", "");
                    contact = objMapper.GetContact(contactDto);
                    //contact.Id=0;
                    dbEnt.Contacts.Add(contact);
                }
                HttpContext.Current.Session["ContactObject"] = contact;
                HttpContext.Current.Session["CurrentUserId"] = CurrentUserId;

                if (dbEnt.SaveChanges() > 0)
                {
                    InsertEventLog("SaveContact", EventType.Log, EventColor.yellow, "Contact saved Successfully of id =" + contact.Id+ "", "TICRM.BusinessLayer.ContactManager.SaveContact", "");
                    return true;
                }

            }
            catch (Exception ex)
            {

                InsertEventMonitor("SaveContact", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.ContactManager.SaveContact", "");
                throw ex;
            }

            return false;
        }
    }
}
