using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using iSabaya;
using NHibernate;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOUser
    {
        private User instance;
        public VOUser(User instance)
        {
            this.instance = instance;
        }

        public int UserID
        {
            get { return instance.UserID; }
        }

        public string UserName
        {
            get { return instance.UserName; }
        }

        public string Person
        {
            get
            {
                if (instance.Person == null)
                    return "-";
                else
                    return instance.Person.ToString();
            }
        }

        public string CurrentPassword
        {
            get
            {
                if (instance.CurrentPassword == null)
                    return "-";
                else
                    return instance.CurrentPassword.ToString();
            }
        }
    }
}
