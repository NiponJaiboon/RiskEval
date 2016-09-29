using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;
namespace WebHelper.pvd.BookAccountLeave
{
    public class HelperBookAccountLeave
    {
        private Dictionary<String, object> terminationsDict;

        public List<VOItem> Vos
        {
            get
            {
                List<VOItem> list = new List<VOItem>();
                list.AddRange(voItemsDeceased.ToArray());
                list.AddRange(voItemsDisable.ToArray());
                list.AddRange(voItemsDischarged.ToArray());
                list.AddRange(voItemsDismissed.ToArray());
                list.AddRange(voItemsCorrupted.ToArray());
                list.AddRange(voItemsTerminated.ToArray());
                list.AddRange(voItemsBusinessClosed.ToArray());
                list.AddRange(voItemsExpelled.ToArray());
                list.AddRange(voItemsExtEmployerTransferred.ToArray());
                list.AddRange(voItemsIntEmployerTransferred.ToArray());
                list.AddRange(voItemsResignIllegally.ToArray());
                list.AddRange(voItemsResign.ToArray());
                list.AddRange(voItemsRetired.ToArray());
                list.AddRange(voItemsQuitMembership.ToArray());
                list.AddRange(voItemsTransferredOut.ToArray());
                return list;
            }

        }

        //public const String MemberStatusCodeDeceased = "I-Deceased"; //เสียชีวิต
        private List<VOSub_Dead> voItemsDeceased = new List<VOSub_Dead>();
        public List<VOSub_Dead> VoItemsDeceased
        {
            get { return voItemsDeceased; }
            set { this.voItemsDeceased = value; }
        }
        //public const String MemberStatusCodeDisable = "H-Disable"; //ทุพลภาพ
        private List<VOSub_Retire> voItemsDisable = new List<VOSub_Retire>();
        public List<VOSub_Retire> VoItemsDisable
        {
            get { return voItemsDisable; }
            set { this.voItemsDisable = value; }
        }
        //public const String MemberStatusCodeDischarged = "F2-Discharged"; //เลิกจ้าง
        private List<VOSub_Retire> voItemsDischarged = new List<VOSub_Retire>();
        public List<VOSub_Retire> VoItemsDischarged
        {
            get { return voItemsDischarged; }
            set { this.voItemsDischarged = value; }
        }
        //public const String MemberStatusCodeDismissed = "F5-Dismissed"; //ให้ออก
        private List<VOSub_Retire> voItemsDismissed = new List<VOSub_Retire>();
        public List<VOSub_Retire> VoItemsDismissed
        {
            get { return voItemsDismissed; }
            set { this.voItemsDismissed = value; }
        }
        //public const String MemberStatusCodeCorrupted = "F3-Corrupted"; //ทุจริต
        private List<VOSub_Retire> voItemsCorrupted = new List<VOSub_Retire>();
        public List<VOSub_Retire> VoItemsCorrupted
        {
            get { return voItemsCorrupted; }
            set { this.voItemsCorrupted = value; }
        }
        //public const String MemberStatusCodeTerminated = "F-Terminated"; //ปลด
        private List<VOSub_Retire> voItemsTerminated = new List<VOSub_Retire>();
        public List<VOSub_Retire> VoItemsTerminated
        {
            get { return voItemsTerminated; }
            set { this.voItemsTerminated = value; }
        }
        //public const String MemberStatusCodeBusinessClosed = "C-Closed"; //เลิกกิจการ
        private List<VOSub_Retire> voItemsBusinessClosed = new List<VOSub_Retire>();
        public List<VOSub_Retire> VoItemsBusinessClosed
        {
            get { return voItemsBusinessClosed; }
            set { this.voItemsBusinessClosed = value; }
        }
        //public const String MemberStatusCodeExpelled = "F4-Expelled"; //ไล่ออก
        private List<VOSub_Retire> voItemsExpelled = new List<VOSub_Retire>();
        public List<VOSub_Retire> VoItemsExpelled
        {
            get { return voItemsExpelled; }
            set { this.voItemsExpelled = value; }
        }
        //public const String MemberStatusCodeExtEmployerTransferred = "TX-ExtEmployerTransferred"; //ย้ายไปนายจ้างนอกระบบ
        private List<VOSub_Move> voItemsExtEmployerTransferred = new List<VOSub_Move>();
        public List<VOSub_Move> VoItemsExtEmployerTransferred
        {
            get { return voItemsExtEmployerTransferred; }
            set { this.voItemsExtEmployerTransferred = value; }
        }
        //public const String MemberStatusCodeIntEmployerTransferred = "TI-IntEmployerTransferred"; //ย้ายไปนายจ้างในระบบ
        private List<VOSub_Move> voItemsIntEmployerTransferred = new List<VOSub_Move>();
        public List<VOSub_Move> VoItemsIntEmployerTransferred
        {
            get { return voItemsIntEmployerTransferred; }
            set { this.voItemsIntEmployerTransferred = value; }
        }
        //public const String MemberStatusCodeResignIllegally = "F1-IllegallyResign"; //ลาออกจากงานผิดกฏระเบียบ
        private List<VOSub_Retire> voItemsResignIllegally = new List<VOSub_Retire>();
        public List<VOSub_Retire> VoItemsResignIllegally
        {
            get { return voItemsResignIllegally; }
            set { this.voItemsResignIllegally = value; }
        }
        //public const String MemberStatusCodeResign = "S-Resign"; //ลาออกจากงาน
        private List<VOSub_Retire> voItemsResign = new List<VOSub_Retire>();
        public List<VOSub_Retire> VoItemsResign
        {
            get { return voItemsResign; }
            set { this.voItemsResign = value; }
        }
        //public const String MemberStatusCodeRetired = "E-Retired"; //เกษียณ
        private List<VOSub_Retire> voItemsRetired = new List<VOSub_Retire>();
        public List<VOSub_Retire> VoItemsRetired
        {
            get { return voItemsRetired; }
            set { this.voItemsRetired = value; }
        }
        //public const String MemberStatusCodeQuitMembership = "M-Quit"; //ลาออกจากสมาชิกภาพ
        private List<VOSub_Retire> voItemsQuitMembership = new List<VOSub_Retire>();
        public List<VOSub_Retire> VoItemsQuitMembership
        {
            get { return voItemsQuitMembership; }
            set { this.voItemsQuitMembership = value; }
        }
        //public const String MemberStatusCodeTransferredOut = "T-TransferredOut"; //ลาออกและรองานใหม่
        private List<VOSub_Retire> voItemsTransferredOut = new List<VOSub_Retire>();
        public List<VOSub_Retire> VoItemsTransferredOut
        {
            get { return voItemsTransferredOut; }
            set { this.voItemsTransferredOut = value; }
        }

