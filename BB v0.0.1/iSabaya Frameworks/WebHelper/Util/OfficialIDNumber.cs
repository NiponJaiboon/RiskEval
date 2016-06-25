using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using BizPortal;
using iSabaya;
using NHibernate.Criterion;

namespace WebHelper.Util
{
    public class OfficialIDNumber
    {
        public const string InvalidIDNumberMessage = "เลขที่บัตรประจำตัวประชาชน ไม่ถูกต้อง";
        public const string DuplicateIDNumberMessage = "เลขที่บัตรประจำตัวประชาชน ถูกใช้งานแล้ว";
        public const string UndefinedIDNumberMessage = "เลขที่บัตรประจำตัวประชาชนซ้ำ/ถูกใช้งานแล้ว";

        /// <summary>
        /// Check ID Number for Thailand
        /// </summary>
        /// <param name="number">ID Number</param>
        /// <returns>True if ID Number correct. Otherwise False</returns>
        public static bool IsValidIDNumber(string number)
        {
            try
            {
                int result = 0;
                int digits13th = 0;
                char[] arrayNumber = number.ToCharArray();

                int m1 = int.Parse(arrayNumber[0].ToString()) * 13;
                int m2 = int.Parse(arrayNumber[1].ToString()) * 12;
                int m3 = int.Parse(arrayNumber[2].ToString()) * 11;
                int m4 = int.Parse(arrayNumber[3].ToString()) * 10;
                int m5 = int.Parse(arrayNumber[4].ToString()) * 9;
                int m6 = int.Parse(arrayNumber[5].ToString()) * 8;
                int m7 = int.Parse(arrayNumber[6].ToString()) * 7;
                int m8 = int.Parse(arrayNumber[7].ToString()) * 6;
                int m9 = int.Parse(arrayNumber[8].ToString()) * 5;
                int m10 = int.Parse(arrayNumber[9].ToString()) * 4;
                int m11 = int.Parse(arrayNumber[10].ToString()) * 3;
                int m12 = int.Parse(arrayNumber[11].ToString()) * 2;
                result = m1 + m2 + m3 + m4 + m5 + m6 + m7 + m8 + m9 + m10 + m11 + m12;

                digits13th = 11 - (result % 11);
                digits13th = digits13th % 10;
                if (digits13th.ToString() == arrayNumber[12].ToString())
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Check OfficialIDNo (ID Number) is existed in database
        /// </summary>
        /// <param name="context">Web Application Context</param>
        /// <param name="number">OfficialIDNo (ID Number)</param>
        /// <returns>True if OfficialIDNo (ID Number) existed. Otherwise False</returns>
        public static bool IsExistedIDNumber(Context context, string number)
        {
            try
            {
                //int numPerson = context.PersistenceSession.CreateCriteria<Person>()
                //.Add(Expression.Eq("OfficialIDNo", number))
                //.List<Person>().Count;

                //if (numPerson > 0)
                //    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Check ID Number is usable
        /// </summary>
        /// <param name="context">Web Application Context</param>
        /// <param name="number">OfficialIDNo (ID Number)</param>
        /// <returns>True if OfficialIDNo (ID Number) usable. Otherwise False</returns>
        public static bool IsUsableIDNumber(Context context, string number)
        {
            return (IsValidIDNumber(number) && !IsExistedIDNumber(context, number));
        }
    }
}