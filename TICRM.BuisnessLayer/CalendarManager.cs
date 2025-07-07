using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.BuisnessLayer
{
    public class CalendarManager : BaseManager
    {




        public CalendarEventDTO GetActivityForCalendar(Guid? id)
        {
            InsertEventLog("GetActivityForCalendar",  EventType.Log, EventColor.yellow, "to get CalendarEventDTO", "TICRM.BuisnessLayer.CalendarManager.GetActivityForCalendar", "");

            List<EventAttendee> attendees = new List<EventAttendee>();

            Activity query = dbEnt.Activities.FirstOrDefault(x => x.ActivityId == id);

            var data = from tu in dbEnt.TeamUsers
                       join u in dbEnt.Users on tu.TeamUserId equals u.UserId
                       where tu.TeamId == query.AssignedTeam
                       select new { u.Email,u.Name };


            foreach (var item in data)
            {
                EventAttendee eventAttendee = new EventAttendee();
                eventAttendee.Email = item.Email;
                eventAttendee.DisplayName = item.Name;
                attendees.Add(eventAttendee);
            }

            CalendarEventDTO calendarEventDTO;
            calendarEventDTO = objMapper.GetCalendarEventDTO(query);
            EventAttendee eventUser = new EventAttendee();
            eventUser.Email = calendarEventDTO.User.Email;
            eventUser.DisplayName = calendarEventDTO.User.Name;
            attendees.Add(eventUser);
            calendarEventDTO.Attendees = attendees;

            //calendarEventDTO.Attendance = Newtonsoft.Json.JsonConvert.SerializeObject(attendees);

            return calendarEventDTO;

            //return objMapper.GetCalendarEventDTO(dbEnt.Activities.FirstOrDefault(x => x.ActivityId == id));
        }









    }
}
