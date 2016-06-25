using System;
using BizPortal;
using Controller;
using DevExpress.Web.ASPxEditors;
using iSabaya;

namespace WebHelper.ServiceLayer
{
    /// <summary>
    /// Class ExtensionMethods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// <other name="Itsada Jitchot" date="2013/10/26"/>
        /// Method Convert DateTime to string and set default format(dd/MM/yyyy HH:mm) from Service class
        /// if DateTime equal MinDate return string Empty
        /// other return dateTme form defaul format
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string of DateTime</returns>
        /// <unitTests>
        /// <unitTest id="1" input="10000101" output="Empty string"/>
        /// <unitTest id="2" input="10000101 130000" output="Empty string"/>
        /// <unitTest id="3" input="20131026" output="26/10/2013 00:00"/>
        /// <unitTest id="4" input="20131026 000000" output="26/10/2013 00:00"/>
        /// <unitTest id="5" input="20131026 130000" output="26/10/2013 13:00"/>
        /// </unitTests>
        //public static string DateTimeFormat(this DateTime value)
        //{
        //    return value == DateTime.MinValue ? "" : value.ToString(Service.DateTimeFormat);
        //}

        /// <summary>
        /// <other name="Itsada Jitchot" date="2013/10/26"></other>
        /// Method Convert TimeInterval to string and set default format(dd/MM/yyyy HH:mm) from Service class
        /// if TimeInterval null or form of TimeInterval equal MinDate return string Empty
        /// other return dateTme form defaul format
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string of DateTime</returns>
        /// <unitTests>
        /// <unitTest id="1" input="" output="Empty string"/>
        /// <unitTest id="2" input="[10000101 130000, 10000101 130000], dd/MM/yyyy HH:mm" output="Empty string"/>
        /// <unitTest id="3" input="[10000101 130000, 23001231 130000], dd/MM/yyyy HH:mm" output="Empty string"/>
        /// <unitTest id="4" input="[20131026 000000, 23001231 130000], dd/MM/yyyy HH:mm" output="26/10/2013 00:00"/>
        /// <unitTest id="5" input="[20131026 130000, 23001231 130000], dd/MM/yyyy HH:mm" output="26/10/2013 13:00"/>
        /// </unitTests>
        //public static string EffectivePeriodFrom(this TimeInterval value)
        //{
        //    if (value == null)
        //        return "";
        //    return value.From == DateTime.MinValue ? "" : value.From.ToString(Service.DateTimeFormat);
        //}

        /// <summary>
        /// <other name="Itsada Jitchot" date="2013/10/26"></other>
        /// Method Convert TimeInterval to string and set default format(dd/MM/yyyy HH:mm) from Service class
        /// if TimeInterval null or to of TimeInterval equal MinDate return string Empty
        /// other return dateTme form defaul format
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string of DateTime</returns>
        /// <unitTests>
        /// <unitTest id="1" input="" output="Empty string"/>
        /// <unitTest id="2" input="[10000101 130000, 10000101 130000], dd/MM/yyyy HH:mm" output="Empty string"/>
        /// <unitTest id="3" input="[20131026 130000, 10000101 130000], dd/MM/yyyy HH:mm" output="Empty string"/>
        /// <unitTest id="4" input="[20131026 000000, 23001231 000000], dd/MM/yyyy HH:mm" output="31/12/2300 00:00"/>
        /// <unitTest id="5" input="[20131026 130000, 23001231 130000], dd/MM/yyyy HH:mm" output="31/12/2300 13:00"/>
        /// </unitTests>
        //public static string EffectivePeriodTo(this TimeInterval value)
        //{
        //    if (value == null)
        //        return "";
        //    return value.To == DateTime.MinValue ? "" : value.To.ToString(Service.DateTimeFormat);
        //}

        //public static string UserActionLoginName(this UserAction value)
        //{
        //    if (value == null)
        //        return "";
        //    return value.ByUser == null ? "" : value.ByUser.LoginName;
        //}

