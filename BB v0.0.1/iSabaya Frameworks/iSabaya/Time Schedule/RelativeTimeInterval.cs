using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public enum DayType
    {
        BusinessDay,
        CalendarDay,
    }

    /// <summary>
    /// To member will be set with respect to a given date + this.LeadDays
    /// </summary>
    public class RelativeTimeInterval : TimeInterval
    {
        public RelativeTimeInterval()
        {
        }

        public RelativeTimeInterval(RelativeTimeInterval original)
        {
            this.From = original.From;
            this.To = original.To;
            this.LeadDays = original.LeadDays;
            this.LeadDayType = original.LeadDayType;
        }

        public RelativeTimeInterval(int leadDays, DayType dayType, DateTime from, DateTime to)
        {
            this.LeadDays = leadDays;
            this.LeadDayType = dayType;
            this.From = from;
            this.To = to;
        }

        #region persistent
        
        public virtual DayType LeadDayType { get; set; }
        
        /// <summary>
        /// The return interval will be later than reference date if LeadDays is negative.
        /// Otherwise, the return interval will be before the reference date.
        /// </summary>
        public virtual int LeadDays { get; set; }

        #endregion persistent
        
        /// <summary>
        /// Delay in days
        /// </summary>
        public virtual int LagDays
        {
            get { return -this.LeadDays; }
            set { this.LeadDays = -value; }
        }

        public virtual TimeInterval FindAbsoluteTimeIntervalWithRespectTo(DateTime referenceDate, TimeSchedule workCalendar, TimeSchedule holidayCalendar)
        {
            DateTime d;
            if (this.LeadDayType == DayType.BusinessDay && null != workCalendar)
                d = workCalendar.FindAbsoluteWorkday(holidayCalendar, referenceDate, this.LeadDays);
            else
                d = referenceDate.AddDays(LeadDays);

            if (this.LeadDays < 0)
            {
                //Happen after the reference date
                DateTime f = new DateTime(d.Year, d.Month, d.Day, this.From.Hour, this.From.Minute, this.From.Second);
                if (this.From.TimeOfDay > this.To.TimeOfDay)
                    d = d.AddDays(1);
                DateTime t = new DateTime(d.Year, d.Month, d.Day, this.To.Hour, this.To.Minute, this.To.Second);
                return new TimeInterval(f, t);
            }
            else
            {
                //Happen earlier than the reference date
                DateTime t = new DateTime(d.Year, d.Month, d.Day, this.To.Hour, this.To.Minute, this.To.Second);
                if (this.From.TimeOfDay > this.To.TimeOfDay)
                    d = d.AddDays(-1);
                DateTime f = new DateTime(d.Year, d.Month, d.Day, this.From.Hour, this.From.Minute, this.From.Second);
                return new TimeInterval(f, t);
            }
        }
    }
}
