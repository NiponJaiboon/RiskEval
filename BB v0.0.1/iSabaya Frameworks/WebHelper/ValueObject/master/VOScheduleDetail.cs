using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOScheduleDetail
    {
        private ScheduleDetail instance;
        public VOScheduleDetail(ScheduleDetail instance)
        {
            this.instance = instance;
        }

        public int ScheduleDetailID
        {
            get { return instance.ScheduleDetailID; }
        }

        public int SeqNo
        {
            get { return instance.SeqNo; }
        }

        public string Title
        {
            get
            {
                if (instance.Title == null)
                    return "-";
                else
                    return instance.Title.ToString();
            }
        }

        public string RescheduleIfHoliday
        {
            get { return instance.RescheduleIfHoliday.ToString(); }
        }

        //public int DayNo
        //{
        //    get { return instance.DayNo; }
        //}

        public string HourInterval
        {
            get
            {
                if (instance.HourInterval == null)
                    return "-";
                else
                    return instance.HourInterval.ToString();
            }
        }

        public string Schedule
        {
            get
            {
                if (instance.Schedule == null)
                    return "-";
                else
                    return instance.Schedule.ToString();
            }
        }

        public string Text
        {
            get { return instance.Text; }
        }
    }
}
