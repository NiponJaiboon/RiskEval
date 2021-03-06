using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{

    [Serializable]
    public class TimeDuration : IComparable<TimeDuration>, ISimpleMath<TimeDuration>
    {
        //public delegate void TimeDurationEvent(object sender, DateTime previousDate);
        //public event TimeDurationEvent FromDateChange;
        public const long DaysPerYear = 365;
        public const long HoursPerDay = 24;
        public const long MinutesPerHour = 60;
        public const long MinutesPerDay = HoursPerDay * MinutesPerHour;
        public const long SecondsPerMinute = 60;
        public const long SecondsPerHour = 3600;
        public const long SecondsPerDay = HoursPerDay * SecondsPerHour;
        public const long SecondsPerYear = SecondsPerDay * DaysPerYear;

        public virtual long TotalSeconds { get; set; }

        public TimeDuration()
        {
        }

        public TimeDuration(long totalSeconds)
        {
            this.TotalSeconds = totalSeconds;
        }

        public TimeDuration(TimeDuration original)
        {
            this.TotalSeconds = original.TotalSeconds;
        }

        public TimeDuration(int years, int days, int hours, int minutes, int seconds)
        {
            SetDuration(years, days, hours, minutes, seconds);
        }

        #region persistent

        public virtual int Years
        {
            get { return (int)(this.TotalSeconds / SecondsPerYear); }
            set { this.TotalSeconds = value * SecondsPerYear + this.TotalSeconds % SecondsPerYear; }
        }

        /// <summary>
        /// Get or set the number of days.  The range is between 0 and 365.
        /// </summary>
        public virtual int Days
        {
            get { return (int)((this.TotalSeconds % SecondsPerYear) / SecondsPerDay); }
            set
            {
                if (Math.Abs(value) > DaysPerYear)
                    throw new ArgumentException();

                long fractionOfDayInSeconds = this.TotalSeconds % SecondsPerDay;
                this.TotalSeconds = RoundDown(SecondsPerYear) + value * SecondsPerDay + fractionOfDayInSeconds;
            }
        }

        /// <summary>
        /// Get or set the number of hours.  The range is between 0 and 24.
        /// </summary>
        public virtual int Hours
        {
            get
            {
                long fractionOfDay = (this.TotalSeconds % SecondsPerDay);
                return (int)((this.TotalSeconds % SecondsPerDay) / SecondsPerHour);
            }
            set
            {
                if (Math.Abs(value) > HoursPerDay)
                    throw new ArgumentException();

                long fractionOfHourInSeconds = this.TotalSeconds % SecondsPerHour;
                this.TotalSeconds = RoundDown(SecondsPerDay) + value * SecondsPerHour + fractionOfHourInSeconds;
            }
        }

        public virtual int Minutes
        {
            get { return (int)((this.TotalSeconds % SecondsPerHour) / SecondsPerMinute); }
            set
            {
                if (Math.Abs(value) > MinutesPerHour)
                    throw new ArgumentException();

                long fractionOfMinutesInSeconds = this.TotalSeconds % SecondsPerMinute;
                this.TotalSeconds = RoundDown(SecondsPerHour) + value * SecondsPerMinute + fractionOfMinutesInSeconds;
            }
        }

        public virtual int Seconds
        {
            get { return (int)(this.TotalSeconds % SecondsPerMinute); }
            set
            {
                if (Math.Abs(value) > SecondsPerMinute)
                    throw new ArgumentException();
                this.TotalSeconds = RoundDown(SecondsPerMinute) + value;
            }
        }

        private long RoundDown(long roundTo)
        {
            return (long)(this.TotalSeconds / roundTo) * roundTo;
        }


        #endregion persistent

        public virtual void SetDuration(int years, int days, int hours, int minutes, int seconds)
        {
            if (days > DaysPerYear || hours > HoursPerDay || minutes > MinutesPerDay || seconds > 60)
                throw new ArgumentException();
            this.TotalSeconds = (years * SecondsPerYear) + (days * SecondsPerDay)
                                + (hours * SecondsPerHour) + (minutes * SecondsPerMinute)
                                + seconds;
        }

        public virtual DateTime DurationEndDate(DateTime fromDate)
        {
            DateTime endDate = fromDate;
            if (this.TotalSeconds != 0)
                endDate = endDate.AddSeconds(this.TotalSeconds);
            return endDate;
        }

        #region IComparable<TimeDuration> Members

        public int CompareTo(TimeDuration other)
        {
            if (object.ReferenceEquals(other, null))
                throw new iSabayaException("TimeDuration.operator < : one of the operand is null.");
            if (this.TotalSeconds < other.TotalSeconds) return -1;
            if (this.TotalSeconds > other.TotalSeconds) return 1;
            return 0;
        }

        #endregion

        #region ISimpleMath<TimeDuration> Members

        public virtual DateTime Add(DateTime startDate)
        {
            return startDate.Add(new TimeSpan(this.Days, this.Hours, this.Minutes, this.Seconds));
        }

        public virtual TimeDuration Add(TimeDuration b)
        {
            return this + b;
        }

        public virtual TimeDuration Subtract(TimeDuration b)
        {
            return this - b;
        }

        public virtual TimeDuration Multiply(double rate)
        {
            throw new NotImplementedException();
        }

        #endregion ISimpleMath

        #region Operators
        
        public static bool operator <(TimeDuration left, TimeDuration right)
        {
            if (object.ReferenceEquals(left, right)) return false;
            if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null))
                throw new iSabayaException("TimeDuration.operator < : one of the operand is null.");
            return (left.TotalSeconds < right.TotalSeconds);
        }

        public static bool operator <=(TimeDuration left, TimeDuration right)
        {
            if (object.ReferenceEquals(left, right)) return true;
            if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null))
                throw new iSabayaException("TimeDuration.operator < : one of the operand is null.");
            return (left.TotalSeconds <= right.TotalSeconds);
        }

        public static bool operator ==(TimeDuration left, TimeDuration right)
        {
            if (object.ReferenceEquals(left, right)) return true;
            if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null)) return false;
            return (left.TotalSeconds == right.TotalSeconds);
        }

        public static bool operator !=(TimeDuration left, TimeDuration right)
        {
            return !(left == right);
        }

        public static bool operator >(TimeDuration left, TimeDuration right)
        {
            if (object.ReferenceEquals(left, right)) return false;
            if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null))
                throw new iSabayaException("TimeDuration.operator < : one of the operand is null.");
            return (left.TotalSeconds > right.TotalSeconds);
        }

        public static bool operator >=(TimeDuration left, TimeDuration right)
        {
            if (object.ReferenceEquals(left, right)) return true;
            if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null))
                throw new iSabayaException("TimeDuration.operator < : one of the operand is null.");
            return (left.TotalSeconds >= right.TotalSeconds);
        }

        public static TimeDuration operator +(TimeDuration a, TimeDuration b)
        {
            return new TimeDuration(a.TotalSeconds + b.TotalSeconds);
        }

        public static TimeDuration operator -(TimeDuration a, TimeDuration b)
        {
            return new TimeDuration(a.TotalSeconds - b.TotalSeconds);
        }

        #endregion Operators
        
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            TimeDuration theOther = obj as TimeDuration;
            return this.TotalSeconds == theOther.TotalSeconds;
        }

        public override int GetHashCode()
        {
            return this.TotalSeconds.GetHashCode();
        }

        public override string ToString()
        {
            return this.Years.ToString("D") + "," + this.Days.ToString("F02");
        }

        public virtual string ToString(String yearSuffix, String daySuffix)
        {
            return this.Years.ToString("D") + yearSuffix + this.Days.ToString("F02") + daySuffix;
        }

        public virtual string ToString(String yearSuffix, String monthSuffix, String daySuffix)
        {
            return this.Years.ToString("D") + yearSuffix + monthSuffix + this.Days.ToString("F02") + daySuffix;
        }

        public static bool IsNullOrZero(TimeDuration timeDuration)
        {
            return timeDuration == null || timeDuration.TotalSeconds == 0;
        }
    }
}