        public HelperBookAccountLeave()
        {
            this.terminationsDict = new Dictionary<string, object>();
            this.terminationsDict.Add(PFConstants.MemberStatusCodeDeceased, this.voItemsDeceased);
            this.terminationsDict.Add(PFConstants.MemberStatusCodeDisable, this.voItemsDisable);
            this.terminationsDict.Add(PFConstants.MemberStatusCodeDischarged, this.voItemsDischarged);
            this.terminationsDict.Add(PFConstants.MemberStatusCodeDismissed, this.voItemsDismissed);
            this.terminationsDict.Add(PFConstants.MemberStatusCodeCorrupted, this.voItemsCorrupted);
            this.terminationsDict.Add(PFConstants.MemberStatusCodeTerminated, this.voItemsTerminated);
            this.terminationsDict.Add(PFConstants.MemberStatusCodeBusinessClosed, this.voItemsBusinessClosed);
            this.terminationsDict.Add(PFConstants.MemberStatusCodeExpelled, this.voItemsExpelled);
            this.terminationsDict.Add(PFConstants.MemberStatusCodeExtEmployerTransferred, this.voItemsExtEmployerTransferred);
            this.terminationsDict.Add(PFConstants.MemberStatusCodeIntEmployerTransferred, this.voItemsIntEmployerTransferred);
            this.terminationsDict.Add(PFConstants.MemberStatusCodeResignIllegally, this.voItemsResignIllegally);
            this.terminationsDict.Add(PFConstants.MemberStatusCodeResign, this.voItemsResign);
            this.terminationsDict.Add(PFConstants.MemberStatusCodeRetired, this.voItemsRetired);
            this.terminationsDict.Add(PFConstants.MemberStatusCodeQuitMembership, this.voItemsQuitMembership);
            this.terminationsDict.Add(PFConstants.MemberStatusCodeTransferredOut, this.voItemsTransferredOut);
        }

        public object GetVOSub(String terminationCode)
        {
            return this.terminationsDict[terminationCode];
        }
        public Dictionary<String, object> TerminationsDict
        {
            get { return this.terminationsDict; }
        }
        public void AddTransfer(VOSub_Move vo)
        {
            List<VOSub_Move> vosub = (List<VOSub_Move>)GetVOSub(vo.TerminationCategory.Code);
            vo.LineNo = vosub.Count > 0 ? vosub[vosub.Count - 1].LineNo + 1 : 0;
            vosub.Add(vo);
        }
        public void AddDeceased(VOSub_Dead vo)
        {
            vo.LineNo = this.voItemsDeceased.Count > 0 ? this.voItemsDeceased[this.voItemsDeceased.Count - 1].LineNo + 1 : 0;
            this.voItemsDeceased.Add(vo);
        }
        public void AddTerminations(VOSub_Retire vo)
        {
            List<VOSub_Retire> vosub = (List<VOSub_Retire>)GetVOSub(vo.TerminationCategory.Code);
            vo.LineNo = vosub.Count > 0 ? vosub[vosub.Count - 1].LineNo + 1 : 0;
            vosub.Add(vo);
        }

        public VOSub_Move GetTransfer(int lineno, string terminationCode)
        {
            List<VOSub_Move> vosub = (List<VOSub_Move>)GetVOSub(terminationCode);
            return vosub.Find(delegate(VOSub_Move vo) { return vo.LineNo == lineno; });
        }
        public VOSub_Dead GetDeceased(int lineno)
        {
            return this.voItemsDeceased.Find(delegate(VOSub_Dead vo) { return vo.LineNo == lineno; });
        }
        public VOSub_Retire GetTerminations(int lineno, string terminationCode)
        {
            List<VOSub_Retire> vosub = (List<VOSub_Retire>)GetVOSub(terminationCode);
            return vosub.Find(delegate(VOSub_Retire vo) { return vo.LineNo == lineno; });
        }
    }
}
