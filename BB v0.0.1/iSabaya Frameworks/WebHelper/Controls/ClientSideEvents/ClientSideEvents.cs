using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebHelper.Controls
{
    [Serializable]
    public class PageControlClientSideEvents
    {
        /// <summary>
        /// PageNumberChaned like ValueChanged of SpinEditClientSideEvent
        /// </summary>
        public string PageNumberChanged { get; set; }
    }

    [Serializable]
    public class AdditionalClientSideEvents
    {
        public string ValueChanged { get; set; }
    }
}
