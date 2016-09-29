using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using iSabaya;

namespace iSabaya
{
    [Serializable]
    public class Password : ITemporal
    {
        public Password()
        {
        }

        public Password(User user, PasswordConfig pwdConfig, String plainPasswordText)
        {
            if (null == user)
                throw new iSabayaException(Messages.SecurityUserPersonIsNull);

            if (null != pwdConfig)
                pwdConfig.ValidatePassword(plainPasswordText);

            this.user = user;
            if (String.IsNullOrEmpty(this.Salt))
                this.encryptedPassword = Encrypt(user.LoginName + plainPasswordText);
            else
                this.encryptedPassword = Encrypt(user.LoginName + plainPasswordText);
        }

        #region persistent

        public virtual long ID { get;set;}

        protected User user;
        public virtual User User
        {
            get { return user; }
            protected set { user = value; }
        }

        protected String encryptedPassword;
        public virtual String EncryptedPassword
        {
            get { return encryptedPassword; }
        }

        protected TimeInterval effectivePeriod = new TimeInterval(DateTime.Now);
        public virtual TimeInterval EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }

        public virtual string Salt { get; set; }

        #endregion persistent

        public virtual bool IsNotFinalized { get; set; }

        public virtual bool Match(string passwordText)
        {
            if (null == this.EncryptedPassword && null == passwordText) return true;
            return this.EncryptedPassword == Encrypt(this.User.LoginName + passwordText);
        }

        public static String Encrypt(String plainPassword)
        {
            try
            {
                Encoder enc = Encoding.UTF8.GetEncoder();
                byte[] plainPasswordBytes = new byte[plainPassword.Length * 2];
                char[] plainPasswordChars = plainPassword.ToCharArray();
                int charUsed;
                int byteUsed;
                bool completed;
                enc.Convert(plainPasswordChars, 0, plainPasswordChars.Length, plainPasswordBytes, 0,
                            plainPasswordBytes.Length, true, out charUsed, out byteUsed, out completed);
                SHA512 shaM = new SHA512Managed();
                return Convert.ToBase64String(shaM.ComputeHash(plainPasswordBytes));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
    }
}