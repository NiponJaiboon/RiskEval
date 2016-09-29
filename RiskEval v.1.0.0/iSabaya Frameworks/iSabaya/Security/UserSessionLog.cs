using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace iSabaya
{
    [Serializable]
    public class UserSessionLog
    {
        public UserSessionLog()
        {
        }

        //public UserSessionLog(long userSessionID, int functionID, int pageID, int menuID, string action, string message)
        //{
        //    //this.UserSessionID = userSessionID;
        //    this.FunctionID = functionID;
        //    this.PageID = pageID;
        //    this.MenuID = menuID;
        //    this.Timestamp = DateTime.Now;
        //    this.Action = action;
        //    this.Message = message;
        //}

        public UserSessionLog(UserSession userSession, int functionID, int pageID, int menuID, string action, string message)
        {
            this.UserSession = userSession;
            this.FunctionID = functionID;
            this.PageID = pageID;
            this.MenuID = menuID;
            this.Timestamp = DateTime.Now;
            this.Action = action;
            this.Message = message;
        }

        #region persistent

        public virtual long ID { get; protected set; }
        public virtual int FunctionID { get; set; }
        public virtual int PageID { get; set; }
        public virtual int MenuID { get; set; }
        public virtual DateTime Timestamp { get; set; }
        public virtual string Action { get; set; }
        public virtual string Message { get; set; }
        //public virtual long UserSessionID { get; set; }

        public virtual UserSession UserSession { get; set; }

        #endregion persistent

    }
}
