using iSabaya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Util
{
    public enum Role
    {
        None,
        Admin, //เจ้าหน้าที่ดูแลระบบ
        User, //ส่วนราชการ
        Budgetor, //เจ้าหน้าที่จัดทำงบประมาณ
        Evaluator //เจ้าหน้าที่ประเมิณงบประมาณ
    }

    public class Menu
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string IdTab { get; set; }

        private IList<Menu> childs;
        public IList<Menu> Childs
        {
            get
            {
                if (childs == null)
                    return new List<Menu>();
                return childs;
            }
            set { childs = value; }
        }

        public static string FullUrl(string applicationName, string url)
        {
            if (string.IsNullOrEmpty(applicationName)) return string.Format("/{0}", url);
            else return string.Format("/{0}/{1}", applicationName, url);
        }
    }
}