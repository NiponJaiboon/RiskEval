using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper.TextFile
{
    #region Field Classes

    //public class CompositeField<U> : PropertyMultiFixedLengthTextFieldMapping<V, U>
    //public class CompositeField<U> : TextFieldType<U>
    //    where U : class, new()
    //{
    //    public virtual TextFieldMapper<U> Mapping { get; set; }

    //    public override void Initialize(ref int colNo)
    //    {
    //        if (null != this.Mapping)
    //        {
    //            this.Mapping.Parent = this;
    //            this.Mapping.Initialize(ref colNo);
    //            this.ColumnNo = this.Mapping.Children[0].ColumnNo;
    //        }
    //    }

    //    public override ImportStatus ExtractIntoTarget(V target, string record)
    //    {
    //        U u = null;
    //        ImportStatus s = this.Mapping.Extract(record, ref target, ref u);
    //        if (s == ImportStatus.Success)
    //            if (null == u)
    //                return this.IsMandatory ? ImportStatus.MandatoryFieldIsEmptyOrIncorrect : s;
    //            else
    //                return this.SetTargetValue(this.RecordMapping, target, u);
    //        else
    //            return s;
    //    }
    //}

    /// Money with two digits after decimal point
    public class DecimalFieldType : TextFieldType<decimal>
    {
        private DecimalFieldType()
        {
        }

        public override decimal ConvertFromString(string valueString)
        {
            return String.IsNullOrEmpty(valueString) ? 0m : decimal.Parse(valueString);
        }

        public override string FormatFixedLengthValue(decimal value, int length)
        {
            return value.ToString().PadLeft(length, '0');
        }

        public static DecimalFieldType GetInstance()
        {
            if (null == Instance)
                Instance = new DecimalFieldType();
            return (DecimalFieldType)Instance;
        }
    }

    public class Decimal_x_100Field : TextFieldType<decimal>
    {
        private Decimal_x_100Field()
        {
        }

        public override decimal ConvertFromString(string valueString)
        {
            return String.IsNullOrEmpty(valueString) ? 0m : decimal.Parse(valueString) / 100;
        }

        public static Decimal_x_100Field GetInstance()
        {
            if (null == Instance)
                Instance = new Decimal_x_100Field();
            return (Decimal_x_100Field)Instance;
        }

        public override string FormatVariableLengthValue(decimal value, int length)
        {
            //return (value * 100).ToString();
            return (value * 100).ToString("#");
        }

        public override string FormatFixedLengthValue(decimal value, int length)
        {
            return (value * 100).ToString("#").PadLeft(length, '0');
        }
    }

    public class DoubleField : TextFieldType<double>
    {
        public int DigitsAfterDecimalPoint = 0;

        private DoubleField()
        {
        }

        public override double ConvertFromString(string valueString)
        {
            return String.IsNullOrEmpty(valueString) ? 0d : double.Parse(valueString);
        }

        public static DoubleField GetInstance()
        {
            if (null == Instance)
                Instance = new DoubleField();
            return (DoubleField)Instance;
        }

        private String format = null;

        public override string FormatFixedLengthValue(double value, int length)
        {
            if (null == this.format)
                this.format = "{0:F" + this.DigitsAfterDecimalPoint.ToString() + "}";
            return String.Format(this.format, value);
        }
    }

    public abstract class GregorianDateField : TextFieldType<DateTime>
    {
        public abstract String DataFormat { get; }

        public override string FormatVariableLengthValue(DateTime value, int length)
        {
            return value.ToString(DataFormat, CultureInfo.InvariantCulture);
        }

        public override string FormatFixedLengthValue(DateTime value, int length)
        {
            return value.ToString(DataFormat, CultureInfo.InvariantCulture);
        }
    }

    public class GregorianDate_DDMMYYField : GregorianDateField
    {
        private static new GregorianDate_DDMMYYField Instance;
        public static GregorianDate_DDMMYYField GetInstance()
        {
            if (null == Instance)
                Instance = new GregorianDate_DDMMYYField();
            return (GregorianDate_DDMMYYField)Instance;
        }

        public override DateTime ConvertFromString(string valueString)
        {
            int day = int.Parse(valueString.Substring(0, 2));
            int month = int.Parse(valueString.Substring(2, 2));
            int year = int.Parse(valueString.Substring(4, 2));
            if (year > 50)
                year += 1900;
            else
                year += 2000;
            return new DateTime(year, month, day, CultureInfo.InvariantCulture.Calendar);
        }

        public override String DataFormat
        {
            get { return "ddMMyy"; }
        }
    }

    public class GregorianDate_DDMMYYYYField : GregorianDateField
    {
        private static new GregorianDate_DDMMYYYYField Instance;
        public static GregorianDate_DDMMYYYYField GetInstance()
        {
            if (null == Instance)
                Instance = new GregorianDate_DDMMYYYYField();
            return (GregorianDate_DDMMYYYYField)Instance;
        }

        public override DateTime ConvertFromString(string valueString)
        {
            int day = int.Parse(valueString.Substring(0, 2));
            int month = int.Parse(valueString.Substring(2, 2));
            int year = int.Parse(valueString.Substring(4, 4));
            return new DateTime(year, month, day, CultureInfo.InvariantCulture.Calendar);
        }

        public override String DataFormat
        {
            get { return "ddMMyyyy"; }
        }
    }

    public class GregorianDate_YYMMDDField : GregorianDateField
    {
        private GregorianDate_YYMMDDField()
        {
        }

        private static new GregorianDate_YYMMDDField Instance;
        public static GregorianDate_YYMMDDField GetInstance()
        {
            if (null == Instance)
                Instance = new GregorianDate_YYMMDDField();
            return (GregorianDate_YYMMDDField)Instance;
        }

        public override DateTime ConvertFromString(string valueString)
        {
            int day = int.Parse(valueString.Substring(4, 2));
            int month = int.Parse(valueString.Substring(2, 2));
            int year = int.Parse(valueString.Substring(0, 2));
            if (year > 50)
                year += 1900;
            else
                year += 2000;
            return new DateTime(year, month, day, CultureInfo.InvariantCulture.Calendar);
        }

        public override String DataFormat
        {
            get { return "yyMMdd"; }
        }
    }

    public class GregorianDate_YYYYMMDDField : GregorianDateField
    {
        private GregorianDate_YYYYMMDDField()
        {
        }

        public override DateTime ConvertFromString(string valueString)
        {
            int day = int.Parse(valueString.Substring(6, 2));
            int month = int.Parse(valueString.Substring(4, 2));
            int year = int.Parse(valueString.Substring(0, 4));
            return new DateTime(year, month, day, CultureInfo.InvariantCulture.Calendar);
        }

        private static new GregorianDate_YYYYMMDDField Instance;
        public static GregorianDate_YYYYMMDDField GetInstance()
        {
            if (null == Instance)
                Instance = new GregorianDate_YYYYMMDDField();
            return (GregorianDate_YYYYMMDDField)Instance;
        }

        public override String DataFormat
        {
            get { return "yyyyMMdd"; }
        }
    }

    public class GregorianDateTime_YYYYMMDDHHmmssField : GregorianDateField
    {
        private GregorianDateTime_YYYYMMDDHHmmssField()
        {
        }

        public override DateTime ConvertFromString(string valueString)
        {
            int year = int.Parse(valueString.Substring(0, 4));
            int month = int.Parse(valueString.Substring(4, 2));
            int day = int.Parse(valueString.Substring(6, 2));
            int hour = int.Parse(valueString.Substring(8, 2));
            int minutes = int.Parse(valueString.Substring(10, 2));
            int seconds = int.Parse(valueString.Substring(12, 2));
            return new DateTime(year, month, day, hour, minutes, seconds, CultureInfo.InvariantCulture.Calendar);
        }

        private static new GregorianDateTime_YYYYMMDDHHmmssField Instance;
        public static GregorianDateTime_YYYYMMDDHHmmssField GetInstance()
        {
            if (null == Instance)
                Instance = new GregorianDateTime_YYYYMMDDHHmmssField();
            return (GregorianDateTime_YYYYMMDDHHmmssField)Instance;
        }

        public override String DataFormat
        {
            get { return "yyyyMMddHHmmss"; }
        }
    }

    public class IntegerField : TextFieldType<int>
    {
        private IntegerField()
        {
        }

        public override int ConvertFromString(string valueString)
        {
            return String.IsNullOrEmpty(valueString) ? 0 : int.Parse(valueString);
        }

        public override string FormatFixedLengthValue(int value, int length)
        {
            return value.ToString().PadLeft(length, '0');
        }

        public static IntegerField GetInstance()
        {
            if (null == Instance)
                Instance = new IntegerField();
            return (IntegerField)Instance;
        }
    }

    public class LongField : TextFieldType<long>
    {
        private LongField()
        {
        }

        public override long ConvertFromString(string valueString)
        {
            return String.IsNullOrEmpty(valueString) ? 0 : long.Parse(valueString);
        }

        public override string FormatFixedLengthValue(long value, int length)
        {
            return value.ToString().PadLeft(length, '0');
        }

        public static LongField GetInstance()
        {
            if (null == Instance)
                Instance = new LongField();
            return (LongField)Instance;
        }
    }

    public class FillerField : TextFieldType<string>
    {
        public FillerField(string fillerString)
        {
            this.Length = fillerString.Length;
            this.FillerString = fillerString;
        }

        public FillerField(char fillerChar, int length)
        {
            this.FillerChar = fillerChar;
            this.Length = length;
            this.FillerString = new string(this.FillerChar, this.Length);
        }

        public override string ConvertFromString(string valueString)
        {
            return this.FillerString;
        }

        public virtual char FillerChar { get; set; }

        public virtual int Length { get; set; }

        public readonly string FillerString;

        public override string FormatFixedLengthValue(string value, int length)
        {
            return this.FillerString;
        }
    }

    public class StringField : TextFieldType<string>
    {
        private StringField()
        {
        }

        public override string ConvertFromString(string valueString)
        {
            return valueString;
        }

        public override string FormatFixedLengthValue(string value, int length)
        {
            if (String.IsNullOrEmpty(value))
                return new string(' ', length);
            else
                return value.Length > length ? value.Substring(0, length) : value.PadRight(length, ' ');
        }

        public static StringField GetInstance()
        {
            if (null == Instance)
                Instance = new StringField();
            return (StringField)Instance;
        }
    }

    public abstract class ThaiDateField : TextFieldType<DateTime>
    {
        public abstract String DataFormat { get; }

        public override string FormatVariableLengthValue(DateTime value, int length)
        {
            return value.ToString(DataFormat);
        }

        public override string FormatFixedLengthValue(DateTime value, int length)
        {
            return value.ToString(DataFormat);
        }
    }

    public class ThaiDate_DDMMYYField : ThaiDateField
    {
        private ThaiDate_DDMMYYField()
        {
        }

        public override DateTime ConvertFromString(string valueString)
        {
            int day = int.Parse(valueString.Substring(0, 2));
            int month = int.Parse(valueString.Substring(2, 2));
            int year = int.Parse(valueString.Substring(4, 2)) + 2500 - 543;
            return (new DateTime(year, month, day, CultureInfo.InvariantCulture.Calendar));
        }

        private static new ThaiDate_DDMMYYField Instance;
        public static ThaiDate_DDMMYYField GetInstance()
        {
            if (null == Instance)
                Instance = new ThaiDate_DDMMYYField();
            return (ThaiDate_DDMMYYField)Instance;
        }

        public override String DataFormat
        {
            get { return "ddMMyy"; }
        }
    }

    public class ThaiDate_DDMMYYYYField : ThaiDateField
    {
        private ThaiDate_DDMMYYYYField()
        {
        }

        public override DateTime ConvertFromString(string valueString)
        {
            int day = int.Parse(valueString.Substring(0, 2));
            int month = int.Parse(valueString.Substring(2, 2));
            int year = int.Parse(valueString.Substring(4, 4)) - 543;
            return (new DateTime(year, month, day, CultureInfo.InvariantCulture.Calendar));
        }

        private static new ThaiDate_DDMMYYYYField Instance;
        public static ThaiDate_DDMMYYYYField GetInstance()
        {
            if (null == Instance)
                Instance = new ThaiDate_DDMMYYYYField();
            return (ThaiDate_DDMMYYYYField)Instance;
        }

        public override String DataFormat
        {
            get { return "ddMMyyyy"; }
        }
    }

    public class ThaiDate_YYYYMMDDField : ThaiDateField
    {
        private ThaiDate_YYYYMMDDField()
        {
        }

        public override DateTime ConvertFromString(string valueString)
        {
            int day = int.Parse(valueString.Substring(6, 2));
            int month = int.Parse(valueString.Substring(4, 2));
            int year = int.Parse(valueString.Substring(0, 4)) - 543;
            return (new DateTime(year, month, day, CultureInfo.InvariantCulture.Calendar));
        }

        private static new ThaiDate_YYYYMMDDField Instance;
        public static ThaiDate_YYYYMMDDField GetInstance()
        {
            if (null == Instance)
                Instance = new ThaiDate_YYYYMMDDField();
            return (ThaiDate_YYYYMMDDField)Instance;
        }

        public override String DataFormat
        {
            get { return "yyyyMMdd"; }
        }
    }

    public class Time_HHMMSSField : TextFieldType<TimeSpan>
    {
        private Time_HHMMSSField()
        {
        }

        public override TimeSpan ConvertFromString(string valueString)
        {
            int hour = int.Parse(valueString.Substring(0, 2));
            int minute = int.Parse(valueString.Substring(2, 2));
            int second = int.Parse(valueString.Substring(4, 2));
            return new TimeSpan(hour, minute, second);
        }

        public override string FormatVariableLengthValue(TimeSpan value, int length)
        {
            return value.ToString(DataFormat, CultureInfo.GetCultureInfo("th-TH"));
        }

        public override string FormatFixedLengthValue(TimeSpan value, int length)
        {
            return value.ToString(DataFormat, CultureInfo.GetCultureInfo("th-TH"));
        }

        public static Time_HHMMSSField GetInstance()
        {
            if (null == Instance)
                Instance = new Time_HHMMSSField();
            return (Time_HHMMSSField)Instance;
        }

        public const String DataFormat = "HHmmss";
    }

    #endregion Field Classes
}
