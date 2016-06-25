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
    public class PasswordConfig
    {
        public virtual int MinNumberOfSpecialCharsInPassword { get; set; }
        public virtual int MinNumberOfCapitalLettersInPassword { get; set; }
        public virtual int MinNumberOfSmallLettersInPassword { get; set; }
        public virtual int MinNumberOfDigitsInPassword { get; set; }

        public virtual int MaxPasswordLength { get; set; }
        public virtual int MinPasswordLength { get; set; }

        public virtual int PasswordAgeInDays { get; set; }
        public virtual int PasswordHistoryDepth { get; set; }

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

        public virtual bool IsInconsistent(out string errorMessage)
        {
            if (MaxPasswordLength < MinPasswordLength)
                MessageBuilder.Append("The maximum length of password is less than the minimum.");

            if (MinNumberOfCapitalLettersInPassword <= 0)
                MessageBuilder.Append("The minimum number of upper case letters in password is not greater than zero.");

            if (MinNumberOfSmallLettersInPassword <= 0)
                MessageBuilder.Append("The minimum number of lower case letters in password is not greater than zero.");

            if (MinNumberOfSpecialCharsInPassword < 0)
                MessageBuilder.Append("The minimum number of special letters in password is negative.");

            if (MinNumberOfDigitsInPassword < 0)
                MessageBuilder.Append("The minimum number of digits in password is negative.");

            if (MinPasswordLength <= 0)
                MessageBuilder.Append("The minimum length of password is negative.");

            int totalRequiredChars = MinNumberOfSpecialCharsInPassword + MinNumberOfCapitalLettersInPassword
                            + MinNumberOfSmallLettersInPassword + MinNumberOfDigitsInPassword;
            if (MaxPasswordLength > 0 && MaxPasswordLength < totalRequiredChars)
                MessageBuilder.Append("The maximum length of password is less than the sum of the minimum number of various characters.");

            if (null == messageBuilder || messageBuilder.Length == 0)
            {
                errorMessage = null;
                return false;
            }
            else
            {
                errorMessage = messageBuilder.ToString();
                messageBuilder.Clear();
                return true;
            }
        }

        public virtual bool PasswordIsValid(string plainPasswordText)
        {
            if (this.MaxPasswordLength > 0
                && (this.MinPasswordLength > plainPasswordText.Length || plainPasswordText.Length > this.MaxPasswordLength))
                return false;

            if (!this.DoNotMeetPasswordStrength(plainPasswordText))
                return false;

            return true;
        }

        public virtual void ValidatePassword(string plainPasswordText)
        {
            if (this.MaxPasswordLength > 0
                && (this.MinPasswordLength > plainPasswordText.Length || plainPasswordText.Length > this.MaxPasswordLength))
                throw new Exception(Messages.SecurityPasswordLengthViolatesPolicy);

            if (!this.DoNotMeetPasswordStrength(plainPasswordText))
                throw new Exception(Messages.SecurityPasswordIsWeak);
        }

        public bool DoNotMeetPasswordStrength(string plainPasswordText)
        {
            int occurenceOfCapitalLetters = 0;
            int occurenceOfSmallLetters = 0;
            int occurenceOfDigits = 0;
            int occurenceOfSpecials = 0;
            foreach (char c in plainPasswordText.ToCharArray())
            {
                if (char.IsUpper(c))
                    ++occurenceOfCapitalLetters;
                else if (char.IsLower(c))
                    ++occurenceOfSmallLetters;
                else if (char.IsDigit(c))
                    ++occurenceOfDigits;
                else
                    ++occurenceOfSpecials;
            }

            int requirementCount = 0;

            if (occurenceOfCapitalLetters >= this.MinNumberOfCapitalLettersInPassword) ++requirementCount;
            if (occurenceOfSmallLetters >= this.MinNumberOfSmallLettersInPassword) ++requirementCount;
            if (occurenceOfDigits >= this.MinNumberOfDigitsInPassword) ++requirementCount;
            if (occurenceOfSpecials >= this.MinNumberOfSpecialCharsInPassword) ++requirementCount;

            return requirementCount >= 3;
        }

        #region password generator

        //public virtual String GeneratePassword()
        //{
        //    string passwordText;
        //    if (this.MinNumberOfSpecialCharsInPassword > 0)
        //    {
        //        do
        //        {
        //            passwordText = System.Web.Security.Membership.GeneratePassword(this.MinPasswordLength, this.MinNumberOfSpecialCharsInPassword);
        //        }
        //        while (!this.PasswordIsValid(passwordText));
        //    }
        //    else
        //    {
        //        passwordText = System.Web.Security.Membership.GeneratePassword(8, 1);
        //    }
        //    return passwordText;
        //}

        private static char[] lowerCaseChars;
        private static char[] LowerCaseChars
        {
            get
            {
                if (null == lowerCaseChars)
                    lowerCaseChars = ConfigurationManager.AppSettings["LowerCaseChars"].ToCharArray();
                if (null == lowerCaseChars)
                    lowerCaseChars = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' } ;
                return lowerCaseChars;
            }
        }

        private static char[] upperCaseChars;
        private static char[] UpperCaseChars
        {
            get
            {
                if (null == upperCaseChars)
                    upperCaseChars = ConfigurationManager.AppSettings["UpperCaseChars"].ToCharArray();
                if (null == upperCaseChars)
                    upperCaseChars = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
                return upperCaseChars;
            }
        }
        
        private static char[] digitChars ;
        private static char[] DigitChars 
        {
            get
            {
                if (null == digitChars)
                    digitChars = ConfigurationManager.AppSettings["DigitChars"].ToCharArray();
                if (null == digitChars)
                    digitChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                return digitChars;
            }
        }

        private static char[] spc;
        private static char[] SpecialChars
        {
            get
            {
                if (null == spc)
                {
                    string[] tempSpc = ConfigurationManager.AppSettings["SpecialChars"].Split(',');
                    if (null == tempSpc)
                        tempSpc = new string[] { "!,-,+,#,$,%,&,*,?,@" };
                    spc = new char[tempSpc.Length];

                    for (int i = 0; i < spc.Length; ++i)
                    {
                        spc[i] = Convert.ToChar(byte.Parse(tempSpc[i]));
                    }
                }

                return spc;
            }
        }

        //private static char[] specialChars = ConfigurationManager.AppSettings["SpecialChars"].ToCharArray();
        private static char[][] paddingChars;
        private static char[][] PaddingChars
        {
            get
            {
                if (null == paddingChars)
                    paddingChars = new char[][] { LowerCaseChars, UpperCaseChars, DigitChars, LowerCaseChars, UpperCaseChars, SpecialChars, LowerCaseChars, UpperCaseChars, };
                return paddingChars;
            }
        }

        private static System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();

        private static int GetCryptographicRandomNumber(int lBound, int uBound)
        {
            // Assumes lBound >= 0 && lBound < uBound
            // returns an int >= lBound and < uBound
            uint urndnum;
            byte[] rndnum = new Byte[4];
            if (lBound == uBound - 1)
            {
                // test for degenerate case where only lBound can be returned
                return lBound;
            }

            uint xcludeRndBase = (uint.MaxValue - (uint.MaxValue % (uint)(uBound - lBound)));

            do
            {
                rng.GetBytes(rndnum);
                urndnum = System.BitConverter.ToUInt32(rndnum, 0);
            } while (urndnum >= xcludeRndBase);

            return (int)(urndnum % (uBound - lBound)) + lBound;
        }

        public virtual string GeneratePassword()
        {
            int minLength = this.MinNumberOfSpecialCharsInPassword + this.MinNumberOfCapitalLettersInPassword
                                + this.MinNumberOfSmallLettersInPassword + this.MinNumberOfDigitsInPassword;

            Random Random = new Random();

            int passwordLength = (minLength < this.MinPasswordLength) ? this.MinPasswordLength : minLength;
            //if (MaxPasswordLength > 0)
            //    passwordLength = Random.Next(passwordLength, this.MaxPasswordLength + 1);
            //else
            //    passwordLength += 2;
            if (passwordLength < 5)
                passwordLength = 5;

            int[] indices = new int[passwordLength];
            for (int i = 0; i < passwordLength; ++i)
            {
                indices[i] = i;
            }

            int indexLength = passwordLength;
            int j = 0;
            int p;
            int[] positionIndices = new int[passwordLength];
            for (int i = 0; i < passwordLength; ++i)
            {
                j = Random.Next(0, indexLength--);
                p = indices[j];
                //collapse indicies
                for (; j < indexLength; ++j)
                {
                    indices[j] = indices[j + 1];
                }

                positionIndices[i] = p;
            }

            char[] password = new char[passwordLength];
            j = 0;
            for (int i = 0; i < this.MinNumberOfCapitalLettersInPassword; ++i)
            {
                password[positionIndices[j++]] = UpperCaseChars[GetCryptographicRandomNumber(0, UpperCaseChars.Length)];
            }
            for (int i = 0; i < this.MinNumberOfSmallLettersInPassword; ++i)
            {
                password[positionIndices[j++]] = LowerCaseChars[GetCryptographicRandomNumber(0, LowerCaseChars.Length)];
            }
            for (int i = 0; i < this.MinNumberOfDigitsInPassword; ++i)
            {
                password[positionIndices[j++]] = DigitChars[GetCryptographicRandomNumber(0, DigitChars.Length)];
            }
            for (int i = 0; i < this.MinNumberOfSpecialCharsInPassword; ++i)
            {
                password[positionIndices[j++]] = SpecialChars[GetCryptographicRandomNumber(0, SpecialChars.Length)];
            }

            int paddingLength = passwordLength - minLength;
            for (int i = 0; i < paddingLength; ++i)
            {
                char[] chars = PaddingChars[Random.Next(0, PaddingChars.Length)];
                password[positionIndices[j++]] = chars[GetCryptographicRandomNumber(0, chars.Length)];
            }

            return new string(password);
        }

        #endregion password generator
    }
}