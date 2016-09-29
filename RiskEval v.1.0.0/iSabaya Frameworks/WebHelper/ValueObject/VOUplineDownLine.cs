using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using iSabaya;
using imSabaya.MutualFundSystem;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VOUplineDownLine
    { 
      
        IPRelation ipr;
        bool isDownlline = true;
        public VOUplineDownLine(IPRelation ipr)
        {
            this.ipr = ipr;
        }
        public VOUplineDownLine(IPRelation ipr, bool isDownlline)
        {
            this.ipr = ipr;
            this.isDownlline = false;
        }
        public String PersonName
        {
            get {
                if (isDownlline)
                {
                    return ipr.Downline.Person.FullName;
                }
                else
                {
                    return ipr.Upline.Person.FullName;
                }
            }          
        }
        public int ID
        {
            get {
                if (isDownlline)
                {
                    return ipr.Downline.Person.PersonID;
                }
                else
                {
                    return ipr.Upline.Person.PersonID;
                }
            }           
        }
       
        //downline
        public InvestmentPlanner Ip
        {
            get
            {
                if (isDownlline)
                { return ipr.Downline; }
                else
                {
                    return ipr.Upline;
                }
            }
          
        }
        public int ParentId
        {
            get { return ipr.Upline.Person.PersonID; }
          
        }
        public String LicenseNo
        {
            get
            {
                if (isDownlline)
                { return ipr.Downline.LicenseNo; }
                else
                {
                    return ipr.Upline.LicenseNo;
                }
            }
         
        }
      
        public TimeInterval EffectivePeriod
        {
            get { return ipr.EffectivePeriod; }
           
        }


    }
}
