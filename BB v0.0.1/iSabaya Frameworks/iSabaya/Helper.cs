using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.IO;

namespace iSabaya
{
    public static class Helper
    {
        public static T DeserializeFromXml<T>(string xml) where T : class
        {
            if (String.IsNullOrEmpty((xml)))
                return null;
            else
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(new StringReader(xml));
            }
        }

        public static string SerializeToXml<T>(this T instance)
        {
            if (null == instance)
                return null;
            else
            {
                var xmlSerializer = new XmlSerializer(instance.GetType());
                var textWriter = new StringWriter();
                xmlSerializer.Serialize(textWriter, instance);
                return textWriter.ToString();
            }
        }

        public static IList<T> Take<T>(this IList<T> list, int startIndex, int elementCount)
        {
            IList<T> result = new List<T>();
            int listCount = list.Count;
            while (elementCount-- > 0 && startIndex < listCount)
            {
                result.Add(list.ElementAt<T>(startIndex++));
            }
            return result;
        }

        public static bool MarkMenuAsAccessible(this IList<DynamicMenu> roots, DynamicMenu menu)
        {
            bool found = false;

            foreach (DynamicMenu m in roots)
            {
                if (m.Id == menu.Id)
                {
                    m.Show = true;
                    found = true;
                    break;
                }
                else if (m.Children.MarkMenuAsAccessible(menu))
                {
                    found = true;
                    break;
                }
            }

            return found;
        }

        public static String GetAllMessages(this Exception ex, int maxLength = 4000)
        {
            return GetAllMessages(ex, maxLength, "\n");
        }

        public static String GetAllMessages(this Exception ex, int maxLength, String connective)
        {
            StringBuilder msgBuilder = new StringBuilder();
            bool notFirst = false;
            int length = 0;
            while (ex != null && length + ex.Message.Length + 1 <= maxLength)
            {
                if (notFirst)
                {
                    notFirst = true;
                    msgBuilder.Append(connective);
                }
                length = ex.Message.Length + 1;
                msgBuilder.Append(ex.Message);
                ex = ex.InnerException;
            }

            return msgBuilder.ToString();
        }

        public static bool IsNullOrEmpty(this TimeInterval t)
        {
            return null == t || t.IsEmpty;
        }

        public static String ValueInCurrentLanguage(Context context, String code)
        {
            IList<MultilingualString> mlss = context.PersistenceSession
                                                    .QueryOver<MultilingualString>()
                                                    .Where(m => m.Code == code)
                                                    .List();
            if (mlss.Count > 0)
            {
                MultilingualString mls = mlss[0];
                if (mls.Values.Count > 0)
                {
                    foreach (MLSValue mlsValue in mls.Values)
                    {
                        if (mlsValue.LanguageCode == context.CurrentLanguage.Code)
                            return mlsValue.Value;
                    }
                }
            }
            return null;
        }
    }
}
