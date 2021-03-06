using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class TimeInterval : IComparable, IComparable<TimeInterval>
    {
        /// <summary>
        /// Safe for persisting to SQL Server database type datetime
        /// </summary>
        public static readonly DateTime MaxDate = System.Globalization.CultureInfo.InvariantCulture.Calendar.ToDateTime(2300, 12, 31, 0, 0, 0, 0);
        /// <summary>
        /// Safe for persisting to SQL Server database
        /// </summary>
        public static readonly DateTime MinDate = System.Globalization.CultureInfo.InvariantCulture.Calendar.ToDateTime(1800, 1, 1, 0, 0, 0, 0);
        public static readonly TimeInterval EmptyInterval = new TimeInterval(MaxDate, MinDate);
        public static readonly TimeInterval Eternal = new TimeInterval(MinDate, MaxDate);

        public static TimeInterval EffectiveNow
        {
            get { return new TimeInterval(DateTime.Now); }
        }

        #region constructors

        public TimeInterval()
        {
            this.From = TimeInterval.MinDate;
            this.To = TimeInterval.MaxDate;
        }

        public TimeInterval(TimeInterval original)
        {
            this.From = original.From;
            this.To = original.To;
        }

        /// <summary>
        /// Create a new instance whose From = from and To = TimeInterval.MaxDate
        /// </summary>
        /// <param name="from"></param>
        public TimeInterval(DateTime from)
        {
            this.From = from;
            this.To = TimeInterval.MaxDate;
        }

        public TimeInterval(DateTime from, DateTime to)
        {
            this.From = from;
            this.To = to;
        }

        /// <summary>
        /// Create a new instance whose date of From and To is date parameter, but with times from timeinterval
        /// </summary>
        /// <param name="date"></param>
        /// <param name="timeInterval"></param>
        public TimeInterval(DateTime date, TimeInterval timeInterval)
        {
            this.From = new DateTime(date.Year, date.Month, date.Day, timeInterval.From.Hour, timeInterval.From.Minute, timeInterval.From.Second);
            this.To = new DateTime(date.Year, date.Month, date.Day, timeInterval.To.Hour, timeInterval.To.Minute, timeInterval.To.Second);
        }

        public TimeInterval(DateTime date, DateTime fromHour, DateTime toHour)
        {
            this.From = new DateTime(date.Year, date.Month, date.Day, fromHour.Hour, fromHour.Minute, fromHour.Second);
            this.To = new DateTime(date.Year, date.Month, date.Day, toHour.Hour, toHour.Minute, toHour.Second);
        }

        #endregion constructors

        #region persistent

        public virtual DateTime From { get; set; }

        public virtual DateTime To { get; set; }

        #endregion persistent

        /// <summary>
        /// Contains date but not time (time is always 00:00:00)
        /// </summary>
        public virtual DateTime EffectiveDate
        {
            get { return From; }
            set { this.From = value.Date; }
        }

        /// <summary>
        /// Contains date but not time (time is always 00:00:00)
        /// </summary>
        public virtual DateTime ExpiryDate
        {
            get { return To.AddSeconds(1); }
            set { this.To = value.Date.AddSeconds(-1); }
        }

        public virtual TimeInterval Intersect(TimeInterval interval)
        {
            DateTime f = (From < interval.From) ? interval.From : From;
            DateTime t = (To < interval.To) ? To : interval.To;

            if (f <= t)
                return new TimeInterval(f, t);
            else
                return EmptyInterval;
        }

        public virtual TimeInterval Intersect(DateTime from, DateTime to)
        {
            DateTime f = (this.From < from) ? from : this.From;
            DateTime t = (this.To < to) ? this.To : to;

            if (f <= t)
                return new TimeInterval(f, t);
            else
                return EmptyInterval;
        }

        public virtual bool IsEmpty
        {
            get { return From > To; }
        }

        //public virtual bool IsEffective
        //{
        //    get
        //    {
        //        DateTime today = DateTime.Today;
        //        return From <= today && today <= To;
        //    }
        //}

        //public virtual bool Includes(DateTime dateTime)
        //{
        //    return From <= dateTime && dateTime <= To;
        //}

        public virtual bool Overlaps(TimeInterval interval)
        {
            return !(this.From > interval.To || this.To < interval.From);

            //DateTime f = (from < interval.from) ? interval.from : from;
            //DateTime t = (to < interval.to) ? to : interval.to;

            //return (f <= t);
        }

        public virtual TimeDuration Duration()
        {
            int days = this.To.DayOfYear - this.From.DayOfYear;
            TimeSpan timeDiff = this.To.TimeOfDay - this.From.TimeOfDay;
            return new TimeDuration(this.Years(), days, timeDiff.Hours, timeDiff.Minutes, timeDiff.Seconds);
        }

        public virtual int Years()
        {
            int years = 0;
            TimeSpan sp = this.To - this.From;
            DateTime sameDayEndYear;

            if (this.From.Day < 29)
            {
                sameDayEndYear = new DateTime(this.To.Year, this.From.Month, this.From.Day);
            }
            else
            {
                sameDayEndYear = new DateTime(this.To.Year, this.From.Month, this.From.Day - 1);
                sameDayEndYear.AddDays(1);
            }

            years = this.To.Year - this.From.Year;
            if (sameDayEndYear <= this.To.Date)
                ++years;
            return years;
        }

        //public virtual TimeInterval Clone()
        //{
        //    return new TimeInterval(this);
        //}

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            TimeInterval theOther = obj as TimeInterval;
            if (theOther == null) return false;
            if (this.From == theOther.From && this.To == theOther.To) return true;
            return false;
        }

        public override int GetHashCode()
        {
            return From.GetHashCode() ^ To.GetHashCode();
        }

        public virtual string ToString(String format, String languageCode)
        {
            if (Object.ReferenceEquals(this, EmptyInterval))
                return "[empty]";
            else if (Object.ReferenceEquals(this, Eternal))
                return "[forever]";

            CultureInfo c = CultureInfo.GetCultureInfo(languageCode);
            StringBuilder b = new StringBuilder("[");

            if (this.From == TimeInterval.MinDate)
                b.Append("-");
            else
                b.Append(this.From.ToString(format, c));
            b.Append(", ");
            if (this.To == TimeInterval.MaxDate)
                b.Append("-");
            else
                b.Append(this.To.ToString(format, c));
            b.Append("]");
            return b.ToString();
        }

        public virtual string ToString(String format)
        {
            if (Object.ReferenceEquals(this, EmptyInterval))
                return "[empty]";
            else if (Object.ReferenceEquals(this, Eternal))
                return "[forever]";

            StringBuilder b = new StringBuilder("[");

            if (this.From == TimeInterval.MinDate)
                b.Append("-");
            else
                b.Append(this.From.ToString(format));
            b.Append(", ");
            if (this.To == TimeInterval.MaxDate)
                b.Append("-");
            else
                b.Append(this.To.ToString(format));
            b.Append("]");
            return b.ToString();
        }

        public override string ToString()
        {
            if (Object.ReferenceEquals(this, EmptyInterval))
                return "[empty]";
            else if (Object.ReferenceEquals(this, Eternal))
                return "[forever]";

            StringBuilder b = new StringBuilder("[");

            if (this.From == TimeInterval.MinDate)
                b.Append("-");
            else
                b.Append(this.From.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            b.Append(", ");
            if (this.To == TimeInterval.MaxDate)
                b.Append("-");
            else
                b.Append(this.To.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            b.Append("]");
            return b.ToString();
        }

        public static bool operator ==(TimeInterval left, TimeInterval right)
        {
            if (object.ReferenceEquals(left, right)) return true;
            if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null)) return false;
            if (left.From == right.From && left.To == right.To) return true;
            return false;
        }

        public static bool operator !=(TimeInterval left, TimeInterval right)
        {
            return !(left == right);
        }

        #region IComparable<TimeInterval> Members

        public virtual int CompareTo(TimeInterval other)
        {
            if (Object.ReferenceEquals(null, other))
                return 1;
            if (this.From < other.From)
                return -1;
            else
                if (this.From > other.From)
                    return 1;
                else
                    return 0;
        }

        #endregion

        #region IComparable Members

        public virtual int CompareTo(object obj)
        {
            return CompareTo((TimeInterval)obj);
        }

        #endregion

        public static bool IsNullOrEmpty(TimeInterval timeInterval)
        {
            return null == timeInterval || timeInterval == TimeInterval.EmptyInterval;
        }

        public static TimeInterval Clone(TimeInterval original)
        {
            return new TimeInterval(original);
        }
    }
}
