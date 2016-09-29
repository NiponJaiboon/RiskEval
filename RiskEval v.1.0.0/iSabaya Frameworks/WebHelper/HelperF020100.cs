using System;
using System.Collections.Generic;
using System.Text;
using WebHelper.ValueObject;

namespace WebHelper
{
    public class HelperF020100
    {
        private List<VOHelperF020100> vos;

        public List<VOHelperF020100> GetVOs()
        {
            if (vos == null)
            {
                vos = new List<VOHelperF020100>();
            }
            return vos;
        }

        public void RemoveVOLineNo(int lineNo)
        {
            int lNo=-1;
            foreach(VOHelperF020100 vo in vos){
                if (vo.LineNo == lineNo)
                {
                    lNo = vo.LineNo;
                    break;
                }
            }
            vos.RemoveAt(lNo);
        }

        public VOHelperF020100 GetVOAt(int lineNo)
        {
            VOHelperF020100 vo = null;
            foreach (VOHelperF020100 v in vos)
            {
                if (v.LineNo == lineNo)
                {
                    vo = v;
                    break;
                }
            }
            return vo;
        }
    }
}