        //public static string UserActionTimeStamp(this UserAction value)
        //{
        //    if (value == null)
        //        return "";
        //    return value.Timestamp.DateTimeFormat();
        //}

        //public static string UserLoginName(this User value)
        //{
        //    return value == null ? "" : value.LoginName;
        //}

        //#region DateTime

        ///// <summary>
        ///// <other name="Itsada Jitchot" date="2013/10/26"/>
        ///// Method Convert DateTime to string and set default format(dd/MM/yyyy) from Service class
        ///// if DateTime equal MinDate return string Empty
        ///// other return date form defaul format
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns>string of Date</returns>
        ///// <unitTests>
        ///// <unitTest id="1" input="10000101" output="Empty string"/>
        ///// <unitTest id="2" input="20131026" output="26/10/2013"/>
        ///// </unitTests>
        //public static string DateFormat(this DateTime value)
        //{
        //    return value == DateTime.MinValue ? "" : value.ToString(Service.DateFormat);
        //}

        ///// <summary>
        ///// <other name="Itsada Jitchot" date="2013/10/26"/>
        ///// Method Convert DateTime to string and set format
        ///// if DateTime equal  return string Empty
        ///// other return date form format
        ///// </summary>
        ///// <param name="value"></param>
        ///// <param name="format"> </param>
        ///// <returns>string of Date</returns>
        ///// <unitTests>
        ///// <unitTest id="1" input="10000101, dd/mm/yyyy" output="Empty string"/>
        ///// <unitTest id="2" input="10000101, dd-mm-yyyy" output="Empty string"/>
        ///// <unitTest id="3" input="20131026, dd/mm/yyyy" output="26/10/2013"/>
        ///// <unitTest id="4" input="20131026, dd-mm-yyyy" output="26-10-2013"/>
        ///// <unitTest id="5" input="20131026, yyyy-mm-dd" output="2013-10-26"/>
        ///// </unitTests>
        //public static string DateFormat(this DateTime value, string format)
        //{
        //    return value == DateTime.MinValue ? "" : value.ToString(format);
        //}

        ///// <summary>
        ///// <other name="Itsada Jitchot" date="2013/10/26"/>
        ///// Method Convert DateTime to string and set default format(dd/MM/yyyy HH:mm) from Service class
        ///// if DateTime equal MinDate return string Empty
        ///// other return dateTme form defaul format
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns>string of DateTime</returns>
        ///// <unitTests>
        ///// <unitTest id="1" input="10000101" output="Empty string"/>
        ///// <unitTest id="2" input="10000101 130000" output="Empty string"/>
        ///// <unitTest id="3" input="20131026" output="26/10/2013 00:00"/>
        ///// <unitTest id="4" input="20131026 000000" output="26/10/2013 00:00"/>
        ///// <unitTest id="5" input="20131026 130000" output="26/10/2013 13:00"/>
        ///// </unitTests>
        //public static string DateTimeFormat(this DateTime value)
        //{
        //    return value == DateTime.MinValue ? "" : value.ToString(Service.DateTimeFormat);
        //}

        ///// <summary>
        ///// <other name="Itsada Jitchot" date="2013/10/26"/>
        ///// Method Convert DateTime to string and set format
        ///// if DateTime equal MinDate return string Empty
        ///// other return dateTme form format
        ///// </summary>
        ///// <param name="value"></param>
        ///// <param name="format"></param>
        ///// <returns>string of DateTime</returns>
        ///// <unitTests>
        ///// <unitTest id="1" input="10000101, dd/MM/yyyy HH:mm" output="Empty string"/>
        ///// <unitTest id="2" input="10000101 130000, dd/MM/yyyy HH:mm" output="Empty string"/>
        ///// <unitTest id="3" input="20131026, dd/MM/yyyy HH:mm" output="26/10/2013 00:00"/>
        ///// <unitTest id="4" input="20131026 000000, dd/MM/yyyy HH:mm" output="26/10/2013 00:00"/>
        ///// <unitTest id="5" input="20131026 130000, dd/MM/yyyy HH:mm" output="26/10/2013 13:00"/>
        ///// </unitTests>
        //public static string DateTimeFormat(this DateTime value, string format)
        //{
        //    return value == DateTime.MinValue ? "" : value.ToString(format);
        //}

