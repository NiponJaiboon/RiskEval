using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOSchedule
    {
        private TimeSchedule instance;
        public VOSchedule(TimeSchedule instance)
        {
            this.instance = instance;
        }

        public int ScheduleID
        {
            get { return instance.ScheduleID; } 
        }

        public string Code
        {
            get { return instance.Code; }
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

        public string Description
        {
            get
            {
                if (instance.Description == null)
                    return "-";
                else
                    return instance.Description.ToString();
            }
        }

        public string EffectivePeriod
        {
            get
            {
                if (instance.EffectivePeriod == null)
                    return "-";
                else
                    return instance.EffectivePeriod.ToString();
            }
        }

        public string Category
        {
            get
            {
                if (instance.Category == null)
                    return "-";
                else
                    return instance.Category.ToString();
            }
        }

        public bool IsWork
        {
            get { return instance.IsWork; }
        }
    }
}
