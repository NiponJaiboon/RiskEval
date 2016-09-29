using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public static class Extensions
    {
        public static Money Clone(this Money original)
        {
            if (null == original)
                return null;
            else
                return new Money(original);
        }

        public static TimeInterval Clone(this TimeInterval m)
        {
            if (m == null)
                return m;
            else
                return new TimeInterval(m);
        }

        public static bool IsEndOfMonth(this DateTime date)
        {
            return date.Day == DateTime.DaysInMonth(date.Year, date.Month);
        }

        public static bool IsEffectiveAtTheEndOfToday(this TimeInterval m)
        {
            DateTime today = DateTime.Today;
            DateTime endOfToday = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59);
            return m.From <= endOfToday && endOfToday <= m.To;
        }

        public static bool IsEffectiveOn(this TimeInterval m, DateTime datetime)
        {
            return m == null ? false : m.From <= datetime && datetime <= m.To;
        }

        public static bool IsEffective(this TimeInterval m)
        {
            if (m == null)
                return false;
            else
            {
                DateTime today = DateTime.Now;
                return m.From <= today && today <= m.To;
            }
        }

        public static bool IncludesTimeInstant(this TimeInterval m, DateTime dateTimeInstant)
        {
            if (m == null)
                return false;
            else
                return m.From <= dateTimeInstant && dateTimeInstant <= m.To;
        }

        public static bool IsNullOrZero(this Money m)
        {
            return (null == m || m.Amount == 0m);
        }

        public static String ToString(this int[] intArray, char dilimiter)
        {
            if (null == intArray) return null;

            char[] Dilimiter = new char[] { dilimiter };
            StringBuilder intString = new StringBuilder();
            bool notFirst = false;
            foreach (int seqNo in intArray)
            {
                if (notFirst)
                    intString.Append(dilimiter);
                intString.Append(seqNo.ToString());
                notFirst = true;
            }
            return intString.ToString();
        }

        public static IList<PersonOrgRelation> FindCurrentEmployments(this Person person, Context context)
        {
            IList<PersonOrgRelation> employments;

            TreeListNode employmentType = GetEmploymentCategory(context);
            if (null == employmentType)
                employments = new List<PersonOrgRelation>();
            else
                employments = PersonOrgRelation.List(context, employmentType, person, DateTime.Now);
            return employments;
        }

        public static TreeListNode GetEmploymentCategory(Context context)
        {
            TreeListNode employmentType = context.Configuration.Organization
                                                .PersonRelationshipCategoryRootNode
                                                .GetChild(iSabayaConstants.PersonOrgRelationshipCodeEmployee);
            return employmentType;
        }

        public static String GetUserInfo(this User user, Context context)
        {
            StringBuilder name = new StringBuilder();
            //Name info
            if (null == user.Person)
            {
                if (null != user.Name)
                    name.Append(user.Name.ToString());
                else
                    name.Append(user.LoginName);
            }
            else
                name.Append(user.Person.FullName);

            //Org info
            if (null != user.Organization)
            {
                name.Append(", ");
                name.Append(user.Organization.CurrentName.ToString()); //Edit By Watchara
            }
            else if (null != user.Person)
            {
                foreach (PersonOrgRelation employer in user.Person.FindCurrentEmployments(context))
                {
                    name.Append(", ");
                    name.Append(employer.Organization.ToString());
                    if (null != employer.OrgUnit)
                    {
                        name.Append("-");
                        name.Append(employer.OrgUnit.ToString());
                    }
                }
            }
            return name.ToString();
        }

        //public static String GetUserInfo(this SelfAuthenticatedUser user, Context context)
        //{
        //    StringBuilder name = new StringBuilder(user.Name.ToString());
        //    foreach (PersonOrgRelation employer in user.Person.FindCurrentEmployments(context))
        //    {
        //        name.Append(", ");
        //        name.Append(employer.Organization.ToString());
        //        if (null != employer.OrgUnit)
        //        {
        //            name.Append("-");
        //            name.Append(employer.OrgUnit.ToString());
        //        }
        //    }
        //    return name.ToString();
        //}

        public static void Persist(this MultilingualString ms, Context context)
        {
            context.Persist(ms);
            foreach (MLSValue v in ms.Values)
            {
                v.Owner = ms;
                context.Persist(v);
            }
        }

        public static Money ToMoney(this MoneyBuilder mb)
        {
            return null == mb ? null : mb.ToMoneyIfNotEmpty();
        }

        public static void Update(this MultilingualString ms, Context context)
        {
            foreach (MLSValue v in ms.Values)
            {
                context.Persist(v);
            }
        }
    }
}
