using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iSabaya;

namespace WebHelper.Util
{
    public class HolidayCalendar
    {

        public DateTime DateHoliday { get; set; }
        public string DateName { get; set; }

 
        private static IList<HolidayCalendar> holidayCalendars;

        public static IList<HolidayCalendar> holidayCalendar(IList<ScheduleDetail> scheduleDetails)
        {
            holidayCalendars = new List<HolidayCalendar>();
            foreach (ScheduleDetail item in scheduleDetails)
            {
                holidayCalendars.Add(
                    new HolidayCalendar
                    {                        
                        DateName = item.Title.ToString(),
                       
                    }
                );
            }
            return holidayCalendars;
        }

    }
}