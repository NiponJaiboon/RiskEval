using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizPortal;
using iSabaya;

namespace WebHelper.ServiceLayer.AuditTrails
{
    public enum AuditTrailsCategory
    {
        ActivityLog = 1,
        AccessLog = 2
    }

    public class AuditTrailsServices
    {
        public long ID { get; set; }
        public User User { get; set; }
        public DateTime Timestamp { get; set; }
        public Member Member { get; set; }
        public string Action { get; set; }
        private string message;
        public string Message
        {
            get
            {
                return this.StripTagsCharArray(message);
            }
            set
            {
                message = value;
            }
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        /// <param name="source">Text HTML</param>
        /// <returns>Text to remove HTML Tag</returns>
        private string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;
            bool tagBR_B = false;
            bool tagBR_R = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }

                if (inside && !tagBR_B && (let == 'b' || let == 'B'))
                {
                    tagBR_B = true;
                    continue;
                }

                if (inside && tagBR_B && (let == 'r' || let == 'R'))
                {
                    tagBR_R = true;
                    continue;
                }

                if (let == '>')
                {
                    inside = false;
                    continue;
                }

                if (!inside && tagBR_B && tagBR_R)
                {
                    tagBR_B = false;
                    tagBR_R = false;
                    array[arrayIndex] = '\n';
                    arrayIndex++;
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
                else if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        protected static long[] GetUserIDFromMemberUserGroup(MemberUserGroup memberUserGroup)
        {
            if (memberUserGroup == null)
                return null;
            int i = 0;
            long[] uID = new long[memberUserGroup.GroupUsers.Count];
            foreach (UserGroupUser ugu in memberUserGroup.GroupUsers)
            {
                uID[i] = ugu.User.ID;
                i++;
            }

            return uID;
        }
    }
}