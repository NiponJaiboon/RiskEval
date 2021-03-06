using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class SchedulePeriodic : ScheduleDetail
    {
        public SchedulePeriodic()
        {
        }

        public SchedulePeriodic(int seqNo, RescheduleOption rescheduleIfHoliday, TimeInterval hourInterval,
                                TreeListNode timeCategory, DateTime dayZeroDate, int periodLength, int fromDayNo, int toDayNo)
            : base(seqNo, rescheduleIfHoliday, hourInterval, timeCategory)
        {
            this.FromDayNo = fromDayNo;
            this.ToDayNo = toDayNo;
            this.PeriodLength = periodLength;
            this.DateOfDayZero = dayZeroDate;
        }

        #region persistent

        public virtual DateTime DateOfDayZero { get; set; }
        /// <summary>
        /// FromDayNo must be between 0 and PeriodLength and less than or equals to ToDayNo 
        /// </summary>
        public virtual int FromDayNo { get; set; }
        public virtual int ToDayNo { get; set; }
        
        private int periodLength = 1; 
        /// <summary>
        /// PeriodLength must be at least 1 day
        /// </summary>
        public virtual int PeriodLength
        {
            get { return this.periodLength; }
            set
            {
                if (value < 1)
                    throw new iSabayaException("PeriodLength must be greater than or equal to 1.");
                else
                    this.periodLength = value;
            }
        }

        #endregion persistent

        #region ScheduleDetail implementaion

        public override string ToLog()
        {
            return base.ToLog(String.Format("SchedulePeriodicInterval: length={0}, interval={1}-{2}", 
                                            this.PeriodLength, this.FromDayNo, this.ToDayNo));
        }

        public override TimeInterval GetScheduledHoursOn(DateTime timestamp)
        {
            if (IsScheduledDate(timestamp))
                return base.hourInterval.Clone();
            else
                return null;
        }

        public virtual int GetDayNo(DateTime date)
        {
            int dayNoOfTimeStamp = (date.Date - this.DateOfDayZero.Date).Days % this.periodLength;
            if (dayNoOfTimeStamp < 0) dayNoOfTimeStamp += this.periodLength;
            return dayNoOfTimeStamp;
        }

        public override bool IsScheduledDate(DateTime date)
        {
            int dayNoOfTimeStamp = GetDayNo(date);
            return (this.FromDayNo <= dayNoOfTimeStamp && dayNoOfTimeStamp <= this.ToDayNo);
        }

        //public override bool IsScheduledOrRescheduledDay(DateTime date, TimeSchedule workCalendar, TimeSchedule nonworkSchedule, 
        //                                                out TimeInterval scheduledHours)
        //{
        //    throw new NotImplementedException();
        //}

        public override string ToLongString()
        {
            throw new NotImplementedException();
        }

        #endregion ScheduleDetail implementaion
    }
}
