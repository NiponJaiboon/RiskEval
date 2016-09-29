using System.Collections.Generic;
using WebHelper.ValueObject;

namespace WebHelper
{
    public class HelperTransferRMFLTF
    {
        private List<VOSubLTF> voSubLTFs;

        public List<VOSubLTF> VOSubLTFs
        {
            get
            {
                if (voSubLTFs == null)
                    voSubLTFs = new List<VOSubLTF>();
                return voSubLTFs;
            }
            set { this.voSubLTFs = value; }
        }
    }
}