        //#endregion DateTime

        //#region  TimeInterval

        ///// <summary>
        ///// <other name="Itsada Jitchot" date="2013/10/26"></other>
        ///// Method Convert TimeInterval to string and set default format(dd/MM/yyyy HH:mm) from Service class
        ///// if TimeInterval null or form of TimeInterval equal MinDate return string Empty
        ///// other return dateTme form defaul format
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns>string of DateTime</returns>
        ///// <unitTests>
        ///// <unitTest id="1" input="" output="Empty string"/>
        ///// <unitTest id="2" input="[10000101 130000, 10000101 130000], dd/MM/yyyy HH:mm" output="Empty string"/>
        ///// <unitTest id="3" input="[10000101 130000, 23001231 130000], dd/MM/yyyy HH:mm" output="Empty string"/>
        ///// <unitTest id="4" input="[20131026 000000, 23001231 130000], dd/MM/yyyy HH:mm" output="26/10/2013 00:00"/>
        ///// <unitTest id="5" input="[20131026 130000, 23001231 130000], dd/MM/yyyy HH:mm" output="26/10/2013 13:00"/>
        ///// </unitTests>
        //public static string EffectivePeriodFrom(this TimeInterval value)
        //{
        //    if (value == null)
        //        return "";
        //    return value.From == DateTime.MinValue ? "" : value.From.ToString(Service.DateTimeFormat);
        //}

        ///// <summary>
        ///// <other name="Itsada Jitchot" date="2013/10/26"></other>
        ///// Method Convert TimeInterval to string and set default format(dd/MM/yyyy HH:mm) from Service class
        ///// if TimeInterval null or to of TimeInterval equal MinDate return string Empty
        ///// other return dateTme form defaul format
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns>string of DateTime</returns>
        ///// <unitTests>
        ///// <unitTest id="1" input="" output="Empty string"/>
        ///// <unitTest id="2" input="[10000101 130000, 10000101 130000], dd/MM/yyyy HH:mm" output="Empty string"/>
        ///// <unitTest id="3" input="[20131026 130000, 10000101 130000], dd/MM/yyyy HH:mm" output="Empty string"/>
        ///// <unitTest id="4" input="[20131026 000000, 23001231 000000], dd/MM/yyyy HH:mm" output="31/12/2300 00:00"/>
        ///// <unitTest id="5" input="[20131026 130000, 23001231 130000], dd/MM/yyyy HH:mm" output="31/12/2300 13:00"/>
        ///// </unitTests>
        //public static string EffectivePeriodTo(this TimeInterval value)
        //{
        //    if (value == null)
        //        return "";
        //    return value.To == DateTime.MinValue ? "" : value.To.ToString(Service.DateTimeFormat);
        //}

        //#endregion

        //#region string
        //public static bool ContainsNotNull(this string value, string find)
        //{
        //    return value != null && value.Contains(find);
        //}

        //#endregion

        //#region UserAction

        //public static string UserActionLoginName(this UserAction value)
        //{
        //    if (value == null)
        //        return "";
        //    return value.ByUser == null ? "" : value.ByUser.LoginName;
        //}

        //public static string UserActionTimeStamp(this UserAction value)
        //{
        //    if (value == null)
        //        return "";
        //    return value.Timestamp.DateTimeFormat();
        //}

        //#endregion UserAction

        //#region User

        //public static string UserLoginName(this User value)
        //{
        //    return value == null ? "" : value.LoginName;
        //}

        //#endregion User

        //#region BizPortalTransactionState

        //public static MemberFunction BizPortalTransactionStateMemberFunction(this BizPortalTransactionState value)
        //{
        //    if (value == null)
        //        return null;
        //    if (value.Transaction == null)
        //        return null;
        //    return value.Transaction.CreatorWorkflow == null ? null : value.Transaction.CreatorWorkflow.MemberFunction;
        //}

