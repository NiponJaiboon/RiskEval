using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Text;
using NHibernate;

namespace iSabaya
{
    [Serializable]
    public class SecurityConfig
    {
        #region persistent

        public virtual int MaxConsecutiveFailedLogonAttempts { get; set; }
        public virtual int MaxDaysOfInactivity { get; set; }

        public virtual int MaxUsernameLength { get; set; }
        public virtual int MinUsernameLength { get; set; }

        public virtual int WebSessionTimeoutValueInMinutes { get; set; }

        public virtual PasswordConfig PasswordPolicy { get; set; }

        #endregion persistent

        private StringBuilder messageBuilder;
        private StringBuilder MessageBuilder
        {
            get
            {
                if (null == messageBuilder)
                    messageBuilder = new StringBuilder();
                return messageBuilder;
            }
        }

        public virtual void ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception(Messages.SecurityUserIsNull);
        }

        public virtual void ValidateUsernameLength(string username)
        {
            if (this.MinUsernameLength <= this.MaxUsernameLength
                && (this.MinUsernameLength > username.Length
                    || (this.MaxUsernameLength > 0 && username.Length > this.MaxUsernameLength)))
                throw new Exception(Messages.SecurityUserNameLengthViolatesPolicy);
        }

        public virtual string GeneratePassword()
        {
            return this.PasswordPolicy.GeneratePassword();
        }
    }
}