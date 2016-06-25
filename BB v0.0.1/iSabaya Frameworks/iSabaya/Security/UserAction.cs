using System;
using System.Xml.Serialization;

namespace iSabaya
{
    [Serializable]
    public class UserAction
    {
        public UserAction()
        {
        }

        public UserAction(User user)
        {
            this.ByUser = user;
            this.Timestamp = DateTime.Now;
        }

        public UserAction(User user, DateTime timestamp)
        {
            this.ByUser = user;
            this.Timestamp = timestamp;
        }

        public UserAction(User user, string remark)
        {
            this.ByUser = user;
            this.Remark = remark;
            this.Timestamp = DateTime.Now;
        }


        [XmlIgnore]
        public virtual User ByUser { get; set; }

        [XmlIgnore]
        public long userID;

        public virtual long UserID
        {
            get
            {
                if (null == ByUser)
                    return this.userID;
                else
                    return this.ByUser.ID;
            }
            set { this.userID = value; }
        }

        public virtual DateTime Timestamp { get; set; }

        public virtual string Remark { get; set; }
    }
}