        //#endregion BizPortalTransactionState

        #region Devexpress

        //#region TextBox

        ///// <summary>
        ///// Method Convert TextBox to Money and valid null
        ///// if textBox null return Money(0m)
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static Money ToMoney(this ASPxTextBox value)
        //{
        //    if (value == null)
        //        return new Money(0m);
        //    return string.IsNullOrEmpty(value.Text) ? new Money(0m) : new Money(decimal.Parse(value.Text));
        //}

        ///// <summary>
        ///// Method Convert TextBox to string format #,##0.#0
        ///// if textBox null return ""
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static string ToMoneyString(this ASPxTextBox value)
        //{
        //    if (value == null)
        //        return "";
        //    return string.IsNullOrEmpty(value.Text) ? "" : new Money(decimal.Parse(value.Text)).Amount.ToString(Service.MoneyFormat);
        //}

        ///// <summary>
        ///// Method Convert TextBox to string
        ///// </summary>
        ///// <param name="value"></param>
        ///// <param name="format"></param>
        ///// <returns></returns>
        //public static string ToMoneyString(this ASPxTextBox value, string format)
        //{
        //    if (value == null)
        //        return "";
        //    return string.IsNullOrEmpty(value.Text) ? "" : new Money(decimal.Parse(value.Text)).Amount.ToString(format);
        //}

        //public static decimal ToDecimal(this ASPxTextBox value)
        //{
        //    if (value == null)
        //        return 0m;
        //    return string.IsNullOrEmpty(value.Text) ? 0m : decimal.Parse(value.Text);
        //}

        //public static string ToDecimalString(this ASPxTextBox value)
        //{
        //    if (value == null)
        //        return "";
        //    return string.IsNullOrEmpty(value.Text) ? "" : decimal.Parse(value.Text).ToString(Service.MoneyFormat);
        //}

        //public static string ToDecimalString(this ASPxTextBox value, string format)
        //{
        //    if (value == null)
        //        return "";
        //    return string.IsNullOrEmpty(value.Text) ? "" : decimal.Parse(value.Text).ToString(format);
        //}

        //#endregion TextBox

        //#region SpinEdit

        //public static Money ToMoney(this ASPxSpinEdit value)
        //{
        //    if (value == null)
        //        return null;
        //    return string.IsNullOrEmpty(value.Text) ? null : new Money(decimal.Parse(value.Text));
        //}

        //public static Money ToMoney(this string value)
        //{
        //    if (value == null)
        //        return new Money(0m);
        //    return new Money(decimal.Parse(value));
        //}

        //public static string ToMoneyString(this ASPxSpinEdit value)
        //{
        //    if (value == null)
        //        return "";
        //    return string.IsNullOrEmpty(value.Text) ? "" : new Money(decimal.Parse(value.Text)).Amount.ToString(Service.MoneyFormat);
        //}

        //public static decimal ToDecimal(this ASPxSpinEdit value)
        //{
        //    if (value == null)
        //        return 0m;
        //    return string.IsNullOrEmpty(value.Text) ? 0m : decimal.Parse(value.Text);
        //}

        //public static string ToDecimalString(this ASPxSpinEdit value)
        //{
        //    if (value == null)
        //        return "";
        //    return string.IsNullOrEmpty(value.Text) ? "" : decimal.Parse(value.Text).ToString(Service.MoneyFormat);
        //}

        //#endregion SpinEdit

        #endregion Devexpress

        #region Money
        //public static string ToMoney(this Money value)
        //{
        //    return value == null ? null : new Money(value).Amount.ToString(Service.MoneyFormat);
        //}

        //public static string ToMoney(this decimal value)
        //{
        //    return value.ToString(Service.MoneyFormat);
        //}
        #endregion Money

        public static ListEditItem FindByTextValue(this ListEditItemCollection listEditItemCollection, string textValue)
        {
            foreach (ListEditItem lei in listEditItemCollection)
            {
                if (lei.ToString().ToLower().Contains(textValue.ToLower()))
                    return lei;
            }

            return null;
        }
    }
}