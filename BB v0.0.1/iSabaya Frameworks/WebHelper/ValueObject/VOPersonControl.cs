using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VOPersonControl
    {
        iSabaya.Context context;
        public VOPersonControl()
        {

        }
        public VOPersonControl(Person person, iSabaya.Context context)
        {
            this.person = person;
            this.context = context;
        }
        private Person person;

        public Person Person
        {
            get { return person; }
            set { person = value; }
        }

        public int PersonID
        {
            get { return person.PersonID; }
        }

        public String PersonName
        {
            get 
            {
                if (person.CurrentName != null)
                    return person.CurrentName.GetValue(context.CurrentLanguage.Code);
                else if (person.Names.Count > 0)
                {
                    return person.GetName(DateTime.Now).ToString(context.CurrentLanguage.Code);
                }
                else
                    return "";
            }      
        }

      


    }
}